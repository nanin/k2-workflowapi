using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;
using Com.Dianping.Cat;
using Newtonsoft.Json;
using DianPing.WorkFlow.Infrastructure.Entity;
using log4net;

namespace DianPing.WorkFlow.Infrastructure.Interception
{
    public class CatAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new CatCallHandler() { Order = 10 };
        }
    }

    public class CatCallHandler : ICallHandler
    {
        public int Order { get; set; }
        private static readonly ILog log = LogManager.GetLogger(typeof(ExceptionCallHandler));

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Cat.GetProducer().NewTransaction(input.Target.GetType().Name, input.MethodBase.Name);
            var a = Cat.GetManager().PeekTransaction;
            
            var result = getNext()(input, getNext);
            
            if (result.Exception != null)
            {
                Cat.GetProducer().LogEvent(input.MethodBase.Name, "Arguments", "0", JsonConvert.SerializeObject(input.Arguments));
           
                Cat.GetProducer().LogError(result.Exception);
                a.SetStatus(result.Exception);

                if (log.IsErrorEnabled)
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
                    log.Error(info, result.Exception);
                }
            }
            else
            {
                a.Status = "0";
            }
            a.Complete();

            return result;
        }
    }
}
