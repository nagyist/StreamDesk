using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ExceptionHandler {
    public class Handler {
        public static void AttachHandler(Action<string[]> mainFunction, string[] args) {
            if (!Debugger.IsAttached) {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

                try {
                    mainFunction(args);
                } catch (Exception ex) {
                    new ExceptionDialog(ex).ShowDialog();
                }
            } else
                mainFunction(args);
        }
    }
}
