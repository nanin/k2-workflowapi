using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity
{
    [Table("_proc", Schema = "dbo")]
    public class Proc
    {
        [Key]
        public int ID { get; set; }
        //public int ProcSetID { get; set; }
        public string MetaData { get; set; }
        public int Priority { get; set; }
        public int ExpectedDuration { get; set; }
        public byte LogLevel { get; set; }
        public string BusinessOwner { get; set; }
        public string TechnicalOwner { get; set; }
        public int Ver { get; set; }
        public DateTime? VerDate { get; set; }
        public virtual IList<ProcInst> ProcInst { get; set; }
        public virtual ProcSet ProcSet { get; set; }
    }

    public class ProcMap : EntityTypeConfiguration<Proc>
    {
        public ProcMap()
        {
            this.HasMany(t => t.ProcInst).WithRequired(t => t.proc).Map(x => x.MapKey("ProcID"));
        }
    }
}
