using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianPing.WorkFlow.Common.Enum
{

    /// <summary>
    /// Represents the sorting style.
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// Indicates that the sorting style is not specified.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Indicates an ascending sorting.
        /// </summary>
        Ascending = 1,

        /// <summary>
        /// Indicates a descending sorting.
        /// </summary>
        Descending = 2
    }

    public enum ResultCode
    {
        Sucess = 200,
        Fail = 500
    }
}
