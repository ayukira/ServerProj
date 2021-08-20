using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using ServerProto;
using System;

namespace Netty.Servre
{
    public class WebSocketHandler : SimpleChannelInboundHandler<WebSocketFrame>
    {
        public event Action<IChannelHandlerContext, string> OnTextMessage;
        public event Action<IChannelHandlerContext, byte[]> OnBufferMessage;
        public event Action<IChannelHandlerContext> OnAddedConnect;
        public event Action<IChannelHandlerContext> OnRemovedConnect;
        public event Action<IChannelHandlerContext, Exception> OnExceptionCaught;
        public event Action<IChannelHandlerContext, object> OnUserEventTriggered;
        protected override void ChannelRead0(IChannelHandlerContext context, WebSocketFrame frame)
        {
            if (frame is TextWebSocketFrame textFrame)
            {
                var text = textFrame?.Text();
                //context.Channel.WriteAndFlushAsync(new TextWebSocketFrame(msg));
                TextMessage(context, text);
                return;
            }
            if (frame is BinaryWebSocketFrame binaryFrame)
            {
                var buffer = binaryFrame.Content;
                if (buffer == null) return;
                if (!buffer.HasArray) { return; }
                if (buffer.Array == null) return;
                var data = buffer.Array.AsSpan().Slice(buffer.ArrayOffset, buffer.ReadableBytes).ToArray();
                if (data == null) return;
                BufferMessage(context, data);
                //ctx.Channel.WriteAndFlushAsync(new BinaryWebSocketFrame(binaryFrame.Content.RetainedDuplicate()));
            }
        }
        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            //Console.WriteLine("flush");
            context.Flush();
        }

        public override void UserEventTriggered(IChannelHandlerContext context, object evt)
        {
            switch (evt)
            {
                case IdleStateEvent stateEvent:
                    //($"{nameof(WebSocketServerFrameHandler)} caught idle state: {stateEvent.State}");
                    break;
                default:
                    break;
            }
            OnUserEventTriggered?.Invoke(context, evt);
        }
        public void TextMessage(IChannelHandlerContext context, string text) 
        {
            OnTextMessage?.Invoke(context, text);
        }

        public void BufferMessage(IChannelHandlerContext context, byte[] data) 
        {
            //BaseMessage.Parser.ParseFrom(data);
            //var msg = new BaseMessage();
            //msg.MergeFrom(data.ToArray());
            //var ser = new Service_Info();
            //ser.MergeFrom(msg.Content);
            //Console.WriteLine(msg);
            //Console.WriteLine(ser);
            OnBufferMessage?.Invoke(context, data);
        }
        public override void HandlerAdded(IChannelHandlerContext context)
        {
            OnAddedConnect?.Invoke(context);
        }
        public override void HandlerRemoved(IChannelHandlerContext context)
        {
            OnRemovedConnect?.Invoke(context);
        }
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            OnExceptionCaught?.Invoke(context, exception);
            context.CloseAsync();
        }
    }
}
