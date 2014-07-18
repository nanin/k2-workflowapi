using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Application.Interface.Dto;

namespace DianPing.WorkFlow.Application.Interface
{
    public interface IWorkFlowTaskService
    {
        QueryListResultBase<MyTaskDto> GetMyTaskList(QueryCriteriaBase<MyTaskCriteria> queryPara);
        ResultModel ReAssign(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName, bool isAddLog);
        ResultModel ApproveK2Process(string processCode, string sn, int loginId, string actionString, string memo, string jsonData);
        ResultModel Involve(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName);
    }
}
