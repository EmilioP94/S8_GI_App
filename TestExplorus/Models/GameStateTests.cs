using Explorus.Models;
using Explorus.Threads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace TestExplorus.Models
{
    [TestClass]
    public class GameStateTests
    {

        [TestInitialize]
        public void GetInstanceTest()
        {
            GameState gameState = GameState.GetInstance();
            Assert.IsNotNull(gameState);
            Assert.AreEqual(0, GameState.GetInstance().level);
            Assert.AreEqual(GameState.GetInstance().state, GameStates.New);
        }

        [TestCleanup]
        public void ResetTest()
        {
            GameState.GetInstance().Reset();
        }

        [TestMethod]
        public void PauseTestManual()
        {
            GameState.GetInstance().Play();
            Assert.AreEqual(GameStates.Play, GameState.GetInstance().state);
            GameState.GetInstance().Pause(true);
            Assert.AreEqual(GameStates.Pause, GameState.GetInstance().state);
            Assert.IsTrue(GameState.GetInstance().manual);
        }

        [TestMethod]
        public void PauseTestAuto()
        {
            GameState.GetInstance().Play();
            Assert.AreEqual(GameStates.Play, GameState.GetInstance().state);
            GameState.GetInstance().Pause(false);
            Assert.AreEqual(GameStates.Pause, GameState.GetInstance().state);
            Assert.IsFalse(GameState.GetInstance().manual);
        }

        [TestMethod]
        public void ResumeTest()
        {
            GameState.GetInstance().Play();
            Assert.AreEqual(GameStates.Play, GameState.GetInstance().state);
            GameState.GetInstance().Pause(false);
            Assert.AreEqual(GameStates.Pause, GameState.GetInstance().state);
            GameState.GetInstance().Resume();
            Assert.AreEqual(GameStates.Resume, GameState.GetInstance().state);
            Task.Delay(new TimeSpan(0, 0, 3)).ContinueWith(o => { Assert.AreEqual(GameStates.Play, GameState.GetInstance().state); });
        }

        [TestMethod]
        public void StopTest()
        {
            GameState.GetInstance().Play();
            Assert.AreEqual(GameStates.Play, GameState.GetInstance().state);
            GameState.GetInstance().Stop();
            Assert.AreEqual(GameStates.Stop, GameState.GetInstance().state);
        }

        [TestMethod]
        public void GameOverTest()
        {
            Assert.AreEqual(GameStates.New, GameState.GetInstance().state);
            GameState.GetInstance().GameOver();
            Assert.AreEqual(GameStates.Over, GameState.GetInstance().state);
        }

        [TestMethod]
        public void LevelTest()
        {
            GameState.GetInstance().Play();
            Assert.AreEqual(GameStates.Play, GameState.GetInstance().state);
            GameState.GetInstance().NextLevel();
            Assert.AreEqual(1, GameState.GetInstance().level);
            GameState.GetInstance().Reset();
            Assert.AreEqual(0, GameState.GetInstance().level);
        }
    }
}
