using DotNetty.Common;
using Netty.Servre;
using System;
using System.Threading.Tasks;

namespace ServerProj.SocketServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Paranoid;
            string address = "127.0.0.1";
            int prot = 33334;
            var websocket = new WebSocketEngine(address, prot);
            await websocket.Run();
            websocket.OnTextMessage += socket_OnTextMessage;
            Console.WriteLine($"Listening on ws://{websocket.Address}:{websocket.Port}{websocket.WebsocketPath}");
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
            Console.WriteLine("close completion");
            await websocket.Stop();
        }

        private static void socket_OnTextMessage(DotNetty.Transport.Channels.IChannelHandlerContext arg1, string arg2)
        {
            Console.WriteLine(arg2);
        }
    }
}
