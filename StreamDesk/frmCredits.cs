#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.Windows.Forms;

#endregion

namespace StreamDesk {
    public partial class frmCredits : Form {
        public frmCredits () {
            InitializeComponent ();
        }

        private void button1_Click (object sender, EventArgs e) {
            Close ();
        }
    }
}