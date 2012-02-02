namespace StreamDesk
{
    partial class MainStreamForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainStreamForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iRCChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.favoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newStreamWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWebBrowserWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateStreamsDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.onTheWebToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamDeskHomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nasuTekHomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutStreamDeskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.streamsToolStripMenuItem,
            this.chatToolStripMenuItem,
            this.favoritesToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.streamInformationToolStripMenuItem});
            this.viewToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.viewToolStripMenuItem.MergeIndex = 1;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Visible = false;
            // 
            // streamInformationToolStripMenuItem
            // 
            this.streamInformationToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.information;
            this.streamInformationToolStripMenuItem.Name = "streamInformationToolStripMenuItem";
            this.streamInformationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.streamInformationToolStripMenuItem.Text = "Stream Information";
            this.streamInformationToolStripMenuItem.Click += new System.EventHandler(this.streamInformationToolStripMenuItem_Click);
            // 
            // streamsToolStripMenuItem
            // 
            this.streamsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.streamsToolStripMenuItem.MergeIndex = 2;
            this.streamsToolStripMenuItem.Name = "streamsToolStripMenuItem";
            this.streamsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.streamsToolStripMenuItem.Text = "Streams";
            // 
            // chatToolStripMenuItem
            // 
            this.chatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webChatToolStripMenuItem,
            this.iRCChatToolStripMenuItem});
            this.chatToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.chatToolStripMenuItem.MergeIndex = 3;
            this.chatToolStripMenuItem.Name = "chatToolStripMenuItem";
            this.chatToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.chatToolStripMenuItem.Text = "Chat";
            this.chatToolStripMenuItem.Visible = false;
            // 
            // webChatToolStripMenuItem
            // 
            this.webChatToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.comments;
            this.webChatToolStripMenuItem.Name = "webChatToolStripMenuItem";
            this.webChatToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.webChatToolStripMenuItem.Text = "Web Chat";
            this.webChatToolStripMenuItem.Click += new System.EventHandler(this.webChatToolStripMenuItem_Click);
            // 
            // iRCChatToolStripMenuItem
            // 
            this.iRCChatToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.comments;
            this.iRCChatToolStripMenuItem.Name = "iRCChatToolStripMenuItem";
            this.iRCChatToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.iRCChatToolStripMenuItem.Text = "IRC Chat";
            this.iRCChatToolStripMenuItem.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripTextBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(624, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::StreamDesk.Properties.Resources.arrow_left;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Back";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::StreamDesk.Properties.Resources.arrow_right;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Forward";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::StreamDesk.Properties.Resources.arrow_refresh;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Refresh";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(500, 25);
            this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 24);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(624, 418);
            this.webBrowser1.TabIndex = 3;
            // 
            // favoritesToolStripMenuItem
            // 
            this.favoritesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.manageToolStripMenuItem,
            this.toolStripMenuItem2});
            this.favoritesToolStripMenuItem.Name = "favoritesToolStripMenuItem";
            this.favoritesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.favoritesToolStripMenuItem.Text = "Favorites";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.manageToolStripMenuItem.Text = "Manage";
            this.manageToolStripMenuItem.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStreamWindowToolStripMenuItem,
            this.newWebBrowserWindowToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.MergeIndex = 1;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newStreamWindowToolStripMenuItem
            // 
            this.newStreamWindowToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.application_add;
            this.newStreamWindowToolStripMenuItem.Name = "newStreamWindowToolStripMenuItem";
            this.newStreamWindowToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.newStreamWindowToolStripMenuItem.Text = "New Window";
            this.newStreamWindowToolStripMenuItem.Click += new System.EventHandler(this.newStreamWindowToolStripMenuItem_Click);
            // 
            // newWebBrowserWindowToolStripMenuItem
            // 
            this.newWebBrowserWindowToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.application_add;
            this.newWebBrowserWindowToolStripMenuItem.Name = "newWebBrowserWindowToolStripMenuItem";
            this.newWebBrowserWindowToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.newWebBrowserWindowToolStripMenuItem.Text = "New Web Browser Window";
            this.newWebBrowserWindowToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(214, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem});
            this.toolsToolStripMenuItem.MergeIndex = 2;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.find;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateStreamsDatabaseToolStripMenuItem,
            this.toolStripMenuItem4,
            this.onTheWebToolStripMenuItem,
            this.supportToolStripMenuItem,
            this.toolStripMenuItem3,
            this.aboutStreamDeskToolStripMenuItem});
            this.helpToolStripMenuItem.MergeIndex = 4;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // updateStreamsDatabaseToolStripMenuItem
            // 
            this.updateStreamsDatabaseToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.database_refresh;
            this.updateStreamsDatabaseToolStripMenuItem.Name = "updateStreamsDatabaseToolStripMenuItem";
            this.updateStreamsDatabaseToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.updateStreamsDatabaseToolStripMenuItem.Text = "Update Streams Database";
            this.updateStreamsDatabaseToolStripMenuItem.Click += new System.EventHandler(this.updateStreamsDatabaseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(205, 6);
            // 
            // onTheWebToolStripMenuItem
            // 
            this.onTheWebToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.streamDeskHomeToolStripMenuItem,
            this.nasuTekHomeToolStripMenuItem});
            this.onTheWebToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.world;
            this.onTheWebToolStripMenuItem.Name = "onTheWebToolStripMenuItem";
            this.onTheWebToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.onTheWebToolStripMenuItem.Text = "On the Web";
            // 
            // streamDeskHomeToolStripMenuItem
            // 
            this.streamDeskHomeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("streamDeskHomeToolStripMenuItem.Image")));
            this.streamDeskHomeToolStripMenuItem.Name = "streamDeskHomeToolStripMenuItem";
            this.streamDeskHomeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.streamDeskHomeToolStripMenuItem.Text = "StreamDesk Home";
            this.streamDeskHomeToolStripMenuItem.Click += new System.EventHandler(this.streamDeskHomeToolStripMenuItem_Click);
            // 
            // nasuTekHomeToolStripMenuItem
            // 
            this.nasuTekHomeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nasuTekHomeToolStripMenuItem.Image")));
            this.nasuTekHomeToolStripMenuItem.Name = "nasuTekHomeToolStripMenuItem";
            this.nasuTekHomeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.nasuTekHomeToolStripMenuItem.Text = "NasuTek Home";
            this.nasuTekHomeToolStripMenuItem.Click += new System.EventHandler(this.nasuTekHomeToolStripMenuItem_Click);
            // 
            // supportToolStripMenuItem
            // 
            this.supportToolStripMenuItem.Name = "supportToolStripMenuItem";
            this.supportToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.supportToolStripMenuItem.Text = "Support";
            this.supportToolStripMenuItem.Click += new System.EventHandler(this.supportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(205, 6);
            // 
            // aboutStreamDeskToolStripMenuItem
            // 
            this.aboutStreamDeskToolStripMenuItem.Name = "aboutStreamDeskToolStripMenuItem";
            this.aboutStreamDeskToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.aboutStreamDeskToolStripMenuItem.Text = "About StreamDesk...";
            this.aboutStreamDeskToolStripMenuItem.Click += new System.EventHandler(this.aboutStreamDeskToolStripMenuItem_Click);
            // 
            // MainStreamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainStreamForm";
            this.Text = "No Stream Loaded";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webChatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iRCChatToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newStreamWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newWebBrowserWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateStreamsDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem onTheWebToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamDeskHomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nasuTekHomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem aboutStreamDeskToolStripMenuItem;
    }
}