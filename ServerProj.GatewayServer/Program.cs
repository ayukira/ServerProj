using Google.Protobuf;
using System;

namespace ServerProj.GatewayServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("gate port:");
            int port = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("userid :");
            int userid = Convert.ToInt32(Console.ReadLine());
            var gate = new GatewayService("127.0.0.1", port, "127.0.0.1", 3001);
            gate.Run();
            Console.WriteLine("Gateway is Running!");
            Console.WriteLine($"Gateway is {gate.ServiceId}");
            while (true)
            {
                int id = Convert.ToInt32(Console.ReadLine());
                var msg = gate.Send(new ServerProto.Server_Message
                {
                    ServiceId = id,
                    Mian = 1,
                    Sub = 2,
                    ServiceType = (int)gate.ServiceType,
                    Userid = userid,
                    Data = ByteString.CopyFromUtf8($"buss send data {DateTime.Now}")
                });
                if (msg != null)
                    Console.WriteLine("return: " + msg.Data.ToStringUtf8());
            }
            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }
}
