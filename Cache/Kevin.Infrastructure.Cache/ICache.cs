using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Cache
{

    /// <summary>
    /// 缓存操作接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否写入成功</returns>
        bool Add(string key, object value);

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expireTime">是否写入成功</param>
        /// <returns></returns>
        bool Add(string key, object value, long expireTime);

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        object Get(string key);

        /// <summary>
        /// 删除指定键缓存
        /// </summary>
        /// <param name="key">键</param>
        void Remove(string key);
    }
}
