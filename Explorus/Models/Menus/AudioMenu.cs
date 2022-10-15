using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal class AudioMenu : GameMenu
    {
        private static AudioMenu _instance;
        public AudioMenu()
        {
            menuOptions = new List<MenuOption>();
            MenuOption musicVolume = new MenuOption("Music Volume", delegate () { ToggleMuteMusic(); }, delegate () { IncrementMusic(); }, delegate () { DecrementMusic(); }, (int)(100 * AudioThread.GetInstance().musicVolume));
            MenuOption soundVolume = new MenuOption("Sound Volume", delegate () { ToggleMuteSounds(); }, delegate () { IncrementSounds(); }, delegate () { DecrementSounds(); }, (int)(100 * AudioThread.GetInstance().soundVolume));
            MenuOption backToMainMenu = new MenuOption("Back to main menu", delegate () { Back(); });
            menuOptions.Add(musicVolume);
            menuOptions.Add(soundVolume);
            menuOptions.Add(backToMainMenu);
        }

        private void Back()
        {
            GameState.GetInstance().MainMenu();
        }

        private void ToggleMuteMusic()
        {
            AudioThread at = AudioThread.GetInstance();
            menuOptions.ElementAt(0).disabled = !at.musicMuted;
            at.QueueEvent(SoundsEvents.MuteMusic);
        }

        private void ToggleMuteSounds()
        {
            AudioThread at = AudioThread.GetInstance();
            menuOptions.ElementAt(1).disabled = !at.soundsMuted;
            at.QueueEvent(SoundsEvents.MuteSounds);
        }

        private void IncrementMusic()
        {
            AudioThread at = AudioThread.GetInstance();
            if(!at.musicMuted)
            {
                at.QueueEvent(SoundsEvents.IncrementMusic);
                MenuOption musicOption = menuOptions.ElementAt(0);
                if(at.musicVolume <= 1)
                {
                    musicOption.value = (int)(100 * (at.musicVolume + 0.01));
                }
            }
        }

        private void DecrementMusic()
        {
            AudioThread at = AudioThread.GetInstance();
            if(!at.musicMuted)
            {
                at.QueueEvent(SoundsEvents.DecrementMusic);
                MenuOption musicOption = menuOptions.ElementAt(0);
                if (at.musicVolume >= 0 )
                {
                    musicOption.value = (int)(100 * (at.musicVolume - 0.01));
                }
            }
        }

        private void IncrementSounds()
        {
            AudioThread at = AudioThread.GetInstance();
            if(!at.soundsMuted)
            {
                at.QueueEvent(SoundsEvents.IncrementSounds);
                MenuOption soundsOption = menuOptions.ElementAt(1);
                if (at.soundVolume <= 1)
                {
                    soundsOption.value = (int)(100 * (at.soundVolume + 0.01));
                }
            }
        }

        private void DecrementSounds()
        {
            AudioThread at = AudioThread.GetInstance();
            if(!at.soundsMuted)
            {
                at.QueueEvent(SoundsEvents.DecrementSounds);
                MenuOption soundsOption = menuOptions.ElementAt(1);
                if (at.soundVolume >= 0)
                {
                    soundsOption.value = (int)(100 * (at.soundVolume - 0.01));
                }
            }
        }

        public static AudioMenu GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AudioMenu();
            }
            return _instance;
        }
    }
}