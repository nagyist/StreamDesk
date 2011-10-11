#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Net.Cache;

#endregion

namespace StreamDesk.AppCore {
    public class StreamDeskDBControl {
        private static Dictionary<string, Dictionary<string, string>> ChatEmbeds;
        public static string downloadpath = "http://streamdesk.ca/update.db";

        public static string path = Path.Combine (
            Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData), "streamdesk.db");

        private static Dictionary<string, Dictionary<string, string>> Providers;
        private static SQLiteConnection SettingsDB;
        private static SQLiteConnection StreamDeskDB;
        private static Dictionary<string, string> StreamEmbeds;
        private static Dictionary<string, Dictionary<string, Dictionary<string, string>>> Streams;

        public static Dictionary<string, Dictionary<string, string>> ChatEmbed {
            get { return ChatEmbeds; }
        }

        public static Dictionary<string, string> StreamEmbed {
            get { return StreamEmbeds; }
        }

        public static void Initialize () {
            Providers = new Dictionary<string, Dictionary<string, string>> ();
            Streams = new Dictionary<string, Dictionary<string, Dictionary<string, string>>> ();
            StreamEmbeds = new Dictionary<string, string> ();
            ChatEmbeds = new Dictionary<string, Dictionary<string, string>> ();
            if (File.Exists (path)) {
                StreamDeskDB =
                    new SQLiteConnection (String.Format ("Data Source={0};Compress=True;Synchronous=Off", path));
                StreamDeskDB.Open ();

                UpdatePrividers ();
                UpdateStreams ();
                UpdateStreamEmbeds ();
                UpdateChatEmbeds ();
            } else {
                Update ();
            }
        }

        private static void UpdatePrividers () {
            var cmd = new SQLiteCommand (StreamDeskDB);
            cmd.CommandText = "SELECT * FROM Providers ORDER BY Pinned DESC, Name";
            SQLiteDataReader reader = cmd.ExecuteReader ();

            while (reader.Read ()) {
                Providers.Add (reader["Name"].ToString (), new Dictionary<string, string> ());
                Providers[reader["Name"].ToString ()].Add ("Description", reader["Description"].ToString ());
                Providers[reader["Name"].ToString ()].Add ("Url", reader["Url"].ToString ());
            }
        }

        private static void UpdateStreams () {
            var cmd = new SQLiteCommand (StreamDeskDB);
            cmd.CommandText = "SELECT * FROM Streams ORDER BY Provider, Name";
            SQLiteDataReader reader = cmd.ExecuteReader ();
            while (reader.Read ()) {
                if (Streams.ContainsKey (reader["Provider"].ToString ()) == false) {
                    Streams.Add (reader["Provider"].ToString (), new Dictionary<string, Dictionary<string, string>> ());
                }
                Streams[reader["Provider"].ToString ()].Add (reader["Name"].ToString (),
                                                             new Dictionary<string, string> ());
                Streams[reader["Provider"].ToString ()][reader["Name"].ToString ()].Add ("Web",
                                                                                         reader["Web"].ToString ());
                Streams[reader["Provider"].ToString ()][reader["Name"].ToString ()].Add ("Size",
                                                                                         reader["Size"].ToString ());
                Streams[reader["Provider"].ToString ()][reader["Name"].ToString ()].Add ("StreamEmbed",
                                                                                         reader["StreamEmbed"].ToString
                                                                                             ());
                Streams[reader["Provider"].ToString ()][reader["Name"].ToString ()].Add ("StreamEmbedData",
                                                                                         reader["StreamEmbedData"].
                                                                                             ToString ());
                Streams[reader["Provider"].ToString ()][reader["Name"].ToString ()].Add ("UseShion",
                                                                                         reader["UseShion"].ToString ());
                Streams[reader["Provider"].ToString ()][reader["Name"].ToString ()].Add ("ChatEmbed",
                                                                                         reader["ChatEmbed"].ToString ());
                Streams[reader["Provider"].ToString ()][reader["Name"].ToString ()].Add ("ChatEmbedData",
                                                                                         reader["ChatEmbedData"].
                                                                                             ToString ());
                Streams[reader["Provider"].ToString ()][reader["Name"].ToString ()].Add ("Description",
                                                                                         reader["Description"].ToString
                                                                                             ());
            }
        }

        private static void UpdateStreamEmbeds () {
            var cmd = new SQLiteCommand (StreamDeskDB);
            cmd.CommandText = "SELECT * FROM StreamEmbed";
            SQLiteDataReader reader = cmd.ExecuteReader ();
            while (reader.Read ()) {
                StreamEmbeds.Add (reader["EmbedName"].ToString (), reader["EmbedStringFormat"].ToString ());
            }
        }

        private static void UpdateChatEmbeds () {
            var cmd = new SQLiteCommand (StreamDeskDB);
            cmd.CommandText = "SELECT * FROM ChatEmbed";
            SQLiteDataReader reader = cmd.ExecuteReader ();
            while (reader.Read ()) {
                ChatEmbeds.Add (reader["EmbedName"].ToString (), new Dictionary<string, string> ());
                ChatEmbeds[reader["EmbedName"].ToString ()].Add ("EmbedStringFormat",
                                                                 reader["EmbedStringFormat"].ToString ());
                ChatEmbeds[reader["EmbedName"].ToString ()].Add ("IRCServer", reader["IRCServer"].ToString ());
            }
        }

        public static bool Update () {
            try {
                StreamDeskDB.Close ();
            } catch (NullReferenceException) {}
            try {
                var m = new WebClient ();
                m.CachePolicy = new RequestCachePolicy (RequestCacheLevel.BypassCache);
                m.DownloadFile (downloadpath, path);
                Initialize ();
                return true;
            } catch (WebException) {
                return false;
            }
        }

        internal static string _GetStreamList () {
            string xml = "<xmlrpc>";
            foreach (KeyValuePair<string, Dictionary<string, string>> i in Providers) {
                xml += String.Format ("<provider name=\"{0}\" description=\"{1}\" url=\"{2}\">", i.Key,
                                      i.Value["Description"], i.Value["Url"]);
                foreach (KeyValuePair<string, Dictionary<string, string>> j in Streams[i.Key]) {
                    string os = "";
                    try {
                        os = ChatEmbeds[j.Value["ChatEmbed"]]["IRCServer"];
                    } catch {
                        os = "";
                    }
                    xml +=
                        String.Format (
                            "<stream Web=\"{0}\" Size=\"{1}\" StreamEmbed=\"{2}\" StreamEmbedData=\"{3}\" UseShion=\"{4}\" ChatEmbed=\"{5}\" ChatEmbedData=\"{6}\" Description=\"{7}\" Name=\"{8}\" IRCServer=\"{9}\" />",
                            new object[] {
                                             j.Value["Web"], j.Value["Size"], j.Value["StreamEmbed"],
                                             j.Value["StreamEmbedData"], j.Value["UseShion"], j.Value["ChatEmbed"],
                                             j.Value["ChatEmbedData"], j.Value["Description"], j.Key, os
                                         });
                }
                xml += "</provider>";
            }
            xml += "</xmlrpc>";
            return xml;
        }

        public static bool IsChatEmbed (string embed) {
            return ChatEmbeds.ContainsKey (embed);
        }

        public static bool IsStreamEmbed (string embed) {
            return StreamEmbeds.ContainsKey (embed);
        }
    }
}