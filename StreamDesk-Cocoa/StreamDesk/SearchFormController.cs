using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using StreamDesk.Managed.Database;

namespace StreamDesk {
    public partial class SearchFormController : MonoMac.AppKit.NSWindowController {
        #region Constructors
     
        // Called when created from unmanaged code
        public SearchFormController(IntPtr handle) : base(handle) {
            Initialize();
        }
     
        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public SearchFormController(NSCoder coder) : base(coder) {
            Initialize();
        }
     
        // Call to load from the XIB/NIB file
        public SearchFormController() : base("SearchForm") {
            Initialize();
        }
     
        // Shared initialization code
        void Initialize() {
        }
     
        #endregion
     
        //strongly typed window accessor
        public new SearchForm Window {
            get {
                return(SearchForm)base.Window;
            }
        }
     
        partial void okClicked(NSObject sender) {
            Window.PerformClose(this);
        }
     
            partial void performSearch(NSObject sender) {
            searchResults.DataSource = new SearchDataSource(((NSSearchField)sender).StringValue);
            searchResults.ReloadData();
        }
    }
 
    public class SearchDataSource : NSTableViewDataSource {
        List<Stream> mSearchResults = new List<Stream>();
     
        public SearchDataSource(string searchResults) {
            foreach (var database in Program.Instance.StreamDeskCoreInstance.ActiveDatabases) {
                mSearchResults.AddRange(database.Search(searchResults));
            }
        }
     
        public override int GetRowCount(NSTableView tableView) {
            return mSearchResults.Count;
        }
     
        public override NSObject GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, int row) {    
            if (row != -1)
                switch ((NSString)tableColumn.Identifier) {
                    case "Name":
                        return(NSString)mSearchResults [row].Name;
                    case "Tags":
                        return(NSString)mSearchResults [row].Tags;
                    case "Description":
                        return(NSString)mSearchResults [row].Description;
                    case "Database":
                        return(NSString)Program.Instance.StreamDeskCoreInstance.GetDatabaseStreamExistsIn(mSearchResults [row]).Name;
                }
         
            return null;
        }
    }
}

