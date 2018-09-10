using System;
using System.Collections.Generic;
using System.Linq;
using TestEntity.Models;

namespace TestEntity.BLL
{
    public class Person
    {
        /// <summary>
        /// 此处仅做示例来展示。 未考虑性能
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Pager<Models.Person> GetPersonList(Params.GetPersonParam param)
        {
            Pager<Models.Person> result = new Pager<Models.Person>();

            //try
            //{
            string filter = " 1=1 ";
            if (param == null)
            {
                param = new Params.GetPersonParam();
            }

            if (!string.IsNullOrWhiteSpace(param.Name))
            {
                filter += $" and  Name like '%{param.Name}%'";
            }
            if (param.BrithdayFrom.HasValue)
            {
                filter += $" and  Brithday >='{param.BrithdayFrom.Value}'";
            }
            if (param.BrithdayTo.HasValue)
            {
                filter += $" and  Brithday <='{param.BrithdayTo.Value}'";
            }

            if (param.PagerIndex <= 0)
            {
                param.PagerIndex = 1;
            }
            if (param.PagerSize <= 0)
            {
                param.PagerSize = 20;
            }
            if (string.IsNullOrWhiteSpace(param.OrderStr))
            {
                param.OrderStr = "ID desc ";
            }

            string sql = $@"select ID, Name, Age, Brithday, Balance from person 
                            where {filter} 
                            order by {param.OrderStr}
                            offset {(param.PagerIndex - 1) * param.PagerSize} row 
                            fetch next {param.PagerSize} row only ";

            string rowCountSql = $"select count(1) from P where {filter}";

            using (Entity entity = new Entity(true))
            {
                result.Items = entity.Database.SqlQuery<Models.Person>(sql).ToList();
                result.RowCount = entity.Database.SqlQuery<int>(rowCountSql).FirstOrDefault();
                result.PagerIndex = param.PagerIndex;
                result.PagerSize = param.PagerSize;
            }
            //}
            /*
            *底层方法供GRPC调用时，不要try-catch 再throw，这样会导致grpc中无法提示准确的错误信息
            * */
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return result;
        }

        public int Insert(List<Models.Person> personList)
        {
            try
            {
                int reslut = 0;
                if (personList == null)
                {
                    return 0;
                }
                using (var dbContext = new Entity())
                {
                    foreach (var model in personList)
                    {
                        dbContext.Person.Add(model);
                        reslut++;
                    }
                    dbContext.SaveChanges();
                }
                return reslut;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 随机生成数据插入数据库
        /// </summary>
        /// <param name="num">生成数据个数</param>
        /// <returns></returns>
        public int RandomInsertPerson(int num)
        {
            if (num <= 0)
            {
                return 0;
            }
            List<Models.Person> list = new List<Models.Person>();
            for (var i = 1; i <= num; i++)
            {
                var random = new Common.RandomPerson();
              var model=  new Models.Person()
                {
                    Age = random.RandomAge,
                    Balance = random.RandomBalance,
                    Name = random.RandomName,
                    Brithday = random.RandomBirthDay
                };
                list.Add(model);                
            }
           return Insert(list);
        }
    }

    public static class TestTool
    {
        public static void SetPersonAction(Models.Person person, Action<Models.Person> act)
        {
            act(person);

            Console.WriteLine("action 进来了");
        }

        public static void SetPersonAction(Action act)
        {
            act();
            Console.WriteLine("无参action 进来了");
        }

        public static void SetPersonFunc(Func<string> func)
        {
            Console.WriteLine(func());
        }

        public static void SetPersonFunc(this Models.Person person, Func<Models.Person, string> func)
        {
            Console.WriteLine(func(person));
        }

        public static void PredicateTest(this string str,Predicate<string> pre)
        {
            Console.WriteLine("Predicate 执行ing……");
            Console.WriteLine(pre(str));
        }
    }
}
