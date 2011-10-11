#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using FireIRC.Resources.Forms;
using StreamDesk.AppCore;
using StreamDesk.AppTools;
using StreamDesk.Properties;
using Skybound.Gecko;

#endregion

namespace StreamDesk {
    public partial class frmMain : Form {
        private string AppName = "StreamDesk";
        private IContainer components;
        private SplitContainer sContainer;
        private string streamHTML = "O_O";
        private ToolTip ttChat;
        private TreeView tvStreams;
        private GeckoWebBrowser webBrowser = new GeckoWebBrowser ();

        public frmMain () {
            InitializeComponent ();
            webBrowser.Dock = DockStyle.Fill;
            wbStream.Controls.Add (webBrowser);
        }

        public bool ShowFileMenu {
            set { fileToolStripMenuItem.Visible = value; }
        }

        private void aboutToolStripMenuItem_Click (object sender, EventArgs e) {
            new frmAbout ().ShowDialog ();
        }

        private void frmMain_FormClosing (object sender, FormClosingEventArgs e) {
/*
            Settings.Default.LastTop = base.Top;
            Settings.Default.LastLeft = base.Left;*/
            if (sContainer.SplitterDistance > 100) {
                Settings.Default.LastSidebar = sContainer.SplitterDistance;
            } else {
                Settings.Default.LastSidebar = 100;
            }
            Settings.Default.Save ();
        }

        private void frmMain_Load (object sender, EventArgs e) {
            sContainer.Panel1Collapsed = Reverse (Settings.Default.ShowTreeView);
            treeMenuToolStripMenuItem.Checked = Settings.Default.ShowTreeView;

            Text = AppName;
            sContainer.SplitterDistance = Settings.Default.LastSidebar;
            /*
            Rectangle workingArea = Screen.GetWorkingArea(this);
            if (Settings.Default.LastTop < (workingArea.Height - base.Height))
            {
                base.Top = Settings.Default.LastTop;
            }
            if (Settings.Default.LastLeft < (workingArea.Width - base.Width))
            {
                base.Left = Settings.Default.LastLeft;
            }
             */
            sContainer.SplitterWidth = 3;
            if (Settings.Default.GetStreamsAtStartup) {
                new Updator ().ShowDialog ();
            }
            base.TopMost = Settings.Default.VideoTopMost;
            toolStripDropDownButton1.Visible = false;
            ReadStreams ();
            var un = new UpdateNotifier ();
            un.ImageIcon = Resources._64v2;
            un.UpdateXMLURL = "http://streamdesk.ca/update.xml";
            un.CheckForUpdates ("StreamDesk", "Windows");
        }

        private void kComputerZoneToolStripMenuItem1_Click (object sender, EventArgs e) {
            SystemBrowser ("http://streamdesk.ca/");
        }

        private void lblActiveTitle_LinkClicked (object sender, LinkLabelLinkClickedEventArgs e) {
            if (lblActiveTitle.Tag.ToString () != "about:blank") {
                SystemBrowser (lblActiveTitle.Tag.ToString ());
            }
        }

        private void OpenChat (object senderTag, int chatMode) {
            var strArray = (string[]) senderTag;
            if (strArray != null) {
                if (chatMode == 0) {
                    new frmChat (strArray[0], strArray[1]).Show ();
                } else if (chatMode == 1) {
                    try {
                        SystemBrowser ("irc://" + strArray[2] + "/" + strArray[1]);
                    } catch (Exception exception) {
                        MessageBox.Show (
                            "Your system does not appear to have a program associated with the irc:// protocol.");
                    }
                }
            }
        }

        private void pbChat_Click (object sender, EventArgs e) {
            OpenChat (pbChat.Tag, 0);
        }

        private void pbToggleStreams_Click (object sender, EventArgs e) {
            if (sContainer.SplitterDistance != 5) {
                sContainer.SplitterDistance = 5;
                sContainer.SplitterWidth = 1;
                sContainer.Panel1.Hide ();
            } else {
                sContainer.SplitterDistance = 170;
                sContainer.SplitterWidth = 3;
                sContainer.Panel1.Show ();
            }
        }

