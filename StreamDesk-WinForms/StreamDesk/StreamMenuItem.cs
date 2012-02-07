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
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Managed;
using StreamDesk.Managed.Database;
using StreamDesk.Properties;

namespace StreamDesk {
    /// <summary>
    /// The Editor Item used for holding Streams.
    /// </summary>
    public class StreamMenuItem : ToolStripMenuItem, IObjectDatabaseTag {
        /// <summary>
        /// The MediaType of this Stream.
        /// </summary>
        private MediaType _mediaType;

        public object[] TagObject { get; set; }

        /// <summary>
        /// Initializes a new instance of the EditorItem class
        /// </summary>
        public StreamMenuItem() {
            SubItems = new List<IObjectDatabaseTag>();
            Image = Resources.folder;
        }

        #region IObjectDatabaseTag Members
        /// <summary>
        /// Gets the sub Object Tags for this Provider
        /// </summary>
        public List<IObjectDatabaseTag> SubItems { get; private set; }

        public StreamDeskDatabase Database { get; set; }

        /// <summary>
        /// Gets or sets the Menu Title
        /// </summary>
        public string MenuTitle {
            get { return Text; }
            set { Text = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether if its a Provider
        /// </summary>
        public bool IsProvider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether its pinned
        /// </summary>
        public bool IsPinned { get; set; }

        /// <summary>
        /// Gets or sets the Stream Object associated to this Menu Item
        /// </summary>
        public Stream StreamObject { get; set; }

        /// <summary>
        /// Gets or sets the Provider Object assiciated to this Menu Item
        /// </summary>
        public Provider ProviderObject { get; set; }

        /// <summary>
        /// Gets or sets the Parent Provider Object. Used on Providers.
        /// </summary>
        public Provider ParentProviderObject { get; set; }

        /// <summary>
        /// Gets or sets the Stream Type of this Stream
        /// </summary>
        public MediaType MediaType {
            get { return _mediaType; }

            set {
                _mediaType = value;
                switch (_mediaType) {
                    case MediaType.VideoStream:
                        Image = Resources.webcam;
                        break;
                    case MediaType.AudioStream:
                        Image = Resources.feed;
                        break;
                }
            }
        }

        /// <summary>
        /// This function moves whats in SubItems into the Menu's SubItem Array
        /// </summary>
        public void CallSubItemsToProperArray() {
            foreach (StreamMenuItem i in SubItems.Cast<StreamMenuItem>())
                DropDownItems.Add(i);
        }
        #endregion

        /// <summary>
        /// On MenuItem Click Navigate to the Stream.
        /// </summary>
        /// <param name="e">Event Arguments</param>
        protected override void OnClick(EventArgs e) {
            switch (_mediaType) {
                case MediaType.VideoStream:
                    ((MainStreamForm) TagObject[0]).NavigateToStream(StreamObject, Database);

                    break;
                case MediaType.AudioStream:
                    break;
                default:
                    base.OnClick(e);
                    break;
            }
        }
    }
}
