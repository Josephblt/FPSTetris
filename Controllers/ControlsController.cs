using System;

using FPSTetris.GameStates;

namespace FPSTetris.Controllers
{
    public class ControlsController : AbstractController
    {
        #region Constructor

        public ControlsController(ControlsState controlsState) : base(controlsState as IGameState)
        {
        }

        #endregion

        #region SignedEvents

        protected override void KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            var controlsState = Controllable as ControlsState;

            if (e.KeyValue == 13)
                controlsState.GoToInGame();
        }

        #endregion
    }
}
