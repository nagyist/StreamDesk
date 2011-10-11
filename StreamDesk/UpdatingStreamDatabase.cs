#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="UpdatingStreamDatabase.cs" company="Developers of the StreamDesk Project">
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
//      Update Stream Database Form
// </summary>
//----------------------------------------------------------------------------------
#endregion

// TODO: Remove me later on after implimentation of new updater engine

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using StreamDesk.Core;

namespace StreamDesk {
    public partial class UpdatingStreamDatabase : Form {
        public UpdatingStreamDatabase() {
            InitializeComponent();
        }

        private void UpdatingStreamDatabase_Load(object sender, EventArgs e) {
            new Thread(() => {
                           var webClient = new WebClient();
                           webClient.DownloadFile(new Uri("http://streamdesk.ca/streams.sdb"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamDesk", "streams.sdb"));
                           Program.Database = StreamDeskDatabase.OpenBinaryDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamDesk", "streams.sdb"));
                           Invoke(new Action(Close));
                       }).Start();
        }
    }
}
