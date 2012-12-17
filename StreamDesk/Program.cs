#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Developers of the StreamDesk Project">
//      Copyright (C) 2011 Developers of the StreamDesk Project.
//          Core Developers/Maintainer: NasuTek Enterprises/Michael Manley
//          Trademark/GUI Designer/Co-Maintainer: KtecK
//          Additional Developers and Contributors are in the DEVELOPERS.txt
//          file
//
//      Licensed under the Apache License, Version 2.0 (the "License");
//      you may not use this file except in compliance with the License.
//      You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//      Unless required by applicable law or agreed to in writing, software
//      distributed under the License is distributed on an "AS IS" BASIS,
//      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//      See the License for the specific language governing permissions and
//      limitations under the License.
// </copyright>
// <summary>
//      Program Starting Point and Globals
// </summary>
//----------------------------------------------------------------------------------
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
using StreamDesk.Core;

namespace StreamDesk {
    internal static class Program {
        internal static StreamDeskDatabase Database { get; set; }
        internal static MainMDIForm MainForm { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] private static void Main(string[] args) {
            Handler.AttachHandler(a => {
                                      Application.SetCompatibleTextRenderingDefault(false);
                                      Application.EnableVisualStyles();

                                      // TODO: Remove me later on after implimentation of new updater engine

                                      #region Obsolete Code
                                      string newVer = new WebClient().DownloadString("http://streamdesk.sourceforge.net/version.txt");

                                      if (new Version(newVer) > new Version(GlobalAssemblyInfo.UpdaterVersion)) {
                                          if (MessageBox.Show("A new version of StreamDesk is Available.\n\nOld Version: " + GlobalAssemblyInfo.UpdaterVersion + "\nNew Version: " + newVer + "\n\nClick yes to go to http://streamdesk.ca to update.", "StreamDesk", MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                                                  == DialogResult.Yes) {
                                              Process.Start("http://streamdesk.ca/pages/windows-linux.php");
                                              return;
                                          }
                                      }
                                      #endregion

                                      if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamDesk")))
                                          Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamDesk"));

                                      StreamDeskSettings.OpenSettings();
                                      MainForm = new MainMDIForm();
                                      Application.Run(MainForm);
                                      StreamDeskSettings.Instance.SaveSettings();
                                  }, args);
        }
    }
}
