using Grpc.Core;
using ServerProj.Base;
using System;

namespace ServerProj.RegistryServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { ServerProto.RegistryGrpc.BindService(new RegistryGrpc()) },
                Ports = { new ServerPort(StaticConfig.RegistryHost, StaticConfig.RegistryPort, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("gRPC server listening on port " + StaticConfig.RegistryPort);
            Console.WriteLine("任意键退出...");
            Console.ReadKey();

            server.ShutdownAsync();

            Console.WriteLine("Hello World!");
        }
    }
}
