#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.Net;
using System.Windows.Forms;

#endregion

namespace StreamDesk.AppTools {
    public partial class Updator : Form {
        public Updator () {
            InitializeComponent ();
        }

        private void Updator_Load (object sender, EventArgs e) {
            UpdateMe ();
        }

        private void wc_DownloadStringCompleted (object sender, DownloadStringCompletedEventArgs e) {
            if (e.Result == "Update Complete") {
                Close ();
            } else {
                if (
                    MessageBox.Show ("Update of stream database failed. Retry?", "StreamDesk",
                                     MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand) == DialogResult.Retry) {
                    UpdateMe ();
                } else {
                    Close ();
                }
            }
        }

        private void UpdateMe () {
            var wc = new WebClient ();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler (wc_DownloadStringCompleted);
            wc.DownloadStringAsync (new Uri ("http://localhost:9898/+update"));
        }
    }
}