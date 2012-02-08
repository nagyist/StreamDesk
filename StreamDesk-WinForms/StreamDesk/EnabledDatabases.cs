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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using StreamDesk.Managed;
using StreamDesk.Managed.Database;

namespace StreamDesk
{
    public partial class EnabledDatabases : Form
    {
        public EnabledDatabases()
        {
            InitializeComponent();
            RefreshDialog();
        }

        private void RefreshDialog()
        {
            listView1.Items.Clear();
            foreach (var streamDeskDatabase in Program.Database.ActiveDatabases)
            {
                listView1.Items.Add(new ListViewItem(new [] { streamDeskDatabase.Name, streamDeskDatabase.Vendor, streamDeskDatabase.Description }) { Tag = streamDeskDatabase });
            }
            foreach (var failedDatabase in Program.Database.FailedDatabases) {
                listView1.Items.Add(new ListViewItem(new[] { failedDatabase.Item1, "", "Failed to Download Database: " + failedDatabase.Item2.Message }) { Tag = failedDatabase });
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            if (listView1.SelectedItems[0] == null) return;
            if (listView1.SelectedItems[0].Tag is StreamDeskDatabase) {
                var db = (StreamDeskDatabase) listView1.SelectedItems[0].Tag;
                StreamDeskSettings.Instance.ActiveDatabases.Remove(db.TagInformation);
                Program.Database.ActiveDatabases.Remove(db);
                listView1.SelectedItems[0].Remove();
            } else {
                StreamDeskSettings.Instance.ActiveDatabases.Remove((string)listView1.SelectedItems[0].Tag);
                Program.Database.FailedDatabases.Remove((Tuple<string, Exception>)listView1.SelectedItems[0].Tag);
                listView1.SelectedItems[0].Remove();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            groupBox1.Enabled = listView1.SelectedItems.Count != 0;
            if (groupBox1.Enabled) {
                if (listView1.SelectedItems[0].Tag is StreamDeskDatabase)
                    button3.Enabled = ((StreamDeskDatabase) listView1.SelectedItems[0].Tag).TagInformation != "http://streamdesk.sf.net/streams.sdnx";
                else
                    button3.Enabled = ((Tuple<string, Exception>) listView1.SelectedItems[0].Tag).Item1 != "http://streamdesk.sf.net/streams.sdnx";
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            var dbUrl = new AddDatabaseUrl();
            if (dbUrl.ShowDialog() != DialogResult.OK) return;

            if (StreamDeskSettings.Instance.ActiveDatabases.Contains(dbUrl.Url)) return;

            StreamDeskSettings.Instance.ActiveDatabases.Add(dbUrl.Url);

            var wc = new WebClient();
            using (var ms = new System.IO.MemoryStream(wc.DownloadData(dbUrl.Url)))
            {
                var db = StreamDeskDatabase.OpenDatabase(ms, System.IO.Path.GetExtension(dbUrl.Url));
                db.TagInformation = dbUrl.Url;
                Program.Database.ActiveDatabases.Add(db);
            }

            RefreshDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
