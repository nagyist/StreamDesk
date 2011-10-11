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
using System.Linq;
using System.Windows.Forms;
using ExceptionHandler;
using StreamDesk.Core;

namespace Editor {
    internal static class Program {
        public static List<IDatabaseImporter> Importers { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] private static void Main(string[] args) {
            Handler.AttachHandler(a => {
                                      Application.EnableVisualStyles();
                                      Application.SetCompatibleTextRenderingDefault(false);
                                      ToolStripManager.RenderMode = ToolStripManagerRenderMode.System;
                                      Importers = new List<IDatabaseImporter> {
                                          new StreamDesk21DBImporter.Importer()
                                      };
                                      Application.Run(new MainForm());
                                  }, args);
        }
    }
}
