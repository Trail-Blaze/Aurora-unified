using System;
using System.Runtime.InteropServices;
using System.Text;

static class Helper
{
    #region Method Region

    public static void InjectDll(int processId, string path)
    {
        var handle = Win32.OpenProcess(Win32.PROCESS_CREATE_THREAD | Win32.PROCESS_QUERY_INFORMATION |
            Win32.PROCESS_VM_OPERATION | Win32.PROCESS_VM_WRITE | Win32.PROCESS_VM_READ, false, processId);

        var loadLibrary = Win32.GetProcAddress(Win32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

        var size = (uint)((path.Length + 1) * Marshal.SizeOf(typeof(char)));
        var address = Win32.VirtualAllocEx(handle, IntPtr.Zero,
            size, Win32.MEM_COMMIT | Win32.MEM_RESERVE, Win32.PAGE_READWRITE);

        Win32.WriteProcessMemory(handle, address,
            Encoding.Default.GetBytes(path), size, out UIntPtr bytesWritten);

        Win32.CreateRemoteThread(handle, IntPtr.Zero, 0, loadLibrary, address, 0, IntPtr.Zero);
    }

    #endregion
}
