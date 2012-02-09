// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace StreamDesk
{
	[Register ("AddNewFavoriteController")]
	partial class AddNewFavoriteController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField titleTextField { get; set; }

		[Action ("cancelClicked:")]
		partial void cancelClicked (MonoMac.Foundation.NSObject sender);

		[Action ("newFolder:")]
		partial void newFolder (MonoMac.Foundation.NSObject sender);

		[Action ("okClicked:")]
		partial void okClicked (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (titleTextField != null) {
				titleTextField.Dispose ();
				titleTextField = null;
			}
		}
	}

	[Register ("AddNewFavorite")]
	partial class AddNewFavorite
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
