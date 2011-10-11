#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace StreamDesk {
    public partial class frmAbout : Form {
        public frmAbout () {
            InitializeComponent ();
        }

        private void frmAbout_Load (object sender, EventArgs e) {
            label1.Text = String.Format (label1.Text, Assembly.GetExecutingAssembly ().GetName ().Version.ToString ());
            Text = String.Format (Text, Assembly.GetExecutingAssembly ().GetName ().Version.ToString ());
        }

        private void linkLabel2_LinkClicked (object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start ("http://antiweasel.org/");
        }

        private void linkLabel1_LinkClicked (object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start ("http://streamdesk.ca/");
        }

        private void button1_Click (object sender, EventArgs e) {
            Close ();
        }

        private void pictureBox1_Click (object sender, EventArgs e) {}

        private void linkLabel1_LinkClicked_1 (object sender, LinkLabelLinkClickedEventArgs e) {
            new frmCredits ().ShowDialog ();
        }

        private void linkLabel2_LinkClicked_1 (object sender, LinkLabelLinkClickedEventArgs e) {
            new License ().ShowDialog ();
        }
    }
}