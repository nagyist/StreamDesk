// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace StreamDesk
{
	[Register("PrefFormController")]
	partial class PrefFormController
	{
		[Outlet]
		MonoMac.AppKit.NSTableView enabledDatabaseTableView { get; set; }

		[Action("addDatabase:")]
		partial void addDatabase(MonoMac.Foundation.NSObject sender);

		[Action("okClicked:")]
		partial void okClicked(MonoMac.Foundation.NSObject sender);

		[Action("removeDatabase:")]
		partial void removeDatabase(MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets()
		{
			if (enabledDatabaseTableView != null) {
				enabledDatabaseTableView.Dispose();
				enabledDatabaseTableView = null;
			}
		}
	}

	[Register("PrefForm")]
	partial class PrefForm
	{
		
		void ReleaseDesignerOutlets()
		{
		}
	}
}
