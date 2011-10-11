using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using StreamDesk.AppCore;

namespace StreamDesk.HTTPDataServer
{
    partial class StreamDeskService : ServiceBase
    {
        Server server;
        public StreamDeskService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StreamDeskDBControl.Initialize();
            server = new Server();
            server.Start();
        }

        protected override void OnStop()
        {
            server.Stop();
        }

        protected override void OnCustomCommand(int command)
        {
            if (command == 128)
            {
                StreamDeskDBControl.Update();
            }
            else
            {
                base.OnCustomCommand(command);
            }
        }
    }
}
