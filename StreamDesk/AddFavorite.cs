#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="AddFavorite.cs" company="Developers of the StreamDesk Project">
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
//      Add Favorite Window
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Core;

namespace StreamDesk {
    public partial class AddFavorite : Form {
        private readonly Guid _streamId;

        public AddFavorite(Media stream) {
            InitializeComponent();
            _streamId = stream.StreamGuid;
            textBox1.Text = stream.Name + " - " + stream.ProviderObject.Name;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is FavoritesFolder) {
                var node = (FavoritesFolder)treeView1.SelectedNode.Tag;
                node.Favorites.Add(new Favorite {
                    Id = _streamId, Name = textBox1.Text
                });
                Close();
            } else if (treeView1.SelectedNode != null && treeView1.SelectedNode.Text == "Favorites Root") {
                StreamDeskSettings.Instance.FavoritesRoot.Favorites.Add(new Favorite {
                    Id = _streamId, Name = textBox1.Text
                });
                Close();
            } else if (treeView1.SelectedNode == null)
                MessageBox.Show("Select a position to add the stream.", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e) {
            Close();
        }

        private void RefreshFavoriteFolders(FavoritesFolder folder, TreeNode currNode) {
            foreach (FavoritesFolder favoritesFolder in folder.SubFolders) {
                var node = new TreeNode(favoritesFolder.Name) {
                    Tag = favoritesFolder
                };
                RefreshFavoriteFolders(favoritesFolder, node);
                if (currNode == null)
                    treeView1.Nodes.Add(node);
                else
                    currNode.Nodes.Add(node);
            }
        }

        private void RefreshFavoriteFolders() {
            treeView1.Nodes.Clear();
            var node = new TreeNode("Favorites Root");
            RefreshFavoriteFolders(StreamDeskSettings.Instance.FavoritesRoot, node);
            treeView1.Nodes.Add(node);
            treeView1.ExpandAll();
        }

        private void AddFavorite_Load(object sender, EventArgs e) {
            RefreshFavoriteFolders();
        }

        private void button3_Click(object sender, EventArgs e) {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is FavoritesFolder[])
                new NewFolder(((FavoritesFolder[])treeView1.SelectedNode.Tag)[1], false).ShowDialog();
            else if (treeView1.SelectedNode != null && treeView1.SelectedNode.Text == "Favorites Root")
                new NewFolder(StreamDeskSettings.Instance.FavoritesRoot, false).ShowDialog();
            else if (treeView1.SelectedNode == null)
                MessageBox.Show("Select a position to add the folder.", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Error);

            RefreshFavoriteFolders();
        }
    }
}
