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
using System.Diagnostics;
using System.Threading;
using System.Net.Sockets;
using System.Net;

#endregion

namespace StreamDesk
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Process[] processes = Process.GetProcessesByName("StreamDesk.Core");
                if (processes.Length == 1)
                {
                    Server server = new Server();
                    server.Start();
                }
                else { }
            }
            else
            {
                if (args[0] == "/kill")
                {
                    Process[] processes = Process.GetProcessesByName("StreamDesk.Core");
                    foreach (Process p in processes)
                    {
                        p.Kill();
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}