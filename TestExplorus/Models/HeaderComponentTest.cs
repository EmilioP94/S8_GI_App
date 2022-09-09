using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class HeaderComponentTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Image2D image = new Image2D(null, ImageType.Door);
            HeaderComponent component = new HeaderComponent(0, 0, image, Bars.none ,"test");
            Assert.AreEqual("test", component.name);
            Assert.AreEqual(Bars.none, component.barName);
            Assert.AreEqual(image, component.image);
            Assert.AreEqual(0, component.x);
            Assert.AreEqual(0, component.y);
        }
    }
}
