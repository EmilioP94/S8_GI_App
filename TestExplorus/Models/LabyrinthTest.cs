using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class LabyrinthTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Labyrinth lab = new Labyrinth(TestConstants.level_1);
            Assert.AreEqual(1, lab.miniSlimes.Count);
            Assert.IsNotNull(lab.playerCharacter);
            Assert.IsFalse(lab.gameEnded);
        }
    }
}
