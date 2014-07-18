using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity
{
    [Table("K2_ProcInstBasicInfo", Schema = "dbo")]
    public class ProcInstBasicInfo
    {
        [Key]
        public int ProcInstBasicInfoID { get; set; }
        public Guid Guid { get; set; }
        public int ProcInstID { get; set; }
        public int ProcessStatus { get; set; }
        public int OriginatorLoginID { get; set; }
        public string OriginatorName { get; set; }
        public string Mobile { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int AdminCityID { get; set; }
        public string AdminCityName { get; set; }
        public DateTime? LaunchDate { get; set; }
        public string ReceiverLoginID { get; set; }
        public string ReceiverName { get; set; }
        public int LoginID { get; set; }
        public string LoginName { get; set; }
        public DateTime? AddDate { get; set; }
        public int ProcessStatus1 { get; set; }
        public string Memo { get; set; }
        public string ProcessCode { get; set; }
    }
}
