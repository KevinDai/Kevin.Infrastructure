using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Cache
{
    using Configuration;

    /// <summary>
    /// 缓存管理器类
    /// </summary>
    public class CacheManager
    {

        #region Fields

        /// <summary>
        /// 缓存管理器实例
        /// </summary>
        public static CacheManager _instance = new CacheManager();

        #endregion

        #region Properties

        /// <summary>
        /// 缓存对象
        /// </summary>
        protected ICache Cache
        {
            get;
            set;
        }

        /// <summary>
        /// 缓存配置数据源对象
        /// </summary>
        protected ICacheSettingProvider CacheSettingProvider
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public CacheManager()
        {
            Cache = new DefaultCache();
            CacheSettingProvider = new ConfigurationCacheSettingProvider();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 配置全局的缓存对象
        /// </summary>
        /// <param name="cache">缓存对象</param>
        public static void SetCache(ICache cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException("cache");
            }
            _instance.Cache = cache;
        }

        /// <summary>
        /// 设置全局的缓存配置数据源对象
        /// </summary>
        /// <param name="provider">缓存配置数据源对象</param>
        public static void SetCacheSettingProvider(ICacheSettingProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            _instance.CacheSettingProvider = provider;
        }

        /// <summary>
        /// 获取默认的缓存对象
        /// </summary>
        /// <returns></returns>
        public static ICache GetCache()
        {
            return _instance.Cache;
        }

        /// <summary>
        /// 根据缓存配置名称获取特定的缓存对象
        /// </summary>
        /// <param name="name">缓存配置名称</param>
        public static ICache GetCache(string name)
        {
            var cacheSetting = _instance.CacheSettingProvider.Get(name);
            if (cacheSetting == null)
            {
                return _instance.Cache;
            }
            else
            {
                return new CacheWapper(_instance.Cache, cacheSetting);
            }
        }

        #endregion

        #region DefaultCache

        internal class DefaultCache : ICache
        {
            public bool Add(string key, object value)
            {
                return false;
            }

            public bool Add(string key, object value, long expireTime)
            {
                return false;
            }

            public object Get(string key)
            {
                return null;
            }

            public void Remove(string key)
            {
            }
        }

        #endregion

    }
}
