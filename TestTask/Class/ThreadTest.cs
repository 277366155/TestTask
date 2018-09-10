using System;
using System.Threading;

namespace TestTask
{
    public class ThreadTest
    {
        public int beginNum = 0;
        public int endNum = 0;
        //声明一个标识线程索引的属性
        public int ThreadIndex = 0;
        //不同线程之间共用一个Statice变量，不同进程之间Statice变量不共用
        public static long Result = 0;
        static readonly object fuck = new object();

        public void Add()
        {
            lock (fuck)//lock (this.GetType())与lock(fuck)等效
            {
                var threadNum = Convert.ToInt32(Thread.CurrentThread.Name);
                for (int num = beginNum; num <= endNum; num++)
                {
                    if (this.ThreadIndex % 2 == 1)
                    {
                        Thread.Sleep(150);
                    }
                    else
                    {
                        Thread.Sleep(200);
                    }
                    Result += num;
                    Console.WriteLine($"线程{ThreadIndex},当前result={Result}");
                }
            }
            //终止当前线程
            Thread.CurrentThread.Abort();
        }
    }
}
