using ServerProto;
using System.Collections.Concurrent;
using System.Threading;

namespace ServerProj.Base.Interface
{
    public class PushQueue
    {
        public ConcurrentQueue<Server_Package> Queue = new();

        public BaseService Service;
        public long Id;
        public readonly ManualResetEvent ResetEvent = new(false);
        public PushQueue(BaseService service) 
        {
            Service = service;
            Id = service.ServiceId;
        }

        /// <summary>
        /// 队列添加数据
        /// </summary>
        /// <param name="packages"></param>
        /// <returns></returns>
        public bool EnqueueDatas(params Server_Package[] packages)
        {
            if (packages == null || packages.Length <= 0) return false;
            foreach (var data in packages)
            {
                Queue.Enqueue(data);
            }
            ResetEvent.Set();
            return true;
        }
    }
}
