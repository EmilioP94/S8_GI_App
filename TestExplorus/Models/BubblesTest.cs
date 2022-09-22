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
        [TestInitialize]
        public void Init()
        {
            AudioThread.GetInstance();
            bubble = new Bubble(100,
                                       100,
                                       SpriteFactory.GetInstance().GetSprite(Sprites.bigBubble),
                                       SpriteFactory.GetInstance().GetSprite(Sprites.poppedBubble),
                                       Direction.Up
                                       );
            Assert.IsNotNull(bubble);
            Assert.AreEqual(100 + Constants.unit / 2, bubble.x);
            Assert.AreEqual(100 + Constants.unit / 2, bubble.y);
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
    }
}
