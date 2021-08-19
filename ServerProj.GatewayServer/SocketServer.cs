using DotNetty.Common;
using DotNetty.Transport.Channels;
using Netty.Servre;
using ServerProto;
using System;

namespace ServerProj.GatewayServer
{
    public class SocketServer
    {
        WebSocketEngine engine;
        public SocketServer(string host,int port)
        {
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;
            engine = new WebSocketEngine(host, port);
            engine.Socket.OnAddedConnect += socket_OnAddedConnect;
            engine.Socket.OnRemovedConnect += socket_OnRemovedConnect;
            engine.Socket.OnExceptionCaught += socket_OnExceptionCaught;
            engine.Socket.OnTextMessage += socket_OnTextMessage;
            engine.Socket.OnBufferMessage += socket_OnBufferMessage;
            engine.Socket.OnUserEventTriggered += socket_OnUserEventTriggered;
            engine.Run().Wait();
        }

        private void socket_OnUserEventTriggered(IChannelHandlerContext context, object evt)
        {
            //var heartPacket = new BaseMessage();
            //heartPacket.MsgType = 4;
            //heartPacket.Auth = context.Channel.Id.AsLongText();
            //heartPacket.Userid = 0;
            //heartPacket.Time = 0;//DateTime.Now.ToTimeStamp();
            //context.WriteAndFlushAsync(Unpooled.CopiedBuffer(heartPacket.ToByteArray()));
        }

        private void socket_OnBufferMessage(IChannelHandlerContext context, byte[] data)
        {
        }

        private void socket_OnTextMessage(IChannelHandlerContext context, string text)
        {
        }

        private void socket_OnExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
        }

        private void socket_OnRemovedConnect(IChannelHandlerContext context)
        {
        }

        private void socket_OnAddedConnect(IChannelHandlerContext context)
        {
            
        }
    }
}
