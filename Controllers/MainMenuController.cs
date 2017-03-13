using System;
using System.Windows.Forms;
using FPSTetris.GameStates;

namespace FPSTetris.Controllers
{
    public class MainMenuController : AbstractController
    {
        #region Constructor

        public MainMenuController(MainMenuState mainMenuState) : base(mainMenuState as IGameState)
        {
        }

        #endregion
        
        #region SignedEvents

        protected override void KeyDown(object sender, KeyEventArgs e)
        {
            var mainMenuState = Controllable as MainMenuState;

            if ((e.KeyValue == 38) || (e.KeyValue == 40))
                mainMenuState.MenuChange();

            if (e.KeyValue == 13)
                mainMenuState.MenuSelect();
        }

        #endregion
    }
}