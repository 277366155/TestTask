using Grpc.Core;
using System.Configuration;
using Test.Services;

namespace TestClient
{
    public  class ClientFactory
    {
        private static string configStr = ConfigurationManager.AppSettings["RemoteServerConfig"].ToString();
        private static Channel channel = new Channel(configStr, ChannelCredentials.Insecure);
        public static TestSrv.TestSrvClient Client;

        static ClientFactory()
        {
            Client = new TestSrv.TestSrvClient(channel);
        }
    }
}
