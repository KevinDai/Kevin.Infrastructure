using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Kevin.Infrastructure.Cache.Test.Configuration
{
    using Kevin.Infrastructure.Cache.Configuration;

    [TestClass]
    public class CachesConfigurationSectionTest
    {
        [TestMethod]
        public void CachesConfigurationSection_Get_Test()
        {
            try
            {
                CachesConfigurationSection ccs = ConfigurationManager.GetSection("Infrastructure.Cache") as CachesConfigurationSection;

                Assert.IsNotNull(ccs);
                Assert.IsTrue(ccs.Settings.Count > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
