using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace StreamDesk {
    public class ActiveDatabasesDataSource : NSTableViewDataSource {
        public override int GetRowCount(NSTableView tableView) {
            return Program.Instance.StreamDeskCoreInstance.SettingsInstance.ActiveDatabases.Count;
        }
        
        public override NSObject GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, int row) {
             if (row != -1)
                return (NSString)Program.Instance.StreamDeskCoreInstance.SettingsInstance.ActiveDatabases[row];
            
            return null;
        }
    }
    
    public partial class ManageDatabasesController : MonoMac.AppKit.NSWindowController {
        AddDatabaseController addDatabaseController;
        
        #region Constructors
        
        // Called when created from unmanaged code
        public ManageDatabasesController(IntPtr handle) : base (handle) {
            Initialize();
        }
        
        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public ManageDatabasesController(NSCoder coder) : base (coder) {
            Initialize();
        }
        
        // Call to load from the XIB/NIB file
        public ManageDatabasesController() : base ("ManageDatabases") {
            Initialize();
        }
        
        // Shared initialization code
        void Initialize() {
        }
        
        #endregion
        
        public override void AwakeFromNib() {
            urlTableView.DataSource = new ActiveDatabasesDataSource();
            urlTableView.ReloadData();
        }
        
        //strongly typed window accessor
        public new ManageDatabases Window {
            get {
                return (ManageDatabases)base.Window;
            }
        }
        
        partial void okClicked(NSObject sender) {
            NSApplication.SharedApplication.EndSheet(Window);
            Window.OrderOut(this);
        }
        
        partial void removeUrl(NSObject sender) {
            Program.Instance.StreamDeskCoreInstance.SettingsInstance.ActiveDatabases.RemoveAt(urlTableView.SelectedRow);
            urlTableView.ReloadData();
        }
        
        partial void activeUrlChanged(NSObject sender) {
            removeButton.Enabled = urlTableView.SelectedRow != -1 && Program.Instance.StreamDeskCoreInstance.SettingsInstance.ActiveDatabases[urlTableView.SelectedRow] != "http://streamdesk.sf.net/streams.sdnx";
        }
        
        partial void addUrl(NSObject sender) {
            if (addDatabaseController == null)
                addDatabaseController = new AddDatabaseController();
            
            NSApplication.SharedApplication.BeginSheet(addDatabaseController.Window, Window, new NSAction(delegate {
                if(addDatabaseController.ReturnValue) {
                    Program.Instance.StreamDeskCoreInstance.SettingsInstance.ActiveDatabases.Add(addDatabaseController.Url);
                    urlTableView.ReloadData();
                }
            }));
        }
    }
}

