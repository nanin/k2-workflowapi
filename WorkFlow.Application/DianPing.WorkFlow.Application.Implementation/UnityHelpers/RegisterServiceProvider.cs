using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;
using DianPing.WorkFlow.ServiceProvider;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace DianPing.WorkFlow.Application.UnityHelpers
{
    public class RegisterServiceProvider
    {
        public void Register(Object sender, RegisterEventArgs e)
        {
            e.Container
                .RegisterType<IK2ServiceProvider, K2ServiceProvider>(new ExternallyControlledLifetimeManager())
                .RegisterType<IEmployeeServiceProvider, EmployeeServiceProvider>(new ExternallyControlledLifetimeManager())
                ;

            e.Container.Configure<Interception>()
                .SetInterceptorFor<IK2ServiceProvider>(new InterfaceInterceptor())
                ;
        }
    }
}
