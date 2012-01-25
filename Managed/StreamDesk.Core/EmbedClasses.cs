using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StreamDesk.Managed
{
    [Serializable]
    public class StreamEmbed
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("friendlyname")]
        public string FriendlyName { get; set; }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), XmlAttribute("embed")]
        public string EmbedFormat { get; set; }

        public string Format(List<EmbedData> embedDatas)
        {
            return embedDatas.Aggregate(EmbedFormat, (current, embedData) => current.Replace("$" + embedData.Name + "$", embedData.Value));
        }
    }

    [Serializable]
    public class ChatEmbed : StreamEmbed
    {
        [XmlAttribute("ircsrv")]
        public string IrcServer { get; set; }
    }
}
