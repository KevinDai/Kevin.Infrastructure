using System.Configuration;
using System;

namespace Kevin.Infrastructure.Cache.Configuration
{
    [ConfigurationCollection(typeof(CacheConfigurationElement))]
    public class CacheConfigurationElementCollection : ConfigurationElementCollection
    {

        #region 字段

        #endregion

        #region 属性

        /// <summary>
        /// 获取配置元素
        /// </summary>
        /// <param name="name">配置元素名称</param>
        /// <returns></returns>
        public new CacheConfigurationElement this[string name]
        {
            get
            {
                return base[name] as CacheConfigurationElement;
            }
        }

        /// <summary>
        /// 获取配置元素
        /// </summary>
        /// <param name="index">配置元素集合中位置</param>
        /// <returns></returns>
        public CacheConfigurationElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as CacheConfigurationElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public CacheConfigurationElementCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// 添加配置元素
        /// </summary>
        /// <param name="index">添加位置</param>
        /// <param name="element">配置元素</param>
        protected override void BaseAdd(int index, ConfigurationElement element)
        {
            //添加配置元素位置是否有效
            if (index < 0)
            {
                base.BaseAdd(element, false);
            }
            else
            {
                base.BaseAdd(index, element);
            }
        }

        /// <summary>
        /// 添加配置元素
        /// </summary>
        /// <param name="element">配置元素</param>
        public void Add(CacheConfigurationElement element)
        {
            BaseAdd(element);
        }

        /// <summary>
        /// 清除配置集合所有元素
        /// </summary>
        public void Clear()
        {
            base.BaseClear();
        }

        /// <summary>
        /// 查询配置元素在集合中的位置
        /// </summary>
        /// <param name="element">配置元素</param>
        /// <returns>配置元素在集合中的位置</returns>
        public int IndexOf(CacheConfigurationElement element)
        {
            return base.BaseIndexOf(element);
        }

        /// <summary>
        /// 添加新的配置元素
        /// </summary>
        /// <returns>配置元素</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheConfigurationElement();
        }

        /// <summary>
        /// 获取配置元素的关键字
        /// </summary>
        /// <param name="element">配置元素</param>
        /// <returns>配置元素的关键字</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as CacheConfigurationElement).Key;
        }

        /// <summary>
        /// 删除配置元素
        /// </summary>
        /// <param name="element">配置元素</param>
        public void Remove(CacheConfigurationElement element)
        {
            if (IndexOf(element) >= 0)
            {
                base.BaseRemove(element.Key);
            }
        }

        /// <summary>
        /// 删除配置元素
        /// </summary>
        /// <param name="key">配置元素的名称属性值</param>
        public void Remove(string key)
        {
            base.BaseRemove(key);
        }

        /// <summary>
        /// 删除配置元素
        /// </summary>
        /// <param name="index">配置元素所处集合中的位置</param>
        public void RemoveAt(int index)
        {
            base.BaseRemoveAt(index);
        }

        #endregion

    }

}