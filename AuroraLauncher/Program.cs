using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AuroraLauncher
{
    class Program
    {
        #region Field Region

        static Process _clientProcess;
        /// <summary>
        /// 0 = None, 1 = BattlEye, 2 = EasyAntiCheat
        /// </summary>
        static byte _clientAnticheat;

        #endregion

        #region Method Region

#if GUI
        [STAThread]
#endif // GUI
        static void Main(string[] args)
        {
            var formattedArguments = string.Join(" ", args);

#if GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Check if -NOSTALGIA exists in args (regardless of case) to run the old GUI.
            if (!formattedArguments.ToUpper().Contains("-NOSTALGIA"))
                Application.Run(new Gui());
            else
                Application.Run(new OldGui());
#else
            // Check if -FORCEBE exists in args (regardless of case) to force BattlEye.
            if (formattedArguments.ToUpper().Contains("-FORCEBE"))
            {
                formattedArguments = Regex.Replace(formattedArguments, "-FORCEBE", string.Empty, RegexOptions.IgnoreCase);

                _clientAnticheat = 1;
            }

            // Check if -FORCEEAC exists in args (regardless of case) to force EasyAntiCheat.
            if (formattedArguments.ToUpper().Contains("-FORCEEAC"))
            {
                formattedArguments = Regex.Replace(formattedArguments, "-FORCEEAC", string.Empty, RegexOptions.IgnoreCase);

                _clientAnticheat = 2;
            }

            if (_clientAnticheat == 0) // None
                formattedArguments += $" {Build.ClientArguments} -noeac -nobe -fltoken=none";
            else if (_clientAnticheat == 1) // BattlEye
                formattedArguments += $" {Build.ClientArguments} -noeac -fromfl=be -fltoken={Build.BeToken}";
            else if (_clientAnticheat == 2) // EasyAntiCheat
                formattedArguments += $" {Build.ClientArguments} -nobe -fromfl=eac -fltoken={Build.EacToken}";

#if !NATIVE
            Win32.AllocConsole();
#endif // NATIVE

            // Check if the client exists in the current work path, if it doesn't, just exit.
            if (!File.Exists(Build.ClientExecutable))
            {
#if NATIVE
                Win32.AllocConsole();
#endif // NATIVE

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\"{Build.ClientExecutable}\" was not found, please make sure it exists.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadKey();

                return;
            }

#if NATIVE
            // Check if the native exists in the current work path, if it doesn't, just exit.
            if (!File.Exists(Build.ClientNative))
            {
                Win32.AllocConsole();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\"{Build.ClientNative}\" was not found, please make sure it exists.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadKey();

                return;
            }
#endif // NATIVE

#if !NATIVE
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Aurora, made with <3 by Cyuubi and Slushia.");
            Console.WriteLine("Discord: https://discord.gg/aurorafn\n");
            Console.ForegroundColor = ConsoleColor.Gray;
#endif // NATIVE

            _clientProcess = new Process
            {
                StartInfo = new ProcessStartInfo(Build.ClientExecutable, formattedArguments)
                {
                    UseShellExecute = false,

                    RedirectStandardOutput = true,

                    CreateNoWindow = false
                }
            };

#if !NO_EGL
            Swap(); // Swap the launcher, to prevent Fortnite from detecting it.
#endif // NO_EGL

            _clientProcess.Start();

#if !NATIVE
            // Setup a HandlerRoutine, for detecting when the console closes.
            Win32.SetConsoleCtrlHandler(new Win32.HandlerRoutine(Routine), true);

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
            Helper.InjectDll(_clientProcess.Id, Build.ClientNative);
#endif // NATIVE

            _clientProcess.WaitForExit(); // Wait for the client process to exit.

#if !NO_EGL
            Swap(); // Before exiting... Swap the launcher, again.
#endif // NO_EGL
#endif // GUI
        }

        static void Swap()
        {
            // Custom -> Original
            if (File.Exists("FortniteLauncher.exe.original"))
            {
                File.Move("FortniteLauncher.exe", "FortniteLauncher.exe.custom");
                File.Move("FortniteLauncher.exe.original", "FortniteLauncher.exe");
            }

            // Original -> Custom
            if (File.Exists("FortniteLauncher.exe.custom"))
            {
                File.Move("FortniteLauncher.exe", "FortniteLauncher.exe.original");
                File.Move("FortniteLauncher.exe.custom", "FortniteLauncher.exe");
            }
        }

        static bool Routine(int dwCtrlType)
        {
            switch (dwCtrlType)
            {
                case 2:
                    {
                        if (!_clientProcess.HasExited)
                            _clientProcess.Kill();

                        break;
                    }
            }

            return false;
        }

#endregion
    }
}
