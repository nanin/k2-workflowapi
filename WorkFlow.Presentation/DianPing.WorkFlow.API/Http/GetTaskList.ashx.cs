using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Infrastructure;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Common.Enum;
using Newtonsoft.Json;
using DianPing.WorkFlow.Application.Interface.Dto;
using Newtonsoft.Json.Converters;
using DianPing.WorkFlow.Infrastructure.Log4Net;
using Com.Dianping.Cat;
using System.Net;

namespace DianPing.WorkFlow.API.Http
{
    /// <summary>
    /// GetTaskList 的摘要说明
    /// </summary>
    public class GetTaskList : IHttpHandler
    {

        public GetTaskList()
        {
            RegisteUnity.Current.BuildUp<GetTaskList>(this as GetTaskList);
        }

        [Dependency]
        public IWorkFlowTaskService WorkFlowTaskService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            Cat.GetProducer().NewTransaction("URL-http", "GetTaskList");
            var a = Cat.GetManager().PeekTransaction;

            context.Response.ContentType = "text/plain";
            QueryListResultBase<MyTaskDto> result = new QueryListResultBase<MyTaskDto>();
            try
            {
                int loginId = 0;
                int.TryParse(context.Request.Params["loginId"], out loginId);
                string originatorLoginIds = context.Request.Params["originatorLoginIds"];
                List<int> originatorLoginId = new List<int>();
                if (!string.IsNullOrEmpty(originatorLoginIds))
                    originatorLoginId = originatorLoginIds.Split(new char[] { ',', ';' }).Select(_ => int.Parse(_)).ToList();
                string procInstIds = context.Request.Params["procInstIds"];
                List<int> procInstId = new List<int>();
                if (!string.IsNullOrEmpty(procInstIds))
                    procInstId = procInstIds.Split(new char[] { ',', ';' }).Select(_ => int.Parse(_)).ToList();
                string processCodes = context.Request.Params["processCodes"];
                List<string> processCode = new List<string>();
                if (!string.IsNullOrEmpty(processCodes))
                    processCode = processCodes.Split(new char[] { ',', ';' }).ToList();
                string folio = context.Request.Params["folio"];
                string taskFrom = context.Request.Params["taskFrom"];
                string taskTo = context.Request.Params["taskTo"];
                string processFrom = context.Request.Params["processFrom"];
                string processTo = context.Request.Params["processTo"];
                int pageIndex = 0;
                int.TryParse(context.Request.Params["pageIndex"], out pageIndex);
                int pageSize = 0;
                int.TryParse(context.Request.Params["pageSize"], out pageSize);
                string sortField = context.Request.Params["sortField"];
                string sortOrder = context.Request.Params["sortOrder"];

                string apiKey = context.Request.Params["apiKey"];

                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    PaginationModel PagingInfo = new PaginationModel()
                    {
                        PageIndex = pageIndex,
                        PageSize = pageSize
                    };
                    if (!string.IsNullOrEmpty(sortField))
                    {
                        PagingInfo.SortField = sortField;
                    }
                    if (!string.IsNullOrEmpty(sortOrder))
                    {
                        switch (sortOrder.ToUpper())
                        {
                            case "ASC":
                                PagingInfo.SortOrder = SortOrder.Ascending;
                                break;
                            case "DESC":
                                PagingInfo.SortOrder = SortOrder.Descending;
                                break;
                            default:
                                PagingInfo.SortOrder = SortOrder.Unspecified;
                                break;
                        }

                    }

                    DatePeriodModel processDate = new DatePeriodModel();
                    if (!string.IsNullOrEmpty(processFrom))
                        processDate.DateFrom = Convert.ToDateTime(processFrom);
                    if (!string.IsNullOrEmpty(processTo))
                        processDate.DateTo = Convert.ToDateTime(processTo);

                    DatePeriodModel taskDate = new DatePeriodModel();
                    if (!string.IsNullOrEmpty(taskFrom))
                        taskDate.DateFrom = Convert.ToDateTime(taskFrom);
                    if (!string.IsNullOrEmpty(taskTo))
                        taskDate.DateTo = Convert.ToDateTime(taskTo);

                    QueryCriteriaBase<MyTaskCriteria> query = new QueryCriteriaBase<MyTaskCriteria>()
                    {
                        PagingInfo = PagingInfo,
                        QueryCriteria = new MyTaskCriteria()
                        {
                            Folio = folio,
                            LoginId = loginId,
                            OriginatorLoginId = originatorLoginId,
                            ProcInstId = procInstId,
                            ProcessCode = processCode,
                            ProcessStartDate = processDate,
                            TaskStartDate = taskDate
                        }
                    };

                    result = WorkFlowTaskService.GetMyTaskList(query);
                }
                a.Status = "0";

            }
            catch (Exception ex)
            {
                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                LogHelper.Error("GetTaskList", ex.Message, ex, context.Request.Params.ToString());
                result = new QueryListResultBase<MyTaskDto>();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                a.Complete();
            }
            context.Response.Write(JsonConvert.SerializeObject(result, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff" }));
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}