using ServerProto;
using System.Collections.Concurrent;
using System.Threading;

namespace ServerProj.Base.Interface
{
    public class PushQueue
    {
        public ConcurrentQueue<Server_Message> Queue = new();

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
        /// <param name="datas"></param>
        /// <returns></returns>
        public bool EnqueueDatas(params Server_Message[] datas)
        {
            if (datas == null || datas.Length <= 0) return false;
            foreach (var data in datas)
            {
                Queue.Enqueue(data);
            }
            ResetEvent.Set();
            return true;
        }
    }
}
