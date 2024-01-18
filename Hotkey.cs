using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopSwitcher
{
    internal class Hotkey
    {
        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private IntPtr hookId = IntPtr.Zero;
        private Action<int, Keys> keyAction;

        public Hotkey(Action<int, Keys> keyAction)
        {
            this.keyAction = keyAction;
            this.hookId = this.SetHook(this.HookCallback);
            Console.WriteLine("instantiate");
        }

        ~Hotkey()
        {
            UnhookWindowsHookEx(this.hookId);
        }

        private IntPtr SetHook(HookProc proc)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, ProcessManager.CurrentProcessHandle, 0);
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYUP))
            {
                var vkCode = Marshal.ReadInt32(lParam);
                var key = (Keys)vkCode;
                this.keyAction?.Invoke((int)wParam, key);
            }

            return CallNextHookEx(this.hookId, nCode, wParam, lParam);
        }
    }
}
