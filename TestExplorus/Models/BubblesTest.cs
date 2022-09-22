using Explorus;
using Explorus.Controllers;
using Explorus.Models;
using Explorus.Threads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace TestExplorus.Models
{
    [TestClass]
    public class BubblesTest
    {
        Bubble bubble;
        int originalX = 100;
        int originalY = 100;

        int elapseTime = 16;

        [TestInitialize]
        public void Init()
        {
            AudioThread.GetInstance();
            bubble = new Bubble(originalX,
                                       originalY,
                                       SpriteFactory.GetInstance().GetSprite(Sprites.bigBubble),
                                       SpriteFactory.GetInstance().GetSprite(Sprites.poppedBubble),
                                       Direction.Up
                                       );
            Assert.IsNotNull(bubble);
            Assert.AreEqual(originalX + Constants.unit / 2, bubble.x);
            Assert.AreEqual(originalY + Constants.unit / 2, bubble.y);
            Assert.AreEqual(new Rectangle(bubble.x, bubble.y, Constants.unit, Constants.unit), bubble.hitbox);
        }

        [TestMethod]
        public void TestCollide()
        {
            Assert.IsFalse(bubble.Collide(new LabyrinthComponent(0, 0, SpriteFactory.GetInstance().GetSprite(Sprites.wall))));
        }

        [TestMethod] 
        public void PopTest()
        {
            Assert.IsFalse(bubble.DeleteCheck());
            bubble.PopBubble();
            Assert.AreEqual(new Rectangle(), bubble.hitbox);
            Assert.IsTrue(bubble.DeleteCheck());
        }

        [TestMethod]
        public void TestMoveUp()
        {
            int xPos = bubble.x;
            int yPos = bubble.y;
            bubble.Move(elapseTime);

            int expectedY = yPos - (int)(elapseTime * Constants.playerSpeed * 2);

            Assert.AreEqual(xPos, bubble.x);
            Assert.AreEqual(expectedY, bubble.y);
        }

        [TestMethod]
        public void TestMoveDown()
        {
            bubble = new Bubble(originalX,
                                       originalY,
                                       SpriteFactory.GetInstance().GetSprite(Sprites.bigBubble),
                                       SpriteFactory.GetInstance().GetSprite(Sprites.poppedBubble),
                                       Direction.Down
                                       );
            int xPos = bubble.x;
            int yPos = bubble.y;
            bubble.Move(elapseTime);

            int expectedY = yPos + (int)(elapseTime * Constants.playerSpeed * 2);

            Assert.AreEqual(xPos, bubble.x);
            Assert.AreEqual(expectedY, bubble.y);
        }

        [TestMethod]
        public void TestMoveRight()
        {
            bubble = new Bubble(originalX,
                                       originalY,
                                       SpriteFactory.GetInstance().GetSprite(Sprites.bigBubble),
                                       SpriteFactory.GetInstance().GetSprite(Sprites.poppedBubble),
                                       Direction.Right
                                       );
            int xPos = bubble.x;
            int yPos = bubble.y;
            bubble.Move(elapseTime);

            int expectedX = xPos + (int)(elapseTime * Constants.playerSpeed * 2);

            Assert.AreEqual(expectedX, bubble.x);
            Assert.AreEqual(yPos, bubble.y);
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            bubble = new Bubble(originalX,
                                       originalY,
                                       SpriteFactory.GetInstance().GetSprite(Sprites.bigBubble),
                                       SpriteFactory.GetInstance().GetSprite(Sprites.poppedBubble),
                                       Direction.Left
                                       );
            int xPos = bubble.x;
            int yPos = bubble.y;
            bubble.Move(elapseTime);

            int expectedX = xPos - (int)(elapseTime * Constants.playerSpeed * 2);

            Assert.AreEqual(expectedX, bubble.x);
            Assert.AreEqual(yPos, bubble.y);
        }
    }
}
