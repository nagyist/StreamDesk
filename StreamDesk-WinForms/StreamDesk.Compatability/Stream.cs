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
    public class Media
    {
        [NonSerialized, XmlIgnore]
        public Provider ProviderObject;

        public Media()
        {
            ChatEmbedData = new List<EmbedData>();
            StreamEmbedData = new List<EmbedData>();
            MediaType = MediaType.VideoStream;
            StreamGuid = Guid.NewGuid();
        }

        public MediaType MediaType { get; set; }
        public string Name { get; set; }
        public string Web { get; set; }
        public Size Size { get; set; }
        public string StreamEmbed { get; set; }
        public List<EmbedData> StreamEmbedData { get; set; }
        public string ChatEmbed { get; set; }
        public List<EmbedData> ChatEmbedData { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public Guid StreamGuid { get; set; }
        public bool Pinned { get; set; }
    }
}
