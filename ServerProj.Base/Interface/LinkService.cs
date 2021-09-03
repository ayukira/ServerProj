using Grpc.Core;
using ServerProj.Base.Extensions;
using ServerProto;
using System;
using System.Threading.Tasks;
using static ServerProto.ServiceGrpc;

namespace ServerProj.Base.Interface
{
    public class LinkService
    {
        public event Action<Server_Package> OnPush;
        public event Action<string> OnError;
        /// <summary>
        /// linkService id 
        /// </summary>
        public long LinkServiceId => Link.ServiceId;
        /// <summary>
        /// main service
        /// </summary>
        public readonly BaseService Service;
        /// <summary>
        /// linkService
        /// </summary>
        public readonly BaseService Link;
        /// <summary>
        /// linkService clinet
        /// </summary>
        private ServiceGrpcClient _client;
        /// <summary>
        /// linkService channel
        /// </summary>
        private Channel _channel;
        /// <summary>
        /// error message
        /// </summary>
        private string errorMsg = string.Empty;
        public LinkService(BaseService service, BaseService linkService)
        {
            Service = service;
            Link = linkService;
            _channel = new Channel(linkService.Host, linkService.Port, ChannelCredentials.Insecure);
            _client = new ServiceGrpcClient(_channel);
            push();
            Console.WriteLine($"sub service {linkService.ServiceType}: {linkService.ServiceId} ");
        }
        /// <summary>
        /// call linkService service
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public async Task<Server_Package> Call(Server_Package package)
        {
            var result = await _client.CallMessageAsync(package);
            //Console.WriteLine($"Call Message from {LinkServiceId}");
            return result;
        }
        /// <summary>
        /// register linkService push service
        /// </summary>
        private void push()
        {
            var _client = new ServiceGrpcClient(_channel);
            var push = _client.PushMessage(Service.ToServerInfo());

            var task = Task.Run(async () =>
            {
                while (await push.ResponseStream.MoveNext())
                {
                    var package = push.ResponseStream.Current;
                    Console.WriteLine($"Push Message from {LinkServiceId}");
                    try
                    {
                        OnPush?.Invoke(package);
                    }
                    catch (Exception exception)
                    {
                        error(exception.Message + exception.StackTrace);
                    }
                }
            });
        }
        /// <summary>
        /// set error
        /// </summary>
        /// <param name="msg"></param>
        private void error(string msg)
        {
            errorMsg = msg;
            OnError?.Invoke(errorMsg);
        }
    }
}
