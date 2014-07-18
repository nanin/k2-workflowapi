using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dianping.WorkFlow.HttpPostService
{
    /// <summary>
    /// 开启K2流程
    /// </summary>
    public class StartProcessCommand :IK2Command
    {
        public string ApiKey { get; set; }
        public string Folio { get; set; }
        public string JsonData { get; set; }
        public int LoginId { get; set; }
        public string ObjectId { get; set; }
        public string ProcessCode { get; set; }
        public string BusinessType { get; set; }

        /// <summary>
        /// 调用K2服务开启流程
        /// </summary>
        public void Excute()
        {
            K2Service.StartProcessRequest request = new K2Service.StartProcessRequest();
            request.Body = new K2Service.StartProcessRequestBody();
            request.Body.apiKey = ApiKey;
            request.Body.Folio = Folio;
            request.Body.jsonData = JsonData;
            request.Body.loginId = LoginId;
            request.Body.ObjectId = ObjectId;
            request.Body.processCode = ProcessCode;

            //TODO:refine k2 service, add businessType
            //request.Body.businessType = BusinessType;

            K2Service.K2ServiceSoap client = new K2Service.K2ServiceSoapClient();
            client.StartProcess(request);            
        }
    }
}