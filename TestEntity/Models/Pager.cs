using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEntity.Models
{
    public class Pager<T>
    {
        public int PagerIndex { get; set; }
        public int PagerSize { get; set; }
        public int RowCount { get; set; }
        public int TotalPages
        {
            get
            {
                return RowCount / PagerSize + RowCount % PagerSize > 0 ? 1 : 0;
            }
        }
        public List<T> Items { get; set; }
    }
}
