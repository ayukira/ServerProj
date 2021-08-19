using ServerProj.Base.Enum;

namespace ServerProj.Base.Interface
{
    public interface IService
    {
        public long ServiceId { get; set; }
        public ServiceType ServiceType { get; set; }
        public string ServiceName { get; set; }
        public long Weight { get; set; }
        public ServiceState ServiceState { get; set; }
    }
}
