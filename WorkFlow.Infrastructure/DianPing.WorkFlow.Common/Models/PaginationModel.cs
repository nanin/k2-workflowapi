using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Common.Enum;

namespace DianPing.WorkFlow.Common.Models
{
    public class PaginationModel
    {
        private const int MaxLength = 1000;

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value >= MaxLength ? MaxLength : value; }
        }
        public int PageIndex { set; get; }
        public string SortField { set; get; }
        public SortOrder SortOrder { set; get; }
        public int ItemCount { set; get; }

        public int PageCount
        {
            get { return PageSize == 0 ? 0 : (int) Math.Ceiling((decimal) ItemCount/PageSize); }
            set { return; }
        }
    }
}
