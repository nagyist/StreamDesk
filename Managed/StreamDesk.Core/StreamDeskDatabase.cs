#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="StreamDeskDatabaseCode.cs" company="Developers of the StreamDesk Project">
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
//      Base code for Database Control and Editing, and Generic UI Code.
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace StreamDesk.Managed {
    [Serializable] public class StreamDeskDatabase {
        public StreamDeskDatabase() {
            ChatEmbeds = new List<ChatEmbed>();
            StreamEmbeds = new List<StreamEmbed>();
            Root = new Provider();
        }

        public List<ChatEmbed> ChatEmbeds { get; set; }
        public List<StreamEmbed> StreamEmbeds { get; set; }
        public Provider Root { get; set; }

        public void SaveDatabase(string path, FormatterEngine engine)
        {
            var ext = Path.GetExtension(path);
            using (var file = File.Open(path, FileMode.Create))
            {
                engine.GetFormatterByExtension(ext).Write(file, this);
            }
        }

        public static StreamDeskDatabase OpenDatabase(string path, FormatterEngine engine)
        {
            var ext = Path.GetExtension(path);
            using (var file = File.Open(path, FileMode.Open))
            {
                return engine.GetFormatterByExtension(ext).Read(file);
            }
        }
    }


    public interface IObjectDatabaseTag {
        List<IObjectDatabaseTag> SubItems { get; }
        string MenuTitle { get; set; }
        bool IsProvider { get; set; }
        bool IsPinned { get; set; }
        Stream StreamObject { get; set; }
        MediaType MediaType { get; set; }
        Provider ProviderObject { get; set; }
        Provider ParentProviderObject { get; set; }
        void CallSubItemsToProperArray();
    }
}