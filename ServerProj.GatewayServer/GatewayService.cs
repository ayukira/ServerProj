using DotNetty.Buffers;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using Netty.Servre;
using ServerProj.Base.Enum;
using ServerProj.Base.Extensions;
using ServerProj.Base.Interface;
using ServerProto;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace ServerProj.GatewayServer
{
    public class GatewayService : AbstractSubService
    {
        private ConcurrentDictionary<IChannelId, long> users = new();
        private ConcurrentDictionary<long, IChannelHandlerContext> handlers = new();

        private WebSocketEngine engine;
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
            initSocket();
        }
        private void initSocket() 
        {
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;
            engine = new WebSocketEngine(SocketHost, SocketPort,useLibuv:false);
            engine.OnAddedConnect += socket_OnAddedConnect;
            engine.OnRemovedConnect += socket_OnRemovedConnect;
            engine.OnExceptionCaught += socket_OnExceptionCaught;
            engine.OnTextMessage += socket_OnTextMessage;
            engine.OnBufferMessage += socket_OnBufferMessage;
            engine.OnUserEventTriggered += socket_OnUserEventTriggered;
            engine.Run().Wait();
        }
        private void onError(string msg)
        {
            Console.WriteLine(msg);
        }
        //接入服务器
        private void subGrpc(Service_Info info) 
        {
            var service = info.ToServer();
            var sub = new SubService(this, service);
            sub.OnPush += onPush;
            subs.AddOrUpdate(sub.SubServiceId, sub, (k, v) => sub);
        }
        //服务器推送
        private void onPush(Server_Package package)
        {
            foreach (var handler in handlers.Values) 
            {
                handler.WriteAndFlushAsync(package.ToByteString());
            }
        }
        //其他服务调用该服务的执行过程
        protected override Server_Package Call(long serviceId, Server_Package package)
        {
            return null;
        }
        #region socket
        private void socket_OnUserEventTriggered(IChannelHandlerContext context, object evt)
        {
            //Console.WriteLine("trick");
            //var heartPacket = new BaseMessage();
            //heartPacket.MsgType = 4;
            //heartPacket.Auth = context.Channel.Id.AsLongText();
            //heartPacket.Userid = 0;
            //heartPacket.Time = 0;//DateTime.Now.ToTimeStamp();
            //context.WriteAndFlushAsync(Unpooled.CopiedBuffer(heartPacket.ToByteArray()));
        }

        private void socket_OnBufferMessage(IChannelHandlerContext context, byte[] data)
        {
            var socket_package = Socket_Package.Parser.ParseFrom(data);
            var sub = subs.Values.FirstOrDefault(x => x.Subservice.ServiceType == ServiceType.Business);
            if (sub == null) return;
            users.TryGetValue(context.Channel.Id, out var userid);
            var server_package = new Server_Package()
            {
                ServiceId = sub.SubServiceId,
                ServiceType = (int)sub.Service.ServiceType,
                Userid = userid,
                MainCommand = socket_package.MainCommand,
                Command = socket_package.Command,
                Content = socket_package.Content,
                Time = socket_package.Time
            };
            var result = sub.Call(server_package).Result;
            if (result == null) return;
            var msg = new Socket_Package()
            {
                MainCommand = result.MainCommand,
                Command = result.Command,
                Content = result.Content,
                MsgType = socket_package.MsgType,
                Time = result.Time
            };
            context.Channel.WriteAndFlushAsync(new BinaryWebSocketFrame(ByteBufferUtil.DefaultAllocator.Buffer().WriteBytes(msg.ToByteArray())));
        }

        private void socket_OnTextMessage(IChannelHandlerContext context, string text)
        {
            var userid = userInfo(text);
            var id = context.Channel.Id;
            users.AddOrUpdate(id, id => userid, (id, old) => userid);
            handlers.AddOrUpdate(userid, i => context, (i, old) => 
            {
                users.TryRemove(old.Channel.Id,out _);
                old.CloseAsync();
                return context;
            });
            //context.Channel.WriteAndFlushAsync(new TextWebSocketFrame(text));
        }

        private void socket_OnExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine(exception.Message);
            if (users.TryRemove(context.Channel.Id, out long userid))
            {
                handlers.TryRemove(userid, out _);
            }
        }

        private void socket_OnRemovedConnect(IChannelHandlerContext context)
        {
            if (users.TryRemove(context.Channel.Id, out long userid)) 
            {
                handlers.TryRemove(userid, out _);
            }
        }

        private void socket_OnAddedConnect(IChannelHandlerContext context)
        {
            //var id = context.Channel.Id;
            //handlers.AddOrUpdate(id, id => context, (id, old) => context);
        }
        #endregion

        #region server
        private void onPullService(Service_Info info)
        {
            //TODO:链接业务服务器
            var type = (ServiceType)info.ServiceType;
            if (checkType.HasFlag(type)) return;
            subGrpc(info);
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
            subGrpc(info);
        }
        #endregion

        #region User

        private int userInfo(string token) 
        {
            var data = Encoding.UTF8.GetBytes(token);
            return BitConverter.ToInt32(data);
        }

        #endregion

    }

}
