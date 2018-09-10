using Grpc.Core;
using System.Threading.Tasks;

namespace Test.Services
{
    public class PersonServiceImpl:TestSrv.TestSrvBase
    {
        public override Task<PersonListResponse> GetPersonList(GetPersonRequest request, ServerCallContext context)
        {
            return Task.Factory.StartNew<PersonListResponse>(() =>
            {
                TestEntity.BLL.Person bll = new TestEntity.BLL.Person();
                var result = bll.GetPersonList(new TestEntity.Params.GetPersonParam());
                var response = new PersonListResponse();
                return response;
            });
        }
    }
}
