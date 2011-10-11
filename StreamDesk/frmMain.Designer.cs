namespace StreamDesk
{
    using StreamDesk.AppTools;
    using StreamDesk.Properties;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;

    partial class frmMain
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tvStreams = new System.Windows.Forms.TreeView();
            this.ttChat = new System.Windows.Forms.ToolTip(this.components);
            this.sContainer = new System.Windows.Forms.SplitContainer();
            this.wbStream = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.liveHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iRCChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.pbChat = new System.Windows.Forms.ToolStripMenuItem();
            this.chatViaIRCClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblActiveTitle = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblActiveDesc = new System.Windows.Forms.ToolStripStatusLabel();
            this.sContainer.Panel1.SuspendLayout();
            this.sContainer.Panel2.SuspendLayout();
            this.sContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvStreams
            // 
            this.tvStreams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvStreams.Location = new System.Drawing.Point(0, 0);
            this.tvStreams.Name = "tvStreams";
            this.tvStreams.Size = new System.Drawing.Size(183, 410);
            this.tvStreams.TabIndex = 0;
            this.tvStreams.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvStreams_AfterSelect_1);
            this.tvStreams.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvStreams_NodeMouseClick);
            // 
            // sContainer
            // 
            this.sContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sContainer.Location = new System.Drawing.Point(0, 24);
            this.sContainer.Name = "sContainer";
            // 
            // sContainer.Panel1
            // 
            this.sContainer.Panel1.Controls.Add(this.tvStreams);
            this.sContainer.Panel1MinSize = 5;
            // 
            // sContainer.Panel2
            // 
            this.sContainer.Panel2.Controls.Add(this.wbStream);
            this.sContainer.Panel2MinSize = 5;
            this.sContainer.Size = new System.Drawing.Size(687, 410);
            this.sContainer.SplitterDistance = 183;
            this.sContainer.TabIndex = 6;
            // 
            // wbStream
            // 
            this.wbStream.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbStream.Location = new System.Drawing.Point(0, 0);
            this.wbStream.Name = "wbStream";
            this.wbStream.Size = new System.Drawing.Size(500, 410);
            this.wbStream.TabIndex = 6;
            this.wbStream.Paint += new System.Windows.Forms.PaintEventHandler(this.wbStream_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.streamsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(687, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWindowToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newWindowToolStripMenuItem
            // 
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.newWindowToolStripMenuItem.Text = "New Window";
            this.newWindowToolStripMenuItem.Click += new System.EventHandler(this.newWindowToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.treeMenuToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // treeMenuToolStripMenuItem
            // 
            this.treeMenuToolStripMenuItem.Name = "treeMenuToolStripMenuItem";
            this.treeMenuToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.treeMenuToolStripMenuItem.Text = "Tree Menu";
            this.treeMenuToolStripMenuItem.Click += new System.EventHandler(this.treeMenuToolStripMenuItem_Click);
            // 
            // streamsToolStripMenuItem
            // 
            this.streamsToolStripMenuItem.Name = "streamsToolStripMenuItem";
            this.streamsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.streamsToolStripMenuItem.Text = "Streams";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem1});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // preferencesToolStripMenuItem1
            // 
            this.preferencesToolStripMenuItem1.Name = "preferencesToolStripMenuItem1";
            this.preferencesToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.preferencesToolStripMenuItem1.Text = "Options";
            this.preferencesToolStripMenuItem1.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem,
            this.toolStripMenuItem1,
            this.liveHelpToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateStreamListToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 6);
            // 
            // liveHelpToolStripMenuItem
            // 
            this.liveHelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webChatToolStripMenuItem,
            this.iRCChatToolStripMenuItem});
            this.liveHelpToolStripMenuItem.Name = "liveHelpToolStripMenuItem";
            this.liveHelpToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.liveHelpToolStripMenuItem.Text = "Live Help";
            // 
            // webChatToolStripMenuItem
            // 
            this.webChatToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.internet_group_chat;
            this.webChatToolStripMenuItem.Name = "webChatToolStripMenuItem";
            this.webChatToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.webChatToolStripMenuItem.Text = "Web Chat";
            this.webChatToolStripMenuItem.Click += new System.EventHandler(this.webChatToolStripMenuItem_Click);
            // 
            // iRCChatToolStripMenuItem
            // 
            this.iRCChatToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.internet_group_chat;
            this.iRCChatToolStripMenuItem.Name = "iRCChatToolStripMenuItem";
            this.iRCChatToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.iRCChatToolStripMenuItem.Text = "IRC Client";
            this.iRCChatToolStripMenuItem.Click += new System.EventHandler(this.iRCChatToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.lblActiveTitle,
            this.lblActiveDesc});
            this.statusStrip1.Location = new System.Drawing.Point(0, 434);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(687, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbChat,
            this.chatViaIRCClientToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::StreamDesk.Properties.Resources.comment;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 20);
            this.toolStripDropDownButton1.Text = "Chat";
            // 
            // pbChat
            // 
            this.pbChat.Image = global::StreamDesk.Properties.Resources.internet_group_chat;
            this.pbChat.Name = "pbChat";
            this.pbChat.Size = new System.Drawing.Size(126, 22);
            this.pbChat.Text = "pbChat";
            this.pbChat.Click += new System.EventHandler(this.pbChat_Click);
            // 
            // chatViaIRCClientToolStripMenuItem
            // 
            this.chatViaIRCClientToolStripMenuItem.Image = global::StreamDesk.Properties.Resources.internet_group_chat;
            this.chatViaIRCClientToolStripMenuItem.Name = "chatViaIRCClientToolStripMenuItem";
            this.chatViaIRCClientToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.chatViaIRCClientToolStripMenuItem.Text = "IRC Client";
            this.chatViaIRCClientToolStripMenuItem.Click += new System.EventHandler(this.chatViaIRCClientToolStripMenuItem_Click);
            // 
            // lblActiveTitle
            // 
            this.lblActiveTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveTitle.IsLink = true;
            this.lblActiveTitle.LinkColor = System.Drawing.Color.Navy;
            this.lblActiveTitle.Name = "lblActiveTitle";
            this.lblActiveTitle.Size = new System.Drawing.Size(67, 17);
            this.lblActiveTitle.Text = "No Stream";
            this.lblActiveTitle.Click += new System.EventHandler(this.lblActiveTitle_Click);
            // 
            // lblActiveDesc
            // 
            this.lblActiveDesc.Name = "lblActiveDesc";
            this.lblActiveDesc.Size = new System.Drawing.Size(319, 17);
            this.lblActiveDesc.Text = "Nothing is playing. Select a stream from the streams menu.";
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(687, 456);
            this.Controls.Add(this.sContainer);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Stream Player";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.sContainer.Panel1.ResumeLayout(false);
            this.sContainer.Panel2.ResumeLayout(false);
            this.sContainer.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Panel wbStream;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem preferencesToolStripMenuItem1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem updateToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem treeMenuToolStripMenuItem;
        private ToolStripMenuItem streamsToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripStatusLabel lblActiveTitle;
        private ToolStripStatusLabel lblActiveDesc;
        private ToolStripMenuItem pbChat;
        private ToolStripMenuItem chatViaIRCClientToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem liveHelpToolStripMenuItem;
        private ToolStripMenuItem webChatToolStripMenuItem;
        private ToolStripMenuItem iRCChatToolStripMenuItem;
        private ToolStripMenuItem newWindowToolStripMenuItem;
    }
}

