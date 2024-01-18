using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DesktopSwitcher
{
    internal class ProcessManager
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public static IntPtr CurrentProcessHandle
        {
            get
            {
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return GetModuleHandle(curModule.ModuleName);
                }
            }
        }
    }
}
