// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace StreamDesk
{
	[Register ("StreamInformationController")]
	partial class StreamInformationController
	{
		[Outlet]
		MonoMac.AppKit.NSTextView description { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField name { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField siteUrl { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField tags { get; set; }

		[Action ("okClicked:")]
		partial void okClicked (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (description != null) {
				description.Dispose ();
				description = null;
			}

			if (name != null) {
				name.Dispose ();
				name = null;
			}

			if (siteUrl != null) {
				siteUrl.Dispose ();
				siteUrl = null;
			}

			if (tags != null) {
				tags.Dispose ();
				tags = null;
			}
		}
	}

	[Register ("StreamInformation")]
	partial class StreamInformation
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
