using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace StreamDesk {
    public partial class AddDatabaseController : MonoMac.AppKit.NSWindowController {
        #region Constructors
        
        // Called when created from unmanaged code
        public AddDatabaseController(IntPtr handle) : base (handle) {
            Initialize();
        }
        
        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public AddDatabaseController(NSCoder coder) : base (coder) {
            Initialize();
        }
        
        // Call to load from the XIB/NIB file
        public AddDatabaseController() : base ("AddDatabase") {
            Initialize();
        }
        
        // Shared initialization code
        void Initialize() {
        }
        
        #endregion
        
        //strongly typed window accessor
        public new AddDatabase Window {
            get {
                return (AddDatabase)base.Window;
            }
        }
        
        partial void cancelClicked(NSObject sender) {
            ReturnValue = false;
            NSApplication.SharedApplication.EndSheet(Window);
            Window.OrderOut(this);
        }
        
        partial void okClicked(NSObject sender) {
            if(urlTextField.StringValue == String.Empty) {
                NSAlert.WithMessage("You need to have a URL entered!", "OK", null, null, "Please enter a URL before clicking OK").BeginSheet(Window);
                return;
            }
            ReturnValue = true;
            NSApplication.SharedApplication.EndSheet(Window);
            Window.OrderOut(this);
        }
        
        public bool ReturnValue { get; private set; }
        
        public string Url {
            get {
                return urlTextField.StringValue;
            }
        }
    }
}

