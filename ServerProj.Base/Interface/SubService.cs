using Grpc.Core;
using ServerProj.Base.Extensions;
using ServerProto;
using System;
using System.Threading.Tasks;
using static ServerProto.ServiceGrpc;

namespace ServerProj.Base.Interface
{
    public class SubService
    {
        public event Action<Server_Package> OnPush;
        public long SubServiceId => Subservice.ServiceId;
        public BaseService Service;
        public BaseService Subservice;
        private ServiceGrpcClient _client;
        private Channel _channel;
        public string errorMsg = string.Empty;
        public SubService(BaseService service, BaseService subService)
        {
            Service = service;
            Subservice = subService;
            _channel = new Channel(subService.Host, subService.Port, ChannelCredentials.Insecure);
            _client = new ServiceGrpcClient(_channel);
            push();
            Console.WriteLine($"sub service {subService.ServiceType}: {subService.ServiceId} ");
        }
        private void error(string msg)
        {
            errorMsg = msg;
        }
        public async Task<Server_Package> Call(Server_Package package)
        {
            var result = _client.CallMessageAsync(package).ResponseAsync.Result;
            Console.WriteLine($"Call Message from {SubServiceId}");
            return result;
        }
        private void push()
        {
            var _client = new ServiceGrpcClient(_channel);
            var push = _client.PushMessage(Service.ToServerInfo());

            var task = Task.Run(async () =>
            {
                while (await push.ResponseStream.MoveNext())
                {
                    var package = push.ResponseStream.Current;
                    Console.WriteLine($"Push Message from {SubServiceId}");
                    OnPush?.Invoke(package);
                }
            });
        }
    }
}
