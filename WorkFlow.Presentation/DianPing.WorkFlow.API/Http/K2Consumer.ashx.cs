using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DianPing.WorkFlow.Domain.Interface;
using DianPing.WorkFlow.Domain.Implementation;
using System.Net;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Common.Json;

namespace DianPing.WorkFlow.API.Http
{
    /// <summary>
    /// K2Consumer 的摘要说明
    /// </summary>
    public class K2Consumer : IHttpHandler
    {
        public K2Consumer()
        {
            RegisteUnity.Current.BuildUp<K2Consumer>(this as K2Consumer);
        }

        [Dependency]
        public IWorkFlowTaskService WorkFlowTaskService { get; set; }

        [Dependency]
        public IWorkFlowProcessService WorkFlowProcessService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            string oType = context.Request.Params["oType"];
            string content = context.Request.Params["content"];

            //string oType = "start";
            //string content = "{\"businessType\":\"crm\",\"apiKey\":\"test\",\"folio\":\"folio\",\"jsonData\":\"{}\",\"loginId\":\"-14683\",\"ObjectId\":\"1234\",\"processCode\":\"TGContractV2\"}";

            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            HttpServerUtility server = context.Server;
            //指定输出头和编码
            response.ContentType = "text/html";
            response.Charset = "utf-8";

            JsonHelper jsonHelper = new JsonHelper(content);

            ResultModel result = null;

            try
            {
                if (oType.ToLower() == "start")
                {
                    string apiKey = jsonHelper.Read("apiKey");
                    string folio = jsonHelper.Read("folio");
                    string jsonData = jsonHelper.Read("jsonData");
                    string objectId = jsonHelper.Read("ObjectId"); ;
                    string processCode = jsonHelper.Read("processCode");
                    int loginId = string.IsNullOrEmpty(jsonHelper.Read("loginId")) ? 0 : int.Parse(jsonHelper.Read("loginId"));

                    result = WorkFlowProcessService.StartProcess(processCode, loginId, objectId, folio, jsonData);
                }
                else if (oType.ToLower() == "approval")
                {
                    string apiKey = jsonHelper.Read("apiKey");
                    string actionString = jsonHelper.Read("actionString");
                    string jsonData = jsonHelper.Read("jsonData");
                    string memo = jsonHelper.Read("memo");
                    string processCode = jsonHelper.Read("processCode");
                    string sn = jsonHelper.Read("sn");
                    int loginId = string.IsNullOrEmpty(jsonHelper.Read("loginId")) ? 0 : int.Parse(jsonHelper.Read("loginId"));
                    result = WorkFlowTaskService.ApproveK2Process(processCode, sn, loginId, actionString, memo, jsonData);

                }

                response.StatusCode = (int)HttpStatusCode.OK;

                if (result != null)
                {
                    if (result.Code == Common.Enum.ResultCode.Sucess)
                    {
                        response.Write("{\"result\":\"success\"} ");
                    }
                    else
                    {
                        response.Write("{\"result\":\"fail\", \"message\":" + result.Msg + "}");
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Write("{\"result\":\"fail\", \"message\":" + ex.Message + "}");
            }  
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}