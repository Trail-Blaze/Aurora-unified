using AuroraLauncher.Providers;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AuroraLauncher
{
    partial class Gui : MaterialForm
    {
        MaterialSkinManager _skinManager;

        public Configuration Configuration;

        Settings _settings;

        bool _acknowledgedOutOfDate;

        public Gui()
        {
            InitializeComponent();

            _skinManager = MaterialSkinManager.Instance;
            _skinManager.AddFormToManage(this);

            Configuration = new Configuration();
            Configuration.Open();

            // settings form manages skinmanager
            _settings = new Settings(this);

#if FDEV
            Text = "Aurora/FDev Launcher";

            materialLabelMadeWithLove.Text += " and Slushia";

            materialSingleLineTextFieldPassword.Enabled = false;
            materialSingleLineTextFieldPassword.Text = "Disabled until future update, coming soon!";
#endif

            materialSingleLineTextFieldUsername.Text = Configuration.Username;

            CheckForUpdates();
        }

        bool CheckForUpdates(string source = "")
        {
            bool isUpToDate = Launcher.IsUpToDate;

            if (isUpToDate)
                materialLabelUpdate.Text = $"Aurora Launcher is up-to-date! ({App.Version})";
            else
            {
                if (source != "Launch")
                {
                    if (!_acknowledgedOutOfDate)
                        _acknowledgedOutOfDate = true;
                }

                materialLabelUpdate.Text =
                    $"Aurora Launcher is out-of-date! ({Api.Version})" +
                    "\n\n" +
                    "You must update in order to launch Fortnite!";

                materialRaisedButtonLaunch.Text = "Update";
            }

            return !isUpToDate;
        }

        private void materialRaisedButtonLaunch_Click(object sender, EventArgs e)
        {
            if (CheckForUpdates("Launch"))
            {
                // check if ackd
                if (!_acknowledgedOutOfDate)
                {
                    DialogResult result = MessageBox.Show("Aurora Launcher is out-of-date, you must update in order to launch Fortnite!", string.Empty,
                        MessageBoxButtons.OKCancel);

                    if (result == DialogResult.Cancel)
                    {
                        _acknowledgedOutOfDate = true;
                        return;
                    }
                }

                Process.Start(Build.LauncherUrl);
                return;
            }
        }

        private void materialFlatButtonSettings_Click(object sender, EventArgs e)
        {
            // this seems kinda hacky? :s
            if (_settings.IsDisposed)
                _settings = new Settings(this);

            _settings.ShowDialog();
        }

        private void materialSingleLineTextFieldUsername_TextChanged(object sender, EventArgs e)
        {
            //dumb coding 2020
            Configuration.Username = materialSingleLineTextFieldUsername.Text;
            Configuration.Save();
        }

        void DiscordClick()
        {
            Process.Start("https://discord.gg/fdev");
        }

        private void pictureBoxDiscord_Click(object sender, EventArgs e)
        {
            DiscordClick();
        }

        private void materialFlatButtonDiscord_Click(object sender, EventArgs e)
        {
            DiscordClick();
        }
    }
}
