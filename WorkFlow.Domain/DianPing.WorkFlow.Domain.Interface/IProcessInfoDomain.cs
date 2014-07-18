using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;

namespace DianPing.WorkFlow.Domain.Interface
{
    public interface IProcessInfoDomain
    {
        IList<ProcessInfo> GetByProcessCode(IList<string> processCode);

        /// <summary>
        /// 获取流程实例基本信息
        /// </summary>
        /// <param name="procInstId"></param>
        /// <returns></returns>
        ProcInstBasicInfo GetProcInstBasicInfo(int procInstId);

        ResultModel StartProcess(string processCode,string processName, int loginId,string realName, string ObjectId, string Folio, Dictionary<string, string> dataFields);

        List<K2Status> GetProcessStatusByProcInstId(int procInstId);
        List<K2Status> GetProcessStatusByFolio(string folio);

        List<K2CommentPO> GetCommentByProcInstIds(List<int> procInstIds);

        ProcInst GetProcInstById(int procInstId);
    }
}
