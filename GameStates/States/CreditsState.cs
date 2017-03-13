using System.Drawing;

using FPSTetris.Controllers;

namespace FPSTetris.GameStates
{
    public class CreditsState : IGameState
    {
        #region Constructor

        public CreditsState()
        {
            Controller = new CreditsController(this);
        }

        #endregion

        #region Attributes and Properties

        public string Name
        {
            get { return GameStatesManager.CREDITS_STATE; }
        }

        private CreditsController Controller { get; set; }
        
        #endregion

        #region Public Methods
        
        public void InitializeState()
        {
        }

        public void FinalizeState()
        {
        }

        public void EnterState()
        {
            Controller.InitializeController();
        }

        public void LeaveState()
        {
            Controller.FinalizeController();
        }

        public void Render(Graphics graphics)
        {
            Font fontBig = new Font(FontFamily.GenericSansSerif, 25);
            Font fontSmall = new Font(FontFamily.GenericSansSerif, 15);

            Brush brush = new SolidBrush(Color.Blue);
            Brush highlightBrush = new SolidBrush(Color.Yellow);

            PointF positionOne = new PointF(125.0f, 180.0f);
            PointF positionTwo = new PointF(40.0f, 250.0f);
            PointF positionThree = new PointF(60.0f, 320.0f);

            graphics.DrawString("All Mighty Creator", fontSmall, brush, positionOne);
            graphics.DrawString("Wagner Scholl Lemos", fontBig, highlightBrush, positionTwo);
        }

        public void Update(float deltaTime)
        {
        }

        #endregion

        #region Controller Actions

        public void GoToMainMenu()
        {
            GameWindow.Instance.GameStatesManager.ChangeToState(GameStatesManager.MAIN_MENU_STATE);
        }

        #endregion
    }
}
