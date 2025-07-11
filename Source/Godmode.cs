using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Left4DeadTrainer
{
    //GodMod Patch
    public static class Godmode
    {
        const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern void CloseHandle(IntPtr hObject);

        public static void Toggle(Process game, bool enable)
        {
            if (game == null) return;

            IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, game.Id);
            if (hProcess == IntPtr.Zero) return;

            IntPtr baseAddress = Memory.GetModuleBaseAddress(game, "server.dll");
            if (baseAddress == IntPtr.Zero) return;

            IntPtr instructionAddress = IntPtr.Add(baseAddress, 0x41DF8);
            byte[] originalBytes = new byte[] { 0x89, 0x37 };
            byte[] nopBytes = new byte[] { 0x90, 0x90 };

            WriteProcessMemory(hProcess, instructionAddress, enable ? nopBytes : originalBytes, 2, out _);
            CloseHandle(hProcess);
        }
    }
}
