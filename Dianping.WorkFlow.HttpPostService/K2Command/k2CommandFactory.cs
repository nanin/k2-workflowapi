using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dianping.WorkFlow.HttpPostService
{
    /// <summary>
    /// 根据页面Post的Json数据构建具体的K2Command
    /// </summary>
    public class k2CommandFactory
    {
        private static k2CommandFactory instance;
        private static object lockObj = new object();

        /// <summary>
        /// 获得Command工厂的单例
        /// </summary>
        public static k2CommandFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new k2CommandFactory();
                        }
                    }
                }

                return instance;
            }
        }

        private k2CommandFactory()
        {
        }

        /// <summary>
        /// 根据消息获得具体的K2操作
        /// </summary>
        /// <param name="oType">操作类型</param>
        /// <param name="content">调用k2服务的参数</param>
        /// <returns></returns>
        public IK2Command GetCommand(string oType,string content)
        {
            IK2Command command = null;
            var msgObj = JsonConvert.DeserializeObject(content);
            var jObject = msgObj as Newtonsoft.Json.Linq.JObject;

            if (jObject != null)
            {
                switch (oType)
                {
                    case "start":
                        command = new StartProcessCommand();
                        if (jObject != null)
                        {
                            var apiKey = getJsonStringProperty(jObject, "apiKey");
                            var folio = getJsonStringProperty(jObject, "folio");
                            var jsonData = getJsonStringProperty(jObject, "jsonData");
                            var ObjectId = getJsonStringProperty(jObject, "ObjectId");
                            var processCode = getJsonStringProperty(jObject, "processCode");
                            var businessType = getJsonStringProperty(jObject, "businessType");
                            var loginId = string.IsNullOrEmpty(getJsonStringProperty(jObject, "loginId")) ? 0 : int.Parse(getJsonStringProperty(jObject, "loginId"));

                            ((StartProcessCommand)command).ApiKey = apiKey;
                            ((StartProcessCommand)command).Folio = folio;
                            ((StartProcessCommand)command).JsonData = jsonData;
                            ((StartProcessCommand)command).ObjectId = ObjectId;
                            ((StartProcessCommand)command).ProcessCode = processCode;
                            ((StartProcessCommand)command).LoginId = loginId;
                            ((StartProcessCommand)command).BusinessType = businessType;
                        }
                        break;
                    case "approval":
                        command = new ApprovalProcessCommand();
                        if (jObject != null)
                        {
                            var apiKey = getJsonStringProperty(jObject, "apiKey");
                            var actionString = getJsonStringProperty(jObject, "actionString");
                            var jsonData = getJsonStringProperty(jObject, "jsonData");
                            var memo = getJsonStringProperty(jObject, "memo");
                            var processCode = getJsonStringProperty(jObject, "processCode");
                            var sn = getJsonStringProperty(jObject, "sn");
                            var businessType = getJsonStringProperty(jObject, "businessType");
                            var loginId = string.IsNullOrEmpty(getJsonStringProperty(jObject, "loginId")) ? 0 : int.Parse(getJsonStringProperty(jObject, "loginId"));

                            ((ApprovalProcessCommand)command).ApiKey = apiKey;
                            ((ApprovalProcessCommand)command).ActionString = actionString;
                            ((ApprovalProcessCommand)command).JsonData = jsonData;
                            ((ApprovalProcessCommand)command).Memo = memo;
                            ((ApprovalProcessCommand)command).ProcessCode = processCode;
                            ((ApprovalProcessCommand)command).LoginId = loginId;
                            ((ApprovalProcessCommand)command).SN = sn;
                            ((ApprovalProcessCommand)command).BusinessType = businessType;
                        }
                        break;

                    default:
                        break;
                }
            }

            return command;
        }

        /// <summary>
        /// 获取Json字符串的特定节点
        /// </summary>
        /// <param name="jObject"></param>
        /// <param name="propertyName">节点名</param>
        /// <returns>节点的字符值</returns>
        private static string getJsonStringProperty(JObject jObject, string propertyName)
        {
            return jObject[propertyName] != null ? (string)jObject[propertyName] : string.Empty;
        }
    }
}