namespace StreamDesk
{
    using System;
    using System.Windows.Forms;
    using System.ServiceProcess;
    using StreamDesk.HTTPDataServer;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;
    using StreamDesk.AppCore;
    using System.Collections;
    using System.Configuration.Install;
using System.Diagnostics;

    public static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			    { 
			            new StreamDeskService() 
			    };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                if (args[0] == "/i")
                {
                    Process.Start(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "InstallUtil"), String.Format("-i \"{0}\"", Application.ExecutablePath));
                }
                else if (args[0] == "/u")
                {
                    Process.Start(Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "InstallUtil"), String.Format("-u \"{0}\"", Application.ExecutablePath));
                }
#if DEBUG
                else if(args[0]== "/x")
                {
                    Main(new string[] { }, true);
                }
#endif
            }
        }
        public static void Main(string[] args, bool launched)
        {
            if (args.Length == 0)
            {
                Run();
            }
            else
            {
#if DEBUG
                if (args[0] == "/d")
                {
                    StreamDeskDBControl.path = Path.Combine(Application.ExecutablePath, "streamdesk_debug.db");
                    StreamDeskDBControl.downloadpath = args[1];
                }
#endif
            }
        }

        private static void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            NasuTek.XUL.Runtime.XULRuntime.Initialize();
            Application.Run(new frmMain());
        }
    }
}