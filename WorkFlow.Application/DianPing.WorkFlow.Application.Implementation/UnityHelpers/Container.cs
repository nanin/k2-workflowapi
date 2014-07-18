using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace DianPing.WorkFlow.Application.UnityHelpers
{
    public class Container
    {
        private IUnityContainer _container;

        public Container(IUnityContainer container)
        {
            this._container = container;
        }

        public delegate void RegisterEventHandler(Object sender, RegisterEventArgs e);

        public event RegisterEventHandler Regiseters;

        public void OnRegiseter(RegisterEventArgs e)
        {
            RegisterEventHandler handler = Regiseters;
            if (handler != null) handler(this, e);
        }

        public void RegisterAll()
        {
            RegisterEventArgs e = new RegisterEventArgs(_container);
            OnRegiseter(e);
        }

    }
    public class RegisterEventArgs : EventArgs
    {
        public readonly IUnityContainer Container;

        public RegisterEventArgs(IUnityContainer container)
        {
            this.Container = container;
        }
    }
}
