using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly Dictionary<SoundTypes, UnmanagedMemoryStream> soundsList = new Dictionary<SoundTypes, UnmanagedMemoryStream> 
        { 
            { SoundTypes.sound01, Properties.Resources.sound01 },
            { SoundTypes.sound02, Properties.Resources.sound02 },
            { SoundTypes.sound03, Properties.Resources.sound03 },
            { SoundTypes.sound04, Properties.Resources.sound04 },
            { SoundTypes.sound05, Properties.Resources.sound05 },
            { SoundTypes.sound06, Properties.Resources.sound06 },
            { SoundTypes.sound07, Properties.Resources.sound07 },
            { SoundTypes.sound08, Properties.Resources.sound08 },
            { SoundTypes.sound09, Properties.Resources.sound09 },
            { SoundTypes.sound10, Properties.Resources.sound10 },
            { SoundTypes.sound11, Properties.Resources.sound11 },
            { SoundTypes.sound12, Properties.Resources.sound12 },
            { SoundTypes.sound13, Properties.Resources.sound13 },
            { SoundTypes.sound14, Properties.Resources.sound14 },
            { SoundTypes.sound15, Properties.Resources.sound15 },
            { SoundTypes.sound16, Properties.Resources.sound16 },
            { SoundTypes.sound17, Properties.Resources.sound17 },
            { SoundTypes.sound18, Properties.Resources.sound18 },
            { SoundTypes.sound19, Properties.Resources.sound19 },
            { SoundTypes.sound20, Properties.Resources.sound20 },
        };
        Thread thread;
        private ConcurrentQueue<SoundTypes> soundsQueue = new ConcurrentQueue<SoundTypes>();
        private SoundPlayer musicPlayer;
        private List<SoundPlayer> playingSounds = new List<SoundPlayer>();
        private readonly object playingSoundsListLock = new object();

        private bool _isPaused = false;
        private AudioThread()
        {
            thread = new Thread(new ThreadStart(()=> PlaySounds()));
            thread.Start();
            musicPlayer = new SoundPlayer(Properties.Resources.sound10);
            musicPlayer.PlayLooping();
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
                            SoundPlayer player = new SoundPlayer(soundsList[sound]);
                            player.Disposed += (object sender, EventArgs e) =>
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
                foreach(SoundPlayer player in playingSounds)
                {
                    player.Play();
                }
            }
            _isPaused = false;
        }
        public void Pause()
        {
            if (!_isPaused)
            {
                foreach(SoundPlayer player in playingSounds)
                {
                    player.Stop();
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
