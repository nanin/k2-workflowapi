using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity
{
    [Table("K2_Participate", Schema = "dbo")]
    public class K2ParticipatePO
    {
        public int ParticipateID { get; set; }
        public int ProcInstID { get; set; }
        public int LoginID { get; set; }
        public string RealName { get; set; }
        public int Category { get; set; }
    }
}
