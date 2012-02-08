using System;
using MonoMac.AppKit;
using StreamDesk.Managed;
using System.Linq;

namespace StreamDesk {
    public class StreamMenuItem : NSMenuItem, IObjectDatabaseTag {
        public StreamMenuItem () {
            SubItems = new System.Collections.Generic.List<IObjectDatabaseTag> ();
            Activated += ActivateStreamMenuItem;
        }

        void ActivateStreamMenuItem (object sender, EventArgs e) {           
            switch (MediaType) {
                case StreamDesk.Managed.Database.MediaType.VideoStream:
                    Program.Instance.GetActiveMainWindowController ().NavigateToStream (StreamObject, Database);

                    break;
                case StreamDesk.Managed.Database.MediaType.AudioStream:
                    break;
            }
        }

        #region IObjectDatabaseTag implementation
        public void CallSubItemsToProperArray () {
            if (SubItems.Count != 0)
                Submenu = new NSMenu ();
         
            foreach(var i in SubItems.Cast<NSMenuItem>())
                this.Submenu.AddItem (i);
        }

        public System.Collections.Generic.List<IObjectDatabaseTag> SubItems { get; private set; }

        public string MenuTitle {
            get {
                return Title;
            }
            set {
                Title = value;
            }
        }

        public bool IsProvider { get; set; }

        public bool IsPinned { get; set; }

        public StreamDesk.Managed.Database.Stream StreamObject { get; set; }

        public StreamDesk.Managed.Database.MediaType MediaType { get; set; }

        public StreamDesk.Managed.Database.Provider ProviderObject { get; set; }

        public StreamDesk.Managed.Database.Provider ParentProviderObject { get; set; }

        public StreamDesk.Managed.Database.StreamDeskDatabase Database { get; set; }

        public object[] TagObject { get; set; }
        #endregion
    }
}

