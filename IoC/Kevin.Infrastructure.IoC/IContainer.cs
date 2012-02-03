using System;
using System.Collections.Generic;

namespace Kevin.Infrastructure.IoC
{
    /// <summary>
    /// 适配不同Ioc容器接口定义
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// 根据类型获取实例对象
        /// </summary>
        /// <param name="serviceType">类型</param>
        /// <returns>实例对象</returns>
        object GetService(Type serviceType);

        /// <summary>
        /// 根据类型获取多个实例对象
        /// </summary>
        /// <param name="serviceType">类型</param>
        /// <returns>多个实例对象</returns>
        IEnumerable<object> GetServices(Type serviceType);

    }
}
