using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianPing.WorkFlow.Domain.Interface;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Common.Models;

namespace DianPing.WorkFlow.Domain.Implementation
{
    public class ApprovalProcessCommand : IK2Command
    {
        public string Memo { get; set; }
        public string ActionString { get; set; }
        public string ApiKey { get; set; }
        public string JsonData { get; set; }
        public int LoginId { get; set; }
        public string ProcessCode { get; set; }
        public string SN { get; set; }

        private IK2ServiceProvider k2ServiceProvider;

        public ApprovalProcessCommand(IK2ServiceProvider k2ServiceProvider)
        {
            this.k2ServiceProvider = k2ServiceProvider;
        }       

        /// <summary>
        /// 调用K2接口审批流程
        /// </summary>
        public ResultModel Excute()
        {
            //return k2ServiceProvider.ApproveK2Process(ProcessCode, SN, LoginId, ActionString, Memo, JsonData);
            return null;
        }
    }
}