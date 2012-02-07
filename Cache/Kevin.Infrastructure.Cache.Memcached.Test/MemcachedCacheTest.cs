using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Kevin.Infrastructure.Cache.Memcached.Test
{

    /// <summary>
    /// Memcached缓存测试，测试前需要本机安装Memcached
    /// </summary>
    /// <remarks>
    /// 配置Memcached的默认过期时间为5秒
    /// </remarks>
    [TestClass]
    public class MemcachedCacheTest
    {

        [TestMethod]
        public void MemcachedCache_Remove_Test()
        {
            //初始化
            MemcachedCache cache = new MemcachedCache();

            var key = "key";
            var value = "value";

            //验证
            cache.Add(key, value);
            string result = cache.Get(key) as string;
            Assert.IsNotNull(result);

            cache.Remove(key);
            result = cache.Get(key) as string;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MemcachedCache_Add_UsingConfigExpireTime_Test()
        {
            //初始化
            MemcachedCache cache = new MemcachedCache();

            var key = "key";
            var value = "value";

            //验证
            cache.Remove(key);
            string result = cache.Get(key) as string;
            Assert.IsNull(result);

            cache.Add(key, value);
            result = cache.Get(key) as string;

            Assert.IsTrue(result == value);
            //6秒后缓存应该过期
            Thread.Sleep(6000);
            result = cache.Get(key) as string;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MemcachedCache_Add_UsingSetExpireTime_Test()
        {
            //初始化
            MemcachedCache cache = new MemcachedCache();

            var key = "key";
            var value = "value";

            //验证
            cache.Remove(key);
            string result = cache.Get(key) as string;
            Assert.IsNull(result);

            cache.Add(key, value,10000);
            result = cache.Get(key) as string;
            Assert.IsTrue(result == value);

            //试用设置的缓存时间，而不是配置的缓存时间
            Thread.Sleep(6000);
            result = cache.Get(key) as string;
            Assert.IsNotNull(result);

            //总共11秒后缓存应该过期
            Thread.Sleep(5000);
            result = cache.Get(key) as string;
            Assert.IsNull(result);
        }


    }
}
