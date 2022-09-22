using Explorus;
using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class LabyrinthTest
    {
        Labyrinth lab;
        [TestInitialize]
        public void Init()
        {
            lab = new Labyrinth(Constants.level_1);
            Assert.AreEqual(1, lab.miniSlimes.Count);
            Assert.IsNotNull(lab.playerCharacter);
            Assert.IsFalse(lab.gameEnded);
        }

        [TestMethod]
        public void TestReload()
        {
            lab.Reload(Constants.level_1);
            Assert.AreEqual(1, lab.miniSlimes.Count);
            Assert.IsNotNull(lab.playerCharacter);
            Assert.IsFalse(lab.gameEnded);
        }
    }
}
