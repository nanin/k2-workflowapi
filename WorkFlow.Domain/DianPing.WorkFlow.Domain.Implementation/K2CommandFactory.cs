using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DianPing.WorkFlow.Domain.Interface;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;

namespace DianPing.WorkFlow.Domain.Implementation
{
    /// <summary>
    /// 根据页面Post的Json数据构建具体的K2Command
    /// </summary>
    public class K2CommandFactory
    {
        private static K2CommandFactory instance;
        private static object lockObj = new object();

        /// <summary>
        /// 获得Command工厂的单例
        /// </summary>
        public static K2CommandFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new K2CommandFactory();
                        }
                    }
                }

                return instance;
            }
        }

        private K2CommandFactory()
        {
        }

        /// <summary>
        /// 根据消息获得具体的K2操作
        /// </summary>
        /// <param name="oType">操作类型</param>
        /// <param name="content">调用k2服务的参数</param>
        /// <returns></returns>
        public IK2Command GetCommand(string oType, string content, IK2ServiceProvider k2ServiceProvider)
        {
            IK2Command command = null;
            var msgObj = JsonConvert.DeserializeObject(content);
            var jObject = msgObj as Newtonsoft.Json.Linq.JObject; 

            if (jObject != null)
            {
                switch (oType.ToLower())
                {
                    case "start":
                        //command = new StartProcessCommand(k2ServiceProvider);                        
                        //if (jObject != null)
                        //{
                        //    ((StartProcessCommand)command).ApiKey = getJsonStringProperty(jObject, "apiKey");
                        //    ((StartProcessCommand)command).Folio = getJsonStringProperty(jObject, "folio");
                        //    ((StartProcessCommand)command).JsonData = getJsonStringProperty(jObject, "jsonData");
                        //    ((StartProcessCommand)command).ObjectId = getJsonStringProperty(jObject, "ObjectId"); ;
                        //    ((StartProcessCommand)command).ProcessCode = getJsonStringProperty(jObject, "processCode");
                        //    ((StartProcessCommand)command).LoginId = string.IsNullOrEmpty(getJsonStringProperty(jObject, "loginId")) ? 0 : int.Parse(getJsonStringProperty(jObject, "loginId"));
                        //}
                        break;
                    case "approval":
                        command = new ApprovalProcessCommand(k2ServiceProvider);
                        if (jObject != null)
                        {
                            ((ApprovalProcessCommand)command).ApiKey = getJsonStringProperty(jObject, "apiKey");
                            ((ApprovalProcessCommand)command).ActionString = getJsonStringProperty(jObject, "actionString");
                            ((ApprovalProcessCommand)command).JsonData = getJsonStringProperty(jObject, "jsonData");
                            ((ApprovalProcessCommand)command).Memo = getJsonStringProperty(jObject, "memo");
                            ((ApprovalProcessCommand)command).ProcessCode = getJsonStringProperty(jObject, "processCode");
                            ((ApprovalProcessCommand)command).LoginId = string.IsNullOrEmpty(getJsonStringProperty(jObject, "loginId")) ? 0 : int.Parse(getJsonStringProperty(jObject, "loginId"));
                            ((ApprovalProcessCommand)command).SN = getJsonStringProperty(jObject, "sn");
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