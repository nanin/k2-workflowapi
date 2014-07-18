using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity
{
    public class DianPingK2ServerLogContext : DbContext
    {
        static DianPingK2ServerLogContext()
        {
            Database.SetInitializer<DianPingK2ServerLogContext>(null);
        }

        public DianPingK2ServerLogContext()
            : base("Name=K2ServerLogConnectionString")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }
        public DbSet<Proc> Proc { get; set; }
        public DbSet<ProcInst> ProcInst { get; set; }
        public DbSet<ProcSet> ProcSet { get; set; }
        public DbSet<Worklist> Worklist { get; set; }
        public DbSet<Act> Act { get; set; }
        public DbSet<ActInst> ActInst { get; set; }
        public DbSet<ActInstDest> ActInstDest { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProcInstMap());
            modelBuilder.Configurations.Add(new ProcMap());
            modelBuilder.Configurations.Add(new ProcSetMap());
            modelBuilder.Configurations.Add(new ActMap());
        }
    }
}
