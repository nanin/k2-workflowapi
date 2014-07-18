using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DianPing.WorkFlow.Application;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using Com.Dianping.Cat;

namespace DianPing.WorkFlow.API
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //JsonSerializerSettings settings = HttpConfiguration.Formatters.JsonFormatter.SerializerSettings;
            //IsoDateTimeConverter dateConverter = new IsoDateTimeConverter
            //{
            //    DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'"
            //};
            //settings.Converters.Add(dateConverter);
            RegisteUnity.RegisterForDefault();

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            var fileMap = Path.Combine(path, "CatConfig.xml");
            Cat.Initialize(fileMap);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            //log.Info(string.Format("success!"));
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}