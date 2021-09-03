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
        public override int Trick_Delay => 1000 * 5;
        public override ServiceType ExcludeType => ServiceType.Registry | ServiceType;

        public BusinessService(string host, int port) : base(host, port)
        {
            OnError += onError;
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

        protected override void OnPush(Server_Package package)
        {
            linkServices.TryGetValue(1, out LinkService linkService);
            PushMessage(1, package);
            //throw new NotImplementedException();
        }
    }
}
