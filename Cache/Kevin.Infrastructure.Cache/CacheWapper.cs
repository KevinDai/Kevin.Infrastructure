using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Cache
{
    /// <summary>
    /// 缓存对象的代理类，缓存操作时增加缓存配置的判断
    /// </summary>
    internal class CacheWapper : ICache
    {
        #region Properties

        /// <summary>
        /// 原始缓存对象
        /// </summary>
        protected ICache Cache
        {
            get;
            private set;
        }

        /// <summary>
        /// 缓存配置对象
        /// </summary>
        protected CacheSetting CacheSetting
        {
            get;
            private set;
        }

        #endregion

        #region Constructors


        public CacheWapper(ICache cache, CacheSetting cacheSetting)
        {
            if (cache == null)
            {
                throw new ArgumentNullException("cache");
            }
            if (cacheSetting == null)
            {
                throw new ArgumentNullException("cacheSetting");
            }

            Cache = cache;
            CacheSetting = cacheSetting;
        }

        #endregion

        #region ICache implementation

        public bool Add(string key, object value)
        {
            if (CacheSetting.Enable)
            {
                if (CacheSetting.ExpireTime > 0)
                {
                    return Cache.Add(key, value, CacheSetting.ExpireTime);
                }
                else
                {
                    return Cache.Add(key, value);
                }
            }
            return false;
        }

        public bool Add(string key, object value, long expireTime)
        {
            if (CacheSetting.Enable)
            {
                return Cache.Add(key, value, expireTime);
            }
            return false;
        }

        public object Get(string key)
        {
            if (CacheSetting.Enable)
            {
                return Cache.Get(key);
            }
            return null;
        }

        public void Remove(string key)
        {
            if (CacheSetting.Enable)
            {
                Cache.Remove(key);
            }
        }

        #endregion
    }
}
