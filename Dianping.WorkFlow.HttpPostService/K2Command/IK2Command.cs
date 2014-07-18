using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianping.WorkFlow.HttpPostService
{
    /// <summary>
    /// 调用K2操作接口
    /// </summary>
    public interface IK2Command
    {
        /// <summary>
        /// 执行具体的k2操作
        /// </summary>
        void Excute();
    }
}
