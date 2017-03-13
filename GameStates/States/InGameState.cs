using System.Drawing;

using FPSTetris.Controllers;
using FPSTetris.GameObjects;

namespace FPSTetris.GameStates
{
    public class InGameState : IGameState
    {
        #region Constructor

        public InGameState()
        {
            Controller = new InGameController(this);
        }

        #endregion

        #region Attributes and Properties

        private InGameController Controller { get; set; }
        public TetrisLevel Level { get; set; }

        public string Name
        {
            get { return GameStatesManager.IN_GAME_STATE; }
        }

        #endregion

        #region Public Methods

        public void EnterState()
        {
            Level = new TetrisLevel(new Size(15, 30));
            Level.InitializeLevel();
            Controller.InitializeController();
        }

        public void FinalizeState()
        {
            GameWindow.ActiveForm.Width = 500;
            GameWindow.ActiveForm.Height = 658;
        }

        public void InitializeState()
        {
        }

        public void LeaveState()
        {
            if (Level.TetrisStatictics.FirstPersonMode)
                ToggleFPS();
            Controller.FinalizeController();
        }

        public void Render(Graphics graphics)
        {
            Level.Render(graphics);
        }

        public void Update(float deltaTime)
        {
            Level.Update(deltaTime);
        }

        #endregion

        #region Controller Actions

        public void Activate()
        {
            if (Level.TetrisStatictics.GameOver)
                GameWindow.Instance.GameStatesManager.ChangeToState(GameStatesManager.MAIN_MENU_STATE);
            else
                Level.Paused = !Level.Paused;
        }

        public void ToggleFPS()
        {
            GameWindow.Instance.ToggleScreenMode();
            Level.TetrisStatictics.FirstPersonMode = !Level.TetrisStatictics.FirstPersonMode;
        }


        public void RotateClockwise()
        {
            if (Level.Paused) return;
            if (Level.TetrisStatictics.GameOver) return;

            if (Level.TetrisStatictics.FirstPersonMode)
                Level.ActiveTetromino.Rotate(Rotation.CounterClockwise);
            else
                Level.ActiveTetromino.Rotate(Rotation.Clockwise);
        }

        public void RotateCounterClockwise()
        {
            if (Level.Paused) return;
            if (Level.TetrisStatictics.GameOver) return;

            if (Level.TetrisStatictics.FirstPersonMode)
                Level.ActiveTetromino.Rotate(Rotation.Clockwise);
            else
                Level.ActiveTetromino.Rotate(Rotation.CounterClockwise);
        }


        public void MoveUp()
        {
            if (Level.Paused) return;
            if (Level.TetrisStatictics.GameOver) return;
            
            if (!Level.TetrisStatictics.FirstPersonMode)
                return;

            if (Level.TetrisStatictics.Angle == 0) return;
            else if (Level.TetrisStatictics.Angle == 90)
                Level.ActiveTetromino.Left();
            else if (Level.TetrisStatictics.Angle == 180)
                Level.ActiveTetromino.Down();
            else if (Level.TetrisStatictics.Angle == 270)
                Level.ActiveTetromino.Right();
        }

        public void MoveDown()
        {
            if (Level.Paused) return;
            if (Level.TetrisStatictics.GameOver) return;

            if (!Level.TetrisStatictics.FirstPersonMode)
            {
                Level.ActiveTetromino.Down();
                return;
            }

            if (Level.TetrisStatictics.Angle == 0)
                Level.ActiveTetromino.Down();
            else if (Level.TetrisStatictics.Angle == 90)
                Level.ActiveTetromino.Right();
            else if (Level.TetrisStatictics.Angle == 180)
                return;
            else if (Level.TetrisStatictics.Angle == 270)
                Level.ActiveTetromino.Left();
        }

        public void MoveLeft()
        {
            if (Level.Paused) return;
            if (Level.TetrisStatictics.GameOver) return;

            if (!Level.TetrisStatictics.FirstPersonMode)
            {
                Level.ActiveTetromino.Left();
                return;
            }

            if (Level.TetrisStatictics.Angle == 0)
                Level.ActiveTetromino.Left();
            else if (Level.TetrisStatictics.Angle == 90)
                Level.ActiveTetromino.Down();
            else if (Level.TetrisStatictics.Angle == 180)
                Level.ActiveTetromino.Right();
            else if (Level.TetrisStatictics.Angle == 270)
                return;
        }

        public void MoveRight()
        {
            if (Level.Paused) return;
            if (Level.TetrisStatictics.GameOver) return;

            if (!Level.TetrisStatictics.FirstPersonMode)
            {
                Level.ActiveTetromino.Right();
                return;
            }

            if (Level.TetrisStatictics.Angle == 0)
                Level.ActiveTetromino.Right();
            else if (Level.TetrisStatictics.Angle == 90)
                return;
            else if (Level.TetrisStatictics.Angle == 180)
                Level.ActiveTetromino.Left();
            else if (Level.TetrisStatictics.Angle == 270)
                Level.ActiveTetromino.Down();
        }

        #endregion
    }
}
