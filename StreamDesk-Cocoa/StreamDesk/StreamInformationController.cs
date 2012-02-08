using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace StreamDesk
{
	public partial class StreamInformationController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors
		
		// Called when created from unmanaged code
		public StreamInformationController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public StreamInformationController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public StreamInformationController () : base ("StreamInformation")
		{
			Initialize ();
		}
		
		public void LoadStreamInformation(string nameString, string tagsString, string urlString, string descriptionString) {
			name.StringValue = nameString;
			tags.StringValue = tagsString;
			siteUrl.StringValue = urlString;
			description.InsertText(new NSString(descriptionString));
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}
		
		#endregion
		
		//strongly typed window accessor
		public new StreamInformation Window {
			get {
				return (StreamInformation)base.Window;
			}
		}
		
		partial void okClicked (NSObject sender)
		{
			Window.PerformClose(this);
		}
	}
}

