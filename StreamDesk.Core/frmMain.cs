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
    using System.Collections.Generic;
    using NasuTek.XUL.Runtime;
    using System.IO;
    using StreamDesk.AppCore;
    using System.Net;
    using System.Text;
    using System.ServiceProcess;
    using System.Threading;

    public partial class frmMain : Form
    {
        private string AppName = "StreamDesk";
        private IContainer components;
        private SplitContainer sContainer;
        private ToolTip ttChat;
        private TreeView tvStreams;
        GeckoWebBrowser webBrowser = new GeckoWebBrowser();
        string streamHTML = "O_O";

        public frmMain()
        {
            InitializeComponent();
            webBrowser.Dock = DockStyle.Fill;
            wbStream.Controls.Add(webBrowser);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.LastWidth = base.Width;
            Settings.Default.LastHeight = base.Height;
            Settings.Default.LastTop = base.Top;
            Settings.Default.LastLeft = base.Left;
            if (this.sContainer.SplitterDistance > 100)
            {
                Settings.Default.LastSidebar = this.sContainer.SplitterDistance;
            }
            else
            {
                Settings.Default.LastSidebar = 100;
            }
            Settings.Default.Save();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            sContainer.Panel1Collapsed = Reverse(Settings.Default.ShowTreeView);

            this.Text = this.AppName;
            this.sContainer.SplitterDistance = Settings.Default.LastSidebar;
            base.Width = Settings.Default.LastWidth;
            base.Height = Settings.Default.LastHeight;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            if (Settings.Default.LastTop < (workingArea.Height - base.Height))
            {
                base.Top = Settings.Default.LastTop;
            }
            if (Settings.Default.LastLeft < (workingArea.Width - base.Width))
            {
                base.Left = Settings.Default.LastLeft;
            }
            this.sContainer.SplitterWidth = 3;
            if (Settings.Default.GetStreamsAtStartup)
            {
                //new frmUpdateStreamList(true).ShowDialog();
            }
            if (Settings.Default.MinSize)
            {
                this.MinimumSize = new Size(550, 400);
            }
            else
            {
                this.MinimumSize = new Size(1, 1);
            }
            base.TopMost = Settings.Default.VideoTopMost;
            toolStripDropDownButton1.Visible = false;
            this.ReadStreams();
        }

        private void kComputerZoneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.SystemBrowser("http://streamdesk.ca/");
        }

        private void lblActiveTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.lblActiveTitle.Tag.ToString() != "about:blank")
            {
                this.SystemBrowser(this.lblActiveTitle.Tag.ToString());
            }
        }

        private void OpenChat(object senderTag, int chatMode)
        {
            string[] strArray = (string[])senderTag;
            if (strArray != null)
            {
                if (chatMode == 0)
                {
                    new frmChat(strArray[0], strArray[1]).Show();
                }
                else if (chatMode == 1)
                {
                    try
                    {
                        this.SystemBrowser("irc://" + StreamDeskDBControl.ChatEmbed[strArray[0]]["IRCServer"] + "/" + strArray[1]);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("      Your system does not appear to have a program" + Environment.NewLine + "      associated with the irc:// protocol." + Environment.NewLine + Environment.NewLine + "      The error given was:" + Environment.NewLine + "      " + exception.Message + Environment.NewLine + Environment.NewLine + "      You can use Web IRC by unchecking the \"use my " + Environment.NewLine + "      IRC client\" option in the preferences window.", "IRC Client Not Found");
                    }
                }
            }
        }

        private void pbChat_Click(object sender, EventArgs e)
        {
            this.OpenChat(this.pbChat.Tag, 0);
        }

        private void pbToggleStreams_Click(object sender, EventArgs e)
        {
            if (this.sContainer.SplitterDistance != 5)
            {
                this.sContainer.SplitterDistance = 5;
                this.sContainer.SplitterWidth = 1;
                this.sContainer.Panel1.Hide();
            }
            else
            {
                this.sContainer.SplitterDistance = 170;
                this.sContainer.SplitterWidth = 3;
                this.sContainer.Panel1.Show();
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.TopMost = false;
            new frmSettings().ShowDialog();
            if (Settings.Default.VideoTopMost)
            {
                base.TopMost = true;
            }
            if (Settings.Default.MinSize)
            {
                this.MinimumSize = new Size(550, 400);
            }
            else
            {
                this.MinimumSize = new Size(1, 1);
            }
        }

        private void ReadStreams()
        {
            try
            {
                this.tvStreams.Nodes.Clear();
                streamsToolStripMenuItem.DropDownItems.Clear();
                tvStreams.Nodes.AddRange(StreamDeskDBControl.GetStreamList().ToArray());
                streamsToolStripMenuItem.DropDownItems.AddRange(StreamDeskDBControl.GetStreamList_Menu(this).ToArray());
            }
            catch (Exception exception)
            {
            }
        }

        private void SystemBrowser(string url)
        {
            Process.Start(url.ToString());
        }

        public void streamClick(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            string[] tag = (string[])menu.Tag;
            WebClient wc = new WebClient();

            this.lblActiveTitle.Text = menu.Text;
            this.lblActiveTitle.Tag = tag[1];
            this.lblActiveDesc.Text = tag[8];

            if (wc.DownloadString(String.Format("http://127.0.0.1:9898/+is_stream_type/{0}", tag[3])) == "True")
            {
                webBrowser.Navigate(String.Format("http://127.0.0.1:9898/+stream/{0}/{1}", tag[3], tag[4]));
                this.Text = this.AppName + " > " + tag[0] + " > " + menu.Text;
            }
            else
            {
                MessageBox.Show("Unrecognized stream type " + tag[3] + "!", "Stream error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.Text = this.AppName;
            }

            if (tag[6] != "none")
            {
                string[] strArray2 = new string[] { tag[6], tag[7] };
                this.pbChat.Tag = strArray2;
                this.pbChat.Visible = true;
                if (strArray2[0] == "chat_justintv")
                {
                    this.pbChat.Text = "Chat via Justin.TV";
                    chatViaIRCClientToolStripMenuItem.Visible = false;
                }
                else
                {
                    this.pbChat.Text = "Chat via Web IRC";
                    chatViaIRCClientToolStripMenuItem.Visible = true;
                }
                toolStripDropDownButton1.Visible = true;
            }
            else
            {
                toolStripDropDownButton1.Visible = false;
            }
            if (Settings.Default.VideoResize)
            {
                int num = (this.tvStreams.Width + 20) + int.Parse(tag[2].Split('x')[0]);
                int num2 = (this.statusStrip1.Height + this.menuStrip1.Height + 0x22) + int.Parse(tag[2].Split('x')[1]);
                if (base.Width < num)
                {
                    base.Width = num;
                }
                if (base.Height < num2)
                {
                    base.Height = num2;
                }
            }
        }

        private void tvStreams_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string[] tag = (string[])e.Node.Tag;
                WebClient wc = new WebClient();

                if (tag[0] == "STREAM")
                {
                    this.lblActiveTitle.Text = e.Node.Text;
                    this.lblActiveTitle.Tag = tag[1];
                    this.lblActiveDesc.Text = tag[8];

                    if (wc.DownloadString(String.Format("http://127.0.0.1:9898/+is_stream_type/{0}", tag[3])) == "True")
                    {
                        webBrowser.Navigate(String.Format("http://127.0.0.1:9898/+stream/{0}/{1}", tag[3], tag[4]));
                        this.Text = this.AppName + " > " + e.Node.Parent.Text + " > " + e.Node.Text;
                    }
                    else
                    {
                        MessageBox.Show("Unrecognized stream type " + tag[3] + "!", "Stream error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.Text = this.AppName;
                    }

                    if (tag[6] != "none")
                    {
                        string[] strArray2 = new string[] { tag[6], tag[7] };
                        this.pbChat.Tag = strArray2;
                        if (strArray2[0] == "chat_justintv")
                        {
                            this.pbChat.Text = "Chat via Justin.TV";
                        }
                        else
                        {
                            this.pbChat.Text = "Chat via Web IRC";
                        }
                        toolStripDropDownButton1.Visible = true;
                    }
                    else
                    {
                        toolStripDropDownButton1.Visible = false;
                    }
                    if (Settings.Default.VideoResize)
                    {
                        int num = (this.tvStreams.Width + 20) + int.Parse(tag[2].Split('x')[0]);
                        int num2 = (this.statusStrip1.Height + this.menuStrip1.Height + 0x22) + int.Parse(tag[2].Split('x')[1]);
                        if (base.Width < num)
                        {
                            base.Width = num;
                        }
                        if (base.Height < num2)
                        {
                            base.Height = num2;
                        }
                    }
                }
            }
        }

        private void updateStreamListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("StreamDeskService");
            sc.ExecuteCommand(128);
            MessageBox.Show("Update command was sent to StreamDesk Service.", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void treeMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeMenuToolStripMenuItem.Checked == false) treeMenuToolStripMenuItem.Checked = true;
            else treeMenuToolStripMenuItem.Checked = false;
            sContainer.Panel1Collapsed = Reverse(treeMenuToolStripMenuItem.Checked);
            Settings.Default.ShowTreeView = treeMenuToolStripMenuItem.Checked;
        }

        private bool Reverse(bool p)
        {
            if (p) { return false; }
            else { return true; }
        }

        private void tvStreams_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void pbChat_MouseEnter(object sender, EventArgs e)
        {

        }

        private void refreshStreamListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ReadStreams();
        }

        private void lblActiveDesc_Click(object sender, EventArgs e)
        {

        }

        private void lblChatType_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void chatViaIRCClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenChat(this.pbChat.Tag, 1);
        }
    }
}

