using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using StreamDesk.Managed;
using System.Net;
using StreamDesk.Managed.Database;
using System.Threading;

namespace StreamDesk
{
	class Program
	{
		internal static AppDelegate Instance;
		
		static void Main (string [] args)
		{		
			NSApplication.Init ();
			NSApplication.Main (args);
		}
	}
}	

