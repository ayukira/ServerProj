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
    public abstract class AbstractSubService :BaseService
    {
        public abstract override ServiceType ServiceType { get;}
        public string ErrorMessage { get; protected set; }
        
        public event Action<Service_Info> OnServiceAdd;
        public event Action<Service_Info> OnServiceRemove;
        public event Action<Service_Info> OnPullService;
        public event Action<string> OnError;
        
        protected abstract int Trick_Delay { get; }
        protected Server Server;
        protected SubServiceGrpc SubServiceGrpc;
        protected readonly ConcurrentDictionary<long, BaseService> Servers = new();
        private string _registryHost;
        private int _registryPort;
        private RegistryGrpcClient _registryClient;
        private Channel _channel;

        protected abstract Server_Package Call(long serviceid, Server_Package package);

        public AbstractSubService(string host, int port) 
        {
            Host = host;
            Port = port;
            if (CUtils.IsPortOccuped(Host, Port)) 
            {
                error($"Failed to bind port {Host}:{Port}");
                throw new Exception(ErrorMessage);
            }
            SubServiceGrpc = new SubServiceGrpc(Call);
            Server = new Server
            {
                Services = { ServiceGrpc.BindService(SubServiceGrpc) },
                Ports = { new ServerPort(Host, Port, ServerCredentials.Insecure) }
            };
            config();
            _channel = new Channel(_registryHost, _registryPort, ChannelCredentials.Insecure);
        }
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
        #region public

        public bool Push(long serviceId, Server_Package package) 
        {
            return SubServiceGrpc.DequeuePush(serviceId, package);
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
                Servers.AddOrUpdate(info.ServiceId, info.ToServer(), (k, v) => info.ToServer());
                OnPullService?.Invoke(info);
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
            Servers.AddOrUpdate(info.ServiceId, info.ToServer(), (k, v) => info.ToServer());
            OnServiceAdd?.Invoke(info);
        }
        private void serviceRemove(ByteString data)
        {
            var info = Service_Info.Parser.ParseFrom(data);
            if (info.ServiceId == ServiceId) return;
            if (Servers.Remove(info.ServiceId, out _))
            {
                OnServiceRemove?.Invoke(info);
            }
        }
        private void error(string msg) 
        {
            ErrorMessage = msg;
            OnError?.Invoke(msg);
        }
        #endregion
    }
}
