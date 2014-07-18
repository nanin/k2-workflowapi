using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianPing.WorkFlow.Domain.Interface.Ddo
{
    public class MyTaskDdo
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

}
