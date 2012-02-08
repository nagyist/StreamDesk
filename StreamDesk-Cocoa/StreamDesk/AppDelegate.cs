using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Collections.Generic;
using StreamDesk.Managed;

namespace StreamDesk {
    public partial class AppDelegate : NSApplicationDelegate {
        public StreamDeskCore StreamDeskCoreInstance;
        List<MainWindowController> mainWindowControllers = new List<MainWindowController> ();
        PrefFormController prefFormController;
        SearchFormController searchFormController;
     
        public AppDelegate () {
            Program.Instance = this;
        }
     
        public MainWindowController GetActiveMainWindowController () {
            //TODO: Right now this just returns the only window controller thats actually added
            //      but this needs to actually figure out the active main window controller
            //      so multi-window support can work.
            return mainWindowControllers [0];
        }
     
        public override void AwakeFromNib () {
            StreamDeskCoreInstance = new StreamDeskCore ();
         
            StreamDeskCoreInstance.Initialize ();
         
            RefreshStreamsMenu ();
        }
     
        public override void FinishedLaunching (NSObject notification) {
            var mainWindowController = new MainWindowController ();
            mainWindowController.Window.MakeKeyAndOrderFront (this);
         
            mainWindowControllers.Add (mainWindowController);
        }

        void HandleStreamDeskCoreInstanceDatabaseLoaded (object sender, EventArgs e) {
            RefreshStreamsMenu ();
        }
     
        public override NSApplicationTerminateReply ApplicationShouldTerminate (NSApplication sender) {
            StreamDeskCoreInstance.SettingsInstance.SaveSettings ();
         
            return NSApplicationTerminateReply.Now;
        }
     
        private void RefreshStreamsMenu () {
            streamsMenu.Submenu.RemoveAllItems ();

            foreach(var streamDeskDatabase in StreamDeskCoreInstance.ActiveDatabases) {
                var dbName = new NSMenuItem (streamDeskDatabase.Name);
                dbName.Submenu = new NSMenu ();
             
                foreach(var i in streamDeskDatabase.GenerateObjectDatabaseTags<StreamMenuItem>(null)) {
                    dbName.Submenu.AddItem (i);
                }

                streamsMenu.Submenu.AddItem (dbName);
            }
        }
     
        partial void openSearchBox (NSObject sender) {
            if (searchFormController == null) 
                searchFormController = new SearchFormController ();
            searchFormController.Window.MakeKeyAndOrderFront (this);
        }
     
        partial void viewStreamInformation (NSObject sender) {
            GetActiveMainWindowController ().ShowStreamInformationWindow ();
        }
     
        partial void openPrefs (NSObject sender) {
            if (prefFormController == null) 
                prefFormController = new PrefFormController ();
            prefFormController.Window.MakeKeyAndOrderFront (this);
        }
     
        partial void openWebChat (NSObject sender) {
         
        }
     
        internal void ShowViewMenu () {
            if (viewMenu.Hidden)
                viewMenu.Hidden = false;
        }
     
        partial void updateStreams (NSObject sender) {
            StreamDeskCoreInstance.Initialize ();
         
            RefreshStreamsMenu ();
        }
    }
}
