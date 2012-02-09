// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace StreamDesk
{
	[Register ("ManageFavoritesController")]
	partial class ManageFavoritesController
	{
		[Outlet]
		MonoMac.AppKit.NSOutlineView favoritesOutlineView { get; set; }

		[Action ("addFolder:")]
		partial void addFolder (MonoMac.Foundation.NSObject sender);

		[Action ("move:")]
		partial void move (MonoMac.Foundation.NSObject sender);

		[Action ("okClicked:")]
		partial void okClicked (MonoMac.Foundation.NSObject sender);

		[Action ("removeButtonClicked:")]
		partial void removeButtonClicked (MonoMac.Foundation.NSObject sender);

		[Action ("rename:")]
		partial void rename (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (favoritesOutlineView != null) {
				favoritesOutlineView.Dispose ();
				favoritesOutlineView = null;
			}
		}
	}

	[Register ("ManageFavorites")]
	partial class ManageFavorites
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
