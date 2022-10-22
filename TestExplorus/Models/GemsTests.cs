using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class GemsTests
    {
        Slimus slimus;

        [TestInitialize]
        public void Init()
        {
            slimus = new Slimus(0, 0, false);
            Collection gems = new Collection(Sprites.gem, Bars.yellow, false);
            slimus.gems = gems;
        }

        [TestMethod]
        public void TestMethod()
        {
            Gems gem1 = new Gems(0, 0, null);
            Gems gem2 = new Gems(0, 1, null);


            Assert.AreEqual(6, slimus.gems.total);
            Assert.AreEqual(0, slimus.gems.acquired);

            bool gem1Collected = gem1.Collide(slimus);
            Assert.AreEqual(1, slimus.gems.acquired);
            Assert.IsFalse(gem1Collected);

            gem2.Collide(slimus);
            Assert.AreEqual(2, slimus.gems.acquired);
        }
    }
}