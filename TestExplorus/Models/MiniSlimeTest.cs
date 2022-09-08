using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class MiniSlimeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Image2D image = new Image2D(null, ImageType.MiniSlime);
            MiniSlime miniSlime = new MiniSlime(0, 0, image);
            Assert.IsNotNull(miniSlime.image);
            Slimus player = new Slimus(0, 0);
            Assert.IsFalse(miniSlime.Collide(player));
            Assert.IsNull(miniSlime.image);
        }
    }
}
