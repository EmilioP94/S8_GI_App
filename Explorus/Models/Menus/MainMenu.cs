using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class MainMenu : GameMenu
    {
        private static MainMenu _instance = null;
        private MainMenu()
        {
            menuOptions = new List<MenuOption>();
            MenuOption playOption = new MenuOption(GetPlayResumeString(), delegate () { SelectPlay(); });
            MenuOption audioOption = new MenuOption("Audio Options", delegate () { SelectAudioMenu(); });
            MenuOption multiplayerOption = new MenuOption(GetMultiplayerString(), delegate () { SelectMultiplayer(); });
            MenuOption exitOption = new MenuOption("Exit Game", delegate () { SelectExit(); });
            menuOptions.Add(playOption);
            menuOptions.Add(audioOption);
            menuOptions.Add(multiplayerOption);
            menuOptions.Add(exitOption);
        }

        private string GetPlayResumeString()
        {
            return GameState.GetInstance().state == GameStates.New ? "Play" : "Resume";
        }
        private string GetMultiplayerString()
        {
            return GameState.GetInstance().multiplayer ? "Multiplayer: Yes" : "Multiplayer: No";
        }

        private void SelectPlay()
        {
            GameState.GetInstance().Play();
            menuOptions.ElementAt(0).label = GetPlayResumeString();
        }

        private void SelectAudioMenu()
        {
            GameState.GetInstance().AudioMenu();
        }

        private void SelectMultiplayer()
        {
            GameState.GetInstance().ToggleMultiplayer();
            menuOptions.ElementAt(2).label = GetMultiplayerString();
        }

        private void SelectExit()
        {
            GameState.GetInstance().ExitGame();
        }

        public static MainMenu GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MainMenu();
            }
            return _instance;
        }
    }
}
