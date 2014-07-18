using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using log4net;
using DianPing.WorkFlow.Infrastructure.Entity;

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
            var info = new LogEntity()
            {
                logger = string.Format("{0}.{1}", input.Target.GetType().Name, input.MethodBase.Name),
                module = "PerformanceCallHandler",
                param = ""
            };
            //var expLog = LogManager.GetLogger(input.Target.GetType());
            var watcher = new Stopwatch();
            watcher.Start();

            var result = getNext()(input, getNext);

            watcher.Stop();

            info.msg = string.Format("[RunTime]:{0}", watcher.ElapsedMilliseconds);
            log.Info(info);

            return result;
        }
    }
}
