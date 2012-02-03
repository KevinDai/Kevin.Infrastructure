using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Cache
{

    /// <summary>
    /// 缓存设置
    /// </summary>
    /// <remarks>
    /// 主要用于不同模块使用缓存时，缓存的状态和默认缓存时间等的配置
    /// </remarks>
    public class CacheSetting
    {

        #region Propertyies

        /// <summary>
        /// 关键字
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 缓存是否开启
        /// </summary>
        public bool Enable
        {
            get;
            private set;
        }

        /// <summary>
        /// 默认的缓存过期时间
        /// </summary>
        public long ExpireTime
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CacheSetting(string name, bool enable, long expireTime)
        {
            Name = name;
            Enable = enable;
            ExpireTime = expireTime;
        }

        #endregion
    }
}
