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

    public partial class frmMain : Form
    {
        private ToolStripMenuItem aboutToolStripMenuItem;
        private string AppName = "StreamDesk";
        private ContextMenuStrip cmStreamList;
        private IContainer components;
        private ToolStripMenuItem kComputerZoneToolStripMenuItem1;
        private Label lblActiveDesc;
        private LinkLabel lblActiveTitle;
        private Label lblChatType;
        private Button pbChat;
        private PictureBox pbToggleStreams;
        private Panel pnlInfo;
        private ToolStripMenuItem preferencesToolStripMenuItem;
        private SplitContainer sContainer;
        private ToolStripSeparator toolStripSeparator2;
        private ToolTip ttChat;
        private TreeView tvStreams;
        private ToolStripMenuItem updateStreamListToolStripMenuItem;
        private WebBrowser wbStream;

        public frmMain()
        {
            InitializeComponent();
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
                new frmUpdateStreamList(true).ShowDialog();
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
            this.pbChat.Visible = false;
            this.lblChatType.Visible = false;
            this.tvStreams.Height = this.sContainer.Panel1.Height;
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

        private void OpenChat(object senderTag)
        {
            string[] strArray = (string[]) senderTag;
            if (strArray != null)
            {
                if (strArray[0] == "justintv")
                {
                    new frmChat(strArray[0], strArray[1]).Show();
                }
                else if (Settings.Default.UseSystemIRC)
                {
                    try
                    {
                        if (strArray[0] != "geekshed")
                        {
                            this.SystemBrowser("irc://" + strArray[0] + "/" + strArray[1]);
                        }
                        else
                        {
                            this.SystemBrowser("irc://irc.geekshed.net/" + strArray[1]);
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("      Your system does not appear to have a program" + Environment.NewLine + "      associated with the irc:// protocol." + Environment.NewLine + Environment.NewLine + "      The error given was:" + Environment.NewLine + "      " + exception.Message + Environment.NewLine + Environment.NewLine + "      You can use Web IRC by unchecking the \"use my " + Environment.NewLine + "      IRC client\" option in the preferences window.", "IRC Client Not Found");
                    }
                }
                else
                {
                    new frmChat(strArray[0], strArray[1]).Show();
                }
            }
        }

        private void pbChat_Click(object sender, EventArgs e)
        {
            this.OpenChat(this.pbChat.Tag);
        }

        private void pbChat_MouseEnter(object sender, EventArgs e)
        {
            this.ttChat.SetToolTip(this.pbChat, "Chat to other people watching this stream");
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
            if (Settings.Default.UseSystemIRC && (this.lblChatType.Text == "via Web IRC"))
            {
                this.lblChatType.Text = "via IRC";
            }
            else if (!Settings.Default.UseSystemIRC && (this.lblChatType.Text == "via IRC"))
            {
                this.lblChatType.Text = "via Web IRC";
            }
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
                XmlDocument document = new XmlDocument();
                string filename = Application.UserAppDataPath + @"\streamlist.xml";
                document.Load(filename);
                foreach (XmlNode node in document.SelectNodes("/streams/provider"))
                {
                    TreeNode node2 = new TreeNode(node.Attributes["name"].Value);
                    node2.Name = node.Attributes["name"].Value;
                    string[] strArray = new string[9];
                    strArray[0] = "PROVIDER";
                    strArray[1] = node.Attributes["desc"].Value;
                    strArray[2] = node.Attributes["url"].Value;
                    node2.Tag = strArray;
                    this.tvStreams.Nodes.Add(node2);
                    foreach (XmlNode node3 in node.ChildNodes)
                    {
                        TreeNode node4 = new TreeNode(node3.Attributes["name"].Value);
                        node4.Name = node3.Attributes["name"].Value;
                        string[] strArray2 = new string[] { "STREAM", node3.ChildNodes[0].InnerText, node3.ChildNodes[0].Attributes[0].Value, node3.ChildNodes[1].InnerText, node3.ChildNodes[2].InnerText, node3.ChildNodes[3].InnerText, node3.ChildNodes[4].InnerText, node3.ChildNodes[5].Attributes[0].Value, node3.ChildNodes[5].InnerText };
                        node4.Tag = strArray2;
                        this.tvStreams.Nodes[node.Attributes["name"].Value].Nodes.Add(node4);
                    }
                }
                this.tvStreams.Focus();
            }
            catch (Exception exception)
            {
                if (MessageBox.Show("There is a problem with the stream dictionary: " + Environment.NewLine + Environment.NewLine + exception.Message + Environment.NewLine + Environment.NewLine + "Would you like to download a fresh copy?", "Stream Dictionary Parse Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    new frmUpdateStreamList(true).ShowDialog();
                    this.ReadStreams();
                }
            }
        }

        private void SystemBrowser(string url)
        {
            Process.Start(url.ToString());
        }

        private void tvStreams_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string[] tag = (string[]) e.Node.Tag;
                if (tag[0] == "STREAM")
                {
                    this.lblActiveTitle.Text = e.Node.Text;
                    string str = tag[4];
                    this.lblActiveTitle.Tag = str;
                    this.lblActiveDesc.Text = tag[3];
                    if (tag[2] == "embed_ustream")
                    {
                        this.wbStream.DocumentText = "<html><body style=\"padding: 0px; margin: 0px;\"><embed src=\"" + tag[1] + "\" width=\"100%\" height=\"100%\" flashvars=\"autoplay=true\" allowfullscreen=\"true\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" bgcolor=\"#000000\" ></embed></body></html>";
                        this.Text = this.AppName + " > " + e.Node.Parent.Text + " > " + e.Node.Text;
                    }
                    else if (tag[2] == "embed_justintv")
                    {
                        this.wbStream.DocumentText = "<html><body style=\"padding: 0px; margin: 0px;\"><object type=\"application/x-shockwave-flash\" \"\" height=\"100%\" width=\"100%\" id=\"live_embed_player_flash\" data=\"http://www.justin.tv/widgets/live_embed_player.swf?channel=" + tag[1] + "\" bgcolor=\"#000000\"><param name=\"allowFullScreen\" value=\"true\" /><param name=\"allowscriptaccess\" value=\"always\" /><param name=\"allownetworking\" value=\"all\" /><param name=\"movie\" value=\"http://www.justin.tv/widgets/live_embed_player.swf\" /><param name=\"flashvars\" value=\"channel=" + tag[1] + "&auto_play=false&start_volume=25\" /></object></body></html>";
                        this.Text = this.AppName + " > " + e.Node.Parent.Text + " > " + e.Node.Text;
                    }
                    else if (tag[2] == "embed_generic")
                    {
                        this.wbStream.DocumentText = "";
                        this.Text = this.AppName + " > " + e.Node.Parent.Text + " > " + e.Node.Text;
                    }
                    else if (tag[2] == "embed_stickam")
                    {
                        this.wbStream.DocumentText = "<html><body style=\"padding: 0px; margin: 0px;\"><embed src=\"" + tag[1] + "\" type=\"application/x-shockwave-flash\" width=\"100%\" height=\"100%\" scale=\"noscale\" allowScriptAccess=\"always\" allowFullScreen=\"true\"></embed></body></html>";
                        this.Text = this.AppName + " > " + e.Node.Parent.Text + " > " + e.Node.Text;
                    }
                    else if (tag[2] == "url")
                    {
                        this.wbStream.Navigate(tag[1]);
                        this.Text = this.AppName + " > " + e.Node.Parent.Text + " > " + e.Node.Text;
                    }
                    else
                    {
                        MessageBox.Show("Unrecognized stream type " + tag[2] + "!", "Stream error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.Text = this.AppName;
                    }
                    if (tag[7] != "none")
                    {
                        string[] strArray2 = new string[] { tag[7], tag[8] };
                        this.pbChat.Tag = strArray2;
                        this.pbChat.Visible = true;
                        if (strArray2[0] == "justintv")
                        {
                            this.lblChatType.Text = "via Justin.TV";
                        }
                        else if (Settings.Default.UseSystemIRC)
                        {
                            this.lblChatType.Text = "via IRC";
                        }
                        else
                        {
                            this.lblChatType.Text = "via Web IRC";
                        }
                        this.lblChatType.Visible = true;
                        this.tvStreams.Height = this.sContainer.Panel1.Height - 0x21;
                    }
                    else
                    {
                        this.tvStreams.Height = this.sContainer.Panel1.Height;
                        this.pbChat.Visible = false;
                        this.lblChatType.Visible = false;
                    }
                    if (Settings.Default.VideoResize)
                    {
                        int num = (this.tvStreams.Width + 20) + int.Parse(tag[5]);
                        int num2 = (this.pnlInfo.Height + 0x22) + int.Parse(tag[6]);
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
            new frmUpdateStreamList(false).ShowDialog();
            this.ReadStreams();
        }
    }
}

