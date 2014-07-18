using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Server.Entity
{
    [Table("_WorklistHeader", Schema = "dbo")]
    public class WorklistHeader
    {
        [Key]
        [Column(Order = 0)]
        public int ProcInstID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ID { get; set; }
        public int ActInstDestId { get; set; }
        public string Data { get; set; }
    }
}
