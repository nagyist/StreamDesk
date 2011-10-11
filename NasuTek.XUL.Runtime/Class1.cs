using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;

namespace NasuTek.XUL.Runtime
{
    public class XULRuntime
    {
        public static void Initialize()
        {
            RegistryKey regkey;
            regkey = Registry.LocalMachine.OpenSubKey(@"Software\NasuTek-Alliant Enterprises\XUL Runtime\1.8");
            string path = regkey.GetValue("InstallPath", "Not Installed").ToString();
            Xpcom.Initialize(path);
        }
    }
}
