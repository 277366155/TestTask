using System;
using AutoMapper;
using Test.Services;
namespace TestTask
{
    public class AutoMapping
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
        }
    }

    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            ReplaceMemberName("_", "");//将下划线替换成空，Grpc生成的属性没有下划线

            //CreateMap<P, TestEntity.Models.P>().AfterMap((source, destination) =>
            //{
            //    destination.Brithday = Convert.ToDateTime(source.Brithday);
            //    destination.Balance = Convert.ToDecimal(source.Balance);
            //});

            CreateMap<TestEntity.Models.Person, Person>().AfterMap((source, destination) =>
            {
                destination.Brithday = source.Brithday.ToString();
                destination.Balance = Convert.ToDouble(source.Balance);
            }).ForAllMembers(p => {
                p.Condition((s, d, sMember) => {
                    return sMember != null;
                });
            });

            CreateMap<GetPersonRequest, TestEntity.Params.GetPersonParam>().AfterMap((source, destination) => {
                destination.PagerIndex = source.Pager.PageIndex;
                destination.PagerSize = source.Pager.PageSize;
            });

            CreateMap<PagerResponse, TestEntity.Models.Pager<TestEntity.Models.Person>>();
        }
    }
}
