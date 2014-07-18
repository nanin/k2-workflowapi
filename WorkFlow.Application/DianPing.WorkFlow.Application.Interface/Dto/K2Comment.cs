using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianPing.WorkFlow.Application.Interface.Dto
{
    public class K2CommentDto
    {
        public string Action { get; set; }
        public string ActionTo { get; set; }
        public string ActivityName { get; set; }
        public DateTime AddDate { get; set; }
        public int CommentID { get; set; }
        public int LoginID { get; set; }
        public string Memo { get; set; }
        public string ProcessCode { get; set; }
        public int ProcInstID { get; set; }
        public string RealName { get; set; }
    }
}
