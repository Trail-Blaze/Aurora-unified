using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AuroraLauncher
{
    public class Program
    {
        private static Process _clientProcess;

        private static byte _clientAnticheat; // 0 = None, 1 = BattlEye, 2 = EasyAntiCheat

#if !NATIVE
        private static Win32.HandlerRoutine _handlerRoutine;
#endif

        private static void Main(string[] args)
        {
            string formattedArgs = string.Join(" ", args);

#if !NATIVE
            // Check if -CONSOLE exists in args (regardless of case) to enable/allocate console.
            if (formattedArgs.ToUpper().Contains("-CONSOLE"))
            {
                formattedArgs = Regex.Replace(formattedArgs, "-CONSOLE", string.Empty, RegexOptions.IgnoreCase);

                Win32.AllocConsole();
            }
#endif

            // Check if -FORCEBE exists in args (regardless of case) to force BattlEye.
            if (formattedArgs.ToUpper().Contains("-FORCEBE"))
            {
                formattedArgs = Regex.Replace(formattedArgs, "-FORCEBE", string.Empty, RegexOptions.IgnoreCase);

                _clientAnticheat = 1;
            }

            // Check if -FORCEEAC exists in args (regardless of case) to force EasyAntiCheat.
            if (formattedArgs.ToUpper().Contains("-FORCEEAC"))
            {
                formattedArgs = Regex.Replace(formattedArgs, "-FORCEEAC", string.Empty, RegexOptions.IgnoreCase);

                _clientAnticheat = 2;
            }

            // Check if the client exists in the current work path, if it doesn't, just exit.
            if (!File.Exists(Configuration.ClientExecutable))
            {
                Win32.AllocConsole();

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"{Configuration.ClientExecutable} was not found, please make sure it exists.");
                Console.ReadKey();

                Console.ForegroundColor = ConsoleColor.Gray;

                return;
            }

#if NATIVE
            // Check if the native exists in the current work path, if it doesn't, just exit.
            if (!File.Exists(Configuration.ClientNative))
            {
                Win32.AllocConsole();

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"{Configuration.ClientNative} was not found, please make sure it exists.");
                Console.ReadKey();

                Console.ForegroundColor = ConsoleColor.Gray;

                return;
            }
#endif

#if !NATIVE
            // Print message.
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("AuroraLauncher by Cyuubi");

            Console.ForegroundColor = ConsoleColor.Gray;
#endif

            // Initialize client process with start info.
            _clientProcess = new Process
            {
                StartInfo =
                {
                    FileName = Configuration.ClientExecutable,
                    Arguments = formattedArgs,

                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };

            if (_clientAnticheat == 0) // None
                _clientProcess.StartInfo.Arguments += $" {Configuration.ClientArguments} -noeac -nobe -fltoken=none";
            else if (_clientAnticheat == 1) // BattlEye
                _clientProcess.StartInfo.Arguments += $" {Configuration.ClientArguments} -noeac -fromfl=be -fltoken={Configuration.BEToken}";
            else if (_clientAnticheat == 2) // EasyAntiCheat
                _clientProcess.StartInfo.Arguments += $" {Configuration.ClientArguments} -nobe -fromfl=eac -fltoken={Configuration.EACToken}";

#if !NO_EGL
            SwapLauncher();
#endif

            _clientProcess.Start();

#if !NATIVE
            // Setup our console HandlerRoutine.
            _handlerRoutine = new Win32.HandlerRoutine(HandlerRoutineCallback);

            Win32.SetConsoleCtrlHandler(_handlerRoutine, true);

            // Setup an AsyncStreamReader for standard output.
            AsyncStreamReader asyncOutputReader = new AsyncStreamReader(_clientProcess.StandardOutput);

            asyncOutputReader.DataReceived += delegate (object sender, string data)
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(data);

                Console.ForegroundColor = ConsoleColor.Gray;
            };

            asyncOutputReader.Start();
#else
            IntPtr clientHandle = Win32.OpenProcess(Win32.PROCESS_CREATE_THREAD | Win32.PROCESS_QUERY_INFORMATION |
                Win32.PROCESS_VM_OPERATION | Win32.PROCESS_VM_WRITE | Win32.PROCESS_VM_READ, false, _clientProcess.Id);

            IntPtr loadLibraryA = Win32.GetProcAddress(Win32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            uint size = (uint)((Configuration.ClientNative.Length + 1) * Marshal.SizeOf(typeof(char)));
            IntPtr address = Win32.VirtualAllocEx(clientHandle, IntPtr.Zero, size, Win32.MEM_COMMIT | Win32.MEM_RESERVE, Win32.PAGE_READWRITE);

            Win32.WriteProcessMemory(clientHandle, address,
                Encoding.Default.GetBytes(Configuration.ClientNative), size, out UIntPtr bytesWritten);

            Win32.CreateRemoteThread(clientHandle, IntPtr.Zero, 0, loadLibraryA, address, 0, IntPtr.Zero);
#endif

            _clientProcess.WaitForExit(); // We'll wait for the client process to exit, otherwise our launcher will just close instantly.

#if !NO_EGL
            SwapLauncher();
#endif
        }

#if !NO_EGL
        private static void SwapLauncher()
        {
            // Swap to original launcher.
            if (File.Exists("FortniteLauncher.exe.original"))
            {
                File.Move("FortniteLauncher.exe", "FortniteLauncher.exe.custom");
                File.Move("FortniteLauncher.exe.original", "FortniteLauncher.exe");
            }

            // Swap to custom launcher.
            if (File.Exists("FortniteLauncher.exe.custom"))
            {
                File.Move("FortniteLauncher.exe", "FortniteLauncher.exe.original");
                File.Move("FortniteLauncher.exe.custom", "FortniteLauncher.exe");
            }
        }
#endif

#if !NATIVE
        private static bool HandlerRoutineCallback(int dwCtrlType)
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
#endif
    }
}
