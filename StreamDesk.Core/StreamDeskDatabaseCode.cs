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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace StreamDesk.Core {
    [Serializable] public class StreamDeskDatabase {
        public StreamDeskDatabase() {
            ChatEmbeds = new List<ChatEmbed>();
            StreamEmbeds = new List<StreamEmbed>();
            Root = new Provider();
        }

        public List<ChatEmbed> ChatEmbeds { get; set; }
        public List<StreamEmbed> StreamEmbeds { get; set; }
        public Provider Root { get; set; }

        public StreamEmbed GetStreamEmbed(string embedName) {
            return StreamEmbeds.Where(v => v.Name == embedName).FirstOrDefault();
        }

        public Media GetMediaObject(Guid streamId) {
            return GetMediaObject(streamId, Root);
        }

        private Media GetMediaObject(Guid streamId, Provider rtProvider) {
            foreach (Media retVarFromLoop in rtProvider.SubProviders.Select(i => GetMediaObject(streamId, i)).Where(retVarFromLoop => retVarFromLoop != null))
                return retVarFromLoop;

            Media retVarFromProvider = rtProvider.Medias.Where(m => m.StreamGuid == streamId).FirstOrDefault();
            return retVarFromProvider ?? null;
        }

        public string GetStream(Media stream) {
            if (stream.StreamEmbed == "html_custom")
                return "<html><body style=\"padding: 0px; margin: 0px;\">" + stream.GetStreamEmbedData("HTML") + "</body></html>";
            StreamEmbed streamEmbed = GetStreamEmbed(stream.StreamEmbed);
            return streamEmbed != null ? streamEmbed.Format(stream.StreamEmbedData) : null;
        }

        public void SaveBinaryDatabase(string path) {
            var binSerializer = new BinaryFormatter();
            using (FileStream file = File.Open(path, FileMode.Create))
                binSerializer.Serialize(file, this);
        }

        public void SaveXMLDatabase(string path) {
            var binSerializer = new XmlSerializer(typeof (StreamDeskDatabase));
            using (FileStream file = File.Open(path, FileMode.Create))
                binSerializer.Serialize(file, this);
        }

        public static StreamDeskDatabase OpenBinaryDatabase(string path) {
            var binSerializer = new BinaryFormatter();
            using (FileStream file = File.Open(path, FileMode.Open))
                return (StreamDeskDatabase)binSerializer.Deserialize(file);
        }

        public static StreamDeskDatabase OpenXMLDatabase(string path) {
            var binSerializer = new XmlSerializer(typeof (StreamDeskDatabase));
            using (FileStream file = File.Open(path, FileMode.Open))
                return (StreamDeskDatabase)binSerializer.Deserialize(file);
        }

        public TObj GenerateObjectDatabaseTag<TObj>(Provider providerObject, Media newMediaObject) {
            IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof (TObj));
            menuItem.MediaObject = newMediaObject;
            menuItem.ProviderObject = providerObject;
            menuItem.MenuTitle = newMediaObject.Name;
            menuItem.IsPinned = newMediaObject.Pinned;
            menuItem.MediaType = newMediaObject.MediaType;
            return (TObj)menuItem;
        }

        public TObj GenerateObjectDatabaseTag<TObj>(Provider providerObject, Provider newProviderObject) {
            IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof (TObj));
            menuItem.MenuTitle = newProviderObject.Name;
            menuItem.IsProvider = true;
            menuItem.ProviderObject = newProviderObject;
            menuItem.IsPinned = newProviderObject.Pinned;
            //FillList(menuItem.SubItems, objectDatabaseTagType, subProvider);
            menuItem.CallSubItemsToProperArray();
            menuItem.ParentProviderObject = providerObject;
            return (TObj)menuItem;
        }

        public TObj[] GenerateObjectDatabaseTags<TObj>() where TObj : IObjectDatabaseTag {
            var list = new List<IObjectDatabaseTag>();
            FillList<TObj>(list, Root);
            return list.Cast<TObj>().ToArray();
        }

        public Media[] Search(string searchParam) {
            var list = new List<Media>();
            Search(list, Root, searchParam);
            return list.ToArray();
        }

        public void Search(List<Media> list, Provider rootProvider, string searchParam) {
            foreach (Provider subProvider in rootProvider.SubProviders)
                Search(list, subProvider, searchParam);

            foreach (Media i in rootProvider.Medias) {
                if (i.Tags != null) {
                    string[] tags = i.Tags.Split(';');

                    list.AddRange(from tag in tags
                        where tag.Contains(searchParam, StringComparison.OrdinalIgnoreCase) && !list.Contains(i)
                        select i);
                }

                if (!string.IsNullOrEmpty(i.Description) && i.Description.Contains(searchParam, StringComparison.OrdinalIgnoreCase) && !list.Contains(i))
                    list.Add(i);
            }
        }

        private void FillList<TObj>(List<IObjectDatabaseTag> items, Provider rtProvider) {
            var pinnedProviders = new List<IObjectDatabaseTag>();
            var unpinnedProviders = new List<IObjectDatabaseTag>();
            var pinnedMedias = new List<IObjectDatabaseTag>();
            var unpinnedMedias = new List<IObjectDatabaseTag>();

            foreach (Provider subProvider in rtProvider.SubProviders.Where(v => v.Pinned)) {
                IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof (TObj));
                menuItem.MenuTitle = subProvider.Name;
                menuItem.IsProvider = true;
                menuItem.ProviderObject = subProvider;
                menuItem.IsPinned = subProvider.Pinned;
                FillList<TObj>(menuItem.SubItems, subProvider);
                menuItem.CallSubItemsToProperArray();
                menuItem.ParentProviderObject = rtProvider;
                pinnedProviders.Add(menuItem);
            }
            foreach (Provider subProvider in rtProvider.SubProviders.Where(v => !v.Pinned)) {
                IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof (TObj));
                menuItem.MenuTitle = subProvider.Name;
                menuItem.IsProvider = true;
                menuItem.ProviderObject = subProvider;
                menuItem.IsPinned = subProvider.Pinned;
                FillList<TObj>(menuItem.SubItems, subProvider);
                menuItem.CallSubItemsToProperArray();
                menuItem.ParentProviderObject = rtProvider;
                unpinnedProviders.Add(menuItem);
            }
            foreach (Media i in rtProvider.Medias.Where(v => v.Pinned)) {
                IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof (TObj));
                menuItem.MediaObject = i;
                menuItem.ProviderObject = rtProvider;
                menuItem.MenuTitle = i.Name;
                menuItem.IsPinned = i.Pinned;
                menuItem.MediaType = i.MediaType;
                pinnedMedias.Add(menuItem);
            }
            foreach (Media i in rtProvider.Medias.Where(v => !v.Pinned)) {
                IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof (TObj));
                menuItem.MediaObject = i;
                menuItem.ProviderObject = rtProvider;
                menuItem.MenuTitle = i.Name;
                menuItem.IsPinned = i.Pinned;
                menuItem.MediaType = i.MediaType;
                menuItem.MediaObject.ProviderObject = rtProvider;
                unpinnedMedias.Add(menuItem);
            }
            pinnedProviders.Sort((x, y) => String.Compare(x.MenuTitle, y.MenuTitle));
            unpinnedProviders.Sort((x, y) => String.Compare(x.MenuTitle, y.MenuTitle));
            pinnedMedias.Sort((x, y) => String.Compare(x.MenuTitle, y.MenuTitle));
            unpinnedMedias.Sort((x, y) => String.Compare(x.MenuTitle, y.MenuTitle));

            items.AddRange(pinnedProviders);
            items.AddRange(unpinnedProviders);
            items.AddRange(pinnedMedias);
            items.AddRange(unpinnedMedias);
        }

        private IObjectDatabaseTag CreateIObjectDatabaseTag(Type objectDatabaseTagType) {
            return (IObjectDatabaseTag)objectDatabaseTagType.GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        public string GetChat(Media stream) {
            if (stream.ChatEmbed == "html_custom")
                return "<html><body style=\"padding: 0px; margin: 0px;\">" + stream.GetChatEmbedData("HTML") + "</body></html>";
            ChatEmbed streamEmbed = GetChatEmbed(stream.ChatEmbed);
            return streamEmbed != null ? streamEmbed.Format(stream.ChatEmbedData) : null;
        }

        public ChatEmbed GetChatEmbed(string embedName) {
            return ChatEmbeds.Where(v => v.Name == embedName).FirstOrDefault();
        }

        public void RegenerateGUIDS() {
            RegenerateGUIDS(Root);
        }

        private void RegenerateGUIDS(Provider rtProvider) {
            foreach (Provider subProvider in rtProvider.SubProviders)
                RegenerateGUIDS(subProvider);

            foreach (Media i in rtProvider.Medias)
                i.StreamGuid = Guid.NewGuid();
        }

        public void FillTags() {
            FillTags(Root);
        }

        private void FillTags(Provider rtProvider) {
            foreach (Provider subProvider in rtProvider.SubProviders)
                FillTags(subProvider);

            foreach (Media i in rtProvider.Medias) {
                if (string.IsNullOrEmpty(i.Tags)) {
                    StreamEmbed embed = GetStreamEmbed(i.StreamEmbed);
                    if (embed != null && !string.IsNullOrEmpty(embed.FriendlyName))
                        i.Tags = i.Name + ";" + rtProvider.Name + ";" + embed.FriendlyName;
                    else
                        i.Tags = i.Name + ";" + rtProvider.Name;
                }
            }
        }
    }

    [Serializable] public class StreamEmbed {
        public string Name { get; set; }
        public string FriendlyName { get; set; }

        [Editor(typeof (MultilineStringEditor), typeof (UITypeEditor))] public string EmbedFormat { get; set; }

        public string Format(List<EmbedData> embedDatas) {
            return embedDatas.Aggregate(EmbedFormat, (current, embedData) => current.Replace("$" + embedData.Name + "$", embedData.Value));
        }
    }

    [Serializable] public class ChatEmbed : StreamEmbed {
        public string IrcServer { get; set; }
    }

    public enum MediaType {
        VideoStream = 1,
        AudioStream = 2,
    }

    [Serializable] public class Media {
        [NonSerialized, XmlIgnore] public Provider ProviderObject;

        public Media() {
            ChatEmbedData = new List<EmbedData>();
            StreamEmbedData = new List<EmbedData>();
            MediaType = MediaType.VideoStream;
            StreamGuid = Guid.NewGuid();
        }

        [DisplayName("Media Type"), Description("The media type of this perticular media item."),
         Category("Media Properties")] public MediaType MediaType { get; set; }

        [Browsable(false)] public string Name { get; set; }

        [DisplayName("Web URL"), Description("Descriptive URL for the Media Type"),
         Category("Media Properties")] public string Web { get; set; }

        [Description("The media type of this perticular media item."),
         Category("Media Properties")] public Size Size { get; set; }

        [DisplayName("Stream Embed"), Description("The stream embed type of this perticular media item."),
         Category("Stream Properties")] public string StreamEmbed { get; set; }

        [DisplayName("Stream Embed Properties"), Description("The stream embed properties of this perticular media item."),
         Category("Stream Properties")] public List<EmbedData> StreamEmbedData { get; set; }

        [DisplayName("Chat Embed"), Description("The chat embed type of this perticular media item."),
         Category("Chat Properties")] public string ChatEmbed { get; set; }

        [DisplayName("Chat Embed Properties"), Description("The chat embed properties of this perticular media item."),
         Category("Chat Properties")] public List<EmbedData> ChatEmbedData { get; set; }

        [Description("The friendly description of this media."),
         Category("Media Properties"), Editor(typeof (MultilineStringEditor), typeof (UITypeEditor))] public string Description { get; set; }

        [Description("Tags of the stream seperated by ;"),
         Category("Media Properties")] public string Tags { get; set; }

        [Description("The Streams GUID. This field is not editable."),
         Category("Media Properties"), ReadOnly(true)] public Guid StreamGuid { get; set; }

        [Description("Pins the provider to the top."), Category("Pinning")] public bool Pinned { get; set; }

        public string GetStreamEmbedData(string p) {
            return StreamEmbedData.Where(v => v.Name == p).FirstOrDefault().Value;
        }

        public string GetChatEmbedData(string p) {
            return ChatEmbedData.Where(v => v.Name == p).FirstOrDefault().Value;
        }
    }

    [Serializable] public class Provider {
        public Provider() {
            SubProviders = new List<Provider>();
            Medias = new List<Media>();
        }

        [Browsable(false)] public List<Provider> SubProviders { get; set; }
        [Browsable(false)] public List<Media> Medias { get; set; }
        [Browsable(false)] public string Name { get; set; }

        [Description("The friendly description of this provider."),
         Category("Provider Properties")] public string Description { get; set; }

        [DisplayName("Web URL"), Description("Descriptive URL for the Provider"),
         Category("Provider Properties")] public string Web { get; set; }

        [Description("Pins the provider to the top."), Category("Pinning")] public bool Pinned { get; set; }

        public Provider GetProvider(string name) {
            return SubProviders.Where(v => v.Name == name).FirstOrDefault();
        }

        public Media GetMedia(string name) {
            return Medias.Where(v => v.Name == name).FirstOrDefault();
        }
    }

    [Serializable] public class EmbedData {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public interface IObjectDatabaseTag {
        List<IObjectDatabaseTag> SubItems { get; }
        string MenuTitle { get; set; }
        bool IsProvider { get; set; }
        bool IsPinned { get; set; }
        Media MediaObject { get; set; }
        MediaType MediaType { get; set; }
        Provider ProviderObject { get; set; }
        Provider ParentProviderObject { get; set; }
        void CallSubItemsToProperArray();
    }
}
