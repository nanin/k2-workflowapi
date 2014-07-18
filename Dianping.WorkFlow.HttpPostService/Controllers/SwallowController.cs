using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Configuration;
using System.Text;
using System.IO;
using log4net;
using Com.Dianping.Cat;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Dianping.WorkFlow.HttpPostService.Controllers
{
    public class SwallowController : Controller
    {
        static readonly ILog log = log4net.LogManager.GetLogger(typeof(SwallowController));

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Producer()
        {
            if (PostToSwallow())
                Response.StatusCode = (int)HttpStatusCode.OK;
            return View();
        }

        public ActionResult Consumer()
        {
            try
            {
                string oType = Request["oType"];
                string content = Request["content"];

                IK2Command command = k2CommandFactory.Instance.GetCommand(oType, content);

                if (command != null)
                {
                    command.Excute();
                    Response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                log.Error("处理Swallow推送过来的消息出错", ex);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return View();
        }

        /// <summary>
        /// 向Swallow的Producer Server发送消息
        /// </summary>
        /// <returns>
        /// true:发送成功
        /// </returns>
        private bool PostToSwallow()
        {
            bool isSuccessed = true;

            try
            {
                string postUrl = ConfigurationManager.AppSettings["postUrl"];

                //string oType = Request["oType"];
                //string content = Request["content"];
                //string topic = Request["topic"];

                string oType = "start";
                string content = "{\"businessType\":\"crm\",\"apiKey\":\"test\",\"folio\":\"folio\",\"jsonData\":\"{}\",\"loginId\":\"-1234\",\"ObjectId\":\"1234\",\"processCode\":\"12345\"}";
                string topic = "topic_1"; ;

                StringBuilder sParams = ConstructHttpPostParams(oType,content, topic);
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(postUrl);
                ConstructHttpRequest(request,sParams);            
                isSuccessed = ((HttpWebResponse)request.GetResponse()).StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                isSuccessed = false;
                log.Error("发送消息到swallow出错", ex);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return isSuccessed;
        }

        /// <summary>
        /// 构建向Swallow的Producer Server Post消息的Http Request
        /// </summary>
        /// <param name='request'>向Swallow的Producer Server Post消息的Http Request</param>
        /// <param name='sParams'>Post的请求参数</param>
        private void ConstructHttpRequest(HttpWebRequest request,StringBuilder sParams)
        {
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            byte[] data = Encoding.UTF8.GetBytes(sParams.ToString());  
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
        }

        /// <summary>
        /// 构造Http Post参数
        /// </summary>
        /// <param name="oType">操作类型</param>
        /// <param name="content">调用k2服务的接口参数</param>
        /// <param name="topic">topic的名称</param>
        private StringBuilder ConstructHttpPostParams(string oType,string content, string topic)
        {
            StringBuilder sParm = new StringBuilder();

            if (!string.IsNullOrEmpty(topic))
            {
                sParm.Append("topic=" + topic);
            }

            if (!string.IsNullOrEmpty(oType))
            {
                if (sParm.Length > 0)
                {
                    sParm.Append("&oType=" + oType);
                }
                else
                {
                    sParm.Append("oType=" + oType);
                }
            }

            if (!string.IsNullOrEmpty(content))
            {
                if (sParm.Length > 0)
                {
                    sParm.Append("&content=" + content);
                }
                else
                {
                    sParm.Append("content=" + content);
                }
            }

            return sParm;
        }
    }
}
