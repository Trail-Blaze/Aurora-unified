using AuroraLauncher.Providers;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace AuroraLauncher
{
    partial class Gui : MaterialForm
    {
        MaterialSkinManager _skinManager;

        public Configuration Configuration;

        Settings _settings;

        Thread _commonHeartbeat;
        Thread _onlineHeartbeat;

        bool _onlinePaused;

        bool _showedUpdate;

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

            Text += $" [{App.Version}]";

            materialSingleLineTextFieldEmail.Text = Configuration.Email;
            materialSingleLineTextFieldPassword.Text = Configuration.Password;

            _commonHeartbeat = new Thread(new ThreadStart(CommonHeartbeat));
            _commonHeartbeat.IsBackground = true;

            _commonHeartbeat.Start();

#if ONLINE
            if (!Configuration.DisableOnline)
            {
                _onlineHeartbeat = new Thread(new ThreadStart(OnlineHeartbeat));
                _onlineHeartbeat.IsBackground = true;

                _onlineHeartbeat.Start();

                materialLabelOnline.Visible = true;
            }

            CheckUpdates();
#endif
        }

        delegate void SetOnlineTextDelegate(string text);

        void SetOnlineText(string text)
        {
            if (InvokeRequired)
            {
                var method = new SetOnlineTextDelegate(SetOnlineText);

                Invoke(method, new object[] { text });
            }
            else
                materialLabelOnline.Text = text;
        }

        delegate void SetShowDelegate();

        void SetShow()
        {
            if (InvokeRequired)
            {
                var method = new SetShowDelegate(SetShow);

                Invoke(method);
            }
            else
                Show();
        }

        delegate void SetHideDelegate();

        void SetHide()
        {
            if (InvokeRequired)
            {
                var method = new SetHideDelegate(SetHide);

                Invoke(method);
            }
            else
                Hide();
        }

        void CommonHeartbeat()
        {
            while (true)
            {
                if (IsHandleCreated)
                {
                    if (_clientProcess != null)
                    {
                        if (!_clientProcess.HasExited)
                        {
                            _onlinePaused = true;

                            SetHide();
                        }
                        else
                            _clientProcess = null; // TODO: Probably a dumb hack?
                    }
                    else
                    {
                        _onlinePaused = false;

                        SetShow();
                    }

                    Thread.Sleep(2000);
                }

                Thread.Sleep(1);
            }
        }

        void OnlineHeartbeat()
        {
            while (true)
            {
                if (IsHandleCreated && !_onlinePaused)
                {
                    SetOnlineText($"Online: {Api.Clients}, Parties: {Api.Parties}");

                    Thread.Sleep(4000);
                }

                Thread.Sleep(1);
            }
        }

        bool CheckUpdates(string source = "")
        {
            var isNotUpToDate = !Launcher.IsUpToDate;
            if (isNotUpToDate)
            {
                if (source != "Launch")
                {
                    if (!_showedUpdate)
                        _showedUpdate = true;
                }

                materialLabelUpdate.Visible = true;
                materialRaisedButtonLaunch.Text = "Update";
            }

            return isNotUpToDate;
        }

        private void materialSingleLineTextFieldEmail_TextChanged(object sender, EventArgs e)
        {
            // Dumb programming, 2020.

            Configuration.Email = materialSingleLineTextFieldEmail.Text;
            Configuration.Save();
        }

        private void materialSingleLineTextFieldPassword_TextChanged(object sender, EventArgs e)
        {
            Configuration.Password = materialSingleLineTextFieldPassword.Text;
            Configuration.Save();
        }

        private void materialRaisedButtonPasswordView_Click(object sender, EventArgs e)
        {
            materialSingleLineTextFieldPassword.UseSystemPasswordChar =
                !materialSingleLineTextFieldPassword.UseSystemPasswordChar;

            if (materialSingleLineTextFieldPassword.UseSystemPasswordChar)
                materialRaisedButtonPasswordView.Text = "Show";
            else
                materialRaisedButtonPasswordView.Text = "Hide";
        }

        void DiscordClick()
        {
            Process.Start("https://discord.gg/AuroraFN");
        }

        private void pictureBoxDiscord_Click(object sender, EventArgs e)
        {
            DiscordClick();
        }

        private void materialFlatButtonDiscord_Click(object sender, EventArgs e)
        {
            DiscordClick();
        }

        public static bool IsValidPath(string path)
        {
            var drive = new Regex(@"^[a-zA-Z]:\\$");
            if (!drive.IsMatch(path.Substring(0, 3)))
                return false;

            var invalidCharacters = new string(Path.GetInvalidPathChars());
            invalidCharacters += @":/?*" + "\"";

            var badCharacter = new Regex($"[{Regex.Escape(invalidCharacters)}]");
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
#if ONLINE
            if (CheckUpdates("Launch"))
            {
                // Check if we previously showed that we required an update.
                if (!_showedUpdate)
                {
                    var result = MessageBox.Show("You must update Aurora Launcher to launch Fortnite.", string.Empty,
                        MessageBoxButtons.OKCancel);

                    if (result == DialogResult.Cancel)
                    {
                        _showedUpdate = true;
                        return;
                    }
                }

                Process.Start(Build.LauncherUri);
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
                var text =
                    $"\"{clientPath}\" was not found, please make sure it exists." + "\n\n" +
                    "Did you set the Install Location correctly?" + "\n\n" +
                    "TIP: The Install Location must be set to a folder that contains 2 folders named \"Engine\" and \"FortniteGame\".";

                MessageBox.Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var nativePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Build.LauncherNative);
            if (!File.Exists(nativePath))
            {
                var text =
                    $"\"{nativePath}\" was not found, please make sure it exists." + "\n\n" +
                    "Did you extract all files from the ZIP when you downloaded Aurora Launcher?" + "\n\n" +
                    $"TIP: \"{Build.LauncherNative}\" must be in the same directory as Aurora Launcher.";

                MessageBox.Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var arguments = $"-AUTH_LOGIN={Configuration.Email} -AUTH_PASSWORD={Configuration.Password} -AUTH_TYPE=epic {Configuration.Arguments}";

            if (_clientAnticheat == 0) // None
                arguments += $" {Build.ClientArguments} -noeac -nobe -fltoken=none";
            else if (_clientAnticheat == 1) // BattlEye
                arguments += $" {Build.ClientArguments} -noeac -fromfl=be -fltoken={Build.BeToken}";
            else if (_clientAnticheat == 2) // EasyAntiCheat
                arguments += $" {Build.ClientArguments} -nobe -fromfl=eac -fltoken={Build.EacToken}";

            _clientProcess = new Process
            {
                StartInfo = new ProcessStartInfo(clientPath, arguments)
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
            Helper.InjectDll(_clientProcess.Id, nativePath);
#endif // NATIVE
        }

        private void materialFlatButtonInfo_Click(object sender, EventArgs e)
        {
            var text =
                "Aurora, made with <3 by Cyuubi and Slushia." + "\n\n" +
                "If you purchased this software, you have been scammed. Please, immediately request a refund." + "\n\n" +
                "Please join our Discord server if you experience any problems!";

            MessageBox.Show(text);
        }

        private void materialFlatButtonSettings_Click(object sender, EventArgs e)
        {
            // This seems kinda hacky? :s
            if (_settings.IsDisposed)
                _settings = new Settings(this);

            _settings.StartPosition = FormStartPosition.Manual;
            _settings.Location = Location;
            _settings.ShowDialog();
        }
    }
}
