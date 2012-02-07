using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using StreamDesk.Managed;
using System.Net;
using StreamDesk.Managed.Database;

namespace StreamDesk
{
	class MainClass
	{
		public static StreamDeskCore StreamDeskCoreInstance;
		internal static AppDelegate AppDelegateInstance;
		
		static void Main (string [] args)
		{
			StreamDeskCoreInstance = new StreamDeskCore();
			
			NSApplication.Init ();
			NSApplication.Main (args);
		}
	}
}	

