using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Application.Interface.Dto;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;

namespace DianPing.WorkFlow.Application.Interface
{
    /// <summary>
    /// 流程相关服务--如开启流程/流程审批
    /// </summary>
    public interface IWorkFlowProcessService
    {
        ///// <summary>
        ///// 开启流程
        ///// </summary>
        ///// <param name="param">
        ///// 开启流程所需参数
        ///// <see cref="DianPing.WorkFlow.Application.Interface.Dto.StartProcessCommandParam"/>
        ///// </param>
        //void StartProcess(StartProcessCommandParam param);


        ///// <summary>
        ///// 审批流程
        ///// </summary>
        ///// <param name="param">
        ///// 审批流程所需参数
        ///// </param>
        //void ApprovalProcess(ApprovalProcessCommandParam param);
        ResultModel StartProcess(string processCode, int loginId, string ObjectId, string Folio, string jsonData);

        List<K2StatusDto> GetProcessStatus(int procInstId, string folio);

        List<K2CommentDto> GetComment(List<int> procInstIds);
    }
}
