using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianPing.WorkFlow.Common.Models;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Infrastructure;
using DianPing.WorkFlow.Common.Enum;
using Newtonsoft.Json;
using DianPing.WorkFlow.Infrastructure.Log4Net;
using Com.Dianping.Cat;
using System.Net;

namespace DianPing.WorkFlow.API.Http
{
    /// <summary>
    /// ReAssign 的摘要说明
    /// </summary>
    public class ReAssign : IHttpHandler
    {
        public ReAssign()
        {
            RegisteUnity.Current.BuildUp<ReAssign>(this as ReAssign);
        }

        [Dependency]
        public IWorkFlowTaskService WorkFlowTaskService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            Cat.GetProducer().NewTransaction("URL-http", "ReAssign");
            var a = Cat.GetManager().PeekTransaction;

            context.Response.ContentType = "text/plain";
            ResultModel result = new ResultModel();
            try
            {
                string sn = context.Request.Params["sn"];
                int assignFromLoginId = 0;
                int.TryParse(context.Request.Params["assignFromLoginId"],out assignFromLoginId);
                string assignFromRealName = context.Request.Params["assignFromRealName"];
                int assignToLoginId = 0;
                int.TryParse(context.Request.Params["assignToLoginId"], out assignToLoginId);
                string assignToRealName = context.Request.Params["assignToRealName"];
                bool isAddLog = false;
                try
                {
                    isAddLog = Convert.ToBoolean(context.Request.Params["isAddLog"]);
                }
                catch { }
                
                string apiKey = context.Request.Params["apiKey"];

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
                LogHelper.Error("ReAssign", ex.Message, ex, context.Request.Params.ToString());
                result = new ResultModel() { Code = ResultCode.Fail, Msg = ex.Message };
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                a.Complete();
            }
            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public bool isAddLog { get; set; }
    }
}