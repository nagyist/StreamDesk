#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.Windows.Forms;
using StreamDesk.Properties;

#endregion

namespace StreamDesk.AppTools {
    public partial class frmSettings : Form {
        public frmSettings () {
            InitializeComponent ();
        }

        private void button1_Click (object sender, EventArgs e) {
            Settings.Default.Save ();
            base.Close ();
        }

        private void cbAlwaysOnTop_CheckedChanged (object sender, EventArgs e) {
            Settings.Default.VideoTopMost = cbAlwaysOnTop.Checked;
        }

        private void cbResize_CheckedChanged (object sender, EventArgs e) {
            Settings.Default.VideoResize = cbResize.Checked;
        }

        private void cbUpdateOnStartup_CheckedChanged (object sender, EventArgs e) {
            Settings.Default.GetStreamsAtStartup = cbUpdateOnStartup.Checked;
        }

        private void frmSettings_Load (object sender, EventArgs e) {
            cbUpdateOnStartup.Checked = Settings.Default.GetStreamsAtStartup;
            cbAlwaysOnTop.Checked = Settings.Default.VideoTopMost;
            cbResize.Checked = Settings.Default.VideoResize;
        }

        private void btnResetDict_Click (object sender, EventArgs e) {}

        private void btnAdvanced_Click (object sender, EventArgs e) {}
    }
}