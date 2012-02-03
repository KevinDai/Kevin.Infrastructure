using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Cache.Configuration
{
    /// <summary>
    /// 通过配置文件获取缓存配置数据的类
    /// </summary>
    public class ConfigurationCacheSettingProvider : ICacheSettingProvider
    {

        #region Fields

        /// <summary>
        /// 配置文件Section名称
        /// </summary>
        private const string SectionName = "Infrastructure.Cache";

        /// <summary>
        /// 缓存配置对象字典
        /// </summary>
        private readonly IDictionary<string, CacheSetting> _cacheSettings;

        #endregion

        #region Properties

        /// <summary>
        /// 缓存配置Section对象
        /// </summary>
        protected CachesConfigurationSection Section
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public ConfigurationCacheSettingProvider()
        {
            Section = ConfigurationManager.GetSection(SectionName) as CachesConfigurationSection;

            _cacheSettings = new Dictionary<string, CacheSetting>();
            if (Section != null
                &&
                Section.Settings != null
                &&
                Section.Settings.Count > 0)
            {
                foreach (CacheConfigurationElement setting in Section.Settings)
                {
                    _cacheSettings.Add(setting.Name,
                        new CacheSetting(setting.Name, setting.Enable, setting.ExpireTime));
                }
            }
        }

        //必要时还应扩展通过制定文件获取配置信息的构造函数

        #endregion

        #region ICacheSettingProvider implementation

        /// <summary>
        /// <see cref="ICacheSettingProvider"/>
        /// </summary>
        /// <param name="name"><see cref="ICacheSettingProvider"/></param>
        /// <returns><see cref="ICacheSettingProvider"/></returns>
        public CacheSetting Get(string name)
        {
            if (_cacheSettings.ContainsKey(name))
            {
                return _cacheSettings[name];
            }
            return null;
        }

        #endregion
    }
}
