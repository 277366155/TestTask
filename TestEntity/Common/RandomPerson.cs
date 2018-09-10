using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEntity.Common
{
    public class RandomPerson
    {
        //声明一个种子，防止在循环中生成的随机数一样
        private int Seed1 ;
        private int Seed2;
        public RandomPerson()
        {
            byte[] buffer1 = Guid.NewGuid().ToByteArray();            
            this.Seed1 = BitConverter.ToInt32(buffer1, 0);

            byte[] buffer2 = Guid.NewGuid().ToByteArray();
            this.Seed2 = BitConverter.ToInt32(buffer2, 0);
        }
        public int RandomAge
        {
            get
            {
                Random rd = new Random(Seed1);
                return rd.Next(1, 100);
            }
        }

        public string RandomName
        {
            get
            {
                string[] firstNames = { "张", "王", "李", "赵", "周", "吴", "郑", "梁", "何" };
                string[] lastNames = { "苗", "锋", "涛", "林", "恒", "磊", "斌", "森", "阳", "文", "东", "洁" };

                //firstName 和 lastName 不能使用同一个种子生成随机数，会导致生成的姓名组合固定！！
                var firstName = firstNames[new Random(Seed1).Next(0, firstNames.Length)];
                var lastName = lastNames[new Random(Seed2).Next(0, lastNames.Length)];

                return firstName + lastName;
            }
        }

        public decimal RandomBalance
        {
            get
            {
                Random rn = new Random(Seed1);
                var result = rn.Next(1000, 1000000);
                return Convert.ToDecimal(result / 100.0);
            }
        }

        public DateTime RandomBirthDay
        {
            get
            {
                Random rd = new Random(Seed1);
                int year = rd.Next(1900, 2017);
                int month = rd.Next(1, 13);
                int day = rd.Next(1, 29);
                int hour = rd.Next(0, 24);
                int minute = rd.Next(0, 60);
                int second = rd.Next(0, 60);

                return new DateTime(year, month, day, hour, minute, second);
            }
        }
    }
}
