using System;
using System.Collections.Generic;

namespace FPSTetris.GameStates
{
    public class GameStatesManager
    {
        #region Costructor

        public GameStatesManager()
        {
            GameStates = new List<IGameState>();
        }

        #endregion

        #region Attributes and Properties

        public IGameState CurrentGameState { get; private set; }
        public List<IGameState> GameStates { get; private set; }

        #endregion

        #region Consts

        public const string MAIN_MENU_STATE = "MainMenu";
        public const string CREDITS_STATE = "Credits";
        public const string CONTROLS_STATE = "Controls";
        public const string IN_GAME_STATE = "InGame";

        #endregion

        #region Private Methods

        private void CreateStates()
        {
            GameStates.Add(new MainMenuState());
            GameStates.Add(new CreditsState());
            GameStates.Add(new ControlsState());
            GameStates.Add(new InGameState());

            ChangeToState(MAIN_MENU_STATE);
        }

        private IGameState GetState(string gameState)
        {
            foreach (IGameState state in GameStates)
                if (string.Compare(state.Name, gameState) == 0)
                    return state;
            return null;
        }

        private void InitializeStates()
        {
            foreach (IGameState state in GameStates)
                state.InitializeState();
        }

        private void FinalizeStates()
        {
            foreach (IGameState state in GameStates)
                state.FinalizeState();
        }

        #endregion

        #region Public Methods

        public void ChangeToState(string gameState)
        {
            if (CurrentGameState != null)
                CurrentGameState.LeaveState();

            CurrentGameState = GetState(gameState);

            if (CurrentGameState != null)
                CurrentGameState.EnterState();
        }

        public void InitializeManager()
        {
            CreateStates();
            InitializeStates();
        }

        public void FinalizeManager()
        {
            FinalizeStates();
        }

        #endregion
    }
}
