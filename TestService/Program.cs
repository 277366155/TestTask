using System;
using Topshelf;
using Test.Services;

namespace TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            //AutoMapping要在程序运行前运行配置
            AutoMapping.Configure();
            ServiceStart();
        }

        static void ServiceStart()
        {
            /*
             *Topshelf的使用实例
             */
            HostFactory.Run( x=>
            {
                //报错时，触发
                x.OnException((ex)=>
                {
                    Console.WriteLine($"【Error】错误信息：{ex.Message}");
                });

                x.Service<GrpcServer>(s=>
                {
                    s.ConstructUsing(name=>new GrpcServer());
                    
                    //启动时调用
                    s.WhenStarted(tc=>tc.Start());
                    //停止时回调
                    s.WhenStopped(tc=>tc.Stop());
                });

                x.RunAsLocalService();
                x.StartAutomatically();
                x.SetDescription("Test Grpc service");
                x.SetDisplayName("TestGrpc");
                x.SetServiceName("TestService");
            });
        }


    }
}
