using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianPing.WorkFlow.Domain.Implementation
{
    public enum OperationType
    {
        /// <summary>
        /// 开启流程
        /// </summary>
        Start,

        /// <summary>
        /// 审批流程
        /// </summary>
        Approval
    }
}
