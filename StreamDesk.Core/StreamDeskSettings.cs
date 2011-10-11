#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="StreamDeskSettings.cs" company="Developers of the StreamDesk Project">
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
//      StreamDesk Settings Class
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace StreamDesk.Core {
    [Serializable] public class StreamDeskSettings {
        public static readonly string SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamDesk", "Settings.xml");

        public StreamDeskSettings() {
            FavoritesRoot = new FavoritesFolder();
        }

        public static StreamDeskSettings Instance { get; private set; }
        public FavoritesFolder FavoritesRoot { get; set; }

        public void SaveSettings() {
            var binSerializer = new XmlSerializer(typeof (StreamDeskSettings));
            using (FileStream file = File.Open(SettingsPath, FileMode.Create))
                binSerializer.Serialize(file, this);
        }

        public static void OpenSettings() {
            if (File.Exists(SettingsPath)) {
                var binSerializer = new XmlSerializer(typeof (StreamDeskSettings));
                using (FileStream file = File.Open(SettingsPath, FileMode.Open))
                    Instance = (StreamDeskSettings)binSerializer.Deserialize(file);
            } else
                Instance = new StreamDeskSettings();
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
