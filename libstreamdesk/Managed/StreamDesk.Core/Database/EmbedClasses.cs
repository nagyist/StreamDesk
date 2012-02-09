#region Licensing Information
/***************************************************************************************************
 * NasuTek StreamDesk
 * Copyright © 2007-2012 NasuTek Enterprises
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
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StreamDesk.Managed.Database
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

        public string Format(List<StreamDeskProperty> embedDatas)
        {
            return embedDatas.Aggregate(EmbedFormat,(current, embedData) => current.Replace("$" + embedData.Name + "$", embedData.Value));
        }
    }

    [Serializable]
    public class ChatEmbed : StreamEmbed
    {
        [XmlAttribute("ircsrv")]
        public string IrcServer { get; set; }
    }
}
