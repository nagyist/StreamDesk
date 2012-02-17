#region Licensing Information
/***************************************************************************************************
 * NasuTek StreamDesk
 * Copyright © 2007-2012 NasuTek Enterprises
 * 
 * Licensed under the Apache License, Version 2.0(the "License");
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
using NasuTek.M3.Database;
using System.Net;

namespace NasuTek.M3
{
    public class StreamDeskCore
    {
        public static FormatterEngine FormatterEngine { get; private set; }
        
        public List<StreamDeskDatabase> ActiveDatabases { get; private set; }
        public List<Tuple<String, Exception>> FailedDatabases { get; private set; } 
		public StreamDeskSettings SettingsInstance { get; internal set; }

        public StreamDeskCore() {
			SettingsInstance = StreamDeskSettings.OpenSettings();
			
            ActiveDatabases = new List<StreamDeskDatabase>();
            FailedDatabases = new List<Tuple<String, Exception>>();
        }
		
		public void Initialize() {		
			ActiveDatabases.Clear();
			
			foreach (var activeDatabase in SettingsInstance.ActiveDatabases) {
				var wc = new WebClient();
				try {
					using(var ms = new System.IO.MemoryStream(wc.DownloadData(activeDatabase))) {
	                    var db = StreamDeskDatabase.OpenDatabase(ms, System.IO.Path.GetExtension(activeDatabase));
	                    db.TagInformation = activeDatabase;
	                    ActiveDatabases.Add(db);
	                }
				} catch (Exception e) {
					FailedDatabases.Add(Tuple.Create(activeDatabase, e));
				}
            }
		}
		
        static StreamDeskCore() {
            FormatterEngine = new FormatterEngine();
			
            AddinManager.Initialize("[ApplicationData]/StreamDesk 3");
            AddinManager.Registry.Update();

            foreach (var extensionObject in AddinManager.GetExtensionObjects<IDatabaseFormatter>("/StreamDesk/DatabaseFormatters")) {
                FormatterEngine.Formatters.Add(extensionObject);
            }
        }
		
		public StreamDeskDatabase GetDatabaseStreamExistsIn(Stream stream) {		
			return ActiveDatabases.Where(v => v.GetStreamObject(stream.StreamGuid) != null).FirstOrDefault();
		}
	}
}
