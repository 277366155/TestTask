using System;
using System.Threading;
using TestEntity.BLL;
namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTest(0, 50);

            //TestEntity.BLL.P bll = new TestEntity.BLL.P();
            //var r=bll.GetPersonList(null);
            //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(r));

            //GetList();
            //InserRandomPerson();
            //ThreadTest(0,50);

            //ActionTest();
            GetList();
            //FuncTest();
            Console.ReadLine();
        }


        #region 多线程示例
        /// <summary>
        /// 多线程示例：
        /// 累加计算 分五个线程进行计算
        /// </summary>
        /// <param name="beginNum"></param>
        /// <param name="endNum"></param>
        public static void ThreadTest(int beginNum, int endNum)
        {
            int threadCount = 5;

            for (int i = 1; i <= threadCount; i++)
            {
                ThreadTest test = new ThreadTest();
                test.ThreadIndex = i;

                test.beginNum = beginNum + (endNum - beginNum) / threadCount * (i - 1) + 1;
                test.endNum = beginNum + (endNum - beginNum) / threadCount * i;
                Console.WriteLine($"{test.beginNum},{test.endNum}");

                ThreadStart threadStart = new ThreadStart(test.Add);
                Thread thread = new Thread(threadStart);//等同于Thread thread = new Thread(test.Add); 依据个人习惯的不同写法，没实质的差别
                /*
                 * thread.IsBackground = true;设置线程为后台线程
                 * 在主线程退出之前，其他线程都不会退出。
                 * 在本例中，如果主线程中没有Console.ReadLine(); 主线程不会等其他线程完成就退出
                 * 此时，本方法可能没有执行完成。
                 */
                thread.IsBackground = true;
                thread.Start();
            }
        }
        #endregion

        #region GRPC服务示例
        private static void GetList()
        {
            AutoMapping.Configure();
            var client = TestClient.ClientFactory.Client;
            var result = client.GetPersonList(new Test.Services.GetPersonRequest()
            {
                Pager = new Test.Services.PagerRequest()
                {
                    PageIndex = 1,
                    PageSize = 20
                },
                Name = "bo"
            });

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }
        #endregion

        #region  随机生成Person信息插入数据库
        public static void InserRandomPerson()
        {
            var bll = new TestEntity.BLL.Person();
            bll.RandomInsertPerson(5000);
        }
        #endregion

        #region 委托测试

        /// <summary>
        /// Action实例
        /// </summary>
        public static void ActionTest()
        {
            var bll = new TestEntity.BLL.Person();
            TestEntity.Models.Person p = new TestEntity.Models.Person()
            {
                Age = 11,
                Name = "张",
                Balance = 100
            };

            TestTool.SetPersonAction(p, (m) => { m.Name += "哒哒"; });
            TestTool.SetPersonAction((() => Console.WriteLine("哈哈哈")));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(p));
        }

        /// <summary>
        /// Func实例
        /// </summary>
        public static void FuncTest()
        {
            var bll = new TestEntity.BLL.Person();
            TestEntity.Models.Person p = new TestEntity.Models.Person()
            {
                Age = 12,
                Name = "郑格",
            };

            TestTool.SetPersonFunc(() => { return "Func 执行ing ……"; });

            p.SetPersonFunc(m => { return "姓名：" + m.Name + "，年龄：" + m.Age.ToString(); });

            string msg = "我是如来佛祖玉皇大帝观音菩萨指派取西经特派使者花果山水帘洞美猴王齐天大圣孙悟空啊！帅的掉渣！";
            msg.PredicateTest(s => { return s.Length > 30; });

        }
        #endregion 委托测试
    }
}
