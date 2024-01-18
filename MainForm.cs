using System;
using System.Collections.Generic;
using System.Data;
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

        private void OnPushShortcut(object sender, OnPushShortcutArgs e)
        {
            Console.WriteLine($"On push shortcut. index: {e.index}");

            if (Switcher.Switchable(e.index))
            {
                Switcher.SwitchTo(e.index);
            }
        }

        private void OnHotkeyCallback(int wParam, Keys key)
        {
            Console.WriteLine($"On hotkey callback. wParam: {wParam}, key: {key}");

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

        private void OnEndPushShortcut(IAsyncResult result)
        {
            Console.WriteLine("On end push shortcut");

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
            var data = new DataTable();
            data.Columns.Add("デスクトップ");
            data.Columns.Add("Shift");
            data.Columns.Add("Ctrl");
            data.Columns.Add("Alt");
            data.Columns.Add("キー");

            this.dataGridViewShortcut.DataSource = data;
        }

        private void Quit()
        {
            Application.Exit();
        }

        public void RefreshShortcuts()
        {
            var data = (DataTable)this.dataGridViewShortcut.DataSource;
            data.Rows.Clear();

            var desktopNames = Switcher.Names();
            for (int i = 0; i < 10; i++)
            {
                var desktopName = i < desktopNames.Count ? desktopNames[i] : "None";
                var row = data.NewRow();
                row.SetField<string>(0, desktopName);
                row.SetField<string>(1, "×");
                row.SetField<string>(2, "〇");
                row.SetField<string>(3, "〇");
                row.SetField<string>(4, (i + 1 % 10).ToString());
                data.Rows.Add(row);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.RefreshShortcuts();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Quit();
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.RefreshShortcuts();
            }
        }
    }
}
