using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianPing.WorkFlow.Common.Models
{
    public class QueryListResultBase<T> where T : new()
    {
        public PaginationModel PagingInfo { set; get; }
        public List<T> ResultList { set; get; }
    }
}
