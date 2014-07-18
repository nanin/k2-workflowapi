using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;

namespace DianPing.WorkFlow.Repositories.Interface
{
    /// <summary>
    /// K2审批意见数据访问对象
    /// </summary>
    public interface IK2CommentRepostories
    {
        /// <summary>
        /// 保存k2审批意见
        /// </summary>
        /// <param name="k2Comment">
        /// k2审批意见持久化对象
        /// <see cref="DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity.K2CommentPO"/>
        /// </param>
        int Save(K2CommentPO k2Comment);

        List<K2CommentPO> QueryByProcInstIds(List<int> procInstIds);
    }
}
