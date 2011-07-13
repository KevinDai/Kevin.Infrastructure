using System;
using System.Collections.Generic;

namespace Kevin.Infrastructure.IoC
{
    /// <summary>
    /// 适配不同Ioc容器的Resolve职责
    /// </summary>
    public interface IContainer
    {

        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);

    }
}
