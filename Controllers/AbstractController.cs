using System;
using FPSTetris.GameStates;

namespace FPSTetris.Controllers
{
    public abstract class AbstractController : IController
    {
        #region Constructor

        public AbstractController(IGameState controllable)
        {
            Controllable = controllable;
        }

        #endregion

        #region Attributes and Properties

        public IGameState Controllable { get; private set; }

        #endregion

        #region  Public Methods
        public virtual void InitializeController()
        {
            GameWindow.Instance.KeyDown += KeyDown;
        }

        public virtual void FinalizeController()
        {
            GameWindow.Instance.KeyDown -= KeyDown;
        }

        #endregion

        #region Signed Events Methods

        protected abstract void KeyDown(object sender, System.Windows.Forms.KeyEventArgs e);

        #endregion
    }
}
