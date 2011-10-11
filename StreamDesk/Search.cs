#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="Search.cs" company="Developers of the StreamDesk Project">
//      Copyright (C) 2011 Developers of the StreamDesk Project.
//          Core Developers/Maintainer: NasuTek Enterprises/Michael Manley
//          Trademark/GUI Designer/Co-Maintainer: KtecK
//          Additional Developers and Contributors are in the DEVELOPERS.txt
//          file
//
//      Licensed under the Apache License, Version 2.0 (the "License");
//      you may not use this file except in compliance with the License.
//      You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//      Unless required by applicable law or agreed to in writing, software
//      distributed under the License is distributed on an "AS IS" BASIS,
//      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//      See the License for the specific language governing permissions and
//      limitations under the License.
// </copyright>
// <summary>
//      Search Form
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Core;

namespace StreamDesk {
    public partial class Search : Form {
        public Search() {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                listView1.Items.Clear();
                foreach (Media media in Program.Database.Search(textBox1.Text)) {
                    listView1.Items.Add(new ListViewItem(new[] {
                        media.Name, media.Description, media.Tags
                    }) {
                        Tag = media
                    });
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e) {
            if (Program.MainForm.ActiveMdiChild is MainStreamForm && listView1.SelectedItems.Count != 0)
                ((MainStreamForm)Program.MainForm.ActiveMdiChild).NavigateToStream((Media)listView1.SelectedItems[0].Tag);
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {}
    }
}
