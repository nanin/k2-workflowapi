using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Domain.Interface.Ddo;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;

namespace DianPing.WorkFlow.Domain.Interface
{
    public interface ITaskDomain
    {
        QueryListResultBase<MyTaskDdo> GetMyTaskList(QueryCriteriaBase<QueryWorkList> queryPara);
        ResultModel ReAssign(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName, bool isAddLog);
        ResultModel ApproveK2Process(string processCode, string sn, int loginId, string realName, string actionString, string memo, Dictionary<string, string> dataFields);
        ResultModel Involve(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName);
    }
}