        private void preferencesToolStripMenuItem_Click (object sender, EventArgs e) {
            base.TopMost = false;
            new frmSettings ().ShowDialog ();
            if (Settings.Default.VideoTopMost) {
                base.TopMost = true;
            }
        }

        private void ReadStreams () {
            try {
                tvStreams.Nodes.Clear ();
                streamsToolStripMenuItem.DropDownItems.Clear ();
                tvStreams.Nodes.AddRange (StreamDeskDBControl.GetStreamList ().ToArray ());
                streamsToolStripMenuItem.DropDownItems.AddRange (
                    StreamDeskDBControl.GetStreamList_Menu (this).ToArray ());
            } catch (Exception exception) {}
        }

        private void SystemBrowser (string url) {
            Process.Start (url.ToString ());
        }

        public void streamClick (object sender, EventArgs e) {
            var menu = (ToolStripMenuItem) sender;
            var tag = (string[]) menu.Tag;
            var wc = new WebClient ();

            lblActiveTitle.Text = menu.Text;
            lblActiveTitle.Tag = tag[1];
            lblActiveDesc.Text = tag[8];

            if (wc.DownloadString (String.Format ("http://127.0.0.1:9898/+is_stream_type/{0}", tag[3])) == "True") {
                webBrowser.Navigate (String.Format ("http://127.0.0.1:9898/+stream/{0}/{1}", tag[3], tag[4]));
                Text = tag[0] + " > " + menu.Text + " - " + AppName;
            } else {
                MessageBox.Show ("Unrecognized stream type " + tag[3] + "!", "Stream error", MessageBoxButtons.OK,
                                 MessageBoxIcon.Hand);
                Text = AppName;
            }

            if (tag[6] != "none") {
                var strArray2 = new string[] {
                                                 tag[6], tag[7], tag[9]
                                             };
                if (tag[8] == null) chatViaIRCClientToolStripMenuItem.Visible = false;
                else chatViaIRCClientToolStripMenuItem.Visible = true;

                pbChat.Tag = strArray2;
                pbChat.Visible = true;
                if (strArray2[0] == "chat_justintv") {
                    pbChat.Text = "Justin.TV";
                    chatViaIRCClientToolStripMenuItem.Visible = false;
                } else {
                    pbChat.Text = "Web Chat";
                    chatViaIRCClientToolStripMenuItem.Visible = true;
                }
                toolStripDropDownButton1.Visible = true;
            } else {
                toolStripDropDownButton1.Visible = false;
            }
            if (Settings.Default.VideoResize) {
                int getWebBoundsWidth;
                if (sContainer.Panel1Collapsed == true) {
                    getWebBoundsWidth = 0;
                } else {
                    getWebBoundsWidth = tvStreams.Width + 3;
                }
                int getWidth = int.Parse (tag[2].Split ('x')[0]);
                int getHeight = int.Parse (tag[2].Split ('x')[1]);
                Height = (getHeight + 46) + 36;
                Width = (getWidth + getWebBoundsWidth) + 16;
            }
        }

