using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DesktopSwitcher
{
    public partial class MainForm : Form
    {
        class OnPushShortcutArgs : EventArgs
        {
            public int index;
            public OnPushShortcutArgs(int index) { this.index = index; }
        }

        private delegate void OnPushShortcutDelegate(object sender, OnPushShortcutArgs e);

        private Hotkey hotkey;
        private event OnPushShortcutDelegate onPushShortcuts;

        public MainForm()
        {
            this.InitializeComponent();
            this.InitializeComponents();
            this.hotkey = new Hotkey(this.OnHotkeyCallback);
            this.onPushShortcuts += this.OnPushShortcut;
        }

        private void OnHotkeyCallback(int wParam, Keys key)
        {
            Logger.Debug($"On hotkey callback. wParam: {wParam}, key: {key}");

            var keyNumbers = new List<Keys>() {
                Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5,
                Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0,
            };
            var onNumber = keyNumbers.Contains(key);
            var onCtrl = (Control.ModifierKeys & Keys.Control) != 0;
            var onAlt = (Control.ModifierKeys & Keys.Alt) != 0;
            if (onNumber && onCtrl && onAlt)
            {
                this.onPushShortcuts?.BeginInvoke(this, new OnPushShortcutArgs(keyNumbers.IndexOf(key)), OnEndPushShortcut, null);
            }
        }

        private void OnPushShortcut(object sender, OnPushShortcutArgs e)
        {
            Logger.Info($"On push shortcut. index: {e.index}");

            if (Switcher.Switchable(e.index))
            {
                Switcher.SwitchTo(e.index);
            }
        }

        private void OnEndPushShortcut(IAsyncResult result)
        {
            Logger.Info("On end push shortcut");

            var ret = (System.Runtime.Remoting.Messaging.AsyncResult)result;
            var handler = (OnPushShortcutDelegate)ret.AsyncDelegate;
            handler.EndInvoke(result);
        }

        private void InitializeComponents()
        {
            this.InitializeTasktray();
            this.InitializeDataGridViewShortcut();
        }

        private void InitializeTasktray()
        {
            var icon = new NotifyIcon();
            icon.Icon = this.Icon;
            icon.Text = this.Text;
            icon.Visible = true;
            icon.ContextMenuStrip = new ContextMenuStrip();
            icon.ContextMenuStrip.Items.Add("開く", null, (s, e) => {
                this.Show();
            });
            icon.ContextMenuStrip.Items.Add("終了", null, (s, e) => {
                this.Quit();
            });
        }

        private void InitializeDataGridViewShortcut()
        {
            var defs = new List<ShortcutDef>();
            for (int i = 0; i < 10; i++)
            {
                var def = new ShortcutDef();
                def.DesktopName = "None";
                def.Shift = false;
                def.Ctrl = true;
                def.Alt = true;
                def.KeyNumber = ((i + 1) % 10).ToString();
                defs.Add(def);
            }

            this.dataGridViewShortcut.DataSource = defs;
        }

        private void Quit()
        {
            Logger.Info("Quit");

            Application.Exit();
        }

        private void RefreshShortcutDefs()
        {
            var defs = (List<ShortcutDef>)this.dataGridViewShortcut.DataSource;
            var desktopNames = Switcher.Names();
            foreach (var (def, index) in defs.Select((def, index) => (def, index)))
            {
                def.DesktopName = index < desktopNames.Count ? desktopNames[index] : def.DesktopName;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Logger.Info("On form closing");

            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.RefreshShortcutDefs();
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Quit();
        }
    }
}
