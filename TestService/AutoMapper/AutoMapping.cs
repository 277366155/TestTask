using System;
using AutoMapper;
using Test.Services;
namespace TestService
{
    public class AutoMapping
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg=>
            {
                cfg.AddProfile<MappingProfile>();
            });
        }
    }

    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            ReplaceMemberName("_","");//将下划线替换成空，Grpc生成的属性没有下划线

            CreateMap<int?, int>().ConvertUsing<IntNullConverter>();
            CreateMap<int, int?>().ConvertUsing<IntConverter>();
            CreateMap<string, string>().ConvertUsing<StringNullConverter>();

            CreateMap<Person, TestEntity.Models.Person>().AfterMap((source, destination) =>
            {
                destination.Brithday = Convert.ToDateTime(source.Brithday);
                destination.Balance = Convert.ToDecimal(source.Balance);
            });

            CreateMap<TestEntity.Models.Person,Person>().AfterMap((source, destination) =>
            {
                destination.Brithday = source.Brithday.ToString();
                destination.Balance = Convert.ToDouble(source.Balance);
            }).ForAllMembers(p=> {
                p.Condition((s,d,sMember)=> {
                    return sMember != null;
                });
            });

            CreateMap<GetPersonRequest, TestEntity.Params.GetPersonParam>().AfterMap((source,destination)=>{
                destination.PagerIndex = source.Pager.PageIndex;
                destination.PagerSize = source.Pager.PageSize;
                if (!string.IsNullOrWhiteSpace(source.BrithdayFrom))
                {
                    destination.BrithdayFrom = Convert.ToDateTime(source.BrithdayFrom);
                }
                else
                {
                    destination.BrithdayFrom = null;
                }
                if (!string.IsNullOrWhiteSpace(source.BrithdayTo))
                {
                    destination.BrithdayTo = Convert.ToDateTime(source.BrithdayTo);
                }
                else
                {
                    destination.BrithdayTo = null;
                }
            });

            CreateMap<TestEntity.Models.Pager<TestEntity.Models.Person>, PagerResponse>();
        }
    }
}
