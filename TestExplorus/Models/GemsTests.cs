using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class GemsTests
    {
        [TestMethod]
        public void TestMethod()
        {
            Gems gems = new Gems(0, 0, null);

            Sprites[,] testMap = new Sprites[1, 1];
            testMap[0, 0] = Sprites.gem;
            Collection collectionGems = new Collection(testMap, Sprites.gem, Bars.yellow, false);

            Slimus slimus = new Slimus(0, 0);
            slimus.SetCollections(collectionGems);

            Assert.AreEqual(1, slimus.gems.total);
            Assert.AreEqual(0, slimus.gems.acquired);

            gems.Collide(slimus);
            Assert.AreEqual(1, slimus.gems.acquired);

            gems.Collide(slimus);
            Assert.AreEqual(1, slimus.gems.acquired);
        }
    }
}