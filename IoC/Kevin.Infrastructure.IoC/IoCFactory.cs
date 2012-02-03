using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Kevin.Infrastructure.IoC
{
    /// <summary>
    /// IoC容器工厂类
    /// </summary>
    /// <remarks>
    /// 主要参考ASP.Net MVC中DependencyResolver的实现
    /// </remarks>
    public class IoCFactory
    {
        #region Static accessors

        private readonly static IoCFactory _instance = new IoCFactory();

        public static IContainer CurrentContainer
        {
            get
            {
                return _instance.InnerCurrentContainer;
            }
        }

        public static void SetContainer(IContainer container)
        {
            _instance.InnerSetContainer(container);
        }

        public static void SetContainer(object commonServiceLocator)
        {
            _instance.InnerSetContainer(commonServiceLocator);
        }

        #endregion

        #region Constructor


        #endregion

        #region Instance implementation

        public IContainer InnerCurrentContainer
        {
            get
            {
                return _innerCurrentContainer;
            }
        }
        private IContainer _innerCurrentContainer;


        private void InnerSetContainer(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _innerCurrentContainer = container;
        }

        private void InnerSetContainer(object commonServiceLocator)
        {
            if (commonServiceLocator == null)
            {
                throw new ArgumentNullException("commonServiceLocator");
            }

            Type locatorType = commonServiceLocator.GetType();
            MethodInfo getInstance = locatorType.GetMethod("GetInstance", new[] { typeof(Type) });
            MethodInfo getInstances = locatorType.GetMethod("GetAllInstances", new[] { typeof(Type) });

            if (getInstance == null ||
                getInstance.ReturnType != typeof(object) ||
                getInstances == null ||
                getInstances.ReturnType != typeof(IEnumerable<object>))
            {
                throw new ArgumentException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Messages.exception_ContainerDoesNotImplementICommonServiceLocator,
                        locatorType.FullName
                    ),
                    "commonServiceLocator"
                );
            }

            var getService = (Func<Type, object>)Delegate.CreateDelegate(typeof(Func<Type, object>), commonServiceLocator, getInstance);
            var getServices = (Func<Type, IEnumerable<object>>)Delegate.CreateDelegate(typeof(Func<Type, IEnumerable<object>>), commonServiceLocator, getInstances);

            _innerCurrentContainer = new DelegateBasedContainer(getService, getServices);
        }

        #endregion

        #region Helper classes

        private class DelegateBasedContainer : IContainer
        {
            Func<Type, object> _getService;
            Func<Type, IEnumerable<object>> _getServices;

            public DelegateBasedContainer(Func<Type, object> getService, Func<Type, IEnumerable<object>> getServices)
            {
                _getService = getService;
                _getServices = getServices;
            }

            public object GetService(Type type)
            {
                try
                {
                    return _getService.Invoke(type);
                }
                catch
                {
                    return null;
                }
            }

            public IEnumerable<object> GetServices(Type type)
            {
                return _getServices(type);
            }
        }

        #endregion
    }
}
