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
using System.Linq;
using System.Text;
using StreamDesk.Managed.Database;

namespace StreamDesk.Managed
{
    public interface IObjectDatabaseTag
    {
        List<IObjectDatabaseTag> SubItems { get; }
        string MenuTitle { get; set; }
        bool IsProvider { get; set; }
        bool IsPinned { get; set; }
        Stream StreamObject { get; set; }
        MediaType MediaType { get; set; }
        Provider ProviderObject { get; set; }
        Provider ParentProviderObject { get; set; }
        StreamDeskDatabase Database { get; set; }
        void CallSubItemsToProperArray();
        object[] TagObject { get; set; }
    }
}
