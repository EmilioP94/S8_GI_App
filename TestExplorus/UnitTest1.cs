using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Explorus.Models;

namespace TestExplorus
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Image2D test = new Image2D();
            Assert.IsNotNull(test);
            Assert.IsNotNull(test.image);
        }
    }
}
