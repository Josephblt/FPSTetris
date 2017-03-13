using System;
using System.Windows.Forms;

namespace FPSTetris
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(GameWindow.Instance);
        }
    }
}
