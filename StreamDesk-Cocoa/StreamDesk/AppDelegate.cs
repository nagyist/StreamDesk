using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Collections.Generic;
using NasuTek.M3;
using NasuTek.M3.Database;
using System.Linq;

namespace StreamDesk {
    public partial class AppDelegate : NSApplicationDelegate {
        public StreamDeskCore StreamDeskCoreInstance;
        List<MainWindowController> mainWindowControllers = new List<MainWindowController>();
        PrefFormController prefFormController;
        SearchFormController searchFormController;
        ManageFavoritesController manageFavoritesController;
        AddNewFavoriteController addNewFavoriteController;
        
        public AppDelegate() {
            Program.Instance = this;
        }
     
        public MainWindowController GetActiveMainWindowController() {
            //TODO: Right now this just returns the only window controller thats actually added
            //      but this needs to actually figure out the active main window controller
            //      so multi-window support can work.
            return mainWindowControllers[0];
        }
     
        public override void AwakeFromNib() {
            
            StreamDeskCoreInstance = new StreamDeskCore();
         
            StreamDeskCoreInstance.Initialize();
         
            RefreshMenu();
            RefreshStreamsMenu();
        }
     
        public override void FinishedLaunching(NSObject notification) {
            var mainWindowController = new MainWindowController();
            mainWindowController.Window.MakeKeyAndOrderFront(this);
         
            mainWindowControllers.Add(mainWindowController);
        }

        void HandleStreamDeskCoreInstanceDatabaseLoaded(object sender, EventArgs e) {
            RefreshStreamsMenu();
        }
     
        public override NSApplicationTerminateReply ApplicationShouldTerminate(NSApplication sender) {
            StreamDeskCoreInstance.SettingsInstance.SaveSettings();
         
            return NSApplicationTerminateReply.Now;
        }
     
        private void RefreshStreamsMenu() {
            streamsMenu.Submenu.RemoveAllItems();

            foreach (var streamDeskDatabase in StreamDeskCoreInstance.ActiveDatabases) {
                var dbName = new NSMenuItem(streamDeskDatabase.Name);
                dbName.Submenu = new NSMenu();
             
                foreach (var i in streamDeskDatabase.GenerateObjectDatabaseTags<StreamMenuItem>(null)) {
                    dbName.Submenu.AddItem(i);
                }

                streamsMenu.Submenu.AddItem(dbName);
            }
        }
     
        partial void openSearchBox(NSObject sender) {
            if (searchFormController == null) 
                searchFormController = new SearchFormController();
            
            NSApplication.SharedApplication.BeginSheet(searchFormController.Window, GetActiveMainWindowController().Window);
        }
     
        partial void viewStreamInformation(NSObject sender) {
            GetActiveMainWindowController().ShowStreamInformationWindow();
        }
     
        partial void openPrefs(NSObject sender) {
            if (prefFormController == null) 
                prefFormController = new PrefFormController();
            prefFormController.Window.MakeKeyAndOrderFront(this);
        }
     
        partial void openWebChat(NSObject sender) {      
			GetActiveMainWindowController().OpenChatWindow();
        }
     
        internal void ShowViewMenu() {
            if (viewMenu.Hidden)
                viewMenu.Hidden = false;
        }
     
		internal void ShowChatMenu(Stream stream) {
			if(stream.ChatEmbed != null && stream.ChatEmbed != "" && stream.ChatEmbed != "none")
	            if (chatMenu.Hidden)
	            	chatMenu.Hidden = false;
			else
				if (!chatMenu.Hidden)
	            	chatMenu.Hidden = true;
        }
		
        partial void updateStreams(NSObject sender) {
            StreamDeskCoreInstance.Initialize();
         
            RefreshStreamsMenu();
        }
        
        NSMenuItem addFavorite = new NSMenuItem("Add");
        NSMenuItem manageFavorites = new NSMenuItem("Manage");
        
        // MAJORHACK: Give me a damn break Apple/Mono Project/Whoever, WHY CANT I DEFINE A LOCAL VARABLE WITHOUT THE
        //            DAMN GARBAGE COLLECTOR CLEARING THE FENTOSECOND I AM DONE >:(
        List<NSMenuItem> menuItemsFavorite = new List<NSMenuItem>();
        
        List<Guid> favoriteStreamTag = new List<Guid>();
        private void RefreshMenu()
        {
            manageFavorites.Activated += HandleManageFavoritesActivated;
            addFavorite.Activated += HandleAddFavoriteActivated;
            favorites.Submenu.RemoveAllItems();
            menuItemsFavorite.Clear();
            favoriteStreamTag.Clear();
            
            favorites.Submenu.AddItem(addFavorite);
            favorites.Submenu.AddItem(manageFavorites);
            favorites.Submenu.AddItem(NSMenuItem.SeparatorItem);
            
            RefreshMenu(Program.Instance.StreamDeskCoreInstance.SettingsInstance.FavoritesRoot, favorites);
        }

        void HandleAddFavoriteActivated (object sender, EventArgs e) {
            if (addNewFavoriteController == null) 
                addNewFavoriteController = new AddNewFavoriteController();
            
            NSApplication.SharedApplication.BeginSheet(addNewFavoriteController.Window, GetActiveMainWindowController().Window);
        }

        void HandleManageFavoritesActivated (object sender, EventArgs e) {
            if (manageFavoritesController == null) 
                manageFavoritesController = new ManageFavoritesController();
            
            NSApplication.SharedApplication.BeginSheet(manageFavoritesController.Window, GetActiveMainWindowController().Window);
        }
        
        private void RefreshMenu(FavoritesFolder folder, NSMenuItem menuItem)
        {
            if(menuItem.HasSubmenu != true && (folder.SubFolders.Count != 0 || folder.Favorites.Count != 0))
                menuItem.Submenu = new NSMenu();
            
            foreach (FavoritesFolder favoritesFolder in folder.SubFolders)
            {
                var newMenuItem = new NSMenuItem(favoritesFolder.Name);
                menuItemsFavorite.Add(newMenuItem);
                RefreshMenu(favoritesFolder, newMenuItem);
                menuItem.Submenu.AddItem(newMenuItem);
            }

            foreach (Favorite favorite in folder.Favorites)
            {
                var newMenuItem = new NSMenuItem(favorite.Name)
                {
                    Tag = favoriteStreamTag.Count,
                };
                favoriteStreamTag.Add(favorite.Id);
                menuItemsFavorite.Add(newMenuItem);
                    
                newMenuItem.Activated += HandleNewMenuItemActivated;
                menuItem.Submenu.AddItem(newMenuItem);
            }
        }

        void HandleNewMenuItemActivated (object sender, EventArgs e) {
            var guid = favoriteStreamTag[((NSMenuItem)sender).Tag];
            var database = Program.Instance.StreamDeskCoreInstance.ActiveDatabases.Where(v=> v.GetStreamObject(guid) != null).FirstOrDefault();
            
            if(database != null)
                GetActiveMainWindowController().NavigateToStream(database.GetStreamObject(guid), database);
        }
    }
}
