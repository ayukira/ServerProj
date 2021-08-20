using System;

namespace ServerProj.BusinessServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("buss port:");
            int port = Convert.ToInt32(Console.ReadLine());
            var business = new BusinessService("127.0.0.1", port);
            business.Run();
            Console.WriteLine("Business is Running!");
            Console.WriteLine($"Business is {business.ServiceId}");
            //while (true)
            //{
            //    Console.ReadLine();
            //    business.Push(gateId, new ServerProto.Server_Package
            //    {

            //    });
            //    Console.WriteLine($"Send to {gateId}");
            //}
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
    }
}
