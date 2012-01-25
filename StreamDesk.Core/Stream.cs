using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StreamDesk.Core
{
    public enum MediaType {
        VideoStream = 1,
        AudioStream = 2,
    }

    [Serializable] public class EmbedData {
        [XmlAttribute("name")] public string Name { get; set; }
        [XmlAttribute("value")] public string Value { get; set; }
    }

    [Serializable]
    public class Stream
    {
        [NonSerialized, XmlIgnore]
        public Provider ProviderObject;

        public Stream()
        {
            ChatEmbedData = new List<EmbedData>();
            StreamEmbedData = new List<EmbedData>();
            MediaType = MediaType.VideoStream;
            StreamGuid = Guid.NewGuid();
        }

        [DisplayName("Stream Type"), Description("The media type of this perticular media item."),
         Category("Stream Properties"), XmlAttribute("mediatype")]
        public MediaType MediaType { get; set; }

        [Browsable(false), XmlAttribute("name")]
        public string Name { get; set; }

        [DisplayName("Web URL"), Description("Descriptive URL for the Stream Type"),
         Category("Stream Properties"), XmlAttribute("url")]
        public string Web { get; set; }

        [Description("The media type of this perticular media item."),
         Category("Stream Properties"), XmlAttribute("size")]
        public Size Size { get; set; }

        [DisplayName("Stream Embed"), Description("The stream embed type of this perticular media item."),
         Category("Stream Properties"), XmlAttribute("streamembed")]
        public string StreamEmbed { get; set; }

        [DisplayName("Stream Embed Properties"), Description("The stream embed properties of this perticular media item."),
         Category("Stream Properties")]
        public List<EmbedData> StreamEmbedData { get; set; }

        [DisplayName("Chat Embed"), Description("The chat embed type of this perticular media item."),
         Category("Chat Properties"), XmlAttribute("chatembed")]
        public string ChatEmbed { get; set; }

        [DisplayName("Chat Embed Properties"), Description("The chat embed properties of this perticular media item."),
         Category("Chat Properties")]
        public List<EmbedData> ChatEmbedData { get; set; }

        [Description("The friendly description of this media."),
         Category("Stream Properties"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), XmlAttribute("desc")]
        public string Description { get; set; }

        [Description("Tags of the stream seperated by ;"),
         Category("Stream Properties"), XmlAttribute("tags")]
        public string Tags { get; set; }

        [Description("The Streams GUID. This field is not editable."),
         Category("Stream Properties"), ReadOnly(true), XmlAttribute("uuid")]
        public Guid StreamGuid { get; set; }

        [Description("Pins the provider to the top."), Category("Pinning"), XmlAttribute("pin")]
        public bool Pinned { get; set; }

        public string GetStreamEmbedData(string p)
        {
            return StreamEmbedData.Where(v => v.Name == p).FirstOrDefault().Value;
        }

        public string GetChatEmbedData(string p)
        {
            return ChatEmbedData.Where(v => v.Name == p).FirstOrDefault().Value;
        }
    }

}
