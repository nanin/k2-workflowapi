using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using DianPing.WorkFlow.Application.Interface.Dto;
using DianPing.WorkFlow.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DianPing.WorkFlow.Infrastructure.Log4Net;
using Com.Dianping.Cat;
using System.Net;

namespace DianPing.WorkFlow.API.Http
{
    /// <summary>
    /// GetProcessComments 的摘要说明
    /// </summary>
    public class GetProcessComments : IHttpHandler
    {

        public GetProcessComments()
        {
            RegisteUnity.Current.BuildUp<GetProcessComments>(this as GetProcessComments);
        }

        [Dependency]
        public IWorkFlowProcessService WorkFlowProcessService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            Cat.GetProducer().NewTransaction("URL-http", "GetProcessComments");
            var a = Cat.GetManager().PeekTransaction;

            context.Response.ContentType = "text/plain";
            List<K2CommentDto> result = new List<K2CommentDto>();
            try
            {
                string procInstIds = context.Request.Params["procInstIds"];
                List<int> procInstId = new List<int>();
                if (!string.IsNullOrEmpty(procInstIds))
                    procInstId = procInstIds.Split(new char[] { ',', ';' }).Select(_ => int.Parse(_)).ToList();
                string apiKey = context.Request.Params["apiKey"];

                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowProcessService.GetComment(procInstId);
                }
                a.Status = "0";

            }
            catch (Exception ex)
            {
                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                LogHelper.Error("GetProcessComments", ex.Message, ex, context.Request.Params.ToString());

                result = new List<K2CommentDto>();
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