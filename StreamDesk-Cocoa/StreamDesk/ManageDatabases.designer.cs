// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace StreamDesk
{
	[Register ("ManageDatabasesController")]
	partial class ManageDatabasesController
	{
		[Outlet]
		MonoMac.AppKit.NSTableView urlTableView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton removeButton { get; set; }

		[Action ("okClicked:")]
		partial void okClicked (MonoMac.Foundation.NSObject sender);

		[Action ("removeUrl:")]
		partial void removeUrl (MonoMac.Foundation.NSObject sender);

		[Action ("activeUrlChanged:")]
		partial void activeUrlChanged (MonoMac.Foundation.NSObject sender);

		[Action ("addUrl:")]
		partial void addUrl (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (urlTableView != null) {
				urlTableView.Dispose ();
				urlTableView = null;
			}

			if (removeButton != null) {
				removeButton.Dispose ();
				removeButton = null;
			}
		}
	}

	[Register ("ManageDatabases")]
	partial class ManageDatabases
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
