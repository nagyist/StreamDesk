using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using StreamDesk.Managed;
using System.Diagnostics;

namespace StreamDesk
{
	public class ActiveDatabases : NSTableViewDataSource {
		public override int GetRowCount (NSTableView tableView)
		{
			return MainClass.StreamDeskCoreInstance.ActiveDatabases.Count;
		}
		
		public override NSObject GetObjectValue (NSTableView tableView, NSTableColumn tableColumn, int row)
		{	
			if(row != -1)
				switch((NSString)tableColumn.Identifier) {
					case "Name":
						return new NSString(MainClass.StreamDeskCoreInstance.ActiveDatabases[row].Name);
					case "Vendor":
						return new NSString(MainClass.StreamDeskCoreInstance.ActiveDatabases[row].Vendor);
					case "Description":
						return new NSString(MainClass.StreamDeskCoreInstance.ActiveDatabases[row].Description);
				}
			
		    return null;
		}
		
		public override void SetObjectValue (NSTableView tableView, NSObject theObject, NSTableColumn tableColumn, int row)
		{
			//Just do nothing, heh
			//base.SetObjectValue (tableView, theObject, tableColumn, row);
		}
	}
	
	public partial class PrefFormController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors
		
		// Called when created from unmanaged code
		public PrefFormController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public PrefFormController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public PrefFormController () : base ("PrefForm")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			
		}
		
		#endregion
		
		public override void AwakeFromNib ()
		{
			enabledDatabaseTableView.DataSource = new ActiveDatabases();
			enabledDatabaseTableView.ReloadData();
		}
		
		partial void addDatabase (NSObject sender)
		{
			
		}
		
		partial void removeDatabase (NSObject sender)
		{
			
		}
		
		//strongly typed window accessor
		public new PrefForm Window {
			get {
				return (PrefForm)base.Window;
			}
		}
		
		partial void okClicked (NSObject sender) {
			Window.PerformClose(this);
		}
	}
}

