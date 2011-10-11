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
            this.cmStreamList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateStreamListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.kComputerZoneToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wbStream = new System.Windows.Forms.WebBrowser();
            this.lblActiveDesc = new System.Windows.Forms.Label();
            this.lblActiveTitle = new System.Windows.Forms.LinkLabel();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.pbToggleStreams = new System.Windows.Forms.PictureBox();
            this.ttChat = new System.Windows.Forms.ToolTip(this.components);
            this.sContainer = new System.Windows.Forms.SplitContainer();
            this.lblChatType = new System.Windows.Forms.Label();
            this.pbChat = new System.Windows.Forms.Button();
            this.cmStreamList.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbToggleStreams)).BeginInit();
            this.sContainer.Panel1.SuspendLayout();
            this.sContainer.Panel2.SuspendLayout();
            this.sContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvStreams
            // 
            this.tvStreams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvStreams.ContextMenuStrip = this.cmStreamList;
            this.tvStreams.Location = new System.Drawing.Point(0, 0);
            this.tvStreams.Name = "tvStreams";
            this.tvStreams.Size = new System.Drawing.Size(181, 423);
            this.tvStreams.TabIndex = 0;
            this.tvStreams.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvStreams_NodeMouseClick);
            // 
            // cmStreamList
            // 
            this.cmStreamList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateStreamListToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.toolStripSeparator2,
            this.kComputerZoneToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.cmStreamList.Name = "cmStreamList";
            this.cmStreamList.Size = new System.Drawing.Size(179, 98);
            // 
            // updateStreamListToolStripMenuItem
            // 
            this.updateStreamListToolStripMenuItem.Name = "updateStreamListToolStripMenuItem";
            this.updateStreamListToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.updateStreamListToolStripMenuItem.Text = "Update stream list...";
            this.updateStreamListToolStripMenuItem.Click += new System.EventHandler(this.updateStreamListToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.preferencesToolStripMenuItem.Text = "Settings";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(175, 6);
            // 
            // kComputerZoneToolStripMenuItem1
            // 
            this.kComputerZoneToolStripMenuItem1.Name = "kComputerZoneToolStripMenuItem1";
            this.kComputerZoneToolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
            this.kComputerZoneToolStripMenuItem1.Text = "StreamDesk Site";
            this.kComputerZoneToolStripMenuItem1.Click += new System.EventHandler(this.kComputerZoneToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.aboutToolStripMenuItem.Text = "About StreamDesk";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // wbStream
            // 
            this.wbStream.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wbStream.Location = new System.Drawing.Point(0, -1);
            this.wbStream.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbStream.Name = "wbStream";
            this.wbStream.ScriptErrorsSuppressed = true;
            this.wbStream.ScrollBarsEnabled = false;
            this.wbStream.Size = new System.Drawing.Size(500, 412);
            this.wbStream.TabIndex = 1;
            // 
            // lblActiveDesc
            // 
            this.lblActiveDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActiveDesc.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveDesc.Location = new System.Drawing.Point(42, 23);
            this.lblActiveDesc.Name = "lblActiveDesc";
            this.lblActiveDesc.Size = new System.Drawing.Size(446, 18);
            this.lblActiveDesc.TabIndex = 3;
            this.lblActiveDesc.Text = "Nothing is playing. Select a stream from the list to the left.";
            // 
            // lblActiveTitle
            // 
            this.lblActiveTitle.ActiveLinkColor = System.Drawing.Color.Maroon;
            this.lblActiveTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActiveTitle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveTitle.LinkColor = System.Drawing.Color.Navy;
            this.lblActiveTitle.Location = new System.Drawing.Point(41, 6);
            this.lblActiveTitle.Name = "lblActiveTitle";
            this.lblActiveTitle.Size = new System.Drawing.Size(449, 19);
            this.lblActiveTitle.TabIndex = 4;
            this.lblActiveTitle.TabStop = true;
            this.lblActiveTitle.Tag = "about:blank";
            this.lblActiveTitle.Text = "No Stream";
            this.lblActiveTitle.VisitedLinkColor = System.Drawing.Color.Navy;
            this.lblActiveTitle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblActiveTitle_LinkClicked);
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.pbToggleStreams);
            this.pnlInfo.Controls.Add(this.lblActiveTitle);
            this.pnlInfo.Controls.Add(this.lblActiveDesc);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfo.Location = new System.Drawing.Point(0, 409);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(500, 47);
            this.pnlInfo.TabIndex = 5;
            // 
            // pbToggleStreams
            // 
            this.pbToggleStreams.Image = global::StreamDesk.Properties.Resources.camera_video1;
            this.pbToggleStreams.Location = new System.Drawing.Point(9, 14);
            this.pbToggleStreams.Name = "pbToggleStreams";
            this.pbToggleStreams.Size = new System.Drawing.Size(22, 22);
            this.pbToggleStreams.TabIndex = 5;
            this.pbToggleStreams.TabStop = false;
            this.ttChat.SetToolTip(this.pbToggleStreams, "Show/hide the stream list");
            this.pbToggleStreams.Click += new System.EventHandler(this.pbToggleStreams_Click);
            // 
            // sContainer
            // 
            this.sContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sContainer.Location = new System.Drawing.Point(0, 0);
            this.sContainer.Name = "sContainer";
            // 
            // sContainer.Panel1
            // 
            this.sContainer.Panel1.Controls.Add(this.lblChatType);
            this.sContainer.Panel1.Controls.Add(this.pbChat);
            this.sContainer.Panel1.Controls.Add(this.tvStreams);
            this.sContainer.Panel1MinSize = 5;
            // 
            // sContainer.Panel2
            // 
            this.sContainer.Panel2.Controls.Add(this.wbStream);
            this.sContainer.Panel2.Controls.Add(this.pnlInfo);
            this.sContainer.Panel2MinSize = 5;
            this.sContainer.Size = new System.Drawing.Size(687, 456);
            this.sContainer.SplitterDistance = 183;
            this.sContainer.TabIndex = 6;
            // 
            // lblChatType
            // 
            this.lblChatType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChatType.AutoSize = true;
            this.lblChatType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChatType.Location = new System.Drawing.Point(63, 432);
            this.lblChatType.Name = "lblChatType";
            this.lblChatType.Size = new System.Drawing.Size(73, 15);
            this.lblChatType.TabIndex = 2;
            this.lblChatType.Text = "via Web IRC";
            // 
            // pbChat
            // 
            this.pbChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbChat.Image = global::StreamDesk.Properties.Resources.comment;
            this.pbChat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pbChat.Location = new System.Drawing.Point(5, 428);
            this.pbChat.Name = "pbChat";
            this.pbChat.Size = new System.Drawing.Size(54, 23);
            this.pbChat.TabIndex = 1;
            this.pbChat.Text = "Chat";
            this.pbChat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pbChat.UseVisualStyleBackColor = true;
            this.pbChat.Click += new System.EventHandler(this.pbChat_Click);
            this.pbChat.MouseEnter += new System.EventHandler(this.pbChat_MouseEnter);
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(687, 456);
            this.Controls.Add(this.sContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "frmMain";
            this.Text = "Stream Player";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.cmStreamList.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbToggleStreams)).EndInit();
            this.sContainer.Panel1.ResumeLayout(false);
            this.sContainer.Panel1.PerformLayout();
            this.sContainer.Panel2.ResumeLayout(false);
            this.sContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

