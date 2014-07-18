using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Infrastructure.Interception;
using Microsoft.Practices.Unity;

namespace DianPing.WorkFlow.Repositories.Implementation.DianPingK2Sln
{
    /// <summary>
    /// k2审批意见数据访问实现类
    /// </summary>
    public class K2CommentRepostories : IK2CommentRepostories
    {
        /// <summary>
        /// 保存k2审批意见
        /// </summary>
        /// <param name="k2Comment">
        /// k2审批意见持久化对象
        /// <see cref="DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity.K2CommentPO"/>
        /// </param>
        public int Save(K2CommentPO k2Comment)
        {
            var edm = new DianPingK2SlnContext();
            edm.K2Comment.Add(k2Comment);
            return edm.SaveChanges();
        }


        public List<K2CommentPO> QueryByProcInstIds(List<int> procInstIds)
        {
            var edm = new DianPingK2SlnContext();            
            return edm.K2Comment.Where(_=>procInstIds.Contains( _.ProcInstID)).OrderBy(_=>_.ProcessCode).ThenBy(_=>_.ProcInstID).ToList();
        }
    }
}
