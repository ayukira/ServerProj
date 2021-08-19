using ServerProj.Base.Enum;
using ServerProj.Base.Extensions;
using ServerProj.Base.Interface;
using ServerProto;
using System;
using System.Collections.Concurrent;

namespace ServerProj.GatewayServer
{
    public class GatewayService : AbstractSubService
    {
        private ConcurrentDictionary<long, SubService> subs = new();
        public override ServiceType ServiceType => ServiceType.Gateway;
        protected override int Trick_Delay => 1000 * 5;
        private ServiceType checkType = ServiceType.Gateway| ServiceType.Registry;

        public GatewayService(string host,int port,string socketHost,int socketPort):base(host,port)
        {
            OnError += onError;
            OnPullService += onPullService;
            OnServiceAdd += onServiceAdd;
            OnServiceRemove += onServiceRemove;
            SocketHost = socketHost;
            SocketPort = socketPort;
        }

        private void onPullService(Service_Info info)
        {
            //TODO:链接业务服务器
            var type = (ServiceType)info.ServiceType;
            if (checkType.HasFlag(type)) return;
            SubGrpc(info);
        }

        private void onServiceRemove(Service_Info info)
        {
            var type = (ServiceType)info.ServiceType;
            if (checkType.HasFlag(type)) return;
            //TODO:业务服务器注销
            subs.TryRemove(info.ServiceId, out _);
        }

        private void onServiceAdd(Service_Info info)
        {
            var type = (ServiceType)info.ServiceType;
            if (checkType.HasFlag(type)) return;
            //TODO:链接业务服务器
            SubGrpc(info);
        }

        private void onError(string msg)
        {
            Console.WriteLine(msg);
        }

        private void SubGrpc(Service_Info info) 
        {
            var service = info.ToServer();
            var sub = new SubService(this, service);
            subs.AddOrUpdate(sub.SubServiceId, sub, (k, v) => sub);
        }

        public override Server_Message Call(long serviceId,Server_Message server_message)
        {
            return server_message;
        }

        public Server_Message Send(Server_Message server_message) 
        {
            if (!subs.TryGetValue(server_message.ServiceId, out var sub)) return null;
            return  sub.Call(server_message).Result;
        }
    }

}
