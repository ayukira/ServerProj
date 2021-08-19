using ServerProj.Base.Enum;

namespace ServerProj.Base.Interface
{
    public class BaseService :IService
    {
        public virtual long ServiceId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual string ServiceName { get; set; }
        public virtual long Weight { get; set; }
        public virtual ServiceState ServiceState { get; set; }
        public virtual string Host { get; set; }
        public virtual int Port { get; set; }
        public virtual long TrickTime { get; set; }
        public virtual long ConnectCount { get; set; }

        public virtual string SocketHost { get; set; }
        public virtual int SocketPort { get; set; }
    }
}
