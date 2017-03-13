using System.Drawing;

using FPSTetris.Controllers;
using System;

namespace FPSTetris.GameStates
{
    public class ControlsState : IGameState
    {
        #region Constructor

        public ControlsState()
        {
            Controller = new ControlsController(this);
        }

        #endregion

        #region Attributes and Properties

        public string Name
        {
            get { return GameStatesManager.CONTROLS_STATE; }
        }

        private ControlsController Controller { get; set; }

        #endregion

        #region Public Methods

        public void InitializeState()
        {
        }

        public void FinalizeState()
        {
        }

        public void EnterState()
        {
            Controller.InitializeController();
        }

        public void LeaveState()
        {
            Controller.FinalizeController();
        }

        public void Render(Graphics graphics)
        {
            Font fontBig = new Font(FontFamily.GenericSansSerif, 25);
            Font fontSmall = new Font(FontFamily.GenericSansSerif, 15);

            Brush brush = new SolidBrush(Color.Blue);
            Brush highlightBrush = new SolidBrush(Color.Yellow);

            RectangleF rectangleTitle = new RectangleF(10f, 10f, 390f, 50f);
            graphics.DrawString("Controls:", fontBig, brush, rectangleTitle, StringFormat.GenericTypographic);

            string message = "A, W, S, D or Arrow keys to move.";
            message += Environment.NewLine;
            message += Environment.NewLine;
            message += "Q and E to rotate.";
            message += Environment.NewLine;
            message += Environment.NewLine;
            message += "Enter to pause.";
            message += Environment.NewLine;
            message += Environment.NewLine;
            message += "F to toggle FPS/Normal Mode.";
            message += Environment.NewLine;
            message += Environment.NewLine;
            message += "Press Enter to continue...";

            RectangleF rectangleMessage = new RectangleF(10f, 60f, 390f, 550f);
            graphics.DrawString(message, fontSmall, highlightBrush, rectangleMessage, StringFormat.GenericTypographic);
        }

        public void Update(float deltaTime)
        {
        }

        #endregion

        #region Controller Actions

        public void GoToInGame()
        {
            GameWindow.Instance.GameStatesManager.ChangeToState(GameStatesManager.IN_GAME_STATE);
        }

        #endregion
    }
}
