using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity;

namespace DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity
{
    public class DianpingK2SQLUMContext : DbContext
    {
        static DianpingK2SQLUMContext()
        {
            Database.SetInitializer<DianpingK2SQLUMContext>(null);
        }

        public DianpingK2SQLUMContext()
            : base("Name=K2SQLUM")
        {
        }

        public DbSet<K2UserPO> K2User { get; set; }
        public DbSet<K2CityPO> K2City { get; set; }
    }
}
