using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Newtonsoft.Json;
using DianPing.WorkFlow.Infrastructure.Entity;

namespace DianPing.WorkFlow.Infrastructure.Log4Net
{
    public class LogHelper
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Error(string logger , string msg, Exception ex, string param)
        {
            log.Error(new LogEntity()
                {
                    logger = logger,
                    module = "LogHelper",
                    msg = msg,
                    param = param
                }, ex);
        }
    }
}
