#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.Windows.Forms;
using Skybound.Gecko;

#endregion

namespace StreamDesk {
    public partial class frmChat : Form {
        private readonly string[] data;
        private readonly GeckoWebBrowser webBrowser = new GeckoWebBrowser ();

        public frmChat (string chatServer, string chatChannel) {
            InitializeComponent ();
            webBrowser.Dock = DockStyle.Fill;
            wbWebIRC.Controls.Add (webBrowser);
            data = new[] {
                             chatServer, chatChannel
                         };
        }

        private void frmChat_Load (object sender, EventArgs e) {
            webBrowser.Navigate (String.Format ("http://127.0.0.1:9898/+chat/{0}/{1}", data[0], data[1]));
        }
    }
}