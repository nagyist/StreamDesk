using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StreamDesk.Core
{
    [Serializable]
    public class StreamEmbed
    {
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string EmbedFormat { get; set; }
    }

    [Serializable]
    public class ChatEmbed : StreamEmbed
    {
        public string IrcServer { get; set; }
    }
}
