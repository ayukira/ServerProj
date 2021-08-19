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
        public long SubServiceId => Subservice.ServiceId;
        public BaseService Service;
        public BaseService Subservice;
        private ServiceGrpcClient _client;
        private Channel _channel;
        public string errorMsg ="";
        public SubService(BaseService  service,BaseService subService)
        {
            Service = service;
            Subservice = subService;
            _channel = new Channel(subService.Host, subService.Port, ChannelCredentials.Insecure);
            _client = new ServiceGrpcClient(_channel);
            Push();
            Console.WriteLine($"sub service {subService.ServiceType}: {subService.ServiceId} ");
        }
        private void error(string msg) 
        {
            errorMsg = msg;
        }
        public async Task<Server_Message> Call(Server_Message message) 
        {
            var mes = await _client.CallMessageAsync(message);
            Console.WriteLine($"Call Message from {SubServiceId}");
            return mes;
        }
        private void Push() 
        {
            var _client = new ServiceGrpcClient(_channel);
            var push = _client.PushMessage(Service.ToServerInfo());

            var task = Task.Run(async () =>
            {
                while (await push.ResponseStream.MoveNext())
                {
                    var res = push.ResponseStream.Current;
                    OnPush(res);
                }
            });
        }

        public void OnPush(Server_Message message) 
        {
            Console.WriteLine($"Push Message from {SubServiceId}");
        }
    }
}
