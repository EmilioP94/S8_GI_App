using Explorus.Views;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Explorus.Threads
{
    enum SoundTypes
    {
        music,
        sound01,
        sound02,
        ennemyCollision,
        sound04,
        sound05,
        sound06,
        bubbleShoot,
        sound08,
        sound09,
        sound10,
        sound11,
        gemCollection,
        slimusMovement,
        allGems,
        bubbleExplode,
        wallCollision,
        sound17,
        sound18,
        gameOver,
        miniSlimeCollection,
        None,
    }
    enum SoundsEvents
    {
        StopMusic,
        StopSounds,
        IncrementSounds,
        DecrementSounds,
        IncrementMusic,
        DecrementMusic,
        ResumeMusic,
        ResumeSounds,
    }
    internal class AudioThread
    {
        private static AudioThread _instance;
        private bool _isStopping = false;
        private readonly Dictionary<SoundTypes, string> soundsList = new Dictionary<SoundTypes, string> 
        {
            { SoundTypes.music, "\\Resources\\Audio\\music.wav" },
            { SoundTypes.sound01, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound02, "\\Resources\\Audio\\sound02.wav" },
            { SoundTypes.ennemyCollision, "\\Resources\\Audio\\sound03.wav" },
            { SoundTypes.sound04, "\\Resources\\Audio\\sound04.wav" },
            { SoundTypes.sound05, "\\Resources\\Audio\\sound05.wav" },
            { SoundTypes.sound06, "\\Resources\\Audio\\sound06.wav" },
            { SoundTypes.bubbleShoot, "\\Resources\\Audio\\sound07.wav" },
            { SoundTypes.sound08, "\\Resources\\Audio\\sound08.wav" },
            { SoundTypes.sound09, "\\Resources\\Audio\\sound09.wav" },
            { SoundTypes.sound10, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound11, "\\Resources\\Audio\\sound11.wav" },
            { SoundTypes.gemCollection, "\\Resources\\Audio\\sound12.wav" },
            { SoundTypes.slimusMovement, "\\Resources\\Audio\\sound13.wav" },
            { SoundTypes.allGems, "\\Resources\\Audio\\sound14.wav" },
            { SoundTypes.bubbleExplode, "\\Resources\\Audio\\sound15.wav" },
            { SoundTypes.wallCollision, "\\Resources\\Audio\\sound16.wav" },
            { SoundTypes.sound17, "\\Resources\\Audio\\sound17.wav" },
            { SoundTypes.sound18, "\\Resources\\Audio\\sound18.wav" },
            { SoundTypes.gameOver, "\\Resources\\Audio\\sound19.wav" },
            { SoundTypes.miniSlimeCollection, "\\Resources\\Audio\\sound20.wav" },
            { SoundTypes.None, "" },
        };
        Thread thread;
        private ConcurrentQueue<SoundTypes> soundsQueue = new ConcurrentQueue<SoundTypes>();
        private ConcurrentQueue<SoundsEvents> soundsEventsQueue = new ConcurrentQueue<SoundsEvents>();
        private MediaPlayer musicPlayer;
        private List<MediaPlayer> playingSounds = new List<MediaPlayer>();
        private readonly object playingSoundsListLock = new object();

        public double musicVolume = 0.5;
        public double soundVolume = 0.5;

        private bool _isPaused = false;
        private AudioThread()
        {
            thread = new Thread(new ThreadStart(()=> PlaySounds()));
            thread.Start();

        }

        public static AudioThread GetInstance()
        {
            if(_instance == null)
            {
                _instance = new AudioThread();
            }
            return _instance;
        }

        private void PlaySounds()
        {
            musicPlayer = new MediaPlayer();
            musicPlayer.Volume = musicVolume;
            musicPlayer.Open(new Uri(Application.StartupPath + soundsList[SoundTypes.music]));
            musicPlayer.MediaEnded += (object sender, EventArgs e) =>
            {
                musicPlayer.Position = TimeSpan.Zero;
                musicPlayer.Play();
            };
            musicPlayer.Play();
            while (!_isStopping)
            {
                int count = soundsQueue.Count;
                if (!_isPaused)
                {
                    //Create new sounds
                    for (int i = 0; i < count; i++)
                    {
                        SoundTypes sound;
                        if (soundsQueue.TryDequeue(out sound))
                        {
                            MediaPlayer player = new MediaPlayer();
                            player.Open(new Uri(Application.StartupPath + soundsList[sound]));
                            player.MediaEnded += (object sender, EventArgs e) =>
                            {
                                lock (playingSoundsListLock)
                                {
                                    playingSounds.Remove(player);
                                }
                            };
                            player.Volume = soundVolume;
                            player.Play();
                        }
                    }
                }
                count = soundsEventsQueue.Count;
                for (int i = 0; i < count; i++)
                {
                    SoundsEvents newEvent;
                    if (soundsEventsQueue.TryDequeue(out newEvent))
                    {
                        List<MediaPlayer> sounds;
                        switch (newEvent)
                        {
                            case SoundsEvents.ResumeSounds:
                                lock (playingSounds)
                                {
                                    sounds = playingSounds.ToList();
                                }
                                foreach (MediaPlayer sound in sounds)
                                {
                                    sound.Play();
                                }
                                break;
                            case SoundsEvents.StopMusic:
                                musicPlayer.Stop();
                                break;
                            case SoundsEvents.ResumeMusic:
                                musicPlayer.Play();
                                break;
                            case SoundsEvents.StopSounds:
                                lock (playingSounds)
                                {
                                    sounds = playingSounds.ToList();
                                }
                                foreach (MediaPlayer sound in sounds)
                                {
                                    sound.Stop();
                                }
                                break;
                            case SoundsEvents.IncrementSounds:
                                IncrementSoundVolume();
                                break;
                            case SoundsEvents.DecrementSounds:
                                DecrementSoundVolume();
                                break;
                            case SoundsEvents.IncrementMusic:
                                IncrementMusicVolume();
                                break;
                            case SoundsEvents.DecrementMusic:
                                DecrementMusicVolume();
                                break;
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

        public void QueueSound(SoundTypes sound)
        {
            soundsQueue.Enqueue(sound);
        }

        public void QueueEvent(SoundsEvents e) {
            soundsEventsQueue.Enqueue(e);
        }

        public void Resume()
        {
            if (_isPaused)
            {
                soundsEventsQueue.Enqueue(SoundsEvents.ResumeSounds);
            }

            _isPaused = false;
        }
        public void Pause()
        {
            if (!_isPaused)
            {
                soundsEventsQueue.Enqueue(SoundsEvents.StopSounds);
            }
            _isPaused = true;
        }
        public void StopMusic()
        {
            soundsEventsQueue.Enqueue(SoundsEvents.StopMusic);
        }
        public void ResumeMusic()
        {
            soundsEventsQueue.Enqueue(SoundsEvents.ResumeMusic);
        }

        public void Stop()
        {
            _isStopping = true;
            StopMusic();
        }

        private void IncrementMusicVolume()
        {
            if (musicVolume <= 1)
            {
                SetMusicVolume(musicVolume + 0.01);
            }
        }
        private void IncrementSoundVolume()
        {
            if (soundVolume <= 1)
            {
                SetSoundsVolume(soundVolume + 0.01);
            }
        }
        
        private void DecrementMusicVolume()
        {
            if (musicVolume >= 0)
            {
                SetMusicVolume(musicVolume - 0.01);
            }
        }
        private void DecrementSoundVolume()
        {
            if (soundVolume >= 0)
            {
                SetSoundsVolume(soundVolume - 0.01);
            }
        }

        private void SetMusicVolume(double newVolume)
        {
            musicPlayer.Volume = newVolume;
            musicVolume = newVolume;
        }
        private void SetSoundsVolume(double newVolume)
        {
            List<MediaPlayer> sounds;
            lock (playingSounds)
            {
                sounds = playingSounds.ToList();
            }
            foreach(MediaPlayer sound in sounds)
            {
                sound.Volume = newVolume;
            }
            soundVolume = newVolume;
        }
    }
}
