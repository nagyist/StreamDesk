// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace StreamDesk
{
	[Register ("AppDelegate")]
	partial class AppDelegate
	{
		[Outlet]
		MonoMac.AppKit.NSMenuItem streamsMenu { get; set; }

		[Outlet]
		MonoMac.AppKit.NSMenuItem chatMenu { get; set; }

		[Outlet]
		MonoMac.AppKit.NSMenuItem viewMenu { get; set; }

		[Action ("openSearchBox:")]
		partial void openSearchBox (MonoMac.Foundation.NSObject sender);

		[Action ("viewStreamInformation:")]
		partial void viewStreamInformation (MonoMac.Foundation.NSObject sender);

		[Action ("openPrefs:")]
		partial void openPrefs (MonoMac.Foundation.NSObject sender);

		[Action ("openWebChat:")]
		partial void openWebChat (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (streamsMenu != null) {
				streamsMenu.Dispose ();
				streamsMenu = null;
			}

			if (chatMenu != null) {
				chatMenu.Dispose ();
				chatMenu = null;
			}

			if (viewMenu != null) {
				viewMenu.Dispose ();
				viewMenu = null;
			}
		}
	}
}
