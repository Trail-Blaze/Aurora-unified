using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
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
#if GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Gui());
#else
            var formattedArguments = string.Join(" ", args);

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
                formattedArguments += $" {Configuration.ClientArguments} -noeac -nobe -fltoken=none";
            else if (_clientAnticheat == 1) // BattlEye
                formattedArguments += $" {Configuration.ClientArguments} -noeac -fromfl=be -fltoken={Configuration.BEToken}";
            else if (_clientAnticheat == 2) // EasyAntiCheat
                formattedArguments += $" {Configuration.ClientArguments} -nobe -fromfl=eac -fltoken={Configuration.EACToken}";

#if !NATIVE
            Win32.AllocConsole();
#endif // NATIVE

            // Check if the client exists in the current work path, if it doesn't, just exit.
            if (!File.Exists(Configuration.ClientExecutable))
            {
#if NATIVE
                Win32.AllocConsole();
#endif // NATIVE

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\"{Configuration.ClientExecutable}\" was not found, please make sure it exists.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadKey();

                return;
            }

#if NATIVE
            // Check if the native exists in the current work path, if it doesn't, just exit.
            if (!File.Exists(Configuration.ClientNative))
            {
                Win32.AllocConsole();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\"{Configuration.ClientNative}\" was not found, please make sure it exists.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadKey();

                return;
            }
#endif // NATIVE

#if !NATIVE
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("AuroraLauncher by Cyuubi");
            Console.ForegroundColor = ConsoleColor.Gray;
#endif // NATIVE

            _clientProcess = new Process
            {
                StartInfo = new ProcessStartInfo(Configuration.ClientExecutable, formattedArguments)
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
            var clientHandle = Win32.OpenProcess(Win32.PROCESS_CREATE_THREAD | Win32.PROCESS_QUERY_INFORMATION |
                Win32.PROCESS_VM_OPERATION | Win32.PROCESS_VM_WRITE | Win32.PROCESS_VM_READ, false, _clientProcess.Id);

            var loadLibrary = Win32.GetProcAddress(Win32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            var size = (uint)((Configuration.ClientNative.Length + 1) * Marshal.SizeOf(typeof(char)));
            var address = Win32.VirtualAllocEx(clientHandle, IntPtr.Zero,
                size, Win32.MEM_COMMIT | Win32.MEM_RESERVE, Win32.PAGE_READWRITE);

            Win32.WriteProcessMemory(clientHandle, address,
                Encoding.Default.GetBytes(Configuration.ClientNative), size, out UIntPtr bytesWritten);

            Win32.CreateRemoteThread(clientHandle, IntPtr.Zero, 0, loadLibrary, address, 0, IntPtr.Zero);
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
