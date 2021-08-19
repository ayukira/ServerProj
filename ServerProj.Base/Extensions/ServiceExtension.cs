using ServerProj.Base.Enum;
using ServerProj.Base.Interface;
using ServerProto;
using System;

namespace ServerProj.Base.Extensions
{
    public static class ServiceExtension
    {
        public static BaseService ToServer(this Service_Info info)
        {
            BaseService service = new();
            service.ServiceId = info.ServiceId;
            service.ServiceType = (ServiceType)info.ServiceType;
            service.Weight = 0;
            service.ServiceState = ServiceState.Runing;
            service.TrickTime = DateTime.Now.ToTimeStamp();
            service.Host = info.Host;
            service.Port = info.Port;
            service.SocketHost = info.SocketHost ?? string.Empty;
            service.SocketPort = info.SocketPort;
            return service;
        }
        public static Service_Info ToServerInfo(this BaseService service)
        {
            Service_Info info = new ()
            {
                ServiceId = service.ServiceId,
                ServiceType = (int)service.ServiceType,
                Host = service.Host,
                Port = service.Port,
                SocketHost = service.SocketHost,
                SocketPort = service.SocketPort
            };
            return info;
        }
    }
}
