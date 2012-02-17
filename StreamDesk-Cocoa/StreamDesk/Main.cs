using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using NasuTek.M3;
using System.Net;
using NasuTek.M3.Database;
using System.Threading;

namespace StreamDesk {
    class Program {
        internal static AppDelegate Instance;
     
        static void Main(string[] args) {      
            NSApplication.Init();
            NSApplication.Main(args);
        }
    }
}    

