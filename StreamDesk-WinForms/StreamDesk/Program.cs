#region Licensing Information
/***************************************************************************************************
 * NasuTek StreamDesk
 * Copyright © 2007-2012 NasuTek Enterprises
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 ***************************************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using ExceptionHandler;
using StreamDesk.Managed;

namespace StreamDesk {
    internal static class Program {
        internal static StreamDeskCore Database { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] private static void Main(string[] args) {
            Handler.AttachHandler(a => {
                Application.SetCompatibleTextRenderingDefault(false);
                Application.EnableVisualStyles();

                // TODO: Remove me later on after implimentation of new updater engine

                #region Obsolete Code
/*
                string newVer = new WebClient().DownloadString("http://streamdesk.ca/version.txt");

                if (new Version(newVer) > Assembly.GetExecutingAssembly().GetName().Version) {
                    if (MessageBox.Show("A new version of StreamDesk is Available.\n\nOld Version: " + Assembly.GetExecutingAssembly().GetName().Version + "\nNew Version: " + newVer + "\n\nClick yes to go to http://streamdesk.ca to update.", "StreamDesk", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                            == DialogResult.Yes) {
                        Process.Start("http://streamdesk.ca/pages/windows-linux.php");
                        return;
                    }
                }
*/
                #endregion

                Database = new StreamDeskCore();

                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamDesk")))
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamDesk"));

                StreamDeskSettings.OpenSettings();

                Application.Run(new LoadDatabases());
                StreamDeskSettings.Instance.SaveSettings();
            }, args);
        }
    }
}
