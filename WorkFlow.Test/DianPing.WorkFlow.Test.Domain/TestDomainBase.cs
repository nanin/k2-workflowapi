using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using Microsoft.Practices.Unity;

namespace DianPing.WorkFlow.Test.Domain
{
    public class TestDomainBase<T> where T : TestDomainBase<T>
    {
        public TestDomainBase()
        {
            RegisteUnity.RegisterForDefault();
            RegisteUnity.Current.BuildUp<T>(this as T);
        }
    }
    public static class UtilContainer
    {
        private static readonly IUnityContainer current = new UnityContainer();
        public static IUnityContainer Current { get { return current; } }

        static UtilContainer()
        {
            DefaultRegiest();
        }
        private static void DefaultRegiest()
        {
            RegisteUnity.RegisterForDefault();
        }
    }
}