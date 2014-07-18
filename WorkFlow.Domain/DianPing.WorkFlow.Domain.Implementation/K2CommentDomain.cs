using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Domain.Interface;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Repositories.Interface;
using DianPing.WorkFlow.Infrastructure.Interception;

namespace DianPing.WorkFlow.Domain.Implementation
{
    public class K2CommentDomain : IK2CommentDomain
    {
        [Dependency]
        public virtual IK2CommentRepostories k2CommentRepostories { get; set; }

        /// <summary>
        /// 保存k2审批意见
        /// </summary>
        /// <param name="k2Comment">
        /// k2审批意见持久化对象
        /// <see cref="DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity.K2CommentPO"/>
        /// </param>
        public void Save(K2CommentPO k2Comment)
        {
            k2CommentRepostories.Save(k2Comment);
        }
    }
}
