// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace StreamDesk
{
	[Register ("ChatWindowController")]
	partial class ChatWindowController
	{
		[Outlet]
		MonoMac.WebKit.WebView webBrowser { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (webBrowser != null) {
				webBrowser.Dispose ();
				webBrowser = null;
			}
		}
	}

	[Register ("ChatWindow")]
	partial class ChatWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
