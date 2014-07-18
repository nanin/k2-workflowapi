using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity
{
    [Table("_ActInstDest", Schema = "dbo")]
    public class ActInstDest
    {
        [Key]
        public int ID { get; set; }

        public int ProcInstID { get; set; }
        public int ActInstID { get; set; }
        public string User { get;set; }
        public byte Status { get; set; }
        public DateTime StartDate { get; set; }

    }

}
