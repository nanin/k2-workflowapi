using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity
{
    /// <summary>
    /// 审批意见持久化类
    /// </summary>
    [Table("K2_Comment", Schema = "dbo")]
    public class K2CommentPO
    {
        [Key]
        public int CommentID { get; set; }
        public int ProcInstID { get; set; }
        public string Action { get; set; }
        public string ActionTo { get; set; }
        public string ProcessCode { get; set; }
        public int LoginID { get; set; }
        public string RealName { get; set; }
        public string ActivityName { get; set; }
        public DateTime AddDate { get; set; }
        public string Memo { get; set; }
    }
}
