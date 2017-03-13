using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using FPSTetris.GameStates;
using FPSTetris.WinForms;
using System.Diagnostics;

namespace FPSTetris
{
    public partial class GameWindow : Form
    {
        #region Constructor

        private GameWindow()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            _formState = new FormState();
            GameStatesManager = new GameStatesManager();
        }

        #endregion

        #region Singleton

        private static GameWindow _instance;
        public static GameWindow Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameWindow();
                return _instance;
            }
        }

        #endregion

        #region Attributes and Properties

        public bool GameRunning { get; private set; }
        public GameStatesManager GameStatesManager { get; private set; }
        public bool FullScreen { get; private set; }

        #endregion

        #region Private Fields

        private readonly FormState _formState;

        #endregion

        #region Overriden Methods

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            GameRunning = false;
            GameStatesManager.FinalizeManager();
            base.OnFormClosing(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            GameStatesManager.InitializeManager();
            StartMainLoop();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (GameStatesManager.CurrentGameState != null)
                GameStatesManager.CurrentGameState.Render(e.Graphics);
        }

        #endregion

        #region Private Methods

        private void StartMainLoop()
        {
            GameRunning = true;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            double lastTime = timer.ElapsedMilliseconds;

            while (GameRunning)
            {
                double gameTime = timer.ElapsedMilliseconds;
                float elapsedTime = (float)(gameTime - lastTime);
                lastTime = gameTime;

                Application.DoEvents();
                if (GameStatesManager.CurrentGameState != null)
                    GameStatesManager.CurrentGameState.Update(elapsedTime);
                Refresh();
            }
            timer.Stop();
        }

        #endregion

        #region Public Methods

        public void ToggleScreenMode()
        {
            FullScreen = !FullScreen;

            if (FullScreen)
                _formState.Maximize(this);
            else
                _formState.Restore(this);
        }

        #endregion
    }
}
