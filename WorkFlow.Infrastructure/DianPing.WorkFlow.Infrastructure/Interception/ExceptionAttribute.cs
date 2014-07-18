using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using log4net;
using DianPing.WorkFlow.Infrastructure.Entity;


namespace DianPing.WorkFlow.Infrastructure.Interception
{
    public class ExceptionAttribute : HandlerAttribute
    {

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new ExceptionCallHandler();
        }

    }

    public class ExceptionCallHandler : ICallHandler
    {
        public int Order { get; set; }
        private static readonly ILog log = LogManager.GetLogger(typeof(ExceptionCallHandler));

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //var id = Guid.NewGuid();

            //调试状态
            //if (log.IsDebugEnabled)
            //{
            // var info = new
            // {
            // id = id,
            // @object = input.Target.GetType().Name,
            // method = input.MethodBase.Name,
            // args = input.Arguments,
            // };
            // log.Debug("[函数调用]" + JsonConvert.SerializeObject(info));
            //}

            var result = getNext()(input, getNext);
            if (result.Exception != null && (!result.Exception.Data.Contains("IsLoged")))
            {

                string logger = string.Empty;
                string param = string.Empty;
                try
                {
                    logger = string.Format("{0}.{1}", input.Target.GetType().Name, input.MethodBase.Name);
                    param = JsonConvert.SerializeObject(new { Input = input.Arguments, ReturnValue = result.ReturnValue });
                }
                catch { }

                var info = new LogEntity()
                {
                    logger = logger,
                    module = "ExceptionCallHandler",
                    msg = result.Exception.Message,
                    param = param
                };
                //var expLog = LogManager.GetLogger(input.Target.GetType());
                //log.Debug(JsonConvert.SerializeObject(input.Arguments));

                log.Error(info, result.Exception);
                result.Exception.Data.Add("IsLoged", true);
            }

            return result;
        }
    }
}