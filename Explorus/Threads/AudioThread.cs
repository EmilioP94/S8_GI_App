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
        sound01,
        sound02,
        sound03,
        sound04,
        sound05,
        sound06,
        sound07,
        sound08,
        sound09,
        sound10,
        sound11,
        sound12,
        sound13,
        sound14,
        sound15,
        sound16,
        sound17,
        sound18,
        sound19,
        sound20,
    }
    internal class AudioThread
    {
        private static AudioThread _instance;
        private readonly Dictionary<SoundTypes, string> soundsList = new Dictionary<SoundTypes, string> 
        { 
            { SoundTypes.sound01, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound02, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound03, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound04, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound05, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound06, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound07, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound08, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound09, "\\Resources\\Audio\\sound01.wav" },
            { SoundTypes.sound10, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound11, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound12, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound13, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound14, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound15, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound16, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound17, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound18, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound19, "\\Resources\\Audio\\sound10.wav" },
            { SoundTypes.sound20, "\\Resources\\Audio\\sound10.wav" },
        };
        Thread thread;
        private ConcurrentQueue<SoundTypes> soundsQueue = new ConcurrentQueue<SoundTypes>();
        private MediaPlayer musicPlayer;
        private List<MediaPlayer> playingSounds = new List<MediaPlayer>();
        private readonly object playingSoundsListLock = new object();

        private bool _isPaused = false;
        private AudioThread()
        {
            thread = new Thread(new ThreadStart(()=> PlaySounds()));
            thread.Start();
            musicPlayer = new MediaPlayer();
            musicPlayer.Open(new Uri(Application.StartupPath + soundsList[SoundTypes.sound01]));
            musicPlayer.MediaEnded += (object sender, EventArgs e) =>
            {
                musicPlayer.Position = TimeSpan.Zero;
                musicPlayer.Play();
            };
            musicPlayer.Play();
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
            while (true)
            {
                if (!_isPaused)
                {
                    for (int i = 0; i < soundsQueue.Count; i++)
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
                            player.Play();
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

        public void Resume()
        {
            if (_isPaused)
            {
                lock (playingSoundsListLock)
                {
                    foreach (MediaPlayer player in playingSounds)
                    {
                        player.Play();
                    }
                }
            }
            _isPaused = false;
        }
        public void Pause()
        {
            if (!_isPaused)
            {
                lock (playingSoundsListLock)
                {
                    foreach (MediaPlayer player in playingSounds)
                    {
                        player.Stop();
                    }
                }
            }
            _isPaused = true;
        }
        public void StopMusic()
        {
            musicPlayer.Stop();
        }
        public void ResumeMusic()
        {
            musicPlayer.Play();
        }
    }
}
