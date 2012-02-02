using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using StreamDesk.Database;
using StreamDesk.Managed;
using Stream = StreamDesk.Managed.Stream;
using StreamDeskProperty = StreamDesk.Managed.StreamDeskProperty;

namespace StreamDesk.Core.DatabaseFormats
{
    class SDXMLFormatter : IDatabaseFormatter
    {
        public string FormatName
        {
            get { return "StreamDesk Database 1.3 (XML)"; }
        }

        public string FileExtension
        {
            get { return ".sdx"; }
        }

        public Managed.StreamDeskDatabase Read(System.IO.FileStream file)
        {
            var formatter = new XmlSerializer(typeof(StreamDeskDatabase));
            var currentDb = (StreamDeskDatabase)formatter.Deserialize(file);
            var newDb = new Managed.StreamDeskDatabase();

            ConvertEmbeds(currentDb, newDb);
            ConvertProviders(currentDb.Root, newDb.Root);

            return newDb;
        }

        private void ConvertEmbeds(StreamDeskDatabase currentDb, Managed.StreamDeskDatabase newDb)
        {
            foreach (var chatEmbedNew in currentDb.ChatEmbeds.Select(chatEmbed => new Managed.ChatEmbed { EmbedFormat = chatEmbed.EmbedFormat, FriendlyName = chatEmbed.FriendlyName, IrcServer = chatEmbed.IrcServer, Name = chatEmbed.Name }))
            {
                newDb.ChatEmbeds.Add(chatEmbedNew);
            }

            foreach (var streamEmbedNew in currentDb.StreamEmbeds.Select(streamEmbed => new Managed.StreamEmbed() { EmbedFormat = streamEmbed.EmbedFormat, FriendlyName = streamEmbed.FriendlyName, Name = streamEmbed.Name }))
            {
                newDb.StreamEmbeds.Add(streamEmbedNew);
            }
        }

        private void ConvertProviders(Provider provider, Managed.Provider newProvider)
        {
            newProvider.Name = provider.Name;
            newProvider.Description = provider.Description;
            newProvider.Web = provider.Web;
            newProvider.Pinned = provider.Pinned;

            foreach (var media in provider.Medias) {
                var stream = new Stream { Name = media.Name, Description = media.Description, Web = media.Web, ChatEmbed = media.ChatEmbed, StreamGuid = media.StreamGuid, StreamEmbed = media.StreamEmbed, Tags = media.Tags, Size = media.Size };
                foreach (var newEmbedData in media.ChatEmbedData.Select(embedData => new StreamDeskProperty { Name = embedData.Name, Value = embedData.Value }))
                {
                    stream.ChatEmbedData.Add(newEmbedData);
                }
                foreach (var newEmbedData in media.StreamEmbedData.Select(embedData => new StreamDeskProperty { Name = embedData.Name, Value = embedData.Value }))
                {
                    stream.StreamEmbedData.Add(newEmbedData);
                }
                newProvider.Streams.Add(stream);
            }

            foreach (var subProvider in provider.SubProviders)
            {
                var newSubProvider = new Managed.Provider();
                ConvertProviders(subProvider, newSubProvider);
                newProvider.SubProviders.Add(newSubProvider);
            }
        }

        public void Write(System.IO.FileStream file, Managed.StreamDeskDatabase streamDeskDatabase)
        {

        }
    }
}
