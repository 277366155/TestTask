using System;
using System.Configuration;
using Grpc.Core;

/*
*GRPC服务端的使用实例
*/
namespace TestService
{
    public class GrpcServer
    {
        Server server;

        public void Start()
        {
            //读取服务端配置
            string config = ConfigurationManager.AppSettings["LocalServerConfig"].ToString();
            var host = config.Split(':');
            if (string.IsNullOrWhiteSpace(host[0]) || string.IsNullOrWhiteSpace(host[1]))
            {
                throw new Exception("LocalServerConfig配置错误");
            }

            //启动一个服务
            server = new Server()
            {
                //注册一个服务
                Services = { Test.Services.TestSrv.BindService(new TestGrpcImpl()) },
                //配置ip，端口，不加密【若要继承ServerCredentials类，传入加密标识，需重载ServerPort，参考源码demo】
                Ports = { new ServerPort(host[0], int.Parse(host[1]), ServerCredentials.Insecure) }
            };

            server.Start();
        }

        public void Stop()
        {
            server?.ShutdownAsync();
        }
    }
}
