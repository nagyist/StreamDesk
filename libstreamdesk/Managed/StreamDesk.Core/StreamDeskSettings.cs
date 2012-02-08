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
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace StreamDesk.Managed {
    [Serializable] public class StreamDeskSettings {
        public static readonly string SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StreamDesk 3", "Settings.xml");

        public StreamDeskSettings() {
            FavoritesRoot = new FavoritesFolder();
            ActiveDatabases = new List<string>();                
        }

        public FavoritesFolder FavoritesRoot { get; set; }
        public List<string> ActiveDatabases { get; set; }

        public void SaveSettings() {
            using (var file = File.Open(SettingsPath, FileMode.Create)) {
                var xmlSerializer = new XmlSerializer(typeof (StreamDeskSettings));
                xmlSerializer.Serialize(file, this);
            }
        }

        public static StreamDeskSettings OpenSettings() {
            if (File.Exists(SettingsPath))
            {
                using (var file = File.Open(SettingsPath, FileMode.Open))
                {
                    var xmlSerializer = new XmlSerializer(typeof(StreamDeskSettings));
                    return (StreamDeskSettings)xmlSerializer.Deserialize(file);
                }
            }
            else {
                var settingsInstance = new StreamDeskSettings();
                settingsInstance.ActiveDatabases.Add("http://streamdesk.sf.net/streams.sdnx");
				return settingsInstance;
            }
        }
    }

    [Serializable] public class FavoritesFolder {
        public FavoritesFolder() {
            SubFolders = new List<FavoritesFolder>();
            Favorites = new List<Favorite>();
        }

        public string Name { get; set; }
        public List<FavoritesFolder> SubFolders { get; set; }
        public List<Favorite> Favorites { get; set; }
    }

    [Serializable] public class Favorite {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}
