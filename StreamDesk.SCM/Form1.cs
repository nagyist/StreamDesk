#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;
using Microsoft.VisualBasic;

#endregion

namespace StreamDesk.SCM {
    public partial class Form1 : Form {
        private readonly ServiceController sc = new ServiceController ("StreamDeskService");

        public Form1 () {
            InitializeComponent ();
            Status ();
        }

        private void Status () {
            try {
                install.Enabled = false;
                label1.Enabled = false;
                if (sc.Status == ServiceControllerStatus.Running) {
                    start.Enabled = false;
                    label3.Enabled = false;
                    stop.Enabled = true;
                    label4.Enabled = true;
                    restart.Enabled = true;
                    label5.Enabled = true;
                }
                if (sc.Status == ServiceControllerStatus.Stopped) {
                    start.Enabled = true;
                    label3.Enabled = true;
                    stop.Enabled = false;
                    label4.Enabled = false;
                    restart.Enabled = false;
                    label5.Enabled = false;
                }
            } catch (Exception) {
                install.Enabled = true;
                label1.Enabled = true;
                uninstall.Enabled = false;
                label2.Enabled = false;
                panel1.Enabled = false;
            }
        }

        private void start_Click (object sender, EventArgs e) {
            sc.Start ();
            sc.WaitForStatus (ServiceControllerStatus.Running);
            Status ();
        }

        private void stop_Click (object sender, EventArgs e) {
            sc.Stop ();
            sc.WaitForStatus (ServiceControllerStatus.Stopped);
            Status ();
        }

        private void restart_Click (object sender, EventArgs e) {
            sc.Stop ();
            sc.WaitForStatus (ServiceControllerStatus.Stopped);
            sc.Start ();
            sc.WaitForStatus (ServiceControllerStatus.Running);
            Status ();
        }

        private void install_Click (object sender, EventArgs e) {
            Interaction.Shell (Path.Combine (Application.StartupPath, "StreamDesk.Core.exe") + " /i", AppWinStyle.Hide,
                               true, -1);
            install.Enabled = false;
            label1.Enabled = false;
            panel1.Enabled = true;
            start.Enabled = false;
            uninstall.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = false;
            stop.Enabled = true;
            label4.Enabled = true;
            restart.Enabled = true;
            label5.Enabled = true;
        }

        private void uninstall_Click (object sender, EventArgs e) {
            Interaction.Shell (Path.Combine (Application.StartupPath, "StreamDesk.Core.exe") + " /u", AppWinStyle.Hide,
                               true, -1);
            install.Enabled = true;
            label1.Enabled = true;
            uninstall.Enabled = false;
            label2.Enabled = false;
            panel1.Enabled = false;
        }
    }
}