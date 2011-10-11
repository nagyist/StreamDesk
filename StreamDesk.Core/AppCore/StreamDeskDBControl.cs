using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
using System.Net.Cache;

namespace StreamDesk.AppCore
{
    public class StreamDeskDBControl
    {
        public static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "streamdesk.db");
        public static string downloadpath = "http://streamdesk.ca/update.db";
        static SQLiteConnection StreamDeskDB;
        static SQLiteConnection SettingsDB;
        static Dictionary<string, Dictionary<string, string>> Providers;
        static Dictionary<string, Dictionary<string, Dictionary<string, string>>> Streams;
        static Dictionary<string, string> StreamEmbeds;
        static Dictionary<string, Dictionary<string, string>> ChatEmbeds;

        public static void Initialize()
        {
            Providers = new Dictionary<string, Dictionary<string, string>>();
            Streams = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            StreamEmbeds = new Dictionary<string, string>();
            ChatEmbeds = new Dictionary<string, Dictionary<string, string>>();
            try
            {
                WebClient m =new WebClient();
                m.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
                m.DownloadFile(downloadpath, path);
            }
            catch (WebException) { }
            StreamDeskDB = new SQLiteConnection(String.Format("Data Source={0};Compress=True;Synchronous=Off", path));
            StreamDeskDB.Open();

            UpdatePrividers();
            UpdateStreams();
            UpdateStreamEmbeds();
            UpdateChatEmbeds();
        }

