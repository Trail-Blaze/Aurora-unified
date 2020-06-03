using AuroraLauncher.Providers;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AuroraLauncher
{
    partial class Gui : MaterialForm
    {
        MaterialSkinManager _skinManager;

        public Configuration Configuration;

        Settings _settings;

        bool _showedOutOfDate;

        Process _clientProcess;
        /// <summary>
        /// 0 = None, 1 = BattlEye, 2 = EasyAntiCheat
        /// </summary>
        int _clientAnticheat = 2; // Forced to EAC, until added into Settings.

        public Gui()
        {
            InitializeComponent();

            _skinManager = MaterialSkinManager.Instance;
            _skinManager.AddFormToManage(this);

            Configuration = new Configuration();
            Configuration.Open();

            // Settings form manages SkinManager
            _settings = new Settings(this);

#if ONLINE
            Text += $" ({new Uri(Build.LauncherUrl).Host})";
#endif

            materialSingleLineTextFieldEmail.Text = Configuration.Email;
            materialSingleLineTextFieldPassword.Text = Configuration.Password;

            timerHeartbeat.Enabled = true;
            timerHeartbeat_Tick(null, null);

#if false
            //materialLabelOnline.Visible = true;

            CheckForUpdates();

            materialLabelUpdate.Visible = true;
#endif
        }

        bool CheckForUpdates(string source = "")
        {
            var isUpToDate = Launcher.IsUpToDate;

            if (isUpToDate)
                materialLabelUpdate.Text = $"Aurora Launcher is up-to-date! ({App.Version})";
            else
            {
                if (source != "Launch")
                {
                    if (!_showedOutOfDate)
                        _showedOutOfDate = true;
                }

                materialLabelUpdate.Text =
                    $"Aurora Launcher is out-of-date! ({Api.Version})" +
                    "\n\n" +
                    "You must update in order to launch Fortnite!";

                materialRaisedButtonLaunch.Text = "Update";
            }

            return !isUpToDate;
        }

        bool IsValidPath(string path)
        {
            var drive = new Regex(@"^[a-zA-Z]:\\$");
            if (!drive.IsMatch(path.Substring(0, 3)))
                return false;

            var invalidCharacters = new string(Path.GetInvalidPathChars());
            invalidCharacters += @":/?*" + "\"";

            var badCharacter = new Regex("[" + Regex.Escape(invalidCharacters) + "]");
            if (badCharacter.IsMatch(path.Substring(3, path.Length - 3)))
                return false;

            return true;
        }

        bool IsValidEmail(string address)
        {
            try
            {
                new MailAddress(address);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void materialRaisedButtonLaunch_Click(object sender, EventArgs e)
        {
#if false
            if (CheckForUpdates("Launch"))
            {
                // Check if we previously showed that we were out-of-date.
                if (!_showedOutOfDate)
                {
                    DialogResult result = MessageBox.Show("Aurora Launcher is out-of-date, you must update in order to launch Fortnite!", string.Empty,
                        MessageBoxButtons.OKCancel);

                    if (result == DialogResult.Cancel)
                    {
                        _showedOutOfDate = true;
                        return;
                    }
                }

                Process.Start(Build.LauncherUrl);
                return;
            }
#endif

            if (string.IsNullOrEmpty(materialSingleLineTextFieldEmail.Text) || materialSingleLineTextFieldEmail.Text.Length < 3)
            {
                MessageBox.Show("Email cannot be empty or below 3 characters.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(materialSingleLineTextFieldEmail.Text))
            {
                MessageBox.Show("Invalid Email, are you sure it's correct?", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(materialSingleLineTextFieldPassword.Text) || materialSingleLineTextFieldPassword.Text.Length < 3)
            {
                MessageBox.Show("Password cannot be empty or below 3 characters.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // TODO: Fix this?
            if (materialSingleLineTextFieldPassword.Text.Contains(" "))
            {
                MessageBox.Show("Invalid Password, are you sure it's correct?", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Sigh...
            try
            {
                if (!IsValidPath(Configuration.InstallLocation))
                {
                    MessageBox.Show("Invalid installation path.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Invalid installation path.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var clientPath = Path.Combine(Configuration.InstallLocation, $"FortniteGame\\Binaries\\Win64\\{Build.ClientExecutable}");

            if (!File.Exists(clientPath))
            {
                var text = $"\"{clientPath}\" was not found, please make sure it exists." + "\n\n" +
                    "Did you set the Install Location correctly?" + "\n\n" +
                    "NOTE: The Install Location must be set to a folder that contains 2 folders named \"Engine\" and \"FortniteGame\".";

                MessageBox.Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var nativePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Build.ClientNative);

            if (!File.Exists(nativePath))
            {
                var text = $"\"{nativePath}\" was not found, please make sure it exists." + "\n\n" +
                    "Did you extract all files from the ZIP when you downloaded the Launcher?" + "\n\n" +
                    $"NOTE: \"{Build.ClientNative}\" must be in the same directory as the Launcher executable.";

                MessageBox.Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var formattedArguments = $"-AUTH_LOGIN={Configuration.Email} -AUTH_PASSWORD={Configuration.Password} -AUTH_TYPE=epic ";

            formattedArguments += Configuration.Arguments;

            if (_clientAnticheat == 0) // None
                formattedArguments += $" {Build.ClientArguments} -noeac -nobe -fltoken=none";
            else if (_clientAnticheat == 1) // BattlEye
                formattedArguments += $" {Build.ClientArguments} -noeac -fromfl=be -fltoken={Build.BEToken}";
            else if (_clientAnticheat == 2) // EasyAntiCheat
                formattedArguments += $" {Build.ClientArguments} -nobe -fromfl=eac -fltoken={Build.EACToken}";

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

            // Inject Native

            var clientHandle = Win32.OpenProcess(Win32.PROCESS_CREATE_THREAD | Win32.PROCESS_QUERY_INFORMATION |
                Win32.PROCESS_VM_OPERATION | Win32.PROCESS_VM_WRITE | Win32.PROCESS_VM_READ, false, _clientProcess.Id);

            var loadLibrary = Win32.GetProcAddress(Win32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            var size = (uint)((nativePath.Length + 1) * Marshal.SizeOf(typeof(char)));
            var address = Win32.VirtualAllocEx(clientHandle, IntPtr.Zero,
                size, Win32.MEM_COMMIT | Win32.MEM_RESERVE, Win32.PAGE_READWRITE);

            Win32.WriteProcessMemory(clientHandle, address,
                Encoding.Default.GetBytes(nativePath), size, out UIntPtr bytesWritten);

            Win32.CreateRemoteThread(clientHandle, IntPtr.Zero, 0, loadLibrary, address, 0, IntPtr.Zero);
        }

        private void materialFlatButtonSettings_Click(object sender, EventArgs e)
        {
            // This seems kinda hacky? :S
            if (_settings.IsDisposed)
                _settings = new Settings(this);

            _settings.StartPosition = FormStartPosition.Manual;
            _settings.Location = Location;
            _settings.ShowDialog();
        }

        private void materialSingleLineTextFieldEmail_TextChanged(object sender, EventArgs e)
        {
            // Dumb Programming 2020
            Configuration.Email = materialSingleLineTextFieldEmail.Text;
            Configuration.Save();
        }

        void DiscordClick()
        {
            Process.Start("https://discord.gg/aurorafn");
        }

        private void pictureBoxDiscord_Click(object sender, EventArgs e)
        {
            DiscordClick();
        }

        private void materialFlatButtonDiscord_Click(object sender, EventArgs e)
        {
            DiscordClick();
        }

        private void timerHeartbeat_Tick(object sender, EventArgs e)
        {
            // TODO: Re-add Online.

            if (_clientProcess != null)
            {
                if (!_clientProcess.HasExited)
                    Hide();
                else
                    _clientProcess = null; // TODO: Probably a dumb hack?
            }
            else
                Show();
        }

        private void materialFlatButtonInfo_Click(object sender, EventArgs e)
        {
            var text = $"Aurora (Launcher = {App.Version}) by Cyuubi and Slushia" + "\n\n" +
                "If you purchased this software, you have been scammed. Please, immediately request a refund." + "\n\n" +
                "Please join our Discord server if you experience any problems!";

            MessageBox.Show(text);
        }

        private void materialSingleLineTextFieldPassword_TextChanged(object sender, EventArgs e)
        {
            Configuration.Password = materialSingleLineTextFieldPassword.Text;
            Configuration.Save();
        }

        private void materialRaisedButtonView_Click(object sender, EventArgs e)
        {
            materialSingleLineTextFieldPassword.UseSystemPasswordChar = !materialSingleLineTextFieldPassword.UseSystemPasswordChar;
        }
    }
}
