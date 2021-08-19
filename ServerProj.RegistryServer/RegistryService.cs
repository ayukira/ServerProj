using ServerProj.Base.Enum;
using ServerProj.Base.Interface;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ServerProj.RegistryServer
{
    public class RegistryService : BaseService
    {
        private readonly ConcurrentDictionary<long, BaseService> _servers = new();
        private IEnumerable<BaseService> _gateways => _servers.Where(x => x.Value.ServiceType == ServiceType.Gateway).Select(x => x.Value) ?? new List<BaseService>();
        private IEnumerable<BaseService> _auths => _servers.Where(x => x.Value.ServiceType == ServiceType.Auth).Select(x => x.Value) ?? new List<BaseService>();
        private IEnumerable<BaseService> _businesses => _servers.Where(x => x.Value.ServiceType == ServiceType.Business).Select(x => x.Value) ?? new List<BaseService>();

        public List<BaseService> Services => _servers.Select(x => x.Value).ToList();

        private long _serviceId = 1000;
        public override ServiceType ServiceType => ServiceType.Registry;
        public override string ServiceName => "注册服务";
        public RegistryService(long serviceId)
        {
            ServiceId = serviceId;
        }

        public long Registry(BaseService service)
        {
            var serviceid = _serviceId++;
            service.ServiceId = serviceid;
            service.ServiceName = service.ServiceType.ToString() + serviceid.ToString();
            if (_servers.ContainsKey(serviceid)) 
            {
                return -1;
            }
            if (!_servers.TryAdd(serviceid, service)) 
            {
                return -2;
            }
            return serviceid;
        }

        public bool UnRegistry(long serviceId,out BaseService service)
        {
            service = null;
            if (!_servers.ContainsKey(serviceId))
            {
                return false;
            }
            return _servers.TryRemove(serviceId, out service);
        }
    }
}
