using Google.Protobuf;
using ServerProj.Base.Enum;
using ServerProj.Base.Interface;
using ServerProto;
using System;

namespace ServerProj.BusinessServer
{
    public class BusinessService : AbstractSubService
    {
        public override ServiceType ServiceType => ServiceType.Business;
        protected override int Trick_Delay => 1000 * 5;
        private ServiceType checkType = ServiceType.Gateway;

        public BusinessService(string host, int port) : base(host, port)
        {
            SocketHost = "";
            SocketPort = 0;
            OnError += onError;
            OnPullService += onPullService;
            OnServiceAdd += onServiceAdd;
            OnServiceRemove += onServiceRemove;
        }

        private void onServiceRemove(Service_Info info)
        {
            var type = (ServiceType)info.ServiceType;
            if (type != checkType) return;
            //TODO:业务服务器注销
        }

        private void onServiceAdd(Service_Info info)
        {
            var type = (ServiceType)info.ServiceType;
            if (type != checkType) return;
            //TODO:链接网关服务器
        }
        private void onPullService(Service_Info info)
        {
            //TODO:链接业务服务器
        }

        private void onError(string msg)
        {
            Console.WriteLine(msg);
        }
        protected override Server_Package Call(long serviceid, Server_Package package)
        {
            return package;
            //var str = package.Data.ToStringUtf8();
            var msg = TestMessage.Parser.ParseFrom(package.Content);
            Console.WriteLine($"gate {serviceid} call message : {package.Userid},mes:"+ msg);
            msg.Msg = "echo:" + msg.Msg;
            package.Content = msg.ToByteString();
            package.ServiceId = serviceid;
            package.ServiceType = (int)ServiceType.Gateway;
            return package;
            //package.Data = ByteString.CopyFromUtf8("echo : " + str);
            //return package;
        }
    }
}
