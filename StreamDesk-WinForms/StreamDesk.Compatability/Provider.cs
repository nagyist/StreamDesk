#region Licensing Information
/***************************************************************************************************
 * NasuTek StreamDesk
 * Copyright � 2007-2012 NasuTek Enterprises
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