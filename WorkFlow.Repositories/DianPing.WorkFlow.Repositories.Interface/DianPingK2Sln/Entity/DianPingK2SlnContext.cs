using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity
{
    public class DianPingK2SlnContext:DbContext
    {
          static DianPingK2SlnContext()
        {
            Database.SetInitializer<DianPingK2SlnContext>(null);
        }

          public DianPingK2SlnContext()
              : base("Name=DP_BPM_K2Sln")
          {
              ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
          }


          public DbSet<ProcessInfo> ProcessInfo { get; set; }
          public DbSet<ProcInstBasicInfo> ProcInstBasicInfo { get; set; }
          public DbSet<K2CommentPO> K2Comment { get; set; }
          //public DbSet<K2ParticipatePO> K2ParticipatePO { get; set; }
    }
}
