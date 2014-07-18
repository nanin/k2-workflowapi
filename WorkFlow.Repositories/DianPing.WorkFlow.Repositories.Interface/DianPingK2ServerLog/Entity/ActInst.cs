using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity
{
    [Table("_actInst", Schema = "dbo")]
    public class ActInst
    {
        [Key]
        public int ID { get; set; }

        //public int ProcInstID { get; set; }
        //public int ActID { get; set; }
        public byte Status { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Act Act { get; set; }
        public virtual ProcInst ProcInst { get; set; }
    }

    //public class ActInstMap : EntityTypeConfiguration<ActInst>
    //{
    //    public ActInstMap()
    //    {
    //        this.HasRequired(t => t.ProcInst).WithMany(t => t.ActInst).Map(x => x.MapKey("ID"));

    //    }
    //}
}
