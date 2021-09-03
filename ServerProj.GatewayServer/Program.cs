using Google.Protobuf;
using ServerProto;
using System;

namespace ServerProj.GatewayServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("gate port:");
            int port = Convert.ToInt32(Console.ReadLine());
            var gate = new GatewayService("127.0.0.1", port, "127.0.0.1", 33334);
            gate.Run();
            Console.WriteLine("Gateway is Running!");
            Console.WriteLine($"Gateway is {gate.ServiceId}");
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
    }
}
