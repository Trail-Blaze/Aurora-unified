using System;
using System.Windows.Forms;

namespace AuroraLauncher
{
    class Program
    {
        #region Field Region

        static Gui _gui;

        #endregion

        #region Method Region

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(_gui = new Gui());
        }

        #endregion
    }
}
