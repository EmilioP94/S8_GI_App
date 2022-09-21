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
    internal class AudioThread
    {
        private static AudioThread _instance;
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
        private MediaPlayer musicPlayer;
        private List<MediaPlayer> playingSounds = new List<MediaPlayer>();
        private readonly object playingSoundsListLock = new object();

        private bool _isPaused = false;
        private AudioThread()
        {
            thread = new Thread(new ThreadStart(()=> PlaySounds()));
            thread.Start();
            musicPlayer = new MediaPlayer();
            musicPlayer.Open(new Uri(Application.StartupPath + soundsList[SoundTypes.music]));
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
