using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;

namespace DianPing.WorkFlow.Repositories.Interface.ServiceProvider
{
    public interface IK2ServiceProvider
    {
        ResultModel ReAssign(string sn, int assignFrom, int assignTo, out string activityName, out string processCode, out int procInstID);
        ResultModel StartProcess(string processName, int loginId, string ObjectId, string Folio, Dictionary<string, string> dataFields,out int procInstID);
        ResultModel ApproveK2Process( string sn, int loginId, string actionString, string memo, Dictionary<string, string> dataFields, out string activityName, out string processCode, out int procInstID);
        ResultModel Involve(string sn, int assignFrom, int assignTo, out string activityName, out string processCode, out int procInstID);
    }
}
