using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class LabyrinthComponentTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            LabyrinthComponent comp = new LabyrinthComponent(0, 0, null);
            Assert.AreEqual(0, comp.x);
            Assert.AreEqual(0, comp.y);
            Assert.IsNull(comp.image);
            Slimus player = new Slimus(0, 0);
            Assert.IsFalse(comp.Collide(player));
            Assert.IsNull(comp.attributes);
        }
    }
}
