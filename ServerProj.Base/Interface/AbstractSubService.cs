using Google.Protobuf;
using Grpc.Core;
using ServerProj.Base.Enum;
using ServerProj.Base.Extensions;
using ServerProj.Base.Utils;
using ServerProto;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ServerProto.RegistryGrpc;

namespace ServerProj.Base.Interface
{
    public abstract class AbstractSubService : BaseService
    {
        public abstract override ServiceType ServiceType { get; }
        /// <summary>
        /// this service can sub service type
        /// </summary>
        public abstract ServiceType ExcludeType { get; }
        /// <summary>
        /// trick delay
        /// </summary>
        public abstract int Trick_Delay { get; }

        #region variable
        public string ErrorMessage { get; protected set; }
        /// <summary>
        /// grpc server
        /// </summary>
        protected readonly Server Server;
        /// <summary>
        /// grpc service instance
        /// </summary>
        protected readonly MainServiceGrpc ServiceGrpc;
        /// <summary>
        /// this service link services list
        /// </summary>
        /// </summary>
        protected readonly ConcurrentDictionary<long, LinkService> linkServices = new();
        /// <summary>
        /// registry host
        /// </summary>
        private string _registryHost;
        /// <summary>
        /// registry port
        /// </summary>
        private int _registryPort;
        /// <summary>
        /// this service registry grpc client
        /// </summary>
        private RegistryGrpcClient _registryClient;
        /// <summary>
        /// this service registry grpc channel
        /// </summary>
        private readonly Channel _channel;
        #endregion

        #region event
        public event Action<Service_Info> OnServiceAddBefore;
        public event Action<Service_Info> OnServiceRemoveBefore;
        public event Action<bool, Service_Info> OnServiceAddAfter;
        public event Action<bool, Service_Info> OnServiceRemoveAfter;
        public event Action<long, Trick_Response> OnTrick;
        public event Action<string> OnError;
        #endregion

        public AbstractSubService(string host, int port)
        {
            Host = host;
            Port = port;
            if (CUtils.IsPortOccuped(Host, Port))
            {
                error($"Failed to bind port {Host}:{Port}");
                throw new Exception(ErrorMessage);
            }
            ServiceGrpc = new MainServiceGrpc(Call);
            Server = new Server
            {
                Services = { ServerProto.ServiceGrpc.BindService(ServiceGrpc) },
                Ports = { new ServerPort(base.Host, base.Port, ServerCredentials.Insecure) }
            };
            config();
            _channel = new Channel(_registryHost, _registryPort, ChannelCredentials.Insecure);
        }

        #region protected abstract action
        /// <summary>
        /// other service call this service execute action
        /// </summary>
        /// <param name="serviceid"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        protected abstract Server_Package Call(long serviceid, Server_Package package);
        /// <summary>
        /// link service push data to this service execute action
        /// </summary>
        /// <param name="package"></param>
        protected abstract void OnPush(Server_Package package);

        #endregion

        #region public
        public bool Run()
        {
            if (_channel.State != ChannelState.Idle)
            {
                error($"channel state is {_channel.State}");
                return false;
            }
            _registryClient = new RegistryGrpcClient(_channel);
            if (!registry())
            {
                error($"channel state is {_channel.State}");
                return false;
            }
            registryServicePush();
            trick();
            pullServices();
            Server.Start();
            return true;
        }
        public bool Stop()
        {
            _channel.ShutdownAsync();
            Server.ShutdownAsync();
            return true;
        }
        /// <summary>
        /// push data to link service 
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        public bool PushMessage(long serviceId, Server_Package package)
        {
            return ServiceGrpc.DequeuePush(serviceId, package);
        }
        #endregion

        #region protected virtual
        protected virtual bool AddLink(Service_Info info)
        {
            var service = info.ToServer();
            var type = (ServiceType)info.ServiceType;
            if (ExcludeType.HasFlag(type)) return false;
            var linkSer = new LinkService(this, service);
            linkSer.OnPush += OnPush;
            linkServices.AddOrUpdate(linkSer.LinkServiceId, linkSer, (k, v) => linkSer);
            return true;
        }
        protected virtual bool RemoveLink(Service_Info info)
        {
            var type = (ServiceType)info.ServiceType;
            if (ExcludeType.HasFlag(type)) return false;
            return linkServices.TryRemove(info.ServiceId, out _);
        }
        #endregion

        #region private
        private void config()
        {
            _registryHost = StaticConfig.RegistryHost;
            _registryPort = StaticConfig.RegistryPort;
        }
        private bool registry()
        {
            var res = _registryClient.RegistryAsync(new RegistryService_Request()
            {
                Info = new Service_Info()
                {
                    ServiceId = 0,
                    ServiceType = (int)ServiceType,
                    Host = Host,
                    Port = Port,
                    SocketHost = SocketHost,
                    SocketPort = SocketPort
                }
            });
            ServiceId = res.ResponseAsync.Result.ServiceId;
            return ServiceId > 0;
        }
        private void pullServices()
        {
            var res = _registryClient.ServiceListAsync(new ServiceList_Request()
            {
                ServiceType = (int)ServiceType.All
            }).ResponseAsync;
            foreach (var info in res.Result.Services)
            {
                if (info.ServiceId == ServiceId) return;
                OnServiceAddBefore?.Invoke(info);
                OnServiceAddAfter?.Invoke(AddLink(info), info);
            }
        }
        private void trick()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var task = Task.Delay(Trick_Delay);
                    var ts = DateTime.Now.ToTimeStamp();
                    var res = await _registryClient.TrickAsync(new Trick_Request()
                    {
                        ServiceId = ServiceId,
                        State = (int)ServiceState,
                        Time = ts
                    });
                    OnTrick?.Invoke(ts, res);
                    await task;
                }
            }, TaskCreationOptions.LongRunning);
        }
        private void registryServicePush()
        {
            var connect = _registryClient.ConnectRegistry(new Connect_Request() { ServiceId = ServiceId });
            var task = Task.Run(async () =>
            {
                while (await connect.ResponseStream.MoveNext())
                {
                    var res = connect.ResponseStream.Current;

                    switch (res.DataType)
                    {
                        case 1:
                            serviceAdd(res.Data);
                            break;
                        case 2:
                            serviceRemove(res.Data);
                            break;
                    }
                }
            });
        }
        private void serviceAdd(ByteString data)
        {
            var info = Service_Info.Parser.ParseFrom(data);
            if (info.ServiceId == ServiceId) return;
            OnServiceAddBefore?.Invoke(info);
            OnServiceAddAfter?.Invoke(AddLink(info), info);
        }
        private void serviceRemove(ByteString data)
        {
            var info = Service_Info.Parser.ParseFrom(data);
            if (info.ServiceId == ServiceId) return;
            OnServiceRemoveBefore?.Invoke(info);
            OnServiceRemoveAfter?.Invoke(RemoveLink(info), info);

        }
        private void error(string msg)
        {
            ErrorMessage = msg;
            OnError?.Invoke(msg);
        }
        #endregion
    }
}
