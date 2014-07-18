using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Server.Entity
{
    public class DianPingK2ServerContext : DbContext
    {
        static DianPingK2ServerContext()
        {
            Database.SetInitializer<DianPingK2ServerContext>(null);
        }

        public DianPingK2ServerContext()
            : base("Name=K2ServerConnectionString")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }
        public DbSet<WorklistHeader> WorklistHeader { get; set; }
    }
}
