using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace kevin.Infrastructure.IoC.Test
{

    using Kevin.Infrastructure.IoC;

    [TestClass]
    public class IoCFactoryTest
    {
        [TestMethod]
        public void GetService_Test()
        {
            //初始化
            var type = typeof(string);
            var testString = "Test";
            var mockContainer = new Mock<IContainer>();
            mockContainer.Setup(m => m.GetService(type)).Returns(testString);

            IoCFactory.SetContainer(mockContainer.Object);

            //操作 
            var result = IoCFactory.CurrentContainer.GetService(type);

            //验证
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual(testString, result);
        }

        [TestMethod]
        public void GetServices_Test()
        {
            //初始化
            var type = typeof(string);
            Object[] testObjects = { "Test" };
            var mockContainer = new Mock<IContainer>();
            mockContainer.Setup(m => m.GetServices(type)).Returns(testObjects);

            IoCFactory.SetContainer(mockContainer.Object);

            //操作 
            var result = IoCFactory.CurrentContainer.GetServices(type);

            //验证
            Assert.AreSame(testObjects, result);
        }

    }
}
