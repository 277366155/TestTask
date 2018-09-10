using System;
using System.Data.Entity;

namespace TestEntity
{
    public class Entity : DbContext
    {
        public Entity() : base("name=DBConnect")
        {

        }

        /// <summary>
        /// 支持主从库读写分离
        /// </summary>
        /// <param name="readOnly"></param>
        public Entity(bool readOnly) : base(readOnly?"name=DBConnectReadOnly":"name=DBConnect")
        {

        }
        public virtual DbSet<Models.Person> Person { get;set;}
    }
}
