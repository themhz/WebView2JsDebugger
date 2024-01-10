namespace WebViewJsDebugger
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.scintilla1 = new ScintillaNET.Scintilla();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.QuerySelectorByIdLike = new System.Windows.Forms.ToolStripMenuItem();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.chkPersistCode = new System.Windows.Forms.CheckBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lstlog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtlog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cbxUrls = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.openUrlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadUrlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(0, 0);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(946, 852);
            this.webView21.TabIndex = 0;
            this.webView21.ZoomFactor = 1D;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webView21);
            this.splitContainer1.Size = new System.Drawing.Size(1552, 852);
            this.splitContainer1.SplitterDistance = 602;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.scintilla1);
            this.splitContainer2.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button6);
            this.splitContainer2.Panel2.Controls.Add(this.button5);
            this.splitContainer2.Panel2.Controls.Add(this.cbxUrls);
            this.splitContainer2.Panel2.Controls.Add(this.button4);
            this.splitContainer2.Panel2.Controls.Add(this.button3);
            this.splitContainer2.Panel2.Controls.Add(this.chkPersistCode);
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.button2);
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Size = new System.Drawing.Size(602, 852);
            this.splitContainer2.SplitterDistance = 426;
            this.splitContainer2.TabIndex = 1;
            // 
            // scintilla1
            // 
            this.scintilla1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla1.Location = new System.Drawing.Point(0, 28);
            this.scintilla1.Name = "scintilla1";
            this.scintilla1.Size = new System.Drawing.Size(602, 398);
            this.scintilla1.TabIndex = 2;
            this.scintilla1.TextChanged += new System.EventHandler(this.scintilla1_TextChanged);
            this.scintilla1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.scintilla1_KeyDown);
            this.scintilla1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scintilla1_KeyPress);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.commandsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(602, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpen,
            this.menuSave,
            this.saveAsToolStripMenuItem,
            this.newToolStripMenuItem,
            this.openUrlsToolStripMenuItem,
            this.loadUrlsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(46, 24);
            this.toolStripMenuItem1.Text = "File";
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(224, 26);
            this.menuOpen.Text = "Open";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(224, 26);
            this.menuSave.Text = "Save";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // commandsToolStripMenuItem
            // 
            this.commandsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.postMessage,
            this.QuerySelectorByIdLike});
            this.commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(98, 26);
            this.commandsToolStripMenuItem.Text = "Commands";
            // 
            // postMessage
            // 
            this.postMessage.Name = "postMessage";
            this.postMessage.Size = new System.Drawing.Size(253, 26);
            this.postMessage.Text = "PostMessage";
            this.postMessage.Click += new System.EventHandler(this.postMessage_Click);
            // 
            // QuerySelectorByIdLike
            // 
            this.QuerySelectorByIdLike.Name = "QuerySelectorByIdLike";
            this.QuerySelectorByIdLike.Size = new System.Drawing.Size(253, 26);
            this.QuerySelectorByIdLike.Text = "document.querySelector";
            this.QuerySelectorByIdLike.Click += new System.EventHandler(this.QuerySelectorByIdLike_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(284, 87);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "test";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(7, 87);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Clear log";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // chkPersistCode
            // 
            this.chkPersistCode.AutoSize = true;
            this.chkPersistCode.Location = new System.Drawing.Point(460, 49);
            this.chkPersistCode.Name = "chkPersistCode";
            this.chkPersistCode.Size = new System.Drawing.Size(106, 20);
            this.chkPersistCode.TabIndex = 8;
            this.chkPersistCode.Text = "Persist Code";
            this.chkPersistCode.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(3, 116);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lstlog);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.txtlog);
            this.splitContainer3.Size = new System.Drawing.Size(596, 303);
            this.splitContainer3.SplitterDistance = 277;
            this.splitContainer3.TabIndex = 7;
            // 
            // lstlog
            // 
            this.lstlog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstlog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstlog.HideSelection = false;
            this.lstlog.Location = new System.Drawing.Point(0, 0);
            this.lstlog.Name = "lstlog";
            this.lstlog.Size = new System.Drawing.Size(277, 303);
            this.lstlog.TabIndex = 4;
            this.lstlog.UseCompatibleStateImageBehavior = false;
            this.lstlog.View = System.Windows.Forms.View.Details;
            // 
            // txtlog
            // 
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.Location = new System.Drawing.Point(0, 0);
            this.txtlog.Multiline = true;
            this.txtlog.Name = "txtlog";
            this.txtlog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtlog.Size = new System.Drawing.Size(315, 303);
            this.txtlog.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Url";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(381, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(458, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Upload Code";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // cbxUrls
            // 
            this.cbxUrls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUrls.FormattingEnabled = true;
            this.cbxUrls.Location = new System.Drawing.Point(34, 16);
            this.cbxUrls.Name = "cbxUrls";
            this.cbxUrls.Size = new System.Drawing.Size(341, 24);
            this.cbxUrls.TabIndex = 11;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(34, 46);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(82, 23);
            this.button5.TabIndex = 12;
            this.button5.Text = "Add Url";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(122, 46);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(73, 23);
            this.button6.TabIndex = 13;
            this.button6.Text = "Delete";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // openUrlsToolStripMenuItem
            // 
            this.openUrlsToolStripMenuItem.Name = "openUrlsToolStripMenuItem";
            this.openUrlsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openUrlsToolStripMenuItem.Text = "Open Urls";
            this.openUrlsToolStripMenuItem.Click += new System.EventHandler(this.openUrlsToolStripMenuItem_Click);
            // 
            // loadUrlsToolStripMenuItem
            // 
            this.loadUrlsToolStripMenuItem.Name = "loadUrlsToolStripMenuItem";
            this.loadUrlsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.loadUrlsToolStripMenuItem.Text = "Load-Reload Urls";
            this.loadUrlsToolStripMenuItem.Click += new System.EventHandler(this.loadUrlsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1552, 852);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "WebView Js Debugger";
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ScintillaNET.Scintilla scintilla1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ListView lstlog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.TextBox txtlog;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.CheckBox chkPersistCode;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem commandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postMessage;
        private System.Windows.Forms.ToolStripMenuItem QuerySelectorByIdLike;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox cbxUrls;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ToolStripMenuItem openUrlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadUrlsToolStripMenuItem;
    }
}

