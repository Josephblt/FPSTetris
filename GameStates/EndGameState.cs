using System;
using System.Drawing;

using Tetris.Controllers;

namespace Tetris.GameStates
{
    public class EndGameState : IGameState
    {
        #region Constructor

        public EndGameState()
        {
        }

        #endregion

        #region Attributes and Properties

        public string Name
        {
            get { return GameStatesManager.END_GAME_STATE; }
        }

        private EndGameController controller;
        private EndGameController Controller
        {
            get
            {
                if (this.controller == null)
                    this.controller = new EndGameController();
                return this.controller;
            }
        }

        #endregion

        #region Public Methods

        public void EnterState()
        {
            this.Controller.InitializeController();
        }

        public void FinalizeState()
        {
        }

        public void InitializeState()
        {
        }

        public void LeaveState()
        {
            this.Controller.FinalizeController();
        }

        public void Render(Graphics graphics)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 25);
            Brush brush = new SolidBrush(Color.Yellow);
            PointF position = new PointF(265.0f, 180.0f);            
            graphics.DrawString(GameWindow.GetGameWindow().GameWinner, font, brush, position);            
        }

        public void Update(float deltaTime)
        {
        }

        #endregion
    }
}
