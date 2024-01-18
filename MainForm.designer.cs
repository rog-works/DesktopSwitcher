namespace DesktopSwitcher
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dataGridViewShortcut = new System.Windows.Forms.DataGridView();
            this.shortcutGroup = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShortcut)).BeginInit();
            this.shortcutGroup.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewShortcut
            // 
            this.dataGridViewShortcut.AllowUserToAddRows = false;
            this.dataGridViewShortcut.AllowUserToDeleteRows = false;
            this.dataGridViewShortcut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShortcut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewShortcut.Location = new System.Drawing.Point(3, 15);
            this.dataGridViewShortcut.Name = "dataGridViewShortcut";
            this.dataGridViewShortcut.ReadOnly = true;
            this.dataGridViewShortcut.RowTemplate.Height = 21;
            this.dataGridViewShortcut.Size = new System.Drawing.Size(569, 168);
            this.dataGridViewShortcut.TabIndex = 2;
            // 
            // shortcutGroup
            // 
            this.shortcutGroup.Controls.Add(this.dataGridViewShortcut);
            this.shortcutGroup.Location = new System.Drawing.Point(12, 27);
            this.shortcutGroup.Name = "shortcutGroup";
            this.shortcutGroup.Size = new System.Drawing.Size(575, 186);
            this.shortcutGroup.TabIndex = 3;
            this.shortcutGroup.TabStop = false;
            this.shortcutGroup.Text = "ショートカット";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(600, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "ファイル";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.quitToolStripMenuItem.Text = "終了";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 223);
            this.Controls.Add(this.shortcutGroup);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "DesktopSwitcher";
            this.VisibleChanged += new System.EventHandler(this.MainForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShortcut)).EndInit();
            this.shortcutGroup.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewShortcut;
        private System.Windows.Forms.GroupBox shortcutGroup;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
    }
}