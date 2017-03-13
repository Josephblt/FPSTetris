using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FPSTetris.WinForms
{
    public class WinApi
    {
        #region Constants

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private const int SWP_SHOWWINDOW = 64;

        #endregion

        #region External Methods

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        private static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        private static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int X, int Y, int width, int height, uint flags);

        #endregion

        #region Static Methods

        public static void SetWinFullScreen(IntPtr hwnd)
        {
            var x = GetSystemMetrics(SM_CXSCREEN);
            var y = GetSystemMetrics(SM_CYSCREEN);
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, x, y, SWP_SHOWWINDOW);
        }

        #endregion
    }
}