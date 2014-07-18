using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Domain.Interface;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Domain.Interface.Ddo;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;
using AutoMapper;
using DianPing.BPM.Common.AppSettings;
using DianPing.WorkFlow.Infrastructure.K2;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;
using DianPing.WorkFlow.Repositories.Interface;
using Newtonsoft.Json;
using DianPing.WorkFlow.Common.Enum;

namespace DianPing.WorkFlow.Domain.Implementation
{
    public class TaskDomain : ITaskDomain
    {
        static TaskDomain()
        {
            Mapper.CreateMap<DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity.Worklist, MyTaskDdo>()
                .ForMember(d => d.ProcessCode, s => s.MapFrom(_ => _.ProcInst.proc.ProcSet.Descr))
                .ForMember(d => d.ProcInstId, s => s.MapFrom(_ => _.ProcInst.ID))
                .ForMember(d => d.Folio, s => s.MapFrom(_ => _.ProcInst.Folio))
                .ForMember(d => d.OriginatorLoginId, s => s.MapFrom(_ => _.ProcInst.Originator.ToLower().Replace("k2sql:","")))
                .ForMember(d => d.SN, s => s.MapFrom(_ => string.Format("{0}_{1}", _.ProcInst.ID.ToString(), _.ActInstDestID.ToString())))
                .ForMember(d => d.TaskStartDate, s => s.MapFrom(_ => _.StartDate))
                .ForMember(d => d.ProcessStartDate, s => s.MapFrom(_ => _.ProcInst.StartDate))
                ;
            Mapper.CreateMap<QueryListResultBase<Worklist>, QueryListResultBase<MyTaskDdo>>();
        }

        [Dependency]
        public virtual IWorklistRepostories WorklistRepostories { get; set; }

        [Dependency]
        public virtual IK2ServiceProvider K2ServiceProvider { get; set; }

        [Dependency]
        public virtual IK2CommentRepostories K2CommentRepostories { get; set; }
        
        public QueryListResultBase<MyTaskDdo> GetMyTaskList(QueryCriteriaBase<QueryWorkList> queryPara)
        {
            var sourceResult = WorklistRepostories.GetWorkList(queryPara);
            return Mapper.Map<QueryListResultBase<MyTaskDdo>>(sourceResult);
        }

        public ResultModel ApproveK2Process(string processCode, string sn, int loginId, string realName, string actionString, string memo, Dictionary<string, string> dataFields)
        {
            string activityName = string.Empty;
            int procInstID = 0;
            var jr = K2ServiceProvider.ApproveK2Process(sn, loginId, actionString, memo, dataFields, out activityName, out processCode, out procInstID);
            if (jr.Code == ResultCode.Sucess)
            {
                if (procInstID > 0)
                {

                    var comment = new K2CommentPO();
                    comment.ActivityName = activityName;
                    comment.ProcInstID = procInstID;
                    comment.ProcessCode = processCode;
                    comment.Action = actionString;
                    comment.LoginID = loginId;
                    comment.RealName = realName;
                    comment.AddDate = DateTime.Now;
                    comment.Memo = string.IsNullOrEmpty(memo) ? string.Empty : memo;
                    try
                    {
                        K2CommentRepostories.Save(comment);
                    }
                    catch { }
                }
            }
            return jr;
        }
        
        public ResultModel ReAssign(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName, bool isAddLog)
        {
            string activityName = string.Empty;
            string processCode = string.Empty;
            int procInstID = 0;
            var jr = K2ServiceProvider.ReAssign(sn, assignFromLoginId, assignToLoginId, out activityName, out processCode, out procInstID);
            if (jr.Code == ResultCode.Sucess)
            {
                if (isAddLog)
                {
                    if (procInstID>0)
                    {
                        var comment = new K2CommentPO();
                        comment.ActivityName = activityName;
                        comment.ProcInstID = procInstID;
                        comment.ProcessCode = processCode;
                        comment.Action = "转签";
                        comment.LoginID = assignFromLoginId;
                        comment.RealName = assignFromRealName;
                        comment.ActionTo = assignToRealName;
                        comment.AddDate = DateTime.Now;
                        comment.Memo = string.Format("{0}转签给{1}", assignFromRealName, assignToRealName);
                        try
                        {
                            K2CommentRepostories.Save(comment);
                        }
                        catch { }
                    }
                }
            }
            return jr;
        }

        public ResultModel Involve(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName)
        {
            string activityName = string.Empty;
            string processCode = string.Empty;
            int procInstID = 0;
            var jr = K2ServiceProvider.Involve(sn, assignFromLoginId, assignToLoginId, out activityName, out processCode, out procInstID);
            if (jr.Code == ResultCode.Sucess)
            {
                if (procInstID > 0)
                {
                    var comment = new K2CommentPO();
                    comment.ActivityName = activityName;
                    comment.ProcInstID = procInstID;
                    comment.ProcessCode = processCode;
                    comment.Action = "加签";
                    comment.LoginID = assignFromLoginId;
                    comment.RealName = assignFromRealName;
                    comment.ActionTo = assignToRealName;
                    comment.AddDate = DateTime.Now;
                    comment.Memo = string.Format("{0}加签了{1}", assignFromRealName, assignToRealName);
                    try
                    {
                        K2CommentRepostories.Save(comment);
                    }
                    catch { }
                }
            }
            return jr;
        }
    }
}
