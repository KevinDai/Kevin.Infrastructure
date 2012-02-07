using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kevin.Infrastructure.Cache.Test
{

    using Kevin.Infrastructure.Cache;

    /// <summary>
    /// CacheManagerTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CacheManagerTest
    {
        public CacheManagerTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CacheManager_GetCache_ReturnSameCacheWhenCacheSettingNotExist_Test()
        {
            //初始化
            var moqCache = new Mock<ICache>();
            var moqProvider = new Mock<ICacheSettingProvider>();
            CacheManager.SetCache(moqCache.Object);
            CacheManager.SetCacheSettingProvider(moqProvider.Object);

            var name = "Test";

            //操作
            var result = CacheManager.GetCache(name);

            //验证
            Assert.IsTrue(result == moqCache.Object);
        }

        [TestMethod]
        public void CacheManager_GetCache_ReturnDifferentCacheWhenCacheSettingExist_Test()
        {
            //初始化
            var moqCache = new Mock<ICache>();
            var moqProvider = new Mock<ICacheSettingProvider>();
            CacheManager.SetCache(moqCache.Object);
            CacheManager.SetCacheSettingProvider(moqProvider.Object);

            var name = "Test";
            moqProvider.Setup((p) => p.Get(name)).Returns(new CacheSetting(name, true, 1000));

            //操作
            var result = CacheManager.GetCache(name);

            //验证
            Assert.IsTrue(result != moqCache.Object);
        }

        [TestMethod]
        public void CacheManager_GetCache_Add_ExecuteUsingCacheSetting_Test()
        {
            //初始化
            var moqCache = new Mock<ICache>();
            var moqProvider = new Mock<ICacheSettingProvider>();
            CacheManager.SetCache(moqCache.Object);
            CacheManager.SetCacheSettingProvider(moqProvider.Object);

            var name = "Test";
            var cacheSetting = new CacheSetting(name, true, 1000);
            moqProvider.Setup((p) => p.Get(name)).Returns(new CacheSetting(name, true, 1000));

            var key = "key";
            var value = "value";

            //操作
            var result = CacheManager.GetCache(name);
            result.Add("key", "value");

            //验证
            moqCache.Verify(c => c.Add(key, value, cacheSetting.ExpireTime), Times.Once());
        }

        [TestMethod]
        public void CacheManager_GetCache_Add_NotUseCacheSettingExpireTime_Test()
        {
            //初始化
            var moqCache = new Mock<ICache>();
            var moqProvider = new Mock<ICacheSettingProvider>();
            CacheManager.SetCache(moqCache.Object);
            CacheManager.SetCacheSettingProvider(moqProvider.Object);

            var name = "Test";
            //注意缓存过期时间设置为无效值
            moqProvider.Setup((p) => p.Get(name)).Returns(new CacheSetting(name, true, 0));

            var key = "key";
            var value = "value";

            //操作
            var result = CacheManager.GetCache(name);
            result.Add(key, value);

            //验证
            moqCache.Verify(c => c.Add(key, value), Times.Once());
            moqCache.Verify(c => c.Add(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()), Times.Never());
        }

        [TestMethod]
        public void CacheManager_GetCache_Add_NotExecuteWhenCacheSettingEnableFalse_Test()
        {
            //初始化
            var moqCache = new Mock<ICache>();
            var moqProvider = new Mock<ICacheSettingProvider>();
            CacheManager.SetCache(moqCache.Object);
            CacheManager.SetCacheSettingProvider(moqProvider.Object);

            var name = "Test";
            //注意缓存过期时间设置为无效值
            moqProvider.Setup((p) => p.Get(name)).Returns(new CacheSetting(name, false, 0));

            //操作
            var result = CacheManager.GetCache(name);
            result.Add("key", "value");

            //验证
            moqCache.Verify(c => c.Add(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            moqCache.Verify(c => c.Add(It.IsAny<string>(), It.IsAny<string>(),It.IsAny<long>()), Times.Never());
        }


    }
}
