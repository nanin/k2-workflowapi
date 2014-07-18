using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Application.Interface;
using DianPing.WorkFlow.Application.Implementation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using DianPing.WorkFlow.Application.Implementation.Task;
using DianPing.WorkFlow.Application.Implementation.Process;

namespace DianPing.WorkFlow.Application.UnityHelpers
{
    public class RegisterApplication
    {
        public void Register(Object sender, RegisterEventArgs e)
        {
            e.Container
                .RegisterType<IWorkFlowTaskService, WorkFlowTaskService>(new ExternallyControlledLifetimeManager())
                .RegisterType<IWorkFlowProcessService, WorkFlowProcessService>(new ExternallyControlledLifetimeManager());

            e.Container.Configure<Interception>()
                .SetInterceptorFor<IWorkFlowTaskService>(new InterfaceInterceptor())
                .SetInterceptorFor<IWorkFlowProcessService>(new InterfaceInterceptor())
                ;
        }
    }
}
