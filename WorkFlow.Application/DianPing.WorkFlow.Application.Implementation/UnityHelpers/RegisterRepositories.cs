using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln;
using DianPing.WorkFlow.Repositories.Implementation.DianPingK2Sln;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog;
using DianPing.WorkFlow.Repositories.Implementation.DianPingK2ServerLog;
using DianPing.WorkFlow.Repositories.Interface;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM;
using DianPing.WorkFlow.Repositories.Implementation.DianpingK2SQLUM;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Server;
using DianPing.WorkFlow.Repositories.Implementation.DianPingK2Server;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace DianPing.WorkFlow.Application.UnityHelpers
{
    public class RegisterRepositories
    {
        public void Register(Object sender, RegisterEventArgs e)
        {
            e.Container
                .RegisterType<IProcessInfoRepostories, ProcessInfoRepostories>(new ExternallyControlledLifetimeManager())
                .RegisterType<IWorklistRepostories, WorklistRepostories>(new ExternallyControlledLifetimeManager())
                .RegisterType<IPorcessInstRepostories, PorcessInstRepostories>(new ExternallyControlledLifetimeManager())
                .RegisterType<IK2CommentRepostories, K2CommentRepostories>(new ExternallyControlledLifetimeManager())
                .RegisterType <IK2UserRepostories,K2UserRepostories>(new ExternallyControlledLifetimeManager())
                .RegisterType<IK2CityRepostories, K2CityRepostories>(new ExternallyControlledLifetimeManager())
                .RegisterType<IK2ParticipateRepostories, K2ParticipateRepostories>(new ExternallyControlledLifetimeManager())
                .RegisterType<IWorklistHeaderRepositories, WorklistHeaderRepositories>(new ExternallyControlledLifetimeManager())
                .RegisterType<IActInstDestRepostories, ActInstDestRepostories>(new ExternallyControlledLifetimeManager())
                ;
            
            e.Container.Configure<Interception>()
                .SetInterceptorFor<IK2CommentRepostories>(new InterfaceInterceptor())
                .SetInterceptorFor<IProcessInfoRepostories>(new InterfaceInterceptor())
                .SetInterceptorFor<IWorklistRepostories>(new InterfaceInterceptor())
                ;
        }
    }
}
