using Explorus.Controllers;
using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace TestExplorus.Models
{
    /// <summary>
    /// Summary description for CollectionTests
    /// </summary>
    [TestClass]
    public class CollectionTests
    {
        [TestMethod]
        public void TestMethod()
        {
            Sprites[,] emptyMap = new Sprites[0, 0];
            Collection collection = new Collection(Sprites.heart, Bars.blue, false);
            Assert.AreEqual<int>(6, collection.total);
            Assert.AreEqual<int>(0, collection.acquired);
            collection.Acquire();
            Assert.AreEqual<int>(1, collection.acquired);
            Labyrinth lab = new Labyrinth(TestConstants.level_1);
            HeaderController observer = new HeaderController(lab);
            collection = new Collection(Sprites.gem, Bars.yellow, false);
            Assert.AreEqual(6, collection.total);
            Assert.AreEqual(0, collection.acquired);
            collection.Acquire();
            Assert.AreEqual(1, collection.acquired);
        }
    }
}
