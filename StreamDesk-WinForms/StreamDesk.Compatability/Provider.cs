using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace StreamDesk.Core
{
    [Serializable] public class Provider {
        public Provider() {
            SubProviders = new List<Provider>();
            Medias = new List<Media>();
        }

        public List<Provider> SubProviders { get; set; }
        public List<Media> Medias { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Web { get; set; }

        public bool Pinned { get; set; }
    }
}