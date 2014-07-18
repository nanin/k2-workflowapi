using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Common.Models;

namespace DianPing.WorkFlow.Application.Interface.Dto
{
    public class MyTaskDto
    {
        public int ProcInstId { get; set; }
        public string SN { get; set; }
        public string Url { get; set; }
        public string Folio { get; set; }
        public int OriginatorLoginId { get; set; }
        public string ProcessCode { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime ProcessStartDate { get; set; }
    }

    public class MyTaskCriteria
    {
        public int LoginId { get; set; }
        public List<int> OriginatorLoginId { get; set; }
        public List<int> ProcInstId { get; set; }
        public List<string> ProcessCode { get; set; }
        public string Folio { get; set; }
        public DatePeriodModel TaskStartDate { get; set; }
        public DatePeriodModel ProcessStartDate { get; set; }     
    }
        
}
