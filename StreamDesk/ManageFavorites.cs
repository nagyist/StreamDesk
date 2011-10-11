#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="ManageFavorites.cs" company="Developers of the StreamDesk Project">
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
//      Manage Favorites Window
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Core;

namespace StreamDesk {
    public partial class ManageFavorites : Form {
        public ManageFavorites() {
            InitializeComponent();
        }

        private void ManageFavorites_Load(object sender, EventArgs e) {
            RefreshFavorites();
        }

        private void RefreshFavorites(FavoritesFolder folder, TreeNode currNode) {
            foreach (FavoritesFolder favoritesFolder in folder.SubFolders) {
                var node = new TreeNode(favoritesFolder.Name) {
                    Tag = new[] {
                        folder, favoritesFolder
                    }
                };
                RefreshFavorites(favoritesFolder, node);
                if (currNode == null)
                    treeView1.Nodes.Add(node);
                else
                    currNode.Nodes.Add(node);
            }

            foreach (Favorite favorite in folder.Favorites) {
                var node = new TreeNode(favorite.Name) {
                    Tag = new object[] {
                        folder, favorite
                    },
                    SelectedImageIndex = 1,
                    ImageIndex = 1
                };
                if (currNode == null)
                    treeView1.Nodes.Add(node);
                else
                    currNode.Nodes.Add(node);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is FavoritesFolder[])
                new NewFolder(((FavoritesFolder[])treeView1.SelectedNode.Tag)[1], false).ShowDialog();
            else if (treeView1.SelectedNode != null && !(treeView1.SelectedNode.Tag is object[]))
                new NewFolder(StreamDeskSettings.Instance.FavoritesRoot, false).ShowDialog();
            else if (treeView1.SelectedNode == null)
                MessageBox.Show("Select a position to add the folder.", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Error);
            RefreshFavorites();
        }

        private void RefreshFavorites() {
            treeView1.Nodes.Clear();
            var node = new TreeNode("Favorites Root");
            RefreshFavorites(StreamDeskSettings.Instance.FavoritesRoot, node);
            treeView1.Nodes.Add(node);
            treeView1.ExpandAll();
        }

        private void button3_Click(object sender, EventArgs e) {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is FavoritesFolder[]) {
                var foo = (FavoritesFolder[])treeView1.SelectedNode.Tag;

                if (MessageBox.Show("You sure you want to delete the folder " + foo[1].Name + "?", "StreamDesk", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    foo[0].SubFolders.Remove(foo[1]);
                    RefreshFavorites();
                }
            } else if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is object[]) {
                var foo = (object[])treeView1.SelectedNode.Tag;

                if (MessageBox.Show("You sure you want to delete the stream " + ((Favorite)foo[1]).Name + "?", "StreamDesk", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    ((FavoritesFolder)foo[0]).Favorites.Remove((Favorite)foo[1]);
                    RefreshFavorites();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is FavoritesFolder[]) {
                var foo = (FavoritesFolder[])treeView1.SelectedNode.Tag;
                new MoveFavorite(foo[1], foo[0]).ShowDialog();
                RefreshFavorites();
            } else if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is object[]) {
                var foo = (object[])treeView1.SelectedNode.Tag;
                new MoveFavorite((Favorite)foo[1], (FavoritesFolder)foo[0]).ShowDialog();
                RefreshFavorites();
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is FavoritesFolder[]) {
                var foo = (FavoritesFolder[])treeView1.SelectedNode.Tag;
                new NewFolder(foo[1], true).ShowDialog();
                RefreshFavorites();
            } else if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is object[]) {
                var foo = (object[])treeView1.SelectedNode.Tag;
                new NewFolder((Favorite)foo[1]).ShowDialog();
                RefreshFavorites();
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
