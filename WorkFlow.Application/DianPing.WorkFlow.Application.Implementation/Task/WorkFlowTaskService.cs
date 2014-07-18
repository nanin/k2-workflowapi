using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Infrastructure.Interception;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Application.Interface.Dto;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Domain.Interface;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;
using DianPing.WorkFlow.Domain.Interface.Ddo;
using AutoMapper;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Common.Enum;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Server;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Server.Entity;

namespace DianPing.WorkFlow.Application.Implementation.Task
{
    public class WorkFlowTaskService : IWorkFlowTaskService
    {
        static WorkFlowTaskService()
        {
            Mapper.CreateMap<MyTaskDdo, MyTaskDto>();
            Mapper.CreateMap<QueryListResultBase<MyTaskDdo>, QueryListResultBase<MyTaskDto>>();
        }

        [Dependency]
        public virtual ITaskDomain MyTaskDomain { get; set; }

        [Dependency]
        public virtual IProcessInfoDomain ProcessInfoDomain { get; set; }

        [Dependency]
        public virtual IK2CommentDomain k2CommentDomain { get; set; }

        [Dependency]
        public virtual IK2UserDomain k2UserDomain { get; set; }

        [Dependency]
        public virtual IWorklistHeaderRepositories WorklistHeaderRepositories { get; set; }
        
        //[Exception]
        //[Performance]
        [Cat]
        public ResultModel ReAssign(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName, bool isAddLog)
        {
            ResultModel jr = new ResultModel() { Code = ResultCode.Fail };

            if (sn.IndexOf('_') <= 0)
            {
                jr.Msg = "sn格式错误";
            }
            else if (assignFromLoginId == 0 || assignToLoginId == 0)
            {
                jr.Msg = "LoginId错误";
            }
            else if (isAddLog && (string.IsNullOrEmpty(assignFromRealName) || string.IsNullOrEmpty(assignToRealName)))
            {
                jr.Msg = "姓名错误";
            }
            else
            {
                jr = MyTaskDomain.ReAssign(sn, assignFromLoginId, assignFromRealName, assignToLoginId, assignToRealName, isAddLog);

                if (jr.Code == ResultCode.Sucess)
                {
                    jr.Msg = "ReAssign成功";
                }
            }

            return jr;
        }

        //[Exception]
        //[Performance]
        [Cat]
        public ResultModel Involve(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName)
        {
            ResultModel jr = new ResultModel() { Code = ResultCode.Fail };

            if (sn.IndexOf('_') <= 0)
            {
                jr.Msg = "sn格式错误";
            }
            else if (assignFromLoginId == 0 || assignToLoginId == 0)
            {
                jr.Msg = "LoginId错误";
            }
            else if (string.IsNullOrEmpty(assignFromRealName) || string.IsNullOrEmpty(assignToRealName))
            {
                jr.Msg = "姓名错误";
            }
            else
            {
                jr = MyTaskDomain.Involve(sn, assignFromLoginId, assignFromRealName, assignToLoginId, assignToRealName);

                if (jr.Code == ResultCode.Sucess)
                {
                    jr.Msg = "Involve成功";
                }
            }

            return jr;
        }

