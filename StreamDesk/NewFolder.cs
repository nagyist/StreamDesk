#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="ObjectNewRename.cs" company="Developers of the StreamDesk Project">
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
//      Rename Favorate/Folder and Add Folder Dialog
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Core;

namespace StreamDesk {
    public partial class NewFolder : Form {
        private readonly Favorite _favorite;
        private readonly FavoritesFolder _folder;
        private readonly bool _rename;

        public NewFolder(FavoritesFolder folder, bool rename) {
            InitializeComponent();
            _folder = folder;
            _rename = rename;
            if (rename) {
                Text = "Rename Folder";
                textBox1.Text = folder.Name;
            }
        }

        public NewFolder(Favorite favorite) {
            InitializeComponent();
            _favorite = favorite;
            Text = "Rename Favorite";
            _rename = true;
            textBox1.Text = favorite.Name;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (!_rename) {
                if (textBox1.Text != "") {
                    _folder.SubFolders.Add(new FavoritesFolder {
                        Name = textBox1.Text
                    });
                    Close();
                } else
                    MessageBox.Show("Please type in a name. Items cannot have a blank name.", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                if (_favorite != null && textBox1.Text != "") {
                    _favorite.Name = textBox1.Text;
                    Close();
                } else if (_folder != null && textBox1.Text != "") {
                    _folder.Name = textBox1.Text;
                    Close();
                } else
                    MessageBox.Show("Please type in a name. Items cannot have a blank name.", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
