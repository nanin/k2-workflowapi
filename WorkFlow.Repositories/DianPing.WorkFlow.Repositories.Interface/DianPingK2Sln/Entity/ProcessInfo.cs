using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity
{
    [Table("K2_ProcessInfo", Schema = "dbo")]
    public class ProcessInfo
    {
        [Key]
        public int ProcessInfoID { get; set; }
        public string ProcessCode { get; set; }
        public string FlowCodePrefix { get; set; }
        public string ProcessFolder { get; set; }
        public int ProcessType { get; set; }
        public string ProcessFullName { get; set; }
        public string Alias { get; set; }
        public string LaunchDomain { get; set; }
        public string LaunchPage { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool EnableFlag { get; set; }
        public bool CancelFlag { get; set; }
        public string CancelActivityName { get; set; }
        public bool BatchApproveFlag { get; set; }
        public bool RedirectFlag { get; set; }
        public bool AddApproverFlag { get; set; }
        public int MessageConfigID { get; set; }
        public bool MessageFlag { get; set; }
        public bool MailFlag { get; set; }
        public bool SMSFlag { get; set; }
    }
}
