#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="StreamDesk21DBImporter.cs" company="Developers of the StreamDesk Project">
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
//      StreamDesk version 2.1.x Database Importer
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Core;

namespace Editor {
    public class StreamDesk21DBImporter {
        private readonly SQLiteConnection _StreamDeskDB;

        public StreamDesk21DBImporter(string path) {
            Providers = new Dictionary<string, Dictionary<string, string>>();
            Streams = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            StreamEmbeds = new Dictionary<string, string>();
            ChatEmbeds = new Dictionary<string, Dictionary<string, string>>();

            _StreamDeskDB = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            _StreamDeskDB.Open();

            UpdatePrividers();
            UpdateStreams();
            UpdateStreamEmbeds();
            UpdateChatEmbeds();
        }

        public Dictionary<string, Dictionary<string, string>> ChatEmbeds { get; private set; }
        public Dictionary<string, Dictionary<string, string>> Providers { get; private set; }
        public Dictionary<string, string> StreamEmbeds { get; private set; }
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> Streams { get; private set; }

        ~StreamDesk21DBImporter() {
            _StreamDeskDB.Close();
        }

        private void UpdatePrividers() {
            var cmd = new SQLiteCommand(_StreamDeskDB);
            cmd.CommandText = "SELECT * FROM Providers ORDER BY Pinned DESC, Name";
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) {
                Providers.Add(reader["Name"].ToString(), new Dictionary<string, string>());
                Providers[reader["Name"].ToString()].Add("Description", reader["Description"].ToString());
                Providers[reader["Name"].ToString()].Add("Url", reader["Url"].ToString());
            }
        }

        private void UpdateStreams() {
            var cmd = new SQLiteCommand(_StreamDeskDB);
            cmd.CommandText = "SELECT * FROM Streams ORDER BY Provider, Name";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                if (Streams.ContainsKey(reader["Provider"].ToString()) == false)
                    Streams.Add(reader["Provider"].ToString(), new Dictionary<string, Dictionary<string, string>>());
                Streams[reader["Provider"].ToString()].Add(reader["Name"].ToString(), new Dictionary<string, string>());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("Web", reader["Web"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("Size", reader["Size"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("StreamEmbed",
                    reader["StreamEmbed"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("StreamEmbedData",
                    reader["StreamEmbedData"].ToString
                        ());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("UseShion",
                    reader["UseShion"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("ChatEmbed",
                    reader["ChatEmbed"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("ChatEmbedData",
                    reader["ChatEmbedData"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("Description",
                    reader["Description"].ToString());
            }
        }

        private void UpdateStreamEmbeds() {
            var cmd = new SQLiteCommand(_StreamDeskDB);
            cmd.CommandText = "SELECT * FROM StreamEmbed";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                StreamEmbeds.Add(reader["EmbedName"].ToString(), reader["EmbedStringFormat"].ToString());
        }

        private void UpdateChatEmbeds() {
            var cmd = new SQLiteCommand(_StreamDeskDB);
            cmd.CommandText = "SELECT * FROM ChatEmbed";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                ChatEmbeds.Add(reader["EmbedName"].ToString(), new Dictionary<string, string>());
                ChatEmbeds[reader["EmbedName"].ToString()].Add("EmbedStringFormat",
                    reader["EmbedStringFormat"].ToString());
                ChatEmbeds[reader["EmbedName"].ToString()].Add("IRCServer", reader["IRCServer"].ToString());
            }
        }

        public StreamDeskDatabase ImportDatabase() {
            var db = new StreamDeskDatabase();

            foreach (var i in Providers) {
                db.Root.SubProviders.Add(new Provider {
                    Name = i.Key, Description = i.Value["Description"], Web = i.Value["Url"]
                });
                foreach (var j in Streams[i.Key]) {
                    var media = new Media {
                        ChatEmbed = j.Value["ChatEmbed"], Description = j.Value["Description"],
                        MediaType = MediaType.VideoStream, Name = j.Key, StreamEmbed = j.Value["StreamEmbed"], Web = j.Value["Web"],
                        Size = new Size(Convert.ToInt32(j.Value["Size"].Split('x')[0]), Convert.ToInt32(j.Value["Size"].Split('x')[1]))
                    };
                    media.ChatEmbedData.Add(new EmbedData {
                        Name = "CHANNEL", Value = j.Value["ChatEmbedData"]
                    });
                    media.StreamEmbedData.Add(new EmbedData {
                        Name = "ID", Value = j.Value["StreamEmbedData"]
                    });
                    db.Root.GetProvider(i.Key).Medias.Add(media);
                }
            }

            foreach (var i in ChatEmbeds) {
                db.ChatEmbeds.Add(new ChatEmbed {
                    EmbedFormat = i.Value["EmbedStringFormat"].Replace("{0}", "$CHANNEL$"), Name = i.Key
                });
            }

            foreach (var i in StreamEmbeds) {
                db.StreamEmbeds.Add(new StreamEmbed {
                    EmbedFormat = i.Value.Replace("{0}", "$ID$"), Name = i.Key
                });
            }

            MessageBox.Show(
                @"Changes in StreamDesk Streams Database Version 2.2

! Embeds now are allowed mutiple values to be replaced with. In result embeds have {0} replaced with %LEGACY_EMBED_DATA%. LEGACY_EMBED_DATA contains what was the old Chat/Stream Embed information.
! The Streams Database is no longer a SQLite Database, it is now split in either a XML Style Database or a Binary Database.
+ We now allow sub-providers to allow easier orginization
+ We now added diffrent media formats. Now you can add RSS Podcast Streams!

For more information on changes view the help documents provided.",
                "StreamDesk Database Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return db;
        }

        #region Nested type: Importer
        public class Importer : IDatabaseImporter {
            #region IDatabaseImporter Members
            public StreamDeskDatabase ImportDatabase(string path) {
                return new StreamDesk21DBImporter(path).ImportDatabase();
            }

            public string Extention {
                get { return ".db"; }
            }

            public string Name {
                get { return "StreamDesk v2.2"; }
            }
            #endregion
        }
        #endregion
    }
}
