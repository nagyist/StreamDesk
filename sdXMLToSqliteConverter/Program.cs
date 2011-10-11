using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SQLite;
using System.IO;

namespace sdXMLToSqliteConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Schema
             * CREATE TABLE Providers (
             *      "ID" INTEGER NOT NULL,
             *      "Name" TEXT NOT NULL,
             *      "Description" TEXT, 
             *      "Url" TEXT 
             * )
             * 
             * CREATE TABLE Streams (
             *      "ID" INTEGER NOT NULL,
             *      "ProviderName" TEXT NOT NULL,
             *      "Name" TEXT NOT NULL,
             *      "Web" TEXT NOT NULL,
             *      "Size" TEXT NOT NULL,
             *      "EmbedName" TEXT NOT NULL,
             *      "StreamEmbedData" TEXT NOT NULL,
             *      "ChatEmbedData" TEXT,
             *      "UseShion" INTEGER NOT NULL,
             *      "IRCServer" TEXT NOT NULL,
             *      "IRCChannel" TEXT,
             * )
             * 
             * CREATE TABLE ChatEmbed (
             *      "ID" INTEGER NOT NULL,
             *      "EmbedName" TEXT NOT NULL,
             *      "EmbedStringFormat" TEXT NOT NULL 
             * )
             * 
             * CREATE TABLE StreamEmbed (
             *      "ID" INTEGER NOT NULL,
             *      "EmbedName" TEXT NOT NULL,
             *      "EmbedStringFormat" TEXT NOT NULL 
             * )
             */

            Console.WriteLine("StreamDesk XML Database to SQLite Database Converter");
            Console.WriteLine("\t(C) 2009 NasuTek-Alliant Enterprises");
            Console.WriteLine("CONFIDENTIAL UTILITY CONTACT mmanley@nasutek.com IF THIS UTILITY WAS STOLEN");
            Console.WriteLine("");

            if (args.Length == 3)
            {
                if (File.Exists(args[2]) == true) { File.Delete(args[2]); }
                Random random = new Random();
                //Description:Url
                Dictionary<string, string[]> Providers = new Dictionary<string, string[]>();
                //ProviderName:Web:Size:EmbedName:StreamEmbedData:ChatEmbedData:UseShion:IRCServer:IRCChannel:CustomGroup
                Dictionary<string, string[]> Streams = new Dictionary<string, string[]>();
                Dictionary<string, string> EmbedsChat = new Dictionary<string, string>();
                Dictionary<string, string> EmbedsStream = new Dictionary<string, string>();

                Console.WriteLine("Reading Stream XML Document");
                XmlDocument streamdocument = new XmlDocument();
                streamdocument.Load(args[0]);

                Console.WriteLine("Reading Embed XML Document");
                XmlDocument embeddocument = new XmlDocument();
                embeddocument.Load(args[1]);

                Console.WriteLine("Getting Providers And Streams from Stream XML Document");
                foreach (XmlNode node in streamdocument.SelectNodes("/streams/provider"))
                {
                    Providers.Add(CleanForSQL(node.Attributes["name"].Value), new string[] { CleanForSQL(node.Attributes["desc"].Value), CleanForSQL(node.Attributes["url"].Value) });
                    foreach (XmlNode node3 in node.ChildNodes)
                    {
                        Streams.Add(CleanForSQL(node3.Attributes["name"].Value), new string[] { CleanForSQL(node.Attributes["name"].Value), CleanForSQL(node3.ChildNodes[2].InnerText), 
                            String.Format("{0}x{1}", node3.ChildNodes[3].InnerText, node3.ChildNodes[4].InnerText), CleanForSQL(node3.ChildNodes[0].Attributes["type"].Value), 
                            CleanForSQL(node3.ChildNodes[0].InnerText), "", "0", CleanForSQL(node3.ChildNodes[5].Attributes["server"].Value).Replace("chat1.ustream.tv", "ustream"), 
                            CleanForSQL(node3.ChildNodes[5].InnerText), CleanForSQL(node3.ChildNodes[1].InnerText) });
                    }
                }

                Console.WriteLine("Getting Chat Embends");
                XmlNodeList embednodes = embeddocument.SelectNodes("/embeds/chat/embed");
                foreach (XmlNode i in embednodes)
                {
                    EmbedsChat.Add(CleanForSQL(i.Attributes["name"].Value), CleanForSQL(i.InnerText));
                }

                Console.WriteLine("Getting Stream Embeds");
                embednodes = embeddocument.SelectNodes("/embeds/stream/embed");
                foreach (XmlNode i in embednodes)
                {
                    EmbedsStream.Add(CleanForSQL(i.Attributes["name"].Value), CleanForSQL(i.InnerText));
                }

                Console.WriteLine("Begin Creation of SQLite DB");
                SQLiteConnection Conn = new SQLiteConnection();
                Conn.ConnectionString = String.Format("Data Source={0};New=True;Compress=True;Synchronous=Off", args[2]);
                Conn.Open();
                SQLiteCommand Cmd = Conn.CreateCommand();

                Console.WriteLine("Creating Table Providers");
                Cmd.CommandText = "CREATE TABLE Providers ( \"ID\" INTEGER NOT NULL, \"Name\" TEXT NOT NULL, \"Description\" TEXT, \"Url\" TEXT, PRIMARY KEY (ID) )";
                Cmd.ExecuteNonQuery();
                Console.WriteLine("Creating Table Streams");
                Cmd.CommandText = "CREATE TABLE Streams ( \"ID\" INTEGER NOT NULL, \"ProviderName\" TEXT NOT NULL, \"Name\" TEXT NOT NULL, \"Web\" TEXT NOT NULL, \"Size\" TEXT NOT NULL, \"EmbedName\" TEXT NOT NULL, \"StreamEmbed\" TEXT NOT NULL, \"UseShion\" INTEGER NOT NULL, \"ChatEmbend\" TEXT NOT NULL, \"IRCServer\" TEXT NOT NULL, \"ChatChannel\" TEXT, \"Description\" TEXT, PRIMARY KEY (ID) )";
                Cmd.ExecuteNonQuery();
                Console.WriteLine("Creating Table ChatEmbed");
                Cmd.CommandText = "CREATE TABLE ChatEmbed ( \"ID\" INTEGER NOT NULL, \"EmbedName\" TEXT NOT NULL, \"EmbedStringFormat\" TEXT NOT NULL, PRIMARY KEY (ID) )";
                Cmd.ExecuteNonQuery();
                Console.WriteLine("Creating Table StreamEmbed");
                Cmd.CommandText = "CREATE TABLE StreamEmbed ( \"ID\" INTEGER NOT NULL, \"EmbedName\" TEXT NOT NULL, \"EmbedStringFormat\" TEXT NOT NULL, PRIMARY KEY (ID) )";
                Cmd.ExecuteNonQuery();

                foreach (KeyValuePair<string, string[]> i in Providers)
                {
                    Console.WriteLine(String.Format("Adding Provider {0}", i.Key));
                    Cmd.CommandText = String.Format("INSERT INTO Providers (ID, Name, Description, Url) VALUES('{0}', '{1}', '{2}', '{3}')", new string[] { random.Next().ToString(), i.Key, i.Value[0], i.Value[1] });
                    Cmd.ExecuteNonQuery();
                }

                foreach (KeyValuePair<string, string[]> i in Streams)
                {
                    Console.WriteLine(String.Format("Adding Stream {0} To Provider {1}", i.Key, i.Value[0]));
                    Cmd.CommandText = String.Format("INSERT INTO Streams (ID, Provider, Name, Web, Size, StreamEmbed, StreamEmbedData, UseShion, ChatEmbed, ChatEmbedData, Description) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')", new string[] { random.Next().ToString(), i.Value[0], i.Key, i.Value[1], i.Value[2], i.Value[3], i.Value[4], i.Value[5], i.Value[6], i.Value[8], i.Value[9] });
                    Cmd.ExecuteNonQuery();
                }

                foreach (KeyValuePair<string, string> i in EmbedsChat)
                {
                    Console.WriteLine(String.Format("Adding Chat Embed {0}", i.Key));
                    Cmd.CommandText = String.Format("INSERT INTO ChatEmbed (ID, EmbedName, EmbedStringFormat) VALUES('{0}', '{1}', '{2}')", new string[] { random.Next().ToString(), i.Key, i.Value });
                    Cmd.ExecuteNonQuery();
                }

                foreach (KeyValuePair<string, string> i in EmbedsStream)
                {
                    Console.WriteLine(String.Format("Adding Stream Embed {0}", i.Key));
                    Cmd.CommandText = String.Format("INSERT INTO StreamEmbed (ID, EmbedName, EmbedStringFormat) VALUES('{0}', '{1}', '{2}')", new string[] { random.Next().ToString(), i.Key, i.Value });
                    Cmd.ExecuteNonQuery();
                }

                Cmd.Dispose();
                Conn.Close();
            }
            else
            {
                Console.WriteLine("Usage: <StreamDesk Stream Data XML> <StremDesk Embed Data XML> <SQLite DB Out File>");
            }
        }

        static private string CleanForSQL(string str)
        {
            string ret = str;
            ret = ret.Replace("'", "''");
            return ret;
        }
    }
}
