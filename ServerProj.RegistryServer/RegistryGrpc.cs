using Google.Protobuf;
using Grpc.Core;
using ServerProj.Base.Extensions;
using ServerProto;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ServerProto.RegistryGrpc;

namespace ServerProj.RegistryServer
{
    public class RegistryGrpc : RegistryGrpcBase
    {
        public RegistryService RegistryService = new RegistryService(0);
        public const long Trick_Time = 1000 * 10;
        public const int Trick_Delay = 1000 * 5;
        private readonly ConcurrentDictionary<long, ServiceQueue> _queues = new();

        private List<Service_Info> _serviceInfos = new List<Service_Info>();

        public RegistryGrpc() 
        {
            CheckService();
        }
        public override Task<RegistryService_Response> Registry(RegistryService_Request request, ServerCallContext context)
        {
            var res = new RegistryService_Response();
            var service = request.Info.ToServer();
            var serviceId = RegistryService.Registry(service);
            res.ServiceId = serviceId;
            if ( serviceId <= 0)
            {
                return Task.FromResult(res);
            }
            var info = new Connect_Response()
            {
                DataType = 1,
                Data = service.ToServerInfo().ToByteString()
            };
            foreach (var queue in _queues)
            {
                queue.Value.EnqueueDatas(info);
            }
            _queues.GetOrAdd(serviceId, o => new ServiceQueue(service));
            _serviceInfos.Add(service.ToServerInfo());
            return  Task.FromResult(res);
        }



        public void CheckService() 
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var task = Task.Delay(Trick_Delay);
                    long ts = DateTime.Now.ToTimeStamp();
                    foreach ( var service in RegistryService.Services.ToArray()) 
                    {
                        if (ts - service.TrickTime < Trick_Time) continue;
                        if (!RegistryService.UnRegistry(service.ServiceId, out _)) continue;
                        _queues.TryRemove(service.ServiceId,out _);
                        _serviceInfos.RemoveAll(x => x.ServiceId == service.ServiceId);
                        //推送
                        var ser_info = new Service_Info();
                        try
                        {
                            ser_info = new Service_Info()
                            {
                                Host = service.Host,
                                Port = service.Port,
                                ServiceId = service.ServiceId,
                                ServiceType = (int)service.ServiceType
                            };
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex .Message);
                        }
                        var info = new Connect_Response()
                        {
                            DataType = 2,
                            Data = ser_info.ToByteString()
                        };
                        foreach (var queue in _queues) 
                        {
                            queue.Value.EnqueueDatas(info);
                        }
                        //Console.WriteLine($"{service.ServiceId} remove");
                    }
                    await task;
                }
            }, TaskCreationOptions.LongRunning);
        }

        public override Task<Trick_Response> Trick(Trick_Request request, ServerCallContext context)
        {
            DateTime dateTime = DateTime.Now;
            long ts = dateTime.ToTimeStamp();
            var service = RegistryService.Services.Find(x => x.ServiceId == request.ServiceId);
            service.TrickTime = ts;
            //Console.WriteLine($"service {service.ServiceId} trick :{ts},{dateTime}");
            return Task.FromResult(new Trick_Response() { State = 1,Time = ts });
        }

        public override async Task ConnectRegistry(Connect_Request request, IServerStreamWriter<Connect_Response> responseStream, ServerCallContext context)
        {
            await Response(request.ServiceId,responseStream);
        }
        public async Task Request(IAsyncStreamReader<Connect_Request> requestStream) 
        {
            while (await requestStream.MoveNext()) 
            {
                //TODO:处理请求
            }
        }

        public async Task Response(long serviceId,IServerStreamWriter<Connect_Response> responseStream)
        {
            var serviceQueue = _queues.Get(serviceId);
            while (serviceQueue.ResetEvent.WaitOne()) 
            {
                if (!serviceQueue.Queue.TryDequeue(out var data)) 
                {
                    serviceQueue.ResetEvent.Reset();
                    continue;
                }
                await responseStream.WriteAsync(data);
            }
        }

        public override Task<ServiceList_Response> ServiceList(ServiceList_Request request, ServerCallContext context)
        {
            var list = new ServiceList_Response();
            list.Services.AddRange(_serviceInfos);
            return Task.FromResult(list);
        }
    }
}
