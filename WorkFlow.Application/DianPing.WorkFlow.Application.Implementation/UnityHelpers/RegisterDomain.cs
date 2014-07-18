using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Domain.Implementation;
using DianPing.WorkFlow.Domain.Interface;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace DianPing.WorkFlow.Application.UnityHelpers
{
    public class RegisterDomain
    {
        public void Register(Object sender, RegisterEventArgs e)
        {
            e.Container
                .RegisterType<ITaskDomain, TaskDomain>(new ExternallyControlledLifetimeManager())
                .RegisterType<IProcessInfoDomain, ProcessInfoDomain>(new ExternallyControlledLifetimeManager())
                .RegisterType<IK2CommentDomain, K2CommentDomain>(new ExternallyControlledLifetimeManager())
                .RegisterType<IK2UserDomain, K2UserDomain>(new ExternallyControlledLifetimeManager())
                .RegisterType<IK2CityDomain, K2CityDomain>(new ExternallyControlledLifetimeManager())
                .RegisterType<IK2ParticipateDomain, K2ParticipateDomain>(new ExternallyControlledLifetimeManager())
                ;


            e.Container.Configure<Interception>()
                .SetInterceptorFor<IK2UserDomain>(new InterfaceInterceptor())
                .SetInterceptorFor<IK2CommentDomain>(new InterfaceInterceptor())
                ;
        }
    }
}
