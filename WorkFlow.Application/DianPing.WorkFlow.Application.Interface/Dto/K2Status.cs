using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity
{
    public class K2StatusDto
    {
        public int ProcInstId { get; set; }
        public string Folio { get; set; }
        public string Activity { get; set; }
        public DateTime StartDate { get; set; }
        public List<int> LoginIds { get; set; }
    }
}
