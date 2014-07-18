using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Application.UnityHelpers;

namespace DianPing.WorkFlow.Application.Implementation.UnityHelpers
{
    public abstract class RegisteUnity
    {
        private static readonly IUnityContainer current = new UnityContainer();
        public static IUnityContainer Current { get { return current; } }

        public static void RegisterForDefault()
        {
            current.AddNewExtension<Interception>();
            Container container = new Container(current);
            container.Regiseters += (new RegisterApplication()).Register;
            container.Regiseters += (new RegisterRepositories()).Register;
            container.Regiseters += (new RegisterServiceProvider()).Register;
            container.Regiseters += (new RegisterInfrastructure()).Register;
            container.Regiseters += (new RegisterDomain()).Register;
            container.RegisterAll();
        }
    }
}
