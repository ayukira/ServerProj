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
        private event Func<long, Server_Message, Server_Message> OnCall;
        public override Task<Server_Message> CallMessage(Server_Message request, ServerCallContext context)
        {
            return Task.FromResult(Call(request.ServiceId,request));
        }

        public SubServiceGrpc(Func<long,Server_Message, Server_Message> func) 
        {
            if (func == null) throw new Exception("func is null");
            OnCall += func;
        }

        public override async Task PushMessage(Service_Info request, IServerStreamWriter<Server_Message> responseStream, ServerCallContext context)
        {
            var queue = _queues.GetOrAdd(request.ServiceId, o => new PushQueue(request.ToServer()));
            await Push(queue, responseStream);
        }

        public Server_Message Call(long serviceid,Server_Message message)
        {
            //DequeuePush(message.ServiceId, message);
            return OnCall(serviceid, message);
        }
        public async Task Push(PushQueue queue, IServerStreamWriter<Server_Message> responseStream)
        {
            while (queue.ResetEvent.WaitOne())
            {
                if (!queue.Queue.TryDequeue(out var data))
                {
                    queue.ResetEvent.Reset();
                    continue;
                }
                await responseStream.WriteAsync(data);
            }
        }

        public bool DequeuePush(long serviceId, Server_Message message)
        {
            var queue = _queues.Get(serviceId);
            if (queue == null) return false;
            return queue.EnqueueDatas(message);
        }
    }
}
