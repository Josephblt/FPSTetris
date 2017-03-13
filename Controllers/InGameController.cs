using System;
using System.Drawing;
using System.Windows.Forms;

using FPSTetris.GameStates;

using FPSTetris.GameObjects;

namespace FPSTetris.Controllers
{
    public class InGameController : AbstractController
    {
        #region Constructor

        public InGameController(InGameState inGameState): base(inGameState as IGameState)
        {
        }

        #endregion

        #region SignedEvents

        protected override void KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            var inGameState = Controllable as InGameState;

            switch (e.KeyData)
            {
                case Keys.Enter:
                    inGameState.Activate();
                    break;
                case Keys.F:
                    inGameState.ToggleFPS();
                    break;
                case Keys.A:
                case Keys.Left:
                    inGameState.MoveLeft();
                    break;
                case Keys.D:
                case Keys.Right:
                    inGameState.MoveRight();
                    break;
                case Keys.S:
                case Keys.Down:
                    inGameState.MoveDown();
                    break;
                case Keys.W:
                case Keys.Up:
                    inGameState.MoveUp();
                    break;
                case Keys.Q:
                case Keys.Delete:
                    inGameState.RotateCounterClockwise();
                    break;
                case Keys.E:
                case Keys.PageDown:
                    inGameState.RotateClockwise();
                    break;
            }
        }

        #endregion
    }
}
