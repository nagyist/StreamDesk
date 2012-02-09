using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace StreamDesk {
    public partial class ManageFavoritesController : MonoMac.AppKit.NSWindowController {
        #region Constructors
        
        // Called when created from unmanaged code
        public ManageFavoritesController(IntPtr handle) : base (handle) {
            Initialize();
        }
        
        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public ManageFavoritesController(NSCoder coder) : base (coder) {
            Initialize();
        }
        
        // Call to load from the XIB/NIB file
        public ManageFavoritesController() : base ("ManageFavorites") {
            Initialize();
        }
        
        // Shared initialization code
        void Initialize() {
        }
        
        #endregion
        
        //strongly typed window accessor
        public new ManageFavorites Window {
            get {
                return (ManageFavorites)base.Window;
            }
        }
        
        partial void addFolder(NSObject sender) {
            
        }
        
        partial void move(NSObject sender) {
            
        }
        
        partial void okClicked(NSObject sender) {
            NSApplication.SharedApplication.EndSheet(Window);
            Window.OrderOut(this);
        }
        
        partial void removeButtonClicked(NSObject sender) {
            
        }
        
        partial void rename(NSObject sender) {
            
        }
    }
}

