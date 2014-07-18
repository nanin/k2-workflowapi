using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianPing.WorkFlow.Infrastructure;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using log4net;
using DianPing.WorkFlow.Infrastructure.Log4Net;
using Com.Dianping.Cat;
using System.Net;

namespace DianPing.WorkFlow.API.Http
{
    /// <summary>
    /// GetProcessStatus 的摘要说明
    /// </summary>
    public class GetProcessStatus : IHttpHandler
    {


        public GetProcessStatus()
        {
            RegisteUnity.Current.BuildUp<GetProcessStatus>(this as GetProcessStatus);
        }

        [Dependency]
        public IWorkFlowProcessService WorkFlowProcessService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            Cat.GetProducer().NewTransaction("URL-http", "GetProcessStatus");
            var a = Cat.GetManager().PeekTransaction;

            context.Response.ContentType = "text/plain";
            List<K2StatusDto> result = new List<K2StatusDto>();
            try
            {
                int procInstId = 0;
                int.TryParse(context.Request.Params["procInstId"], out procInstId);
                string folio = context.Request.Params["folio"];
                string apiKey = context.Request.Params["apiKey"];

                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowProcessService.GetProcessStatus(procInstId, folio);
                }
                a.Status = "0";
            }
            catch (Exception ex)
            {
                Cat.GetProducer().LogError(ex);
                a.SetStatus(ex);
                LogHelper.Error("GetProcessStatus", ex.Message, ex, context.Request.Params.ToString());

                result = new List<K2StatusDto>();
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