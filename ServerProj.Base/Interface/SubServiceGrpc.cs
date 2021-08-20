using Grpc.Core;
using ServerProj.Base.Extensions;
using ServerProto;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using static ServerProto.ServiceGrpc;

namespace ServerProj.Base.Interface
{
    public class SubServiceGrpc : ServiceGrpcBase
    {
        private readonly ConcurrentDictionary<long, PushQueue> _queues = new();
        private event Func<long, Server_Package, Server_Package> call;
        public override Task<Server_Package> CallMessage(Server_Package package, ServerCallContext context)
        {
            return Task.FromResult(call(package.ServiceId,package));
        }

        public SubServiceGrpc(Func<long, Server_Package, Server_Package> func) 
        {
            if (func == null) throw new Exception("func is null");
            call = func;
        }

        public override async Task PushMessage(Service_Info serverInfo, IServerStreamWriter<Server_Package> responseStream, ServerCallContext context)
        {
            var queue = _queues.GetOrAdd(serverInfo.ServiceId, o => new PushQueue(serverInfo.ToServer()));
            await Push(queue, responseStream);
        }
        public async Task Push(PushQueue queue, IServerStreamWriter<Server_Package> responseStream)
        {
            while (queue.ResetEvent.WaitOne())
            {
                if (!queue.Queue.TryDequeue(out var package))
                {
                    queue.ResetEvent.Reset();
                    continue;
                }
                await responseStream.WriteAsync(package);
            }
        }

        public bool DequeuePush(long serviceId, Server_Package package)
        {
            var queue = _queues.Get(serviceId);
            if (queue == null) return false;
            return queue.EnqueueDatas(package);
        }
    }
}
