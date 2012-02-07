using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemcachedProviders.Cache;

namespace Kevin.Infrastructure.Cache.Memcached
{

    /// <summary>
    /// Memcached缓存
    /// </summary>
    public class MemcachedCache : ICache
    {

        #region ICache implementation

        public bool Add(string key, object value)
        {
            return DistCache.Add(key, value, true);
        }

        public bool Add(string key, object value, long expireTime)
        {
            return DistCache.Add(key, value, expireTime);
        }

        public object Get(string key)
        {
            return DistCache.Get(key);
        }

        public void Remove(string key)
        {
            DistCache.Remove(key);
        }

        #endregion
    }
}
