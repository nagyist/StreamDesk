// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace StreamDesk
{
	[Register ("SearchFormController")]
	partial class SearchFormController
	{
		[Outlet]
		MonoMac.AppKit.NSTableView searchResults { get; set; }

		[Action ("okClicked:")]
		partial void okClicked (MonoMac.Foundation.NSObject sender);

		[Action ("performSearch:")]
		partial void performSearch (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (searchResults != null) {
				searchResults.Dispose ();
				searchResults = null;
			}
		}
	}

	[Register ("SearchForm")]
	partial class SearchForm
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
