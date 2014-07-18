using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity
{
    [Table("_act", Schema = "dbo")]
    public class Act
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }


        public virtual IList<ActInst> ActInst { get; set; }
        //public string ActName { get; set; }

        //public int ActInstId { get; set; }

        //public DateTime StartDate { get; set; }

        //public DateTime FinishDate { get; set; }

        //public int Status { get; set; }

        //public int ProcInstId { get; set; }

        //public string Folio { get; set; }

    }

    public class ActMap : EntityTypeConfiguration<Act>
    {
        public ActMap()
        {
            this.HasMany(t => t.ActInst).WithRequired(t => t.Act).Map(x => x.MapKey("ActID"));

        }
    }
}
