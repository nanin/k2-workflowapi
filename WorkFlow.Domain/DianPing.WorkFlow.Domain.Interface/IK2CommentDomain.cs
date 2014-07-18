using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;

namespace DianPing.WorkFlow.Domain.Interface
{
    /// <summary>
    /// K2审批意见领域接口
    /// </summary>
    public interface IK2CommentDomain
    {
        /// <summary>
        /// 保存k2审批意见
        /// </summary>
        /// <param name="k2Comment">
        /// k2审批意见持久化对象
        /// <see cref="DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity.K2CommentPO"/>
        /// </param>
        void Save(K2CommentPO k2Comment);
    }
}
