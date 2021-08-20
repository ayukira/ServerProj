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
            //while (true)
            //{
            //    int id = Convert.ToInt32(Console.ReadLine());
            //    var msg = gate.Send(new Server_Package
            //    {
            //        ServiceId = id,
            //        MainCommand = 1,
            //        Command = 2,
            //        ServiceType = (int)gate.ServiceType,
            //        Userid = userid,
            //        Content = ByteString.CopyFromUtf8($"buss send data {DateTime.Now}")
            //    });
            //    if (msg != null)
            //        Console.WriteLine("return: " + msg.Content.ToStringUtf8());
            //}
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
    }
}
