using DotNetty.Buffers;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using Netty.Servre;
using ServerProj.Base.Enum;
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
        public override ServiceType ServiceType => ServiceType.Gateway;
        public override ServiceType ExcludeType => ServiceType | ServiceType.Registry;
        public override int Trick_Delay => 1000 * 5;

        /// <summary>
        /// channelid userid dic
        /// </summary>
        private readonly ConcurrentDictionary<IChannelId, long> users = new();
        /// <summary>
        /// userid, channelContext dic
        /// </summary>
        private readonly ConcurrentDictionary<long, IChannelHandlerContext> handlers = new();
        /// <summary>
        /// webSocket Engine
        /// </summary>
        private WebSocketEngine engine;

        /// <summary>
        /// create gate service
        /// </summary>
        /// <param name="host">gate grpc host</param>
        /// <param name="port">gate grpc port</param>
        /// <param name="socketHost">gate websocket host</param>
        /// <param name="socketPort">gate websocket port</param>
        public GatewayService(string host, int port, string socketHost, int socketPort) : base(host, port)
        {
            OnError += onError;
            SocketHost = socketHost;
            SocketPort = socketPort;
            initSocket();
        }
        private void initSocket()
        {
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;
            engine = new WebSocketEngine(SocketHost, SocketPort, useLibuv: false);
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

        #region override
        protected override void OnPush(Server_Package package)
        {
            foreach (var handler in handlers.Values)
            {
                handler.WriteAndFlushAsync(package.ToByteString());
            }
        }

        protected override Server_Package Call(long serviceId, Server_Package package)
        {
            return null; //其他服务调用该服务的执行过程
        }
        #endregion

        #region socket
        private void socket_OnUserEventTriggered(IChannelHandlerContext context, object evt)
        {

        }

        private void socket_OnBufferMessage(IChannelHandlerContext context, byte[] data)
        {
            if (!users.TryGetValue(context.Channel.Id, out var userid))
            {
                return;
            }
            var request_socket_package = Socket_Package.Parser.ParseFrom(data);
            if (request_socket_package == null)
            {
                return;
            }
            var linkSer = linkServices.Values.FirstOrDefault(x => x.Link.ServiceType == ServiceType.Business);
            if (linkSer == null)
            {
                return;
            }
            var server_package = PackageTool.ToServerPackage(request_socket_package, userid, linkSer);
            var result = linkSer.Call(server_package).Result;
            if (result == null)
            {
                return;
            }
            var response_socket_package = PackageTool.ToSocketPackage(result, request_socket_package.MsgType);
            context.Channel.WriteAndFlushAsync(new BinaryWebSocketFrame(ByteBufferUtil.DefaultAllocator.Buffer().WriteBytes(response_socket_package.ToByteArray())));
        }

        private void socket_OnTextMessage(IChannelHandlerContext context, string text)
        {
            var userid = userInfo(text);
            var id = context.Channel.Id;
            users.AddOrUpdate(id, id => userid, (id, old) => userid);
            handlers.AddOrUpdate(userid, i => context, (i, old) =>
            {
                users.TryRemove(old.Channel.Id, out _);
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

        #region User

        private int userInfo(string token)
        {
            var data = Encoding.UTF8.GetBytes(token);
            return BitConverter.ToInt32(data);
        }
        #endregion
    }

}
