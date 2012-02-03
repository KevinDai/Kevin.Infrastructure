using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevin.Infrastructure.Cache.Test.Configuration
{

    using Kevin.Infrastructure.Cache.Configuration;

    /// <summary>
    /// ConfigurationCacheSettingProviderTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ConfigurationCacheSettingProviderTest
    {
        public ConfigurationCacheSettingProviderTest()
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
        public void ConfigurationCacheSettingProvider_Get_Test()
        {
            //初始化
            var provider = new ConfigurationCacheSettingProvider();
            var name = "Test";

            //操作
            var result = provider.Get(name);

            //验证
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == name);
        }
    }
}
