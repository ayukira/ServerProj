using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Codecs.Http.WebSockets.Extensions.Compression;
using DotNetty.Handlers;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using System;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Netty.Servre
{
    public class WebSocketEngine
    {
        public string Address { get; private set; }
        public IPAddress Host { get; private set; }
        public int Port { get; private set; }
        public string WebsocketPath { get; private set; }
        public WebSocketHandler Socket { get; private set; }

        private bool _useLibuv = true;
        private IEventLoopGroup _bossGroup;
        private IEventLoopGroup _workGroup;
        private IChannel _bootstrapChannel = null;
        private ServerBootstrap _bootstrap = null;
        private int _maxFrameSize = 65536;

        private bool isRun = false;

        public WebSocketEngine(string host, int port,string websocketPath ="/" ,bool useLibuv = true)
        {
            Address = host;
            Host = IPAddress.Parse(host);
            Port = port;
            WebsocketPath = string.IsNullOrWhiteSpace(websocketPath) ? "/" : websocketPath;

            #region Libuv
            _useLibuv = useLibuv;
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            }
            if (_useLibuv)
            {
                var dispatcher = new DispatcherEventLoopGroup();
                _bossGroup = dispatcher;
                _workGroup = new WorkerEventLoopGroup(dispatcher);
            }
            else
            {
                _bossGroup = new MultithreadEventLoopGroup(1);
                _workGroup = new MultithreadEventLoopGroup();
            }
            _bootstrap = new ServerBootstrap();
            _bootstrap.Group(_bossGroup, _workGroup);
            if (_useLibuv)
            {
                _bootstrap.Channel<TcpServerChannel>();
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    _bootstrap
                        .Option(ChannelOption.SoReuseport, true)
                        .ChildOption(ChannelOption.SoReuseaddr, true);
                }
            }
            else
            {
                _bootstrap.Channel<TcpServerSocketChannel>();
            }
            #endregion

            init();
        }

        public async Task Run() 
        {
            if (isRun) return;
            isRun = true;
            _bootstrapChannel = await _bootstrap.BindAsync(Host, Port);
        }
        public async Task Stop() 
        {
            if (!isRun) return;
            await Task.WhenAll(
                 _bootstrapChannel.CloseAsync()
                ,_workGroup.ShutdownGracefullyAsync()
                ,_bossGroup.ShutdownGracefullyAsync()
                );
        }

        private async void reBind()
        {
            await _bootstrapChannel.CloseAsync();
            //Console.WriteLine("rebind......");
            var newChannel = await _bootstrap.BindAsync(Host, Port);
            //Console.WriteLine("rebind complate");
            Interlocked.Exchange(ref _bootstrapChannel, newChannel);
        }

        private void init()
        {
            _bootstrap
                .Option(ChannelOption.SoBacklog, 8192)
                .Handler(new ServerChannelRebindHandler(reBind))
                .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast("idleStateHandler", new IdleStateHandler(0, 0, 120));
                    pipeline.AddLast(new HttpServerCodec());
                    pipeline.AddLast(new HttpObjectAggregator(_maxFrameSize));
                    pipeline.AddLast(new WebSocketServerCompressionHandler());
                    pipeline.AddLast(new WebSocketServerProtocolHandler(
                        websocketPath: WebsocketPath,
                        subprotocols: null,
                        allowExtensions: true,
                        maxFrameSize: _maxFrameSize,
                        allowMaskMismatch: true,
                        checkStartsWith: false,
                        dropPongFrames: true,
                        enableUtf8Validator: false));
                    pipeline.AddLast(new WebSocketFrameAggregator(_maxFrameSize));
                    Socket = new WebSocketHandler();
                    pipeline.AddLast(Socket);
                }));
            isRun = false;
        }
    }
}
