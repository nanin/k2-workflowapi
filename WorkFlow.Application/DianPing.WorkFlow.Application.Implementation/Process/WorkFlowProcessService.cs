using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Application.Interface.Dto;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Domain.Interface;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Infrastructure.Interception;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;
using AutoMapper;

namespace DianPing.WorkFlow.Application.Implementation.Process
{
    public class WorkFlowProcessService : IWorkFlowProcessService
    {
        static WorkFlowProcessService()
        {
            Mapper.CreateMap<K2Status, K2StatusDto>();
            Mapper.CreateMap<K2CommentPO, K2CommentDto>();
        }

        [Dependency]
        public virtual IProcessInfoDomain processInfoDomain { get; set; }

        [Dependency]
        public virtual IK2UserDomain k2UserDomain { get; set; }


        //[Exception]
        //[Performance]
        [Cat]
        public ResultModel StartProcess(string processCode, int loginId, string ObjectId, string Folio, string jsonData)
        {
            ResultModel result = new ResultModel();
            if (loginId == 0)
            {
                result.Code = Common.Enum.ResultCode.Fail;
                result.Msg = "LoginId错误";

                return result;
            }
            var originator = k2UserDomain.GetK2User(loginId);
            if (originator == null)
            {
                result.Code = Common.Enum.ResultCode.Fail;
                result.Msg = "发起人不存在" + loginId.ToString();

                return result;
            }
            var process = processInfoDomain.GetByProcessCode(new List<string>() { processCode });
            if (process == null || process.Count != 1)
            {

                result.Code = Common.Enum.ResultCode.Fail;
                result.Msg = string.Format("流程不存在:{0}", processCode);

                return result;
            }

            Dictionary<string, string> dataFields = new Dictionary<string, string>();
            try
            {
                k2UserDomain.GenerateApprovalUser(loginId, dataFields, processCode, "发起人");

                //更新业务数据
                if (!string.IsNullOrEmpty(jsonData))
                {
                    dataFields.Add("OBJECT", jsonData);
                }

                if (!dataFields.ContainsKey("dest_Creator"))
                {
                    dataFields.Add("dest_Creator", loginId.ToString());
                }

            }
            catch (Exception ex)
            {
                result.Code = Common.Enum.ResultCode.Fail;
                result.Msg = ex.Message;

                return result;
            }
            result = processInfoDomain.StartProcess(processCode, process[0].ProcessFullName, loginId, originator.UserDescription, ObjectId, Folio, dataFields);
            return result;
        }

        //[Exception]
        //[Performance]
        [Cat]
        public List<K2StatusDto> GetProcessStatus(int procInstId, string folio)
        {
            List<K2Status> list = new List<K2Status>();
            if (procInstId > 0)
            {
                list = processInfoDomain.GetProcessStatusByProcInstId(procInstId);
            }
            if (list.Count == 0 && !string.IsNullOrEmpty(folio))
            {
                list = processInfoDomain.GetProcessStatusByFolio(folio);
            }

            var result = Mapper.Map<List<K2StatusDto>>(list);
            return result;
        }

        //[Exception]
        //[Performance]
        [Cat]
        public List<K2CommentDto> GetComment(List<int> procInstIds)
        {
            var result = processInfoDomain.GetCommentByProcInstIds(procInstIds);
            return Mapper.Map<List<K2CommentDto>>(result);
        }
    }
}
