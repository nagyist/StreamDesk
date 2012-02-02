using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace StreamDesk.Managed
{
    [Serializable] public class Provider {
        public Provider() {
            SubProviders = new List<Provider>();
            Streams = new List<Stream>();

            Name = "";
            Web = "";
            Description = "";
        }

        [Browsable(false)] public List<Provider> SubProviders { get; set; }
        [Browsable(false)] public List<Stream> Streams { get; set; }
        [Browsable(false), XmlAttribute("name")] public string Name { get; set; }

        [Description("The friendly description of this provider."),
         Category("Provider Properties"), XmlAttribute("desc")] public string Description { get; set; }

        [DisplayName("Web URL"), Description("Descriptive URL for the Provider"),
         Category("Provider Properties"), XmlAttribute("url")] public string Web { get; set; }

        [Description("Pins the provider to the top."), Category("Pinning"), XmlAttribute("pin")] public bool Pinned { get; set; }

        public Provider GetProvider(string name) {
            return SubProviders.Where(v => v.Name == name).FirstOrDefault();
        }

        public Stream GetStream(string name) {
            return Streams.Where(v => v.Name == name).FirstOrDefault();
        }
    }
}