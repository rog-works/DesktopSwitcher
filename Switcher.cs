using System;
using System.Collections.Generic;

namespace DesktopSwitcher
{
    internal class Switcher
    {
        public static int Count
        {
            get { return VirtualDesktop.Desktop.Count; }
        }

        public static VirtualDesktop.Desktop Current
        {
            get { return VirtualDesktop.Desktop.Current; }
        }

        public static bool Switchable(int index)
        {
            return 0 <= index && index < Switcher.Count;
        }

        public static List<string> Names()
        {
            var names = new List<string>();
            for (var i = 0; i < Switcher.Count; i++)
            {
                names.Add(VirtualDesktop.Desktop.DesktopNameFromIndex(i));
            }

            return names;
        }

        public static VirtualDesktop.Desktop At(int index)
        {
            return VirtualDesktop.Desktop.FromIndex(index);
        }

        public static void SwitchTo(int index)
        {
            if (!Switcher.Switchable(index))
            {
                Console.WriteLine($"Invalid index. counts: {Switcher.Count}, index: {index}");
                return;
            }

            Switcher.SwitchToInternal(index);
        }

        private static void SwitchToInternal(int index)
        {
            var to = VirtualDesktop.DesktopManager.GetDesktop(index);
            VirtualDesktop.DesktopManager.VirtualDesktopManagerInternal.SwitchDesktop(to);
            VirtualDesktop.Desktop.FocusForegroundWindow();
        }
    }
}
