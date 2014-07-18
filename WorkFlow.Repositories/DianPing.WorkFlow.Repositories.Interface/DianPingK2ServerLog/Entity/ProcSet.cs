using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity
{
    [Table("_procset", Schema = "dbo")]
    public class ProcSet
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Folder { get; set; }
        public string Descr { get; set; }
        public int ProcVerID { get; set; }
        public virtual IList<Proc> Proc { get; set; }
    }
    public class ProcSetMap : EntityTypeConfiguration<ProcSet>
    {
        public ProcSetMap()
        {
            this.HasMany(t => t.Proc).WithRequired(t => t.ProcSet).Map(x => x.MapKey("ProcSetID"));
        }
    }
}
