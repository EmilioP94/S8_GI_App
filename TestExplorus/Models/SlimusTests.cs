using Explorus;
using Explorus.Models;
using Explorus.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestExplorus.Models
{
    [TestClass]
    public class SlimusTests
    {
        Slimus slimus;
        int originalX = 0;
        int originalY = 10;
        int elapseTime = 16;

        [TestInitialize]
        public void Initialize()
        {
            slimus = new Slimus(originalX, originalY);
        }

        [TestMethod]
        public void TestMoveUp()
        {

            Assert.AreEqual(originalX, slimus.x);
            Assert.AreEqual(originalY, slimus.y);

            slimus.Move(Direction.Up);
            slimus.UpdatePosition(elapseTime);

            int expectedY = originalY - (int)(elapseTime * Constants.playerSpeed);

            Assert.AreEqual(originalX, slimus.x);
            Assert.AreEqual(expectedY, slimus.y);
        }

        [TestMethod]
        public void TestMoveDown()
        {
            Assert.AreEqual(originalX, slimus.x);
            Assert.AreEqual(originalY, slimus.y);

            slimus.Move(Direction.Down);
            slimus.UpdatePosition(elapseTime);

            int expectedY = originalY + (int)(elapseTime * Constants.playerSpeed);

            Assert.AreEqual(originalX, slimus.x);
            Assert.AreEqual(expectedY, slimus.y);
        }

        [TestMethod]
        public void TestMoveRight()
        {
            Assert.AreEqual(originalX, slimus.x);
            Assert.AreEqual(originalY, slimus.y);

            slimus.Move(Direction.Right);
            slimus.UpdatePosition(elapseTime);

            int expectedX = originalX + (int)(elapseTime * Constants.playerSpeed);

            Assert.AreEqual(expectedX, slimus.x);
            Assert.AreEqual(originalY, slimus.y);
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            Assert.AreEqual(originalX, slimus.x);
            Assert.AreEqual(originalY, slimus.y);

            slimus.Move(Direction.Left);
            slimus.UpdatePosition(elapseTime);


            int expectedX = originalX - (int)(elapseTime * Constants.playerSpeed);


            Assert.AreEqual(expectedX, slimus.x);
            Assert.AreEqual(originalY, slimus.y);
        }

        [TestMethod]
        public void TestNewLevel()
        {
            int newX = 55;
            int newY = 60;
            Assert.AreEqual(originalX, slimus.x);
            Assert.AreEqual(originalY, slimus.y);

            slimus.NewLevel(newX, newY);

            Assert.AreEqual(newX, slimus.x);
            Assert.AreEqual(newY, slimus.y);
        }

        [TestMethod]
        public void TestLifeDecrease()
        {
            Assert.AreEqual(slimus.hearts.acquired, 6);

            ToxicSlime toxicSlime = new ToxicSlime(0,0);                        
            slimus.Collide(toxicSlime);

            Assert.AreEqual(slimus.hearts.acquired, 5);
        }
    }
}

