using System.Configuration;
using System;

namespace Kevin.Infrastructure.Cache.Configuration
{
    public class CachesConfigurationSection : ConfigurationSection
    {

        #region 字段

        private const string SettingsItem = "settings";

        #endregion

        #region 属性

        [ConfigurationProperty(SettingsItem)]
        public CacheConfigurationElementCollection Settings
        {
            get
            {
                return base[SettingsItem] as CacheConfigurationElementCollection;
            }
        }

        #endregion

    }

}
