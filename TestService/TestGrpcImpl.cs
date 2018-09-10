
using System.Threading.Tasks;
using Test.Services;
using Grpc.Core;
using AutoMapper;
using System.Collections.Generic;

namespace TestService
{
    public class TestGrpcImpl:TestSrv.TestSrvBase
    {
        /*
        * 1，AutoMapping的使用实例
        * 2，Task异步
        */
        public override Task<PersonListResponse> GetPersonList(GetPersonRequest request, ServerCallContext context)
        {
            return Task.Factory.StartNew<PersonListResponse>(() =>
            {
                var response = new PersonListResponse();

                TestEntity.BLL.Person bll = new TestEntity.BLL.Person();
                var result = bll.GetPersonList(Mapper.Map<GetPersonRequest,TestEntity.Params.GetPersonParam>(request));
                response.Pager = Mapper.Map<TestEntity.Models.Pager<TestEntity.Models.Person>, PagerResponse>(result);
                response.List.AddRange(Mapper.Map<List<TestEntity.Models.Person>, List<Person>>(result.Items));
                
                return response;
            });
        }
    }
}