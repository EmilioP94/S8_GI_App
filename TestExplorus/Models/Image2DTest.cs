using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class Image2DTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Image2D image = new Image2D(null, ImageType.Wall);
            Assert.IsNull(image.image);
            Assert.AreEqual(ImageType.Wall, image.type);
        }
    }
}