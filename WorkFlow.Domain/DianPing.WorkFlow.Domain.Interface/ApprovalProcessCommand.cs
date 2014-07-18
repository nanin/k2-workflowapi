using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DianPing.WorkFlow.Domain.Implementation
{
    public class ApprovalProcessCommand : IK2Command
    {
        public string Memo { get; set; }
        public string ActionString { get; set; }
        public string ApiKey { get; set; }
        public string JsonData { get; set; }
        public int LoginId { get; set; }
        public string ProcessCode { get; set; }
        public string SN { get; set; }
        public string BusinessType { get; set; }


        /// <summary>
        /// 调用K2接口审批流程
        /// </summary>
        public void Excute()
        {
            K2Service.ApproveK2ProcessRequest request = new K2Service.ApproveK2ProcessRequest();
            request.Body = new K2Service.ApproveK2ProcessRequestBody();
            request.Body.actionString = ActionString;
            request.Body.apiKey = ApiKey;
            request.Body.jsonData = JsonData;
            request.Body.loginId = LoginId;
            request.Body.memo = Memo;
            request.Body.processCode = ProcessCode;
            request.Body.sn = SN;

            //TODO:refine k2 service, add businessType
            //request.Body.businessType = BusinessType;

            K2Service.K2ServiceSoap client = new K2Service.K2ServiceSoapClient();
            client.ApproveK2Process(request);
        }
    }
}