        private void tvStreams_NodeMouseClick (object sender, TreeNodeMouseClickEventArgs e) {
            var tag = (string[]) e.Node.Tag;
            if (tag.Length == 11) {
                if (tag[10] == "STREAM") {
                    var wc = new WebClient ();

                    lblActiveTitle.Text = e.Node.Text;
                    lblActiveTitle.Tag = tag[1];
                    lblActiveDesc.Text = tag[8];

                    if (wc.DownloadString (String.Format ("http://127.0.0.1:9898/+is_stream_type/{0}", tag[3])) ==
                        "True") {
                        webBrowser.Navigate (String.Format ("http://127.0.0.1:9898/+stream/{0}/{1}", tag[3], tag[4]));
                        Text = tag[0] + " > " + e.Node.Text + " - " + AppName;
                    } else {
                        MessageBox.Show ("Unrecognized stream type " + tag[3] + "!", "Stream error",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Hand);
                        Text = AppName;
                    }

                    if (tag[6] != "none") {
                        var strArray2 = new string[] {
                                                         tag[6], tag[7], tag[9]
                                                     };
                        if (tag[8] == null) chatViaIRCClientToolStripMenuItem.Visible = false;
                        else chatViaIRCClientToolStripMenuItem.Visible = true;

                        pbChat.Tag = strArray2;
                        pbChat.Visible = true;
                        if (strArray2[0] == "chat_justintv") {
                            pbChat.Text = "Justin.TV";
                            chatViaIRCClientToolStripMenuItem.Visible = false;
                        } else {
                            pbChat.Text = "Web Chat";
                            chatViaIRCClientToolStripMenuItem.Visible = true;
                        }
                        toolStripDropDownButton1.Visible = true;
                    } else {
                        toolStripDropDownButton1.Visible = false;
                    }
                    if (Settings.Default.VideoResize) {
                        int getWebBoundsWidth;
                        if (sContainer.Panel1Collapsed == true) {
                            getWebBoundsWidth = 0;
                        } else {
                            getWebBoundsWidth = tvStreams.Width + 3;
                        }
                        int getWidth = int.Parse (tag[2].Split ('x')[0]);
                        int getHeight = int.Parse (tag[2].Split ('x')[1]);
                        Height = (getHeight + 46) + 36;
                        Width = (getWidth + getWebBoundsWidth) + 16;
                    }
                }
            }
        }

        private void updateStreamListToolStripMenuItem_Click (object sender, EventArgs e) {
            new Updator ().ShowDialog ();
            ReadStreams ();
        }

        private void quitToolStripMenuItem_Click (object sender, EventArgs e) {
            Application.Exit ();
        }

        private void treeMenuToolStripMenuItem_Click (object sender, EventArgs e) {
            if (treeMenuToolStripMenuItem.Checked == false) treeMenuToolStripMenuItem.Checked = true;
            else treeMenuToolStripMenuItem.Checked = false;
            sContainer.Panel1Collapsed = Reverse (treeMenuToolStripMenuItem.Checked);
            Settings.Default.ShowTreeView = treeMenuToolStripMenuItem.Checked;
            if (treeMenuToolStripMenuItem.Checked == true) Width += tvStreams.Width + 3;
            else Width -= tvStreams.Width + 3;
        }

        private bool Reverse (bool p) {
            if (p) {
                return false;
            } else {
                return true;
            }
        }

        private void tvStreams_AfterSelect (object sender, TreeViewEventArgs e) {}

        private void pbChat_MouseEnter (object sender, EventArgs e) {}

        private void refreshStreamListToolStripMenuItem_Click (object sender, EventArgs e) {
            ReadStreams ();
        }

        private void lblActiveDesc_Click (object sender, EventArgs e) {}

        private void lblChatType_Click (object sender, EventArgs e) {}

        private void toolStripButton3_Click (object sender, EventArgs e) {}

        private void chatViaIRCClientToolStripMenuItem_Click (object sender, EventArgs e) {
            OpenChat (pbChat.Tag, 1);
        }

        private void lblActiveTitle_Click (object sender, EventArgs e) {
            if (lblActiveTitle.Tag != null) {
                SystemBrowser (lblActiveTitle.Tag.ToString ());
            }
        }

        private void webChatToolStripMenuItem_Click (object sender, EventArgs e) {
            new frmChat ("chat_nasutek", "streamdesk").Show ();
        }

        private void iRCChatToolStripMenuItem_Click (object sender, EventArgs e) {
            try {
                SystemBrowser ("irc://irc.nasutek.com/streamdesk");
            } catch (Exception exception) {
                MessageBox.Show (
                    String.Format ("Your system does not appear to have a program associated with the irc:// protocol."));
            }
        }

        private void newWindowToolStripMenuItem_Click (object sender, EventArgs e) {
            Process.Start (Application.ExecutablePath);
        }

        private void tvStreams_AfterSelect_1 (object sender, TreeViewEventArgs e) {}

        private void wbStream_Paint (object sender, PaintEventArgs e) {}
    }
}