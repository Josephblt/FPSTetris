using System.Drawing;
using System.Windows.Forms;

namespace FPSTetris.WinForms
{
    public class FormState
    {
        #region Private Fields

        private FormBorderStyle _borderStyle;
        private Rectangle _bounds;
        private bool _isMaximized = false;
        private bool _topMost;
        private FormWindowState _winState;

        #endregion

        #region Public Methods

        public void Maximize(Form targetForm)
        {
            if (!_isMaximized)
            {
                _isMaximized = true;
                Save(targetForm);
                targetForm.WindowState = FormWindowState.Maximized;
                targetForm.FormBorderStyle = FormBorderStyle.None;
                targetForm.TopMost = true;
                WinApi.SetWinFullScreen(targetForm.Handle);
            }
        }

        public void Restore(Form targetForm)
        {
            targetForm.WindowState = _winState;
            targetForm.FormBorderStyle = _borderStyle;
            targetForm.TopMost = _topMost;
            targetForm.Bounds = _bounds;
            _isMaximized = false;
        }

        public void Save(Form targetForm)
        {
            _winState = targetForm.WindowState;
            _borderStyle = targetForm.FormBorderStyle;
            _topMost = targetForm.TopMost;
            _bounds = targetForm.Bounds;
        }

        #endregion
    }
}