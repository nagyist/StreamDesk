using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace StreamDesk
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MainWindowController mainWindowController;
		
		public AppDelegate ()
		{
			MainClass.AppDelegateInstance = this;
		}

		public override void FinishedLaunching (NSObject notification)
		{
			mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
			
			RefreshStreamsMenu();
		}
		
		private void RefreshStreamsMenu()
        {
            streamsMenu.Submenu.RemoveAllItems();

            foreach (var streamDeskDatabase in MainClass.StreamDeskCoreInstance.ActiveDatabases)
            {
                var dbName = new NSMenuItem(streamDeskDatabase.Name);
				dbName.Submenu = new NSMenu();
				
                foreach (var i in streamDeskDatabase.GenerateObjectDatabaseTags<StreamMenuItem>(new object[] { mainWindowController }))
                {
                    dbName.Submenu.AddItem(i);
                }

                streamsMenu.Submenu.AddItem(dbName);
            }
        }
		
		partial void openSearchBox (NSObject sender)
		{
			var searchForm = new SearchFormController();
			searchForm.Window.MakeKeyAndOrderFront(this);
		}
		
		partial void viewStreamInformation (NSObject sender)
		{
			var streamInformation = new StreamInformationController();
			streamInformation.Window.MakeKeyAndOrderFront (this);
			streamInformation.LoadStreamInformation(mainWindowController.ActiveStreamObject.Name, mainWindowController.ActiveStreamObject.Tags, mainWindowController.ActiveStreamObject.Web, mainWindowController.ActiveStreamObject.Description);
			//streamInformation.ShowWindow(this);
		}
		
		partial void openPrefs (NSObject sender)
		{
			var prefForm = new PrefFormController();
			prefForm.Window.MakeKeyAndOrderFront(this);
		}
		
		partial void openWebChat (NSObject sender)
		{
			
		}
		
		internal void ShowViewMenu() {
			if(viewMenu.Hidden)
				viewMenu.Hidden = false;
		}
	}
}

