using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity;

namespace DianPing.WorkFlow.Domain.Interface
{
    public interface IK2UserDomain
    {
        /// <summary>
        /// 通过LoginID从K2Users获取一个用户信息
        /// </summary>
        /// <param name="loginID">loginID</param>
        /// <returns></returns>
        K2UserPO GetK2User(int loginID);
        void GenerateApprovalUser(int loginID, Dictionary<string, string> dataFields, string processCode, string actName);
    }
}
