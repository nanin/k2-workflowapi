using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianPing.WorkFlow.Infrastructure.Entity
{
    public class LogEntity
    {
        public string logger { get; set; }
        public string module { get; set; }
        public string msg { get; set; }
        public object param { get; set; }
    }
}