        //[Exception]
        //[Performance]
        [Cat]
        public ResultModel ApproveK2Process(string processCode, string sn, int loginId, string actionString, string memo, string jsonData)
        {
            ResultModel result = new ResultModel();
            int procInstid = 0;
            if (loginId == 0)
            {
                result.Code =  ResultCode.Fail;
                result.Msg = "LoginId错误";
                return result;
            }
            if (string.IsNullOrEmpty(sn))
            {
                result.Code = ResultCode.Fail;
                result.Msg = "SN不能为空";
            }
            else
            {
                try
                {
                    procInstid = Convert.ToInt32(sn.Split('_')[0]);
                }
                catch (Exception ex)
                {
                    result.Code = ResultCode.Fail;
                    result.Msg = string.Format("SN格式错误:{0}", ex.Message);
                }
            }

            var approvalUser = k2UserDomain.GetK2User(loginId);
            if (approvalUser == null)
            {
                result.Code = Common.Enum.ResultCode.Fail;
                result.Msg = string.Format("人员不存在:{0}", loginId.ToString());

                return result;
            }
            
            Dictionary<string, string> dataFields = new Dictionary<string, string>();
            try
            {
                //如果流程打回重新发起,审批人会清空,所以需要重新获取审批人
                if (actionString == "拒绝" || actionString == "结束流程")
                {
                }
                else
                {
                    if (!string.IsNullOrEmpty(processCode))
                    {
                        //取发起人,如果出异常,不处理,保持原有发起人
                        try
                        {
                            var procInst = ProcessInfoDomain.GetProcInstById(procInstid);
                            if (procInst != null)
                            {
                                int originatorLoginId = int.Parse(procInst.Originator.Replace("K2SQL:", ""));
                                k2UserDomain.GenerateApprovalUser(originatorLoginId, dataFields, processCode, "");
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
            //更新业务数据
            if (!string.IsNullOrEmpty(jsonData))
            {
                dataFields.Add("OBJECT", jsonData);
            }

            return MyTaskDomain.ApproveK2Process(processCode, sn, loginId, approvalUser.UserDescription, actionString, memo, dataFields);
        }

        //[Exception]
        //[Performance]
        [Cat]
        public QueryListResultBase<MyTaskDto> GetMyTaskList(QueryCriteriaBase<MyTaskCriteria> queryPara)
        {
            if (queryPara != null && queryPara.QueryCriteria != null && queryPara.PagingInfo != null)
            {
                var processCode = new List<string>();
                var procInstIds = new List<int>();
                var orginatorLoginIds = new List<int>();

                if (queryPara.QueryCriteria.ProcessCode != null && queryPara.QueryCriteria.ProcessCode.Count > 0)
                {
                    processCode = queryPara.QueryCriteria.ProcessCode.Where(_ => _ != string.Empty).ToList();
                }

                if (queryPara.QueryCriteria.ProcInstId != null && queryPara.QueryCriteria.ProcInstId.Count > 0)
                {
                    procInstIds = queryPara.QueryCriteria.ProcInstId.Where(_ => _ != 0).ToList();
                }
                if (queryPara.QueryCriteria.OriginatorLoginId != null && queryPara.QueryCriteria.OriginatorLoginId.Count > 0)
                {
                    orginatorLoginIds = queryPara.QueryCriteria.OriginatorLoginId.Where(_ => _ != 0).ToList();
                }

                var queryWorkList = new QueryCriteriaBase<QueryWorkList>()
                {
                    QueryCriteria = new QueryWorkList()
                    {
                        ProcessCodes = processCode,
                        ProcInstIds = procInstIds,
                        LoginIds = new List<int>() { queryPara.QueryCriteria.LoginId },
                        Folio = queryPara.QueryCriteria.Folio,
                        ProcessStartDate = queryPara.QueryCriteria.ProcessStartDate,
                        TaskStartDate = queryPara.QueryCriteria.TaskStartDate,
                        OriginatorLoginIds = orginatorLoginIds
                    },
                    PagingInfo = queryPara.PagingInfo
                };

                var result = Mapper.Map<QueryListResultBase<MyTaskDto>>(MyTaskDomain.GetMyTaskList(queryWorkList));

                MapWorkListURL(result.ResultList);
                return result;
            }
            else
            {
                return new QueryListResultBase<MyTaskDto>()
                {
                    PagingInfo = new PaginationModel(),
                    ResultList = new List<MyTaskDto>()
                };
            }
        }

        private void MapWorkListURL(List<MyTaskDto> target)
        {
            if (target != null && target.Count > 0)
            {
                List<string> snList = target.Select(_ => _.SN).ToList();

                var workHeaderList = WorklistHeaderRepositories.GetListBySnList(snList);
                if (workHeaderList != null && workHeaderList.Count > 0)
                {
                    foreach (var item in target)
                    {
                        var workHeader = workHeaderList.FirstOrDefault(_ => string.Format("{0}_{1}", _.ProcInstID, _.ActInstDestId) == item.SN);
                        if(workHeader!=null)
                        {
                            item.Url = workHeader.Data;
                        }
                        else
                        {
                            item.Url = string.Empty;
                        }
                        
                    }
                }
            }
        }
        
    }
}