        static void UpdatePrividers()
        {
            SQLiteCommand cmd = new SQLiteCommand(StreamDeskDB);
            cmd.CommandText = "SELECT * FROM Providers";
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Providers.Add(reader["Name"].ToString(), new Dictionary<string, string>());
                Providers[reader["Name"].ToString()].Add("Description", reader["Description"].ToString());
                Providers[reader["Name"].ToString()].Add("Url", reader["Url"].ToString());
            }
        }

        static void UpdateStreams()
        {
            SQLiteCommand cmd = new SQLiteCommand(StreamDeskDB);
            cmd.CommandText = "SELECT * FROM Streams";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (Streams.ContainsKey(reader["Provider"].ToString()) == false) { Streams.Add(reader["Provider"].ToString(), new Dictionary<string, Dictionary<string, string>>()); }
                Streams[reader["Provider"].ToString()].Add(reader["Name"].ToString(), new Dictionary<string, string>());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("Web", reader["Web"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("Size", reader["Size"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("StreamEmbed", reader["StreamEmbed"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("StreamEmbedData", reader["StreamEmbedData"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("UseShion", reader["UseShion"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("ChatEmbed", reader["ChatEmbed"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("ChatEmbedData", reader["ChatEmbedData"].ToString());
                Streams[reader["Provider"].ToString()][reader["Name"].ToString()].Add("Description", reader["Description"].ToString());
            }
        }

        static void UpdateStreamEmbeds()
        {
            SQLiteCommand cmd = new SQLiteCommand(StreamDeskDB);
            cmd.CommandText = "SELECT * FROM StreamEmbed";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                StreamEmbeds.Add(reader["EmbedName"].ToString(), reader["EmbedStringFormat"].ToString());
            }
        }

        static void UpdateChatEmbeds()
        {
            SQLiteCommand cmd = new SQLiteCommand(StreamDeskDB);
            cmd.CommandText = "SELECT * FROM ChatEmbed";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ChatEmbeds.Add(reader["EmbedName"].ToString(), new Dictionary<string, string>());
                ChatEmbeds[reader["EmbedName"].ToString()].Add("EmbedStringFormat", reader["EmbedStringFormat"].ToString());
                ChatEmbeds[reader["EmbedName"].ToString()].Add("IRCServer", reader["IRCServer"].ToString());
            }
        }

        public static void Update()
        {
            StreamDeskDB.Close();
            Initialize();
        }

        public static List<TreeNode> GetStreamList()
        {
            WebClient wc = new WebClient();
            string data = wc.DownloadString("http://localhost:9898/+gettree");
            List<TreeNode> ret = new List<TreeNode>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            foreach (XmlNode i in doc.SelectNodes("/xmlrpc/provider"))
            {
                TreeNode node = new TreeNode(i.Attributes["name"].Value); node.Name = i.Attributes["name"].Value;
                string[] strArray = new string[9];
                strArray[0] = "PROVIDER";
                strArray[1] = i.Attributes["description"].Value;
                strArray[2] = i.Attributes["url"].Value;
                node.Tag = strArray;
                foreach (XmlNode j in i.ChildNodes)
                {
                    TreeNode node2 = new TreeNode(j.Attributes["Name"].Value); node2.Name = j.Attributes["Name"].Value;
                    node2.Tag = new string[] { "STREAM", j.Attributes["Web"].Value, j.Attributes["Size"].Value, j.Attributes["StreamEmbed"].Value, j.Attributes["StreamEmbedData"].Value, j.Attributes["UseShion"].Value, j.Attributes["ChatEmbed"].Value, j.Attributes["ChatEmbedData"].Value, j.Attributes["Description"].Value };
                    node.Nodes.Add(node2);
                }

                ret.Add(node);
            }

            return ret;
        }
        public static List<ToolStripMenuItem> GetStreamList_Menu(frmMain main)
        {
            WebClient wc = new WebClient();
            string data = wc.DownloadString("http://localhost:9898/+gettree");
            List<ToolStripMenuItem> ret = new List<ToolStripMenuItem>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            foreach (XmlNode i in doc.SelectNodes("/xmlrpc/provider"))
            {
                ToolStripMenuItem node = new ToolStripMenuItem(i.Attributes["name"].Value); node.Name = i.Attributes["name"].Value;
                string[] strArray = new string[9];
                strArray[0] = "PROVIDER";
                strArray[1] = i.Attributes["description"].Value;
                strArray[2] = i.Attributes["url"].Value;
                node.Tag = strArray;
                foreach (XmlNode j in i.ChildNodes)
                {
                    ToolStripMenuItem node2 = new ToolStripMenuItem(j.Attributes["Name"].Value); node2.Name = j.Attributes["Name"].Value;
                    node2.Tag = new string[] { i.Attributes["name"].Value, j.Attributes["Web"].Value, j.Attributes["Size"].Value, j.Attributes["StreamEmbed"].Value, j.Attributes["StreamEmbedData"].Value, j.Attributes["UseShion"].Value, j.Attributes["ChatEmbed"].Value, j.Attributes["ChatEmbedData"].Value, j.Attributes["Description"].Value };
                    node2.Click += new EventHandler(main.streamClick);
                    node.DropDownItems.Add(node2);
                }

                ret.Add(node);
            }

            return ret;
        }

        static internal string _GetStreamList()
        {
            string xml = "<xmlrpc>";
            foreach (KeyValuePair<string, Dictionary<string, string>> i in Providers)
            {
                xml += String.Format("<provider name=\"{0}\" description=\"{1}\" url=\"{2}\">", i.Key, i.Value["Description"], i.Value["Url"]);
                foreach (KeyValuePair<string, Dictionary<string, string>> j in Streams[i.Key])
                {
                    xml += String.Format("<stream Web=\"{0}\" Size=\"{1}\" StreamEmbed=\"{2}\" StreamEmbedData=\"{3}\" UseShion=\"{4}\" ChatEmbed=\"{5}\" ChatEmbedData=\"{6}\" Description=\"{7}\" Name=\"{8}\" />", new object[] { j.Value["Web"], j.Value["Size"], j.Value["StreamEmbed"], j.Value["StreamEmbedData"], j.Value["UseShion"], j.Value["ChatEmbed"], j.Value["ChatEmbedData"], j.Value["Description"], j.Key });
                }
                xml += "</provider>";
            }
            xml += "</xmlrpc>";
            return xml;
        }

        public static bool IsChatEmbed(string embed) { return ChatEmbeds.ContainsKey(embed); }
        public static bool IsStreamEmbed(string embed) { return StreamEmbeds.ContainsKey(embed); }

        public static Dictionary<string, Dictionary<string, string>> ChatEmbed { get { return ChatEmbeds; } }
        public static Dictionary<string, string> StreamEmbed { get { return StreamEmbeds; } }
    }
}