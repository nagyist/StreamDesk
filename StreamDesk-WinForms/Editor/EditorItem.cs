﻿#region Licensing Information
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
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Managed;
using StreamDesk.Managed.Database;

namespace Editor {
    public class EditorItem : TreeNode, IObjectDatabaseTag {
        private MediaType _mediaType;

        public EditorItem() {
            SubItems = new List<IObjectDatabaseTag>();
        }

        #region IObjectDatabaseTag Members
        public List<IObjectDatabaseTag> SubItems { get; private set; }

        public string MenuTitle {
            get { return Text; }
            set { Text = value; }
        }

        public object[] TagObject { get; set; }

        public bool IsProvider { get; set; }

        public bool IsPinned { get; set; }

        public Stream StreamObject { get; set; }

        public Provider ProviderObject { get; set; }

        public StreamDeskDatabase Database { get; set; }

        public void CallSubItemsToProperArray() {
            Nodes.AddRange(SubItems.Cast<TreeNode>().ToArray());
        }

        public Provider ParentProviderObject { get; set; }

        public MediaType MediaType {
            get { return _mediaType; }
            set {
                _mediaType = value;
                switch (_mediaType) {
                    case MediaType.VideoStream:
                        SelectedImageIndex = 4;
                        ImageIndex = 4;
                        break;
                    case MediaType.AudioStream:
                        SelectedImageIndex = 5;
                        ImageIndex = 5;
                        break;
                }
            }
        }
        #endregion
    }
}
