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

namespace StreamDesk {
    public partial class Search : Form {
        private MainStreamForm mForm;

        public Search(MainStreamForm form) {
            InitializeComponent();
            mForm = form;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                listView1.Items.Clear();

                foreach (var streamDeskDatabase in Program.Database.ActiveDatabases) {
                    foreach (Stream media in streamDeskDatabase.Search(textBox1.Text))
                    {
                        listView1.Items.Add(new ListViewItem(new[] {
                        media.Name, media.Description, media.Tags, streamDeskDatabase.Name
                    })
                    {
                        Tag = new object[] {media,streamDeskDatabase}
                    });
                    }
                }               
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e) {
            mForm.NavigateToStream((Stream)((object[])listView1.SelectedItems[0].Tag)[0], (StreamDeskDatabase)((object[])listView1.SelectedItems[0].Tag)[1]);
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {}
    }
}
