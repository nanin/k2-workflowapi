using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using log4net;
using DianPing.WorkFlow.Infrastructure.Entity;
using Newtonsoft.Json;
using System.IO;
using Com.Dianping.Cat;

namespace DianPing.WorkFlow.Infrastructure.Interception
{
    public class PerformanceAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new PerformanceCallHandler() { Order = 10 };
        }
        
    }

    public class PerformanceCallHandler : ICallHandler
    {
        public int Order { get; set; }
        //private static readonly ILog log = LogManager.GetLogger(typeof(PerformanceCallHandler));
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {

            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            //var fileMap = Path.Combine(path, "CatConfig.xml");
            //Cat.Initialize(fileMap);
            var a = Cat.GetProducer().NewTransaction("WorkFlowService", input.MethodBase.Name);

            
            string beginDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            var watcher = new Stopwatch();
            watcher.Start();

            Cat.GetProducer().LogEvent("WorkFlowService", "Arguments", "0", JsonConvert.SerializeObject(input.Arguments));
            var result = getNext()(input, getNext);
            Cat.GetProducer().LogEvent("WorkFlowService", "ReturnValue", "0", JsonConvert.SerializeObject(result.ReturnValue));

            string param = string.Empty;
            string logger = string.Empty;
            try
            {
                logger = string.Format("{0}.{1}", input.Target.GetType().Name, input.MethodBase.Name);
                param = JsonConvert.SerializeObject(new { BeginDate = beginDate, Input = input.Arguments, ReturnValue = result.ReturnValue });
            }
            catch { }

            var info = new LogEntity()
            {
                logger = logger,
                module = "PerformanceCallHandler",
                param = param
            };
            
            watcher.Stop();
            if (result.Exception != null)
            {
                Cat.GetProducer().LogError(result.Exception);
                a.SetStatus(result.Exception);
            }
            else
            {
                a.Status = "0";
            }
            a.Complete();

            info.msg = string.Format("{0}", watcher.ElapsedMilliseconds);
            log.Info(info);


            return result;
        }
    }
}