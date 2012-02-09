using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace StreamDesk {
    public partial class AddNewFavoriteController : MonoMac.AppKit.NSWindowController {
        #region Constructors
        
        // Called when created from unmanaged code
        public AddNewFavoriteController(IntPtr handle) : base (handle) {
            Initialize();
        }
        
        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public AddNewFavoriteController(NSCoder coder) : base (coder) {
            Initialize();
        }
        
        // Call to load from the XIB/NIB file
        public AddNewFavoriteController() : base ("AddNewFavorite") {
            Initialize();
        }
        
        // Shared initialization code
        void Initialize() {
        }
        
        #endregion
        
        //strongly typed window accessor
        public new AddNewFavorite Window {
            get {
                return (AddNewFavorite)base.Window;
            }
        }
        
        partial void okClicked(NSObject sender) {
            NSApplication.SharedApplication.EndSheet(Window);
            Window.OrderOut(this);
        }
        
        partial void cancelClicked(NSObject sender) {
            NSApplication.SharedApplication.EndSheet(Window);
            Window.OrderOut(this);
        }
        
        partial void newFolder(NSObject sender) {
            
        }
    }
}

