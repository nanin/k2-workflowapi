using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Common.Enum;
using DianPing.WorkFlow.Infrastructure;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Application.Interface;
using Newtonsoft.Json;
using DianPing.WorkFlow.Infrastructure.Log4Net;
using Com.Dianping.Cat;

namespace DianPing.WorkFlow.API.Http
{
    /// <summary>
    /// Approve 的摘要说明
    /// </summary>
    public class Approve : IHttpHandler
    {
        public Approve()
        {
            RegisteUnity.Current.BuildUp<Approve>(this as Approve);
        }

        [Dependency]
        public IWorkFlowTaskService WorkFlowTaskService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            Cat.GetProducer().NewTransaction("URL-http", "Approve");
            var a = Cat.GetManager().PeekTransaction;

            context.Response.ContentType = "text/plain";
            ResultModel result = new ResultModel();
            try
            {
                string processCode = context.Request.Params["processCode"];
                int loginId = 0;
                int.TryParse(context.Request.Params["loginId"], out loginId);
                string sn = context.Request.Params["sn"];
                string actionString = context.Request.Params["actionString"];
                string memo = context.Request.Params["memo"];
                string jsonData = context.Request.Params["jsonData"];
                string apiKey = context.Request.Params["apiKey"];

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
                LogHelper.Error("Approve", ex.Message, ex, context.Request.Params.ToString());

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