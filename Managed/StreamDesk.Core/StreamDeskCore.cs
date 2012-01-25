using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamDesk.Managed
{
    class StreamDeskCore
    {
        public StreamDeskDatabase ActiveDatabase { get; set; }
        public FormatterEngine FormatterEngine { get; private set; }

        public StreamEmbed GetStreamEmbed(string embedName)
        {
            return ActiveDatabase.StreamEmbeds.Where(v => v.Name == embedName).FirstOrDefault();
        }

        public Stream GetStreamObject(Guid streamId)
        {
            return GetStreamObject(streamId, ActiveDatabase.Root);
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
            return (TObj)menuItem;
        }

        public TObj GenerateObjectDatabaseTag<TObj>(Provider providerObject, Provider newProviderObject)
        {
            IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof(TObj));
            menuItem.MenuTitle = newProviderObject.Name;
            menuItem.IsProvider = true;
            menuItem.ProviderObject = newProviderObject;
            menuItem.IsPinned = newProviderObject.Pinned;
            //FillList(menuItem.SubItems, objectDatabaseTagType, subProvider);
            menuItem.CallSubItemsToProperArray();
            menuItem.ParentProviderObject = providerObject;
            return (TObj)menuItem;
        }

        public TObj[] GenerateObjectDatabaseTags<TObj>() where TObj : IObjectDatabaseTag
        {
            var list = new List<IObjectDatabaseTag>();
            FillList<TObj>(list, ActiveDatabase.Root);
            return list.Cast<TObj>().ToArray();
        }

        public Stream[] Search(string searchParam)
        {
            var list = new List<Stream>();
            Search(list, ActiveDatabase.Root, searchParam);
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

        private void FillList<TObj>(List<IObjectDatabaseTag> items, Provider rtProvider)
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
                FillList<TObj>(menuItem.SubItems, subProvider);
                menuItem.CallSubItemsToProperArray();
                menuItem.ParentProviderObject = rtProvider;
                pinnedProviders.Add(menuItem);
            }
            foreach (Provider subProvider in rtProvider.SubProviders.Where(v => !v.Pinned))
            {
                IObjectDatabaseTag menuItem = CreateIObjectDatabaseTag(typeof(TObj));
                menuItem.MenuTitle = subProvider.Name;
                menuItem.IsProvider = true;
                menuItem.ProviderObject = subProvider;
                menuItem.IsPinned = subProvider.Pinned;
                FillList<TObj>(menuItem.SubItems, subProvider);
                menuItem.CallSubItemsToProperArray();
                menuItem.ParentProviderObject = rtProvider;
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
            return (IObjectDatabaseTag)objectDatabaseTagType.GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        public string GetChat(Stream stream)
        {
            if (stream.ChatEmbed == "html_custom")
                return "<html><body style=\"padding: 0px; margin: 0px;\">" + stream.GetChatEmbedData("HTML") + "</body></html>";
            ChatEmbed chatEmbed = ActiveDatabase.ChatEmbeds.Where(v => v.Name == stream.ChatEmbed).FirstOrDefault();
            return chatEmbed != null ? chatEmbed.Format(stream.ChatEmbedData) : null;
        }

        public void RegenerateGUIDS()
        {
            RegenerateGUIDS(ActiveDatabase.Root);
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
            FillTags(ActiveDatabase.Root);
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
