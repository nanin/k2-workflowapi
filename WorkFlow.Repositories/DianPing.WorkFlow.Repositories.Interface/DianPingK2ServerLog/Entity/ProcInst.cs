using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity
{
    [Table("_ProcInst", Schema = "dbo")]
    public class ProcInst
    {
        [Key]
        public int ID { get; set; }
        public int BatchID { get; set; }
        //public int ProcID { get; set; }
        public byte Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Originator { get; set; }
        public int Priority { get; set; }
        public int ExpectedDuration { get; set; }
        public string Folio { get; set; }
        public bool Delegated { get; set; }
        public string DelegatedBy { get; set; }
        public int ExecutingProcID { get; set; }
        public virtual IList<Worklist >Worklist { get; set; }
        public virtual Proc proc { get; set; }
        public virtual IList<ActInst> ActInst { get; set; }
    }

    public class ProcInstMap : EntityTypeConfiguration<ProcInst>
    {
        public ProcInstMap()
        {
            //this.HasOptional(t => t.Worklist).WithRequired(t => t.ProcInst).Map(x => x.MapKey("ProcInstID"));
            this.HasMany(t => t.Worklist).WithRequired(t => t.ProcInst).Map(x => x.MapKey("ProcInstID"));
            this.HasMany(t => t.ActInst).WithRequired(t => t.ProcInst).Map(x => x.MapKey("ProcInstID"));

        }
    }
}
