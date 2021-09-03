using ServerProj.Base.Interface;
using ServerProto;

namespace ServerProj.GatewayServer
{
    public class PackageTool
    {
        public static Server_Package ToServerPackage(Socket_Package package, long userid, LinkService subService)
        {
            return new Server_Package()
            {
                ServiceId = subService.LinkServiceId,
                ServiceType = (int)subService.Service.ServiceType,
                Userid = userid,
                MainCommand = package.MainCommand,
                Command = package.Command,
                Content = package.Content,
                Time = package.Time
            };
        }

        public static Socket_Package ToSocketPackage(Server_Package package, int MessageType)
        {
            return new Socket_Package()
            {
                MainCommand = package.MainCommand,
                Command = package.Command,
                Content = package.Content,
                MsgType = MessageType,
                Time = package.Time
            };
        }
    }
}
