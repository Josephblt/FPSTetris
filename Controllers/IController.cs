using FPSTetris.GameStates;

namespace FPSTetris.Controllers
{
    public interface IController
    {
        #region Interface Properties

        IGameState Controllable { get; }

        #endregion

        #region Interface Methods

        void FinalizeController();

        void InitializeController();

        #endregion

    }
}
