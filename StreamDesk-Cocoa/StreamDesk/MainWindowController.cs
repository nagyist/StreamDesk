using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.Drawing;
using NasuTek.M3.Database;

namespace StreamDesk {
    public partial class MainWindowController : MonoMac.AppKit.NSWindowController {
        StreamInformationController streamInformationController;
        ChatWindowController chatWindowController;
        
        #region Constructors
     
        // Called when created from unmanaged code
        public MainWindowController(IntPtr handle) : base(handle) {
            Initialize();
        }
     
        // Called when created directly from a XIB file
       [Export("initWithCoder:")]
        public MainWindowController(NSCoder coder) : base(coder) {
            Initialize();
        }
     
        // Call to load from the XIB/NIB file
        public MainWindowController() : base("MainWindow") {
            Initialize();
        }
     
        // Shared initialization code
        void Initialize() {
            Window.Title = "No Stream Loaded";
#if DEBUG
            Window.Title += " (Debug Build)";
#endif
        }
     
        #endregion
     
        //strongly typed window accessor
        public new MainWindow Window {
            get {
                return(MainWindow)base.Window;
            }
        }
     
        public Stream ActiveStreamObject { get; private set; }

        public StreamDeskDatabase ActiveDatabase { get; private set; }
     
        public void OpenChatWindow() {
            if (chatWindowController == null)
                chatWindowController = new ChatWindowController();
         
            chatWindowController.Window.MakeKeyAndOrderFront(this);
            chatWindowController.SetChatWindow(ActiveStreamObject, ActiveDatabase);
        }
     
        public void NavigateToStream(Stream streamObject, StreamDeskDatabase database) {                
            Program.Instance.ShowViewMenu();
         	Program.Instance.ShowChatMenu(streamObject);
			
            ActiveStreamObject = streamObject;
            ActiveDatabase = database;
         
            Window.Title = streamObject.Name + " > " + streamObject.ProviderObject.Name;
#if DEBUG
            Window.Title += " (Debug Build)";
#endif
         
            if (streamObject.StreamEmbed == "url_browser" || streamObject.StreamEmbed == "url_custom")
                webBrowser.MainFrame.LoadRequest(new NSUrlRequest(new NSUrl(streamObject.GetStreamEmbedData("URL"))));
            else {
                Window.SetContentSize(streamObject.Size);
                webBrowser.MainFrame.LoadHtmlString(database.GetStream(streamObject), new NSUrl("http://example.org"));
            }
        }
     
        public void ShowStreamInformationWindow() {
            if (streamInformationController == null)
                streamInformationController = new StreamInformationController();
            
            NSApplication.SharedApplication.BeginSheet(streamInformationController.Window, Window);
            streamInformationController.LoadStreamInformation(ActiveStreamObject.Name, ActiveStreamObject.Tags, ActiveStreamObject.Web, ActiveStreamObject.Description);
        }
    }
}

