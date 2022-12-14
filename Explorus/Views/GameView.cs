using Explorus.Controllers;
using Explorus.Models;
using Explorus.Views.Menus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using MainMenu = Explorus.Models.MainMenu;

namespace Explorus.Views
{
    enum WindowEvents
    {
        Minimize,
        Restore,
        Focus,
        Unfocus
    }
    internal class GameView : Models.IObservable<WindowEvents>
    {
        private float _framerate = 0;
        public delegate void HandleInput(object sender, KeyEventArgs e);
        public delegate void HandleResize(object sender, EventArgs e);
        private LabyrinthView labyrinthView;
        public double scaleFactor { get; set; }
        private int originalWidth { get; set; }

        private int originalHeight { get; set; }

        private HeaderView headerView;
        private MenuView mainMenuView;
        private MenuView audioMenuView;
        private GameOverView gameOverView;
        private Point offset { get; set; }
        public GameStates state;
        public int level;
        public bool running;

        private List<Models.IObserver<WindowEvents>> observers = new List<Models.IObserver<WindowEvents>>();

        public float framerate
        {
            get
            {
                return _framerate;
            }
            set
            {
                _framerate = value;
            }
        }

        GameForm oGameForm;
        public GameView(HandleInput keyDown, HandleInput keyUp, ILabyrinth lab, HeaderController headerController)
        {
            offset = new Point(0, 0);
            //add header in calculation ( its 2 units )
            originalHeight = (lab.map.GetLength(0) + 1) * 2 * Constants.unit;
            originalWidth = lab.map.GetLength(1) * 2 * Constants.unit;
            oGameForm = new GameForm();
            oGameForm.MinimumSize = new Size(600, 600);
            mainMenuView = new MenuView(originalWidth, originalHeight, MenuTypes.Main);
            audioMenuView = new MenuView(originalWidth, originalHeight, MenuTypes.Audio);
            gameOverView = new GameOverView(originalWidth, originalHeight);
            DoProcessResize();
            oGameForm.Paint += new PaintEventHandler(this.GameRenderer);
            oGameForm.KeyDown += new KeyEventHandler(keyDown);
            oGameForm.KeyUp += new KeyEventHandler(keyUp);
            oGameForm.Resize += new EventHandler(ProcessResize);
            labyrinthView = new LabyrinthView(lab);
            headerView = new HeaderView(headerController);
            oGameForm.FormClosing += new FormClosingEventHandler(Close);
            oGameForm.Activated += new EventHandler(Focus);
            oGameForm.Deactivate += new EventHandler(Unfocus);
            running = true;
        }



        public void Show() { Application.Run(oGameForm); }

        public void Render()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate
                {
                    oGameForm.Refresh();
                });
        }

        public void Close(object sender, FormClosingEventArgs e)
        {
            if (oGameForm.Visible)
            {
                oGameForm.BeginInvoke((MethodInvoker)delegate
                {
                    oGameForm.Close();
                });
                running = false;
            }
        }

        public void Focus(object sender, EventArgs e)
        {
            NotifyObservers(WindowEvents.Focus);
        }
        public void Unfocus(object sender, EventArgs e)
        {
            NotifyObservers(WindowEvents.Unfocus);
        }

        // ceci devra être mis sur un thread
        private void GameRenderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            g.ScaleTransform((float)this.scaleFactor, (float)this.scaleFactor);
            headerView.Render(sender, e, offset);
            labyrinthView.Render(sender, e, offset);
            oGameForm.Text = string.Format("Explorus     - FPS {0} - {1} - level {2}", framerate.ToString(), Enum.GetName(typeof(GameStates), state), level + 1);

            if (state == GameStates.Pause || state == GameStates.New)
            {
                if(GameState.GetInstance().menu == MenuTypes.Main)
                {
                    mainMenuView.Render(sender, e, offset);
                }
                if(GameState.GetInstance().menu == MenuTypes.Audio)
                {
                    audioMenuView.Render(sender, e, offset);
                }
            }


            if (state == GameStates.ReplayPlaying)
            {
                FontFamily fontFamily = new FontFamily("Arial");
                Font font = new Font(
                   fontFamily,
                   40,
                   FontStyle.Regular,
                   GraphicsUnit.Pixel);
                SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                String text = "Replay - " + (GameRecorder.GetInstance().millSinceLast / 1000 - GameRecorder.GetInstance().totalElapsed / 1000) + "seconds";
                Size sizeOfText = TextRenderer.MeasureText(text, font);
                Rectangle rect = new Rectangle(new Point(24, 120), sizeOfText);
                g.FillRectangle(Brushes.Black, rect);
                e.Graphics.DrawString(text, font, brush, 24, 120);
            }

            if (state == GameStates.Over)
            {
                gameOverView.Render(sender, e, offset);
            }

        }

        private void ProcessResize(object sender, EventArgs e)
        {
            if (oGameForm.WindowState == FormWindowState.Minimized)
            {
                NotifyObservers(WindowEvents.Minimize);
            }
            else
            {
                DoProcessResize();
            }
        }

        private void DoProcessResize()
        {
            Console.WriteLine($"width: {oGameForm.ClientSize.Width}, height: {oGameForm.ClientSize.Height}");

            float clientAspectRatio = (float)oGameForm.ClientSize.Width / (float)oGameForm.ClientSize.Height;
            float labyrinthAspectRatio = (float)originalWidth / (float)originalHeight;
            double newScaleFactor;
            if (clientAspectRatio >= labyrinthAspectRatio)
            {
                newScaleFactor = this.computeScaleFactor(originalHeight, (double)oGameForm.ClientSize.Height);
                this.scaleFactor = newScaleFactor;
                int horizontalPadding = (int)(oGameForm.ClientSize.Width - (double)originalWidth * scaleFactor) / 2;
                offset = new Point(horizontalPadding, 0);
            }
            if (labyrinthAspectRatio >= clientAspectRatio)
            {
                newScaleFactor = this.computeScaleFactor(originalWidth, (double)oGameForm.ClientSize.Width);
                this.scaleFactor = newScaleFactor;
                int verticalPadding = (int)(oGameForm.ClientSize.Height - (double)originalHeight * scaleFactor) / 2;
                offset = new Point(0, verticalPadding);
            }
        }

        private double computeScaleFactor(double originalSideLength, double newSideLength)
        {
            double newScaleFactordelta = newSideLength / originalSideLength;
            return Math.Round(newScaleFactordelta, 2);

        }

        public FormWindowState GetWindowState()
        {
            return oGameForm.WindowState;
        }

        private void NotifyObservers(WindowEvents windowEvent)
        {
            foreach (Models.IObserver<WindowEvents> observer in observers)
            {
                observer.OnNext(windowEvent);
            }
        }

        public IDisposable Subscribe(Models.IObserver<WindowEvents> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<WindowEvents>(observers, observer);
        }
    }
}
