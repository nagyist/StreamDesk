using System;
using MonoMac.AppKit;
using NasuTek.M3;
using System.Linq;
using NasuTek.M3.UiInterfaces;

namespace StreamDesk {
    public class StreamMenuItem : NSMenuItem, IObjectDatabaseTag {
        public StreamMenuItem() {
            SubItems = new System.Collections.Generic.List<IObjectDatabaseTag>();
            Activated += ActivateStreamMenuItem;
        }

        void ActivateStreamMenuItem(object sender, EventArgs e) {           
            switch (MediaType) {
                case NasuTek.M3.Database.MediaType.VideoStream:
                    Program.Instance.GetActiveMainWindowController().NavigateToStream(StreamObject, Database);

                    break;
                case NasuTek.M3.Database.MediaType.AudioStream:
                    break;
            }
        }

        #region IObjectDatabaseTag implementation
        public void CallSubItemsToProperArray() {
            if (SubItems.Count != 0)
                Submenu = new NSMenu();
         
            foreach (var i in SubItems.Cast<NSMenuItem>())
                this.Submenu.AddItem(i);
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

        public NasuTek.M3.Database.Stream StreamObject { get; set; }

        public NasuTek.M3.Database.MediaType MediaType { get; set; }

        public NasuTek.M3.Database.Provider ProviderObject { get; set; }

        public NasuTek.M3.Database.Provider ParentProviderObject { get; set; }

        public NasuTek.M3.Database.StreamDeskDatabase Database { get; set; }

        public object[] TagObject { get; set; }
        #endregion
    }
}

