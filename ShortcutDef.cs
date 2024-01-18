using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopSwitcher
{
    class ShortcutDef
    {
        public string DesktopName { get; set; }
        public bool Shift { get; set; }
        public bool Ctrl { get; set; }
        public bool Alt { get; set; }
        public string KeyNumber { get; set; }
    }
}
