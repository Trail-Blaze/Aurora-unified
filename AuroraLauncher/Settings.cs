using AuroraLauncher.Providers;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace AuroraLauncher
{
    partial class Settings : MaterialForm
    {
        Gui _gui;

        MaterialSkinManager _skinManager;

        public Settings(Gui gui)
        {
            InitializeComponent();

            _gui = gui;

            _skinManager = MaterialSkinManager.Instance;
            _skinManager.AddFormToManage(this);

            _skinManager.Theme = _gui.Configuration.DarkMode ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT;
            _skinManager.ColorScheme = new ColorScheme(Primary.Pink600, Primary.Pink800, Primary.Pink400, Accent.Pink200, TextShade.WHITE);

            materialSingleLineTextFieldInstallLocation.Text = _gui.Configuration.InstallLocation;

            if (_gui.Configuration.DarkMode)
                materialRadioButtonDark.Checked = true;
            else
                materialRadioButtonLight.Checked = true;

            materialSingleLineTextFieldArguments.Text = _gui.Configuration.Arguments;
        }

        void Save()
        {
            _gui.Configuration.InstallLocation = materialSingleLineTextFieldInstallLocation.Text;

            _gui.Configuration.Arguments = materialSingleLineTextFieldArguments.Text;
            
            _gui.Configuration.Save();
        }

        private void materialRaisedButtonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void materialRaisedButtonReset_Click(object sender, EventArgs e)
        {
            foreach (EpicGames.Installed.Installation installation
                in EpicGames.LauncherInstalled.InstallationList)
            {
                if (installation.AppName == "Fortnite")
                    materialSingleLineTextFieldInstallLocation.Text = installation.InstallLocation;
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        private void materialFlatButtonBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogBrowse.ShowDialog() == DialogResult.OK)
                materialSingleLineTextFieldInstallLocation.Text = folderBrowserDialogBrowse.SelectedPath;
        }

        private void materialRadioButtonDark_CheckedChanged(object sender, EventArgs e)
        {
            _gui.Configuration.DarkMode = materialRadioButtonDark.Checked;

            _skinManager.Theme = _gui.Configuration.DarkMode ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT;
        }
    }
}
