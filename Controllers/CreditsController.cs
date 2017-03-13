using System;

using FPSTetris.GameStates;

namespace FPSTetris.Controllers
{
    public class CreditsController : AbstractController
    {
        #region Constructor

        public CreditsController(CreditsState creditsState) : base(creditsState as IGameState)
        {
        }

        #endregion

        #region SignedEvents

        protected override void KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            var creditsState = Controllable as CreditsState;

            if (e.KeyValue == 13)
                creditsState.GoToMainMenu();
        }

        #endregion
    }
}
