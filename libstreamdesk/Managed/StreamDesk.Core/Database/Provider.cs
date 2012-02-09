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
using System.Linq;
using System.Xml.Serialization;

namespace StreamDesk.Managed.Database
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