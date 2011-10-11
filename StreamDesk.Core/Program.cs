#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using StreamDesk.HTTPDataServer;

#endregion

namespace StreamDesk {
    public static class Program {
        private static void Main (string[] args) {
            if (args.Length == 0) {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] {
                                                      new StreamDeskService ()
                                                  };
                ServiceBase.Run (ServicesToRun);
            } else {
                if (args[0] == "/i") {
                    Interaction.Shell (
                        Path.Combine (RuntimeEnvironment.GetRuntimeDirectory (), "InstallUtil.exe ") +
                        String.Format ("-i \"{0}\"", Application.ExecutablePath), AppWinStyle.Hide, true, -1);
                    new ServiceController ("StreamDeskService").Start ();
                } else if (args[0] == "/u") {
                    try {
                        var sc = new ServiceController ("StreamDeskService");
                        sc.Stop ();
                        sc.WaitForStatus (ServiceControllerStatus.Stopped);
                    } catch (Exception) {}
                    Interaction.Shell (
                        Path.Combine (RuntimeEnvironment.GetRuntimeDirectory (), "InstallUtil.exe ") +
                        String.Format ("-u \"{0}\"", Application.ExecutablePath), AppWinStyle.Hide, true, -1);
                }
            }
        }
    }
}