using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using DianPing.WorkFlow.Application.Interface.Dto;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Infrastructure;
using DianPing.WorkFlow.Common.Enum;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;
using DianPing.WorkFlow.Infrastructure.Interception;
using Com.Dianping.Cat;


//注意下面的语句一定要加上，指定log4net使用.config文件来读取配置信息
//如果是WinForm（假定程序为MyDemo.exe，则需要一个MyDemo.exe.config文件）
//如果是WebForm，则从web.config中读取相关信息
namespace DianPing.WorkFlow.API.WebService
{
    /// <summary>
    /// WorkFlowService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class WorkFlowService : System.Web.Services.WebService
    {
        public WorkFlowService()
        {
            RegisteUnity.Current.BuildUp<WorkFlowService>(this as WorkFlowService);
        }

        [Dependency]
        public IWorkFlowTaskService WorkFlowTaskService { get; set; }

        [Dependency]
        public IWorkFlowProcessService WorkFlowProcessService { get; set; }

        [Cat]
        [WebMethod]
        public ResultModel Start(string processCode, int loginId, string objectId, string folio, string jsonData, string apiKey)
        {
            Cat.GetProducer().NewTransaction("URL-WebService", "Start");
            var a = Cat.GetManager().PeekTransaction;
            ResultModel result = null;

            try
            {
                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowProcessService.StartProcess(processCode, loginId, objectId, folio, jsonData); ;
                }
                else
                {
                    result = new ResultModel() { Code = ResultCode.Fail, Msg = "ApiKey错误" };
                }
                a.Status = "0";
            }
            catch (Exception ex)
            {

                //Cat.GetProducer().LogEvent("Start", "Arguments", "0", string.Format);

                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                throw ex;
            }
            finally {
                a.Complete();
            }
            return result;
        }

        [Cat]
        [WebMethod]
        public ResultModel Approve(string processCode, string sn, int loginId, string actionString, string memo, string jsonData, string apiKey)
        {
            Cat.GetProducer().NewTransaction("URL-WebService", "Approve");
            var a = Cat.GetManager().PeekTransaction;
            ResultModel result = null;

            try
            {
                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowTaskService.ApproveK2Process(processCode, sn, loginId, actionString, memo, jsonData);
                }
                else
                {
                    result = new ResultModel() { Code = ResultCode.Fail, Msg = "ApiKey错误" };
                }
                a.Status = "0";
            }
            catch (Exception ex)
            {
                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                throw ex;
            }
            finally
            {
                a.Complete();
            }
            return result;
        }

        [Cat]
        [WebMethod]
        public ResultModel InvolveTask(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName, string apiKey)
        {
            Cat.GetProducer().NewTransaction("URL-WebService", "InvolveTask");
            var a = Cat.GetManager().PeekTransaction;
            ResultModel result = null;

            try
            {
                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowTaskService.Involve(sn, assignFromLoginId, assignFromRealName, assignToLoginId, assignToRealName);
                }
                else
                {
                    result = new ResultModel() { Code = ResultCode.Fail, Msg = "ApiKey错误" };
                }
                a.Status = "0";
            }
            catch (Exception ex)
            {
                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                throw ex;
            }
            finally
            {
                a.Complete();
            }
            return result;
        }

        [Cat]
        [WebMethod]
        public ResultModel ReAssignTask(string sn, int assignFromLoginId, string assignFromRealName, int assignToLoginId, string assignToRealName, bool isAddLog, string apiKey)
        {
            Cat.GetProducer().NewTransaction("URL-WebService", "ReAssignTask");
            var a = Cat.GetManager().PeekTransaction;
            ResultModel result = null;

            try
            {
                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowTaskService.ReAssign(sn, assignFromLoginId, assignFromRealName, assignToLoginId, assignToRealName, isAddLog);
                }
                else
                {
                    result = new ResultModel() { Code = ResultCode.Fail, Msg = "ApiKey错误" };
                }
                a.Status = "0";
            }
            catch (Exception ex)
            {
                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                throw ex;
            }
            finally
            {
                a.Complete();
            }
            return result;
        }

        /// <summary>
        /// folio:按照单号排序
        /// worklisttime:按照任务到达时间排序
        /// procstarttime:按照流程发起时间排序
        /// </summary>
        /// <param name="queryPara"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [Cat]
        [WebMethod]
        public QueryListResultBase<MyTaskDto> GetTaskList(QueryCriteriaBase<MyTaskCriteria> queryPara, string apiKey)
        {
            Cat.GetProducer().NewTransaction("URL-WebService", "GetTaskList");
            var a = Cat.GetManager().PeekTransaction;
            QueryListResultBase<MyTaskDto> result = null;

            try
            {
                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowTaskService.GetMyTaskList(queryPara);
                }
                else
                {
                    result = new QueryListResultBase<MyTaskDto>();
                }
                a.Status = "0";
            }
            catch (Exception ex)
            {
                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                throw ex;
            }
            finally
            {
                a.Complete();
            }
            return result;
        }

        [Cat]
        [WebMethod]
        public List<K2StatusDto> GetProcessStatus(int procInstId, string folio, string apiKey)
        {
            Cat.GetProducer().NewTransaction("URL-WebService", "GetProcessStatus");
            var a = Cat.GetManager().PeekTransaction;
            List<K2StatusDto> result = null;

            try
            {
                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowProcessService.GetProcessStatus(procInstId, folio);
                }
                else
                {
                    result = new List<K2StatusDto>();
                }
                a.Status = "0";
            }
            catch (Exception ex)
            {
                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                throw ex;
            }
            finally
            {
                a.Complete();
            }
            return result;
        }

        [Cat]
        [WebMethod]
        public List<K2CommentDto> GetProcessComments(List<int> procInstIds, string apiKey)
        {
            Cat.GetProducer().NewTransaction("URL-WebService", "GetProcessComments");
            var a = Cat.GetManager().PeekTransaction;
            List<K2CommentDto> result = null;

            try
            {
                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowProcessService.GetComment(procInstIds);
                }
                else
                {
                    result = new List<K2CommentDto>();
                }
                a.Status = "0";
            }
            catch (Exception ex)
            {
                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                throw ex;
            }
            finally
            {
                a.Complete();
            }
            return result;
        }
    }
}
