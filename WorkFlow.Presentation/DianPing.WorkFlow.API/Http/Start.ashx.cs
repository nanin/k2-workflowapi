using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Infrastructure;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using DianPing.WorkFlow.API.WebService;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Common.Enum;
using Newtonsoft.Json;
using DianPing.WorkFlow.Infrastructure.Log4Net;
using Com.Dianping.Cat;

namespace DianPing.WorkFlow.API.Http
{
    /// <summary>
    /// Start 的摘要说明
    /// </summary>
    public class Start : IHttpHandler
    {
        public Start()
        {
            RegisteUnity.Current.BuildUp<Start>(this as Start);
        }

        [Dependency]
        public IWorkFlowProcessService WorkFlowProcessService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            Cat.GetProducer().NewTransaction("URL-http", "Start");
            var a = Cat.GetManager().PeekTransaction;

            context.Response.ContentType = "text/plain";
            ResultModel result = new ResultModel();
            try
            {
                string processCode = context.Request.Params["processCode"];
                int loginId = 0;
                int.TryParse(context.Request.Params["loginId"], out loginId);
                string objectId = context.Request.Params["objectId"];
                string folio = context.Request.Params["folio"];
                string jsonData = context.Request.Params["jsonData"];
                string apiKey = context.Request.Params["apiKey"];

                if (APIKeyUtility.IsRightAPIKey(apiKey))
                {
                    result = WorkFlowProcessService.StartProcess(processCode, loginId, objectId, folio, jsonData);
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
                LogHelper.Error("Start", ex.Message, ex, context.Request.Params.ToString());

                result = new ResultModel() { Code = ResultCode.Fail, Msg = ex.Message };
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
    }
}