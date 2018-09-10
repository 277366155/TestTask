using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEntity.Params
{

    public class PagerParam
    {
        public int PagerIndex { get; set; }
        public int PagerSize { get; set; }
        public string OrderStr { get; set; }
    }

    public class GetPersonParam: PagerParam
    {
        public string Name { get; set; }
        public DateTime? BrithdayFrom { get; set; }
        public DateTime? BrithdayTo { get; set; }
    }
}
