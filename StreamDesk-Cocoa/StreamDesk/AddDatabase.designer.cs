// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace StreamDesk
{
	[Register ("AddDatabaseController")]
	partial class AddDatabaseController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField urlTextField { get; set; }

		[Action ("cancelClicked:")]
		partial void cancelClicked (MonoMac.Foundation.NSObject sender);

		[Action ("okClicked:")]
		partial void okClicked (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (urlTextField != null) {
				urlTextField.Dispose ();
				urlTextField = null;
			}
		}
	}

	[Register ("AddDatabase")]
	partial class AddDatabase
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
