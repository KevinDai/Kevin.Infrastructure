using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Kevin.Infrastructure.IoC.Unity
{
    public class IoCUnityContainer : IContainer
    {

        #region Members

        public IUnityContainer UnityContainer
        {
            get
            {
                return _unityContainer;
            }
        }
        private IUnityContainer _unityContainer;

        #endregion

        #region Constructor

        public IoCUnityContainer()
        {
            _unityContainer = new UnityContainer();
        }

        public IoCUnityContainer(IUnityContainer unityContainer)
        {
            if (unityContainer == null)
            {
                throw new ArgumentNullException("unityContainer");
            }
            _unityContainer = unityContainer;
        }

        #endregion

        #region Methods

        #endregion

        #region IContainer Members

        public object GetService(Type serviceType)
        {
            return UnityContainer.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return UnityContainer.ResolveAll(serviceType);
        }

        #endregion
    }
}
