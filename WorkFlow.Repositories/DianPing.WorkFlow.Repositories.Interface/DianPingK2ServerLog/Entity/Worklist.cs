using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DianPing.WorkFlow.Common.Models;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity
{
    [Table("_worklist",Schema="dbo")]
    public class Worklist
    {
        //public int ProcInstID { get; set; }
        [Key]
        public int Id { get; set; }
        public int ActInstDestID { get; set; }
        public string Destination { get; set; }
        public int DestType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public virtual ProcInst ProcInst { get; set; }
    }

    public class QueryWorkList
    {
        public IList<string> ProcessCodes { get; set; }
        public IList<int> ProcInstIds{get;set;}
        public string Folio { get; set; }
        public IList<int> OriginatorLoginIds { get; set; }
        public IList<int> LoginIds{get;set;}
        public DatePeriodModel TaskStartDate { get; set; }
        public DatePeriodModel ProcessStartDate { get; set; }
    }
}
