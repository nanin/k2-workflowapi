using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Domain.Interface;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using System.Reflection;
using DianPing.WorkFlow.Infrastructure.K2Exception;
using DianPing.WorkFlow.Infrastructure.K2;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Repositories.Interface;
using DianPing.WorkFlow.Common.Enum;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog;

namespace DianPing.WorkFlow.Domain.Implementation
{
    public class ProcessInfoDomain : IProcessInfoDomain
    {
        [Dependency]
        public virtual IProcessInfoRepostories ProcessInfoRepostories { get; set; }

        [Dependency]
        public virtual IPorcessInstRepostories PorcessInstRepostories { get; set; }

        [Dependency]
        public virtual IK2ServiceProvider K2ServiceProvider { get; set; }

        [Dependency]
        public virtual IK2CommentRepostories K2CommentRepostories { get; set; }

        [Dependency]
        public virtual IActInstDestRepostories ActInstDestRepostories { get; set; }
        

        public IList<ProcessInfo> GetByProcessCode(IList<string> processCode)
        {
            return ProcessInfoRepostories.GetByProcessCode(processCode);
        }
        /// <summary>
        /// 获取流程实例基本信息
        /// </summary>
        /// <param name="procInstId"></param>
        /// <returns></returns>
        public ProcInstBasicInfo GetProcInstBasicInfo(int procInstId)
        {
            return ProcessInfoRepostories.GetProcInstBasicInfo(procInstId);
        }

        public ResultModel StartProcess(string processCode, string processName, int loginId, string realName, string ObjectId, string Folio, Dictionary<string, string> dataFields)
        {
            int procInstID = 0;
            var jr = K2ServiceProvider.StartProcess(processName, loginId, ObjectId, Folio, dataFields, out procInstID);
            if (jr.Code == ResultCode.Sucess)
            {
                if (procInstID > 0)
                {
                    var comment = new K2CommentPO();
                    comment.ActivityName = "发起人";
                    comment.ProcessCode = processCode;
                    comment.AddDate = DateTime.Now;
                    comment.ProcInstID = procInstID;
                    comment.Action = "提交";
                    comment.Memo = "发起流程";
                    comment.RealName = realName;
                    comment.LoginID = loginId;
                    try
                    {
                        K2CommentRepostories.Save(comment);
                    }
                    catch{ }

                }
            }
            return jr;
        }
                        
        public List<K2CommentPO> GetCommentByProcInstIds(List<int> procInstIds)
        {
            return K2CommentRepostories.QueryByProcInstIds(procInstIds);
        }
        
        public List<K2Status> GetProcessStatusByProcInstId(int procInstId)
        {
            List<K2Status> list = new List<K2Status>();
            var actList = ProcessInfoRepostories.GetProcessStatusByProcInstId(procInstId);
            list = MapStatus(actList);

            if (list.Count == 0)
            {
                var procInst = ProcessInfoRepostories.GetProcInstById(procInstId);

                list.Add(new K2Status()
                {
                    ProcInstId = procInstId,
                    Activity = MapProcInstStatus(procInst == null ? -1 : procInst.Status),
                    Folio = procInst == null ? null : procInst.Folio,
                    StartDate = procInst == null ? DateTime.MinValue : procInst.FinishDate
                });
            }
            return list;
        }

        public List<K2Status> GetProcessStatusByFolio(string folio)
        {
            List<K2Status> list = new List<K2Status>();
            var actList = ProcessInfoRepostories.GetProcessStatusByFolio(folio);
            list = MapStatus(actList);

            if (list.Count == 0)
            {
                var procInst = ProcessInfoRepostories.GetProcInstByFolio(folio);
                list.Add(new K2Status()
                {
                    ProcInstId = procInst.ID,
                    Activity = MapProcInstStatus(procInst == null ? -1 : procInst.Status),
                    Folio = procInst == null ? null : procInst.Folio,
                    StartDate = procInst == null ? DateTime.MinValue : procInst.FinishDate
                });
            }
            return list;
        }

        private List<K2Status> MapStatus(List<ActInst> ActInstList)
        {
            List<K2Status> list = new List<K2Status>();
            foreach (var a in ActInstList)
            {

                K2Status status = new K2Status();
                status.ProcInstId = a.ProcInst.ID;
                status.Folio = a.ProcInst.Folio;
                status.StartDate = a.StartDate;
                if (a.Status == 2)
                {
                    status.Activity = a.Act.Name;

                    try
                    {
                        var userlist2 = ActInstDestRepostories.queryByPorcInstIdAndActInstId(status.ProcInstId, a.ID).Select(_=>_.ID);

                        var userlist = a.ProcInst.Worklist;
                        //var userlist = a.Act.ActInst.FirstOrDefault(_ => _.ID == a.ID).ProcInst.Worklist;
                        if (userlist != null)
                        {
                            status.LoginIds = userlist.Count > 0
                                ? userlist.Where(_ => _.Status == 0)
                                    .Where(_ => userlist2.Contains(_.ActInstDestID))
                                    .Select(t => Convert.ToInt32(t.Destination.Replace("K2SQL:", ""))).ToList()
                                : new List<int>();
                            //status.LoginIds = userlist.Count > 0
                            //    ? userlist.Where(_ => _.Status == 0).Select(t => Convert.ToInt32(t.Destination.Replace("K2SQL:", ""))).ToList()
                            //    : new List<int>();
                        }
                    }
                    catch { }
                }
                else
                {
                    status.Activity = MapProcInstStatus(a.Status);
                }
                list.Add(status);
            }
            return list;
        }
        
        private static string MapProcInstStatus(int status)
        {
            string result = "未知";
            switch (status)
            {
                case 0:
                    result = "流程异常";
                    break;
                case 1:
                    result = "流程处理中";
                    break;
                case 2:
                    break;
                case 3:
                    result = "流程结束";
                    break;
                case 4:
                    result = "流程停止";
                    break;
                case 5:
                    result = "流程删除";
                    break;
                default: break;
            }
            return result;
        }



        public ProcInst GetProcInstById(int procInstId)
        {
            return PorcessInstRepostories.GetProcInst(procInstId);
        }
    }
}
