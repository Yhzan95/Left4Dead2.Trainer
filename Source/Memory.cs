using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Left4DeadTrainer
{
    // Simple read and write memory csharp + Pattern Scanner (Source: UC)
    public static class Memory
    {
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern void CloseHandle(IntPtr hObject);

        public static Process GetGameProcess()
        {
            return Process.GetProcessesByName("left4dead2").FirstOrDefault();
        }

        public static IntPtr GetModuleBaseAddress(Process process, string moduleName)
        {
            return process.Modules.Cast<ProcessModule>()
                .FirstOrDefault(m => m.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase))?.BaseAddress ?? IntPtr.Zero;
        }

        public static void WriteInt(Process process, IntPtr address, int value)
        {
            IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);
            if (hProcess == IntPtr.Zero) return;

            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(hProcess, address, buffer, buffer.Length, out _);
            CloseHandle(hProcess);
        }

        public static void WriteByte(Process process, IntPtr address, byte value)
        {
            IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);
            if (hProcess == IntPtr.Zero) return;

            byte[] buffer = new byte[] { value };
            WriteProcessMemory(hProcess, address, buffer, 1, out _);
            CloseHandle(hProcess);
        }
        public static IntPtr PatternScan(Process process, string moduleName, string pattern)
        {
            var module = process.Modules.Cast<ProcessModule>().FirstOrDefault(m => m.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase));
            if (module == null) return IntPtr.Zero;

            IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);
            if (hProcess == IntPtr.Zero) return IntPtr.Zero;

            IntPtr baseAddress = module.BaseAddress;
            int moduleSize = module.ModuleMemorySize;
            byte[] moduleBytes = new byte[moduleSize];
            ReadProcessMemory(hProcess, baseAddress, moduleBytes, moduleSize, out _);
            CloseHandle(hProcess);

            string[] patternParts = pattern.Split(' ');
            byte?[] patternBytes = patternParts.Select(p => p == "?" ? (byte?)null : Convert.ToByte(p, 16)).ToArray();

            for (int i = 0; i < moduleBytes.Length - patternBytes.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < patternBytes.Length; j++)
                {
                    if (patternBytes[j].HasValue && moduleBytes[i + j] != patternBytes[j])
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    return IntPtr.Add(baseAddress, i);
                }
            }

            return IntPtr.Zero;
        }
        public static IntPtr ResolvePointer(Process process, string moduleName, int baseOffset, int[] pointerOffsets)
        {
            IntPtr moduleBase = GetModuleBaseAddress(process, moduleName);
            if (moduleBase == IntPtr.Zero) return IntPtr.Zero;

            IntPtr address = IntPtr.Add(moduleBase, baseOffset);
            IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);
            if (hProcess == IntPtr.Zero) return IntPtr.Zero;

            byte[] buffer = new byte[4];
            int bytesRead;

            foreach (int offset in pointerOffsets)
            {
                ReadProcessMemory(hProcess, address, buffer, buffer.Length, out bytesRead);
                address = (IntPtr)BitConverter.ToInt32(buffer, 0);
                address = IntPtr.Add(address, offset);
            }

            CloseHandle(hProcess);
            return address;
        }
        public static void ToggleMemory(string module, int offset, int onValue, int offValue, bool isChecked)
        {
            var game = GetGameProcess();
            if (game == null) return;

            IntPtr address = GetModuleBaseAddress(game, module);
            if (address == IntPtr.Zero) return;

            address = IntPtr.Add(address, offset);
            WriteInt(game, address, isChecked ? onValue : offValue);
        }
        public static void TogglePointerMemory(string module, int baseOffset, int[] pointerOffsets, int value)
        {
            var game = GetGameProcess();
            if (game == null) return;

            IntPtr address = ResolvePointer(game, module, baseOffset, pointerOffsets);
            if (address != IntPtr.Zero)
            {
                WriteInt(game, address, value);
            }
        }
    }
}
