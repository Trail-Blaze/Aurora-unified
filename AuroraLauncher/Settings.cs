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
            _skinManager.ColorScheme = new ColorScheme(Primary.Teal600, Primary.Teal800, Primary.Teal700, Accent.Teal700, TextShade.WHITE);

            materialSingleLineTextFieldInstallLocation.Text = _gui.Configuration.InstallLocation;

            if (_gui.Configuration.DarkMode)
                materialRadioButtonThemeDark.Checked = true;
            else
                materialRadioButtonThemeLight.Checked = true;

            materialSingleLineTextFieldArguments.Text = _gui.Configuration.Arguments;
        }

        void Save()
        {
            _gui.Configuration.InstallLocation = materialSingleLineTextFieldInstallLocation.Text;

            _gui.Configuration.Arguments = materialSingleLineTextFieldArguments.Text;
            
            _gui.Configuration.Save();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        private void materialRaisedButtonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void materialFlatButtonInstallLocationBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogInstallLocationBrowse.ShowDialog() == DialogResult.OK)
                materialSingleLineTextFieldInstallLocation.Text = folderBrowserDialogInstallLocationBrowse.SelectedPath;
        }

        private void materialRaisedButtonInstallLocationReset_Click(object sender, EventArgs e)
        {
            foreach (EpicGames.Installed.Installation installation
                in EpicGames.LauncherInstalled.InstallationList)
            {
                if (installation.AppName == "Fortnite")
                    materialSingleLineTextFieldInstallLocation.Text = installation.InstallLocation;
            }
        }

        private void materialRadioButtonThemeDark_CheckedChanged(object sender, EventArgs e)
        {
            _gui.Configuration.DarkMode = materialRadioButtonThemeDark.Checked;

            _skinManager.Theme = _gui.Configuration.DarkMode ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT;
        }
    }
}
