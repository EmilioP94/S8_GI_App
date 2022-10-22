using Explorus.Controllers;
using Explorus.Models;
using Explorus.Threads;
using Explorus.Views.Menus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainMenu = Explorus.Models.MainMenu;

namespace TestExplorus.Controllers
{
    [TestClass]
    public class MenuControllerTest
    {
        // Note à l'évaluateur: la mise en pause/resume du jeu est déjà testée depuis
        // l'APP 2 dans le fichier TestExplorus/Models/GameState.cs
        // la logique des fonctions n'a pas été modifiée
        // on teste toutefois le bouton play dans cette classe de test
        readonly MenuController menuController = new MenuController();

        [TestInitialize]
        public void Init()
        {
            MainMenu.GetInstance().Reset();
            AudioMenu.GetInstance().Reset();
            GameState.GetInstance().Reset();
        }

        [TestMethod]
        public void NavigateDown()
        {
            Assert.AreEqual(0, MainMenu.GetInstance().selectedMenuOptionIndex);
            for (int i = 0; i < MainMenu.GetInstance().menuOptions.Count - 1; i++)
            {
                menuController.ProcessInput(null, GenerateKeyEvent(Keys.Down), true, MainMenu.GetInstance());
                Assert.AreEqual(i + 1, MainMenu.GetInstance().selectedMenuOptionIndex);
            }
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Down), true, MainMenu.GetInstance());
            Assert.AreEqual(0, MainMenu.GetInstance().selectedMenuOptionIndex);
        }

        [TestMethod]
        public void NavigateUp()
        {
            Assert.AreEqual(0, MainMenu.GetInstance().selectedMenuOptionIndex);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Up), true, MainMenu.GetInstance());
            Assert.AreEqual(MainMenu.GetInstance().menuOptions.Count - 1, MainMenu.GetInstance().selectedMenuOptionIndex);
            for (int i = MainMenu.GetInstance().menuOptions.Count - 1; i > 0; i--)
            {
                menuController.ProcessInput(null, GenerateKeyEvent(Keys.Up), true, MainMenu.GetInstance());
                Assert.AreEqual(i - 1, MainMenu.GetInstance().selectedMenuOptionIndex);
            }
            Assert.AreEqual(0, MainMenu.GetInstance().selectedMenuOptionIndex);
        }

        [TestMethod]
        public void TestSwitchMultiplayer()
        {
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Down), true, MainMenu.GetInstance());
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Down), true, MainMenu.GetInstance());
            Assert.AreEqual(2, MainMenu.GetInstance().selectedMenuOptionIndex);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Enter), true, MainMenu.GetInstance());
            Assert.AreEqual(true, GameState.GetInstance().multiplayer);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Enter), true, MainMenu.GetInstance());
            Assert.AreEqual(false, GameState.GetInstance().multiplayer);
        }

        [TestMethod]
        public void NavigateToAndFromAudioMenu()
        {
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Down), true, MainMenu.GetInstance());
            Assert.AreEqual(1, MainMenu.GetInstance().selectedMenuOptionIndex);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Enter), true, MainMenu.GetInstance());
            Assert.AreEqual(GameState.GetInstance().menu, MenuTypes.Audio);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Up), true, AudioMenu.GetInstance());
            Assert.AreEqual(2, AudioMenu.GetInstance().selectedMenuOptionIndex);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Enter), true, AudioMenu.GetInstance());
            Assert.AreEqual(GameState.GetInstance().menu, MenuTypes.Main);
        }

        [TestMethod]
        public void TestAudioControls()
        {
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Down), true, MainMenu.GetInstance());
            Assert.AreEqual(1, MainMenu.GetInstance().selectedMenuOptionIndex);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Enter), true, MainMenu.GetInstance());
            Assert.AreEqual(GameState.GetInstance().menu, MenuTypes.Audio);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Left), true, AudioMenu.GetInstance());
            Thread.Sleep(100);
            Assert.AreEqual(100 * AudioThread.GetInstance().musicVolume, AudioMenu.GetInstance().menuOptions[0].value);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.M), true, AudioMenu.GetInstance());
            Thread.Sleep(100);
            Assert.AreEqual(AudioThread.GetInstance().musicMuted, true);
        }

        [TestMethod]
        public void TestStartGame()
        {
            Assert.AreEqual(GameStates.New, GameState.GetInstance().state);
            menuController.ProcessInput(null, GenerateKeyEvent(Keys.Enter), true, MainMenu.GetInstance());
            Assert.AreEqual(GameStates.Play, GameState.GetInstance().state);
        }
        private KeyEventArgs GenerateKeyEvent(Keys key)
        {
            KeyEventArgs keyEventArgs = new KeyEventArgs(key);
            return keyEventArgs;
        }
    }
}
