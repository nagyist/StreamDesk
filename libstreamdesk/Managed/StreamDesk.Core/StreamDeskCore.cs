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
using System.Linq;
using System.Text;
using Mono.Addins;
using StreamDesk.Managed.Database;
using System.Net;

namespace StreamDesk.Managed
{
    public class StreamDeskCore
    {
        public List<StreamDeskDatabase> ActiveDatabases { get; private set; }
        public static FormatterEngine FormatterEngine { get; private set; }
        public List<Tuple<String, Exception>> FailedDatabases { get; private set; } 
		public static StreamDeskSettings SettingsInstance { get; internal set; }

        public StreamDeskCore() {
            ActiveDatabases = new List<StreamDeskDatabase>();
            FailedDatabases = new List<Tuple<String, Exception>>();
			
			foreach (var activeDatabase in SettingsInstance.ActiveDatabases) {
                var wc = new WebClient();
                wc.DownloadDataCompleted += wc_DownloadDataCompleted;
                wc.DownloadDataAsync(new Uri(activeDatabase), activeDatabase);

                while (wc.IsBusy) {
                    //Application.DoEvents();
                }
            }
        }

        static StreamDeskCore() {
            FormatterEngine = new FormatterEngine();
			StreamDeskSettings.OpenSettings();
			
            AddinManager.Initialize("[ApplicationData]/StreamDesk 3");
            AddinManager.Registry.Update();

            foreach (var extensionObject in AddinManager.GetExtensionObjects<IDatabaseFormatter>("/StreamDesk/DatabaseFormatters")) {
                FormatterEngine.Formatters.Add(extensionObject);
            }
        }
		
		private void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e) {
            if (e.Error != null)
                FailedDatabases.Add(Tuple.Create((string)e.UserState, e.Error));
            else {
                using (var ms = new System.IO.MemoryStream(e.Result)) {
                    var db = StreamDeskDatabase.OpenDatabase(ms, System.IO.Path.GetExtension((string) e.UserState));
                    db.TagInformation = (string) e.UserState;
                    ActiveDatabases.Add(db);
                }
            }
        }
    }
}
