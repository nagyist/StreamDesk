#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System.ServiceProcess;
using StreamDesk.AppCore;

#endregion

namespace StreamDesk.HTTPDataServer {
    internal partial class StreamDeskService : ServiceBase {
        private Server server;

        public StreamDeskService () {
            InitializeComponent ();
        }

        protected override void OnStart (string[] args) {
            StreamDeskDBControl.Initialize ();
            server = new Server ();
            server.Start ();
        }

        protected override void OnStop () {
            server.Stop ();
        }

        protected override void OnCustomCommand (int command) {
            if (command == 128) {
                StreamDeskDBControl.Update ();
            } else {
                base.OnCustomCommand (command);
            }
        }
    }
}