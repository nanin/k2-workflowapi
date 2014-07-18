using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using DianPing.WorkFlow.Infrastructure;

namespace DianPing.WorkFlow.Application.UnityHelpers
{
    public class RegisterInfrastructure
    {
        public void Register(Object sender, RegisterEventArgs e)
        {
            //e.Container
            //    .RegisterType<IWorkFlowTaskService, WorkFlowTaskService>(new ExternallyControlledLifetimeManager());

            //e.Container.Configure<Interception>().SetInterceptorFor<APIKeyUtility>(new VirtualMethodInterceptor());
        }
    }
}
