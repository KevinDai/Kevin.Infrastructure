using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Cache
{
    /// <summary>
    /// 获取缓存配置数据接口定义
    /// </summary>
    public interface ICacheSettingProvider
    {

        /// <summary>
        /// 获取缓存配置对象
        /// </summary>
        /// <param name="name">缓存配置对象关键字</param>
        /// <returns>缓存配置对象</returns>
        CacheSetting Get(string name);

    }
}
