using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Collections.Generic;

namespace StreamDesk
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		List<MainWindowController> mainWindowControllers = new List<MainWindowController>();
		
		PrefFormController prefFormController;
		SearchFormController searchFormController;
		
		public AppDelegate ()
		{
			MainClass.AppDelegateInstance = this;
		}
		
		public MainWindowController GetActiveMainWindowController() 
		{
			return mainWindowControllers[0];
		}
		
		public override void FinishedLaunching (NSObject notification)
		{
			var mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
			
			mainWindowControllers.Add(mainWindowController);
			                          
			RefreshStreamsMenu();
		}
		
		private void RefreshStreamsMenu()
        {
            streamsMenu.Submenu.RemoveAllItems();

            foreach (var streamDeskDatabase in MainClass.StreamDeskCoreInstance.ActiveDatabases)
            {
                var dbName = new NSMenuItem(streamDeskDatabase.Name);
				dbName.Submenu = new NSMenu();
				
                foreach (var i in streamDeskDatabase.GenerateObjectDatabaseTags<StreamMenuItem>(null))
                {
                    dbName.Submenu.AddItem(i);
                }

                streamsMenu.Submenu.AddItem(dbName);
            }
        }
		
		partial void openSearchBox (NSObject sender)
		{
			if(searchFormController == null) 
				searchFormController = new SearchFormController();
			searchFormController.Window.MakeKeyAndOrderFront(this);
		}
		
		partial void viewStreamInformation (NSObject sender)
		{
			GetActiveMainWindowController().ShowStreamInformationWindow();
		}
		
		partial void openPrefs (NSObject sender)
		{
			if(prefFormController == null) 
				prefFormController = new PrefFormController();
			prefFormController.Window.MakeKeyAndOrderFront(this);
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

