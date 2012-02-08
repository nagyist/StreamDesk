using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using StreamDesk.Managed.Database;

namespace StreamDesk {
    public partial class ChatWindowController : MonoMac.AppKit.NSWindowController {
     #region Constructors
     
        // Called when created from unmanaged code
        public ChatWindowController (IntPtr handle) : base (handle) {
            Initialize ();
        }
     
        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public ChatWindowController (NSCoder coder) : base (coder) {
            Initialize ();
        }
     
        // Call to load from the XIB/NIB file
        public ChatWindowController () : base ("ChatWindow") {
            Initialize ();
        }
     
        // Shared initialization code
        void Initialize () {
        }
     
     #endregion
     
        public void SetChatWindow (Stream stream, StreamDeskDatabase database) {
            webBrowser.MainFrame.LoadHtmlString ((NSString)database.GetChat (stream), new NSUrl ("http://example.com"));
        }
     
        //strongly typed window accessor
        public new ChatWindow Window {
            get {
                return (ChatWindow)base.Window;
            }
        }
    }
}

