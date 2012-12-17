namespace StreamDesk
{
    partial class MainMDIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMDIForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newStreamWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWebBrowserWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
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
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.favoritesToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.windowToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
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
            this.newWebBrowserWindowToolStripMenuItem.Click += new System.EventHandler(this.newWebBrowserWindowToolStripMenuItem_Click);
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
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
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
            this.addToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.manageToolStripMenuItem.Text = "Manage";
            this.manageToolStripMenuItem.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(114, 6);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileHorizontallyToolStripMenuItem,
            this.tileVerticallyToolStripMenuItem,
            this.toolStripMenuItem5});
            this.windowToolStripMenuItem.MergeIndex = 3;
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.cascadeToolStripMenuItem.Text = "Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
            // 
            // tileHorizontallyToolStripMenuItem
            // 
            this.tileHorizontallyToolStripMenuItem.Name = "tileHorizontallyToolStripMenuItem";
            this.tileHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.tileHorizontallyToolStripMenuItem.Text = "Tile Horizontally";
            this.tileHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.tileHorizontallyToolStripMenuItem_Click);
            // 
            // tileVerticallyToolStripMenuItem
            // 
            this.tileVerticallyToolStripMenuItem.Name = "tileVerticallyToolStripMenuItem";
            this.tileVerticallyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.tileVerticallyToolStripMenuItem.Text = "Tile Vertically";
            this.tileVerticallyToolStripMenuItem.Click += new System.EventHandler(this.tileVerticallyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(157, 6);
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
            this.streamDeskHomeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.streamDeskHomeToolStripMenuItem.Text = "StreamDesk";
            this.streamDeskHomeToolStripMenuItem.Click += new System.EventHandler(this.streamDeskHomeToolStripMenuItem_Click);
            // 
            // nasuTekHomeToolStripMenuItem
            // 
            this.nasuTekHomeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nasuTekHomeToolStripMenuItem.Image")));
            this.nasuTekHomeToolStripMenuItem.Name = "nasuTekHomeToolStripMenuItem";
            this.nasuTekHomeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.nasuTekHomeToolStripMenuItem.Text = "Michael Manley";
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
            // MainMDIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMDIForm";
            this.Text = "StreamDesk";
            this.Load += new System.EventHandler(this.MainMDIForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newStreamWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutStreamDeskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newWebBrowserWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateStreamsDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onTheWebToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem streamDeskHomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nasuTekHomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    }
}