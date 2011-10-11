#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

#endregion

namespace FireIRC.Resources.Forms {
    public partial class UpdateNotifier : Form {
        private string updateXMLURL;
        private string url;

        public UpdateNotifier () {
            InitializeComponent ();
        }

        public string UpdateXMLURL {
            get { return updateXMLURL; }
            set { updateXMLURL = value; }
        }

        public Image ImageIcon {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        private void button1_Click (object sender, EventArgs e) {
            if (button1.Text == "<< Details") {
                Size = new Size (548, 190);
                button1.Text = "Details >>";
            } else if (button1.Text == "Details >>") {
                Size = new Size (548, 540);
                button1.Text = "<< Details";
            }
            CenterToScreen ();
        }

        private void UpdateNotifier_Load (object sender, EventArgs e) {
            Size = new Size (548, 190);
            CenterToScreen ();
        }

        public void CheckForUpdates (string application, string os) {
            var update = new XmlDocument ();
            update.Load (UpdateXMLURL);
            XmlNode appnode = update.SelectSingleNode ("/NasuTekUpdateNotifier/" + application);
            XmlNode updatenode = update.SelectSingleNode ("/NasuTekUpdateNotifier/" + application + "/" + os);
            Version currentver = GetType ().Assembly.GetName ().Version;
            var newver = new Version (updatenode.Attributes["version"].Value);
            button3.Visible = Reverse (Convert.ToBoolean (updatenode.Attributes["noskip"].Value));
            if (currentver < newver) {
                label1.Text = String.Format (label1.Text, appnode.Attributes["friendlyname"].Value);
                label2.Text = String.Format (label2.Text, currentver.ToString ());
                label3.Text = String.Format (label3.Text, newver.ToString ());
                webBrowser1.Navigate ("about:blank");
                webBrowser1.Document.Write (updatenode.InnerText);
                webBrowser1.Refresh ();
                url = updatenode.Attributes["url"].Value;
                ShowDialog ();
            }
        }

        private void button2_Click (object sender, EventArgs e) {
            Process.Start (url);
            Close ();
        }

        private bool Reverse (bool @in) {
            if (@in == true) return false;
            else return true;
        }
    }
}