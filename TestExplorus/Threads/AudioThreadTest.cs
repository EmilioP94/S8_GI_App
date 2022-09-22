using Explorus.Controllers;
using Explorus.Threads;
using Explorus.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExplorus.Threads
{
    [TestClass]
    public class AudioThreadTest
    {
        [TestMethod]
        public void TestGetAudioThread()
        {
            AudioThread audioThread = AudioThread.GetInstance();
            Assert.IsTrue(audioThread != null);

        }

        [TestMethod]
        public void TestMusicVolumeIncrement()
        {
            AudioThread audioThread = AudioThread.GetInstance();
            Assert.IsTrue(audioThread != null);

            double volume = audioThread.musicVolume;
            audioThread.QueueEvent(SoundsEvents.IncrementMusic);

            Thread.Sleep(100);
            Assert.AreEqual(audioThread.musicVolume, volume + 0.01);
        }
        [TestMethod]
        public void TestMusicVolumeDecrement()
        {
            AudioThread audioThread = AudioThread.GetInstance();
            Assert.IsTrue(audioThread != null);

            double volume = audioThread.musicVolume;
            audioThread.QueueEvent(SoundsEvents.DecrementMusic);

            Thread.Sleep(100);
            Assert.AreEqual(audioThread.musicVolume, volume - 0.01);
        }

        [TestMethod]
        public void TestSoundVolumeIncrement()
        {
            AudioThread audioThread = AudioThread.GetInstance();
            Assert.IsTrue(audioThread != null);

            double volume = audioThread.soundVolume;
            audioThread.QueueEvent(SoundsEvents.IncrementSounds);

            Thread.Sleep(100);
            Assert.AreEqual(audioThread.soundVolume, volume + 0.01);
        }
        [TestMethod]
        public void TestSoundVolumeDecrement()
        {
            AudioThread audioThread = AudioThread.GetInstance();
            Assert.IsTrue(audioThread != null);

            double volume = audioThread.soundVolume;
            audioThread.QueueEvent(SoundsEvents.DecrementSounds);

            Thread.Sleep(100);
            Assert.AreEqual(audioThread.soundVolume, volume - 0.01);
        }

        [TestMethod]
        public void TestMusicPause()
        {
            AudioThread audioThread = AudioThread.GetInstance();
            Assert.IsTrue(audioThread != null);

            audioThread.StopMusic();
            audioThread.ResumeMusic();
        }
    }
}
