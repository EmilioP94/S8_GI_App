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
            Collection collection = new Collection(emptyMap, Sprites.heart, Bars.blue, false);
            Assert.AreEqual<int>(0, collection.total);
            Assert.AreEqual<int>(0, collection.acquired);
        }
    }
}
