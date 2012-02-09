#region Licensing Information
/***************************************************************************************************
 * NasuTek StreamDesk
 * Copyright � 2007-2012 NasuTek Enterprises
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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace StreamDesk.Managed.Database
{
    [Serializable] public class StreamDeskDatabase {
        public StreamDeskDatabase() {
            ChatEmbeds = new List<ChatEmbed>();
            StreamEmbeds = new List<StreamEmbed>();
            Root = new Provider();
            Name = "";
            Vendor = "";
            Description = "";
        }

        [Browsable(false), XmlIgnore]
        public string TagInformation { get; set; }

        [Browsable(false)]
        public List<ChatEmbed> ChatEmbeds { get; set; }
        [Browsable(false)]
        public List<StreamEmbed> StreamEmbeds { get; set; }
        [Browsable(false)]
        public Provider Root { get; set; }

        [Description("The name of the Database."),
         Category("Database Manifest"), XmlAttribute("name")]
        public string Name { get; set; }
        [Description("The vendor of the Database."),
         Category("Database Manifest"), XmlAttribute("vendor")]
        public string Vendor { get; set; }
        [Description("The description of the Database."),
         Category("Database Manifest"), XmlAttribute("description")]
        public string Description { get; set; }

        public void SaveDatabase(string path)
        {
            var ext = Path.GetExtension(path);
            using(var file = File.Open(path, FileMode.Create))
            {
                StreamDeskCore.FormatterEngine.GetFormatterByExtension(ext).Write(file, this);
            }
        }

        public static StreamDeskDatabase OpenDatabase(string path)
        {
            var ext = Path.GetExtension(path);
            using(var file = File.Open(path, FileMode.Open)) {
                var engine = StreamDeskCore.FormatterEngine.GetFormatterByExtension(ext);
                if (engine != null)
                    return engine.Read(file);

                throw new Exception();
            }
        }

        public static StreamDeskDatabase OpenDatabase(System.IO.Stream stream, string dbExtension) {
            var ext = Path.GetExtension(dbExtension);

            using(stream) {
                var engine = StreamDeskCore.FormatterEngine.GetFormatterByExtension(ext);
                if (engine != null)
                    return engine.Read(stream);

                throw new Exception();
            }
        }

        public StreamEmbed GetStreamEmbed(string embedName)
        {
            return StreamEmbeds.Where(v => v.Name == embedName).FirstOrDefault();
        }

        public Stream GetStreamObject(Guid streamId)
        {
            return GetStreamObject(streamId, Root);
        }

        private Stream GetStreamObject(Guid streamId, Provider rtProvider)
        {
            foreach (Stream retVarFromLoop in rtProvider.SubProviders.Select(i => GetStreamObject(streamId, i)).Where(retVarFromLoop => retVarFromLoop != null))
                return retVarFromLoop;

            Stream retVarFromProvider = rtProvider.Streams.Where(m => m.StreamGuid == streamId).FirstOrDefault();
            return retVarFromProvider ?? null;
        }

        public string GetStream(Stream stream)
        {
            if (stream.StreamEmbed == "html_custom")
                return "<html><body style=\"padding: 0px; margin: 0px;\">" + stream.GetStreamEmbedData("HTML") + "</body></html>";
            StreamEmbed streamEmbed = GetStreamEmbed(stream.StreamEmbed);
            return streamEmbed != null ? streamEmbed.Format(stream.StreamEmbedData) : null;
        }

        public TObj GenerateObjectDatabaseTag<TObj>(Provider providerObject, Stream newStreamObject)
        {
            IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof(TObj));
            menuItem.StreamObject = newStreamObject;
            menuItem.ProviderObject = providerObject;
            menuItem.MenuTitle = newStreamObject.Name;
            menuItem.IsPinned = newStreamObject.Pinned;
            menuItem.MediaType = newStreamObject.MediaType;
            menuItem.Database = this;
            return(TObj)menuItem;
        }

        public TObj GenerateObjectDatabaseTag<TObj>(Provider providerObject, Provider newProviderObject, object[] tag)
        {
            IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof(TObj));
            menuItem.MenuTitle = newProviderObject.Name;
            menuItem.IsProvider = true;
            menuItem.ProviderObject = newProviderObject;
            menuItem.IsPinned = newProviderObject.Pinned;
            //FillList(menuItem.SubItems, objectDatabaseTagType, subProvider);
            menuItem.CallSubItemsToProperArray();
            menuItem.ParentProviderObject = providerObject;
            menuItem.Database = this;
            menuItem.TagObject = tag;
            return(TObj)menuItem;
        }

        public TObj[] GenerateObjectDatabaseTags<TObj>(object[] tag) where TObj : IObjectDatabaseTag
        {
            var list = new List<IObjectDatabaseTag>();
            FillList<TObj>(list, Root, tag);
            return list.Cast<TObj>().ToArray();
        }

        public Stream[] Search(string searchParam)
        {
            var list = new List<Stream>();
            Search(list, Root, searchParam);
            return list.ToArray();
        }

        public void Search(List<Stream> list, Provider rootProvider, string searchParam)
        {
            foreach (Provider subProvider in rootProvider.SubProviders)
                Search(list, subProvider, searchParam);

            foreach (Stream i in rootProvider.Streams)
            {
                if (i.Tags != null)
                {
                    string[] tags = i.Tags.Split(';');

                    list.AddRange(from tag in tags
                                  where tag.Contains(searchParam, StringComparison.OrdinalIgnoreCase) && !list.Contains(i)
                                  select i);
                }

                if (!string.IsNullOrEmpty(i.Description) && i.Description.Contains(searchParam, StringComparison.OrdinalIgnoreCase) && !list.Contains(i))
                    list.Add(i);
            }
        }

        private void FillList<TObj>(List<IObjectDatabaseTag> items, Provider rtProvider, object[] tag)
        {
            var pinnedProviders = new List<IObjectDatabaseTag>();
            var unpinnedProviders = new List<IObjectDatabaseTag>();
            var pinnedMedias = new List<IObjectDatabaseTag>();
            var unpinnedMedias = new List<IObjectDatabaseTag>();

            foreach (Provider subProvider in rtProvider.SubProviders.Where(v => v.Pinned))
            {
                IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof(TObj));
                menuItem.MenuTitle = subProvider.Name;
                menuItem.IsProvider = true;
                menuItem.ProviderObject = subProvider;
                menuItem.IsPinned = subProvider.Pinned;
                FillList<TObj>(menuItem.SubItems, subProvider, tag);
                menuItem.CallSubItemsToProperArray();
                menuItem.ParentProviderObject = rtProvider;
                menuItem.Database = this;
                menuItem.TagObject = tag;
                pinnedProviders.Add(menuItem);
            }
            foreach (Provider subProvider in rtProvider.SubProviders.Where(v => !v.Pinned))
            {
                IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof(TObj));
                menuItem.MenuTitle = subProvider.Name;
                menuItem.IsProvider = true;
                menuItem.ProviderObject = subProvider;
                menuItem.IsPinned = subProvider.Pinned;
                FillList<TObj>(menuItem.SubItems, subProvider, tag);
                menuItem.CallSubItemsToProperArray();
                menuItem.ParentProviderObject = rtProvider;
                menuItem.Database = this;
                menuItem.TagObject = tag;
                unpinnedProviders.Add(menuItem);
            }
            foreach (Stream i in rtProvider.Streams.Where(v => v.Pinned))
            {
                IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof(TObj));
                menuItem.StreamObject = i;
                menuItem.ProviderObject = rtProvider;
                menuItem.MenuTitle = i.Name;
                menuItem.IsPinned = i.Pinned;
                menuItem.MediaType = i.MediaType;
                menuItem.Database = this;
                menuItem.TagObject = tag;
                pinnedMedias.Add(menuItem);
            }
            foreach (Stream i in rtProvider.Streams.Where(v => !v.Pinned))
            {
                IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof(TObj));
                menuItem.StreamObject = i;
                menuItem.ProviderObject = rtProvider;
                menuItem.MenuTitle = i.Name;
                menuItem.IsPinned = i.Pinned;
                menuItem.MediaType = i.MediaType;
                menuItem.StreamObject.ProviderObject = rtProvider;
                menuItem.Database = this;
                menuItem.TagObject = tag;
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

        private IObjectDatabaseTag CreateIObjectDatabaseTag(Type objectDatabaseTagType)
        {
            return(IObjectDatabaseTag)objectDatabaseTagType.GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        public string GetChat(Stream stream)
        {
            if (stream.ChatEmbed == "html_custom")
                return "<html><body style=\"padding: 0px; margin: 0px;\">" + stream.GetChatEmbedData("HTML") + "</body></html>";
            ChatEmbed chatEmbed = ChatEmbeds.Where(v => v.Name == stream.ChatEmbed).FirstOrDefault();
            return chatEmbed != null ? chatEmbed.Format(stream.ChatEmbedData) : null;
        }

        public void RegenerateGUIDS()
        {
            RegenerateGUIDS(Root);
        }

        private void RegenerateGUIDS(Provider rtProvider)
        {
            foreach (Provider subProvider in rtProvider.SubProviders)
                RegenerateGUIDS(subProvider);

            foreach (Stream i in rtProvider.Streams)
                i.StreamGuid = Guid.NewGuid();
        }

        public void FillTags()
        {
            FillTags(Root);
        }

        private void FillTags(Provider rtProvider)
        {
            foreach (Provider subProvider in rtProvider.SubProviders)
                FillTags(subProvider);

            foreach (Stream i in rtProvider.Streams)
            {
                if (string.IsNullOrEmpty(i.Tags))
                {
                    StreamEmbed embed = GetStreamEmbed(i.StreamEmbed);
                    if (embed != null && !string.IsNullOrEmpty(embed.FriendlyName))
                        i.Tags = i.Name + ";" + rtProvider.Name + ";" + embed.FriendlyName;
                    else
                        i.Tags = i.Name + ";" + rtProvider.Name;
                }
            }
        }
    }
}
