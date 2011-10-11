#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;
using NasuTek.XUL.Runtime;

#endregion

namespace StreamDesk {
    internal static class Program {
        [STAThread] private static void Main (string[] args) {
            var sc = new ServiceController ("StreamDeskService");
            try {
                if (sc.Status == ServiceControllerStatus.Running) {
                    Application.EnableVisualStyles ();
                    Application.SetCompatibleTextRenderingDefault (false);
                    XULRuntime.Initialize ();
                    Application.Run (new frmMain ());
                } else {
                    if (
                        MessageBox.Show (
                            "The StreamDesk HTTP Core Service is not started. Launch StreamDesk SCM to start it?",
                            "StreamDesk", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        Process.Start (Path.Combine (Application.StartupPath, "StreamDesk.SCM.exe"));
                    }
                }
            } catch (InvalidOperationException) {
                if (
                    MessageBox.Show (
                        "The StreamDesk HTTP Core Service is not installed. Launch StreamDesk SCM to install it?",
                        "StreamDesk", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    Process.Start (Path.Combine (Application.StartupPath, "StreamDesk.SCM.exe"));
                }
            }
        }
    }
}