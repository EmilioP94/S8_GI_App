using Explorus;
using Explorus.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class SlimusTests
    {
        [TestMethod]
        public void TestMoveUp()
        {
            Slimus slimus = new Slimus(0, 10);

            Assert.AreEqual(slimus.x, 0);
            Assert.AreEqual(slimus.y, 10);

            slimus.Move(Explorus.Controllers.Direction.Up);

            Assert.AreEqual(slimus.x, 0);
            Assert.AreEqual(slimus.y, 10 - (int)(16 * Constants.playerSpeed));
        }

        [TestMethod]
        public void TestMoveDown()
        {
            Slimus slimus = new Slimus(0, 10);

            Assert.AreEqual(slimus.x, 0);
            Assert.AreEqual(slimus.y, 10);

            slimus.Move(Explorus.Controllers.Direction.Down);

            Assert.AreEqual(slimus.x, 0);
            Assert.AreEqual(slimus.y, 10 + (int)(16 * Constants.playerSpeed));
        }

        [TestMethod]
        public void TestMoveRight()
        {
            Slimus slimus = new Slimus(0, 0);

            Assert.AreEqual(slimus.x, 0);
            Assert.AreEqual(slimus.y, 0);

            slimus.Move(Explorus.Controllers.Direction.Right);

            Assert.AreEqual(slimus.x, (int)(16 * Constants.playerSpeed));
            Assert.AreEqual(slimus.y, 0);
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            Slimus slimus = new Slimus(10, 0);

            Assert.AreEqual(slimus.x, 10);
            Assert.AreEqual(slimus.y, 0);

            slimus.Move(Explorus.Controllers.Direction.Left);

            Assert.AreEqual(slimus.x, 10 - (int)(16 * Constants.playerSpeed));
            Assert.AreEqual(slimus.y, 0);
        }
    }
}
