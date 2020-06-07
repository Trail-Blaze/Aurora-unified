using AuroraLauncher.Providers;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AuroraLauncher
{
    public partial class OldGui : Form
    {
        static Process _clientProcess;
        /// <summary>
        /// 0 = None, 1 = BattlEye, 2 = EasyAntiCheat
        /// </summary>
        static byte _clientAnticheat = 2; // Forced to EAC.

        public OldGui()
        {
            InitializeComponent();

            foreach (EpicGames.Installed.Installation installation
                in EpicGames.LauncherInstalled.InstallationList)
            {
                if (installation.AppName == "Fortnite")
                    textBoxFortnitePath.Text = installation.InstallLocation;
            }
        }

        private void OldGui_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_clientProcess != null)
            {
                if (!_clientProcess.HasExited)
                {
                    MessageBox.Show("You cannot close Aurora Launcher while Fortnite is running!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    e.Cancel = true;
                }
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogBrowse.ShowDialog() == DialogResult.OK)
                textBoxFortnitePath.Text = folderBrowserDialogBrowse.SelectedPath;
        }

        private void linkLabelDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/aurorafn");
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            var text =
                "Having problems using Aurora? Make sure you follow these guidelines:\n" +
                "- Your Fortnite is up-to-date.\n" +
                "- Your username does not contain special characters.\n\n" +
                "If you still seem to have problems, then please join our Discord server: https://discord.gg/aurorafn";

            MessageBox.Show(text);
        }

        private void buttonLaunch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUsername.Text) || textBoxUsername.Text.Length < 3)
            {
                MessageBox.Show("Username cannot be empty or below 3 characters.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textBoxUsername.Text = Regex.Replace(textBoxUsername.Text, "[^a-zA-Z0-9_.]+", string.Empty, RegexOptions.Compiled);

            // Sigh...
            try
            {
                if (!Gui.IsValidPath(textBoxFortnitePath.Text))
                {
                    MessageBox.Show("Invalid Fortnite path.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Invalid Fortnite path.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var clientPath = Path.Combine(textBoxFortnitePath.Text, $"FortniteGame\\Binaries\\Win64\\{Build.ClientExecutable}");
            if (!File.Exists(clientPath))
            {
                MessageBox.Show($"\"{Build.ClientExecutable}\" was not found, please make sure it exists.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var nativePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Build.LauncherNative);
            if (!File.Exists(nativePath))
            {
                MessageBox.Show($"\"{Build.LauncherNative}\" was not found, please make sure it exists.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var formattedArguments = $"-AUTH_LOGIN=\"{textBoxUsername.Text}@unused.com\" -AUTH_PASSWORD=unused -AUTH_TYPE=epic";

            if (_clientAnticheat == 0) // None
                formattedArguments += $" {Build.ClientArguments} -noeac -nobe -fltoken=none";
            else if (_clientAnticheat == 1) // BattlEye
                formattedArguments += $" {Build.ClientArguments} -noeac -fromfl=be -fltoken={Build.BeToken}";
            else if (_clientAnticheat == 2) // EasyAntiCheat
                formattedArguments += $" {Build.ClientArguments} -nobe -fromfl=eac -fltoken={Build.EacToken}";

            _clientProcess = new Process
            {
                StartInfo = new ProcessStartInfo(clientPath, formattedArguments)
                {
                    UseShellExecute = false,

                    RedirectStandardOutput = true,

                    CreateNoWindow = false
                }
            };

            _clientProcess.Start();

#if !NATIVE
            // Allocate the console, for standard output.
            Win32.AllocConsole();

            // Setup an AsyncStreamReader, for standard output.
            var reader = new AsyncStreamReader(_clientProcess.StandardOutput);

            reader.ValueRecieved += delegate (object sender, string value)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(value);
                Console.ForegroundColor = ConsoleColor.Gray;
            };

            reader.Start();
#else
            Helper.InjectDll(_clientProcess.Id, Build.LauncherNative);
#endif // NATIVE
        }
    }
}
