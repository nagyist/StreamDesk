#region Licensing Information
/***************************************************************************************************
 * NasuTek StreamDesk
 * Copyright © 2007-2012 NasuTek Enterprises
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
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
        public string Name { get; set; }
        public string Value { get; set; }
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
