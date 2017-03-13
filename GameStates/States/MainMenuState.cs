using System.Drawing;

using FPSTetris.Controllers;

namespace FPSTetris.GameStates
{
    public class MainMenuState : IGameState
    {
        #region Constructor

        public MainMenuState()
        {
            Controller = new MainMenuController(this);
        }

        #endregion

        #region Attributes and Properties

        public string Name
        {
            get { return GameStatesManager.MAIN_MENU_STATE; }
        }

        private MainMenuController Controller { get; set; }

        #endregion

        #region Private Fields

        private bool _optionsToggle;

        #endregion

        #region Public Methods

        public void EnterState()
        {
            _optionsToggle = true;
            Controller.InitializeController();
        }

        public void FinalizeState()
        {
        }

        public void InitializeState()
        {
        }

        public void LeaveState()
        {
            Controller.FinalizeController();
        }

        public void Render(Graphics graphics)
        {
            Font fontBig = new Font(FontFamily.GenericSansSerif, 35);
            Font fontSmall = new Font(FontFamily.GenericSansSerif, 15);
            
            Brush brush = new SolidBrush(Color.Blue);
            Brush highlightBrush = new SolidBrush(Color.Yellow);

            PointF positionOne = new PointF(65.0f, 200.0f);
            PointF positionTwo = new PointF(250.0f, 400.0f);
            PointF positionThree = new PointF(250.0f, 430.0f);
            
            graphics.DrawString("FPS Tetris", fontBig, highlightBrush, positionOne);

            if (_optionsToggle)
            {
                graphics.DrawString("Start Game", fontSmall, highlightBrush, positionTwo);
                graphics.DrawString("Credits", fontSmall, brush, positionThree);
            }
            else
            {
                graphics.DrawString("Start Game", fontSmall, brush, positionTwo);
                graphics.DrawString("Credits", fontSmall, highlightBrush, positionThree);
            }
        }

        public void Update(float deltaTime)
        {
        }

        #endregion

        #region Controller Actions

        public void MenuChange()
        {
            _optionsToggle = !_optionsToggle;
        }

        public void MenuSelect()
        {
            if (_optionsToggle)
                GameWindow.Instance.GameStatesManager.ChangeToState(GameStatesManager.CONTROLS_STATE);
            else
                GameWindow.Instance.GameStatesManager.ChangeToState(GameStatesManager.CREDITS_STATE);
        }

        #endregion
    }
}
