#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="StreamDatabaseEditor.cs" company="Developers of the StreamDesk Project">
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
//      Stream Editor Form
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Core;

namespace Editor {
    public partial class StreamDatabaseEditor : Form {
        private readonly StreamDeskDatabase _database;

        public StreamDatabaseEditor() : this(new StreamDeskDatabase()) {}

        public StreamDatabaseEditor(StreamDeskDatabase db) {
            InitializeComponent();
            _database = db;
            RefreshTree(null);
        }

        public void RefreshTree(string selectionNode) {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("MAP", "Medias and Providers", 2, 2);
            treeView1.Nodes["MAP"].Nodes.AddRange(_database.GenerateObjectDatabaseTags<EditorItem>());
            treeView1.Nodes.Add("StreamEmbeds", "Stream Embeds", 1, 1);
            foreach (StreamEmbed i in _database.StreamEmbeds) {
                treeView1.Nodes["StreamEmbeds"].Nodes.Add(new TreeNode(i.Name, 3, 3) {
                    Tag = i
                });
            }
            treeView1.Nodes.Add("ChatEmbeds", "Chat Embeds", 1, 1);
            foreach (ChatEmbed i in _database.ChatEmbeds) {
                treeView1.Nodes["ChatEmbeds"].Nodes.Add(new TreeNode(i.Name, 3, 3) {
                    Tag = i
                });
            }

            //treeView1.ExpandAll();

            propertyGrid1.SelectedObject = null;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node is IObjectDatabaseTag) {
                var ex = (IObjectDatabaseTag)e.Node;
                propertyGrid1.SelectedObject = ex.IsProvider ? ex.ProviderObject : (object)ex.MediaObject;
            } else if (e.Node.Tag is StreamEmbed)
                propertyGrid1.SelectedObject = e.Node.Tag;
            else
                propertyGrid1.SelectedObject = null;
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e) {
            var ex = (IObjectDatabaseTag)e.Node;
            if (e.Label == null) {
                MessageBox.Show("You cannot name a object by nothing.", "StreamDesk Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
                return;
            }

            if (ex.ProviderObject.SubProviders.Where(v => v.Name == e.Label).Count() != 0 || ex.ProviderObject.Medias.Where(v => v.Name == e.Label).Count() != 0) {
                MessageBox.Show("This object already exists.", "StreamDesk Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
                return;
            }

            ex.MenuTitle = e.Label;
            if (ex.IsProvider)
                ex.ProviderObject.Name = e.Label;
            else
                ex.MediaObject.Name = e.Label;
        }

        public void NavigateToLastSelection(string xpath) {
            foreach (TreeNode i in treeView1.Nodes) {
                if (i.FullPath == xpath) {
                    treeView1.SelectedNode = i;
                    break;
                }
                foreach (TreeNode j in i.Nodes)
                    NavigateToLastSelection(j, xpath);
            }
        }

        private void NavigateToLastSelection(TreeNode root, string xpath) {
            foreach (TreeNode i in root.Nodes) {
                if (i.FullPath == xpath) {
                    treeView1.SelectedNode = i;
                    break;
                }
                foreach (TreeNode j in i.Nodes)
                    NavigateToLastSelection(j, xpath);
            }
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e) {
            if (e.Item is EditorItem)
                DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e) {
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            TreeNode destinationNode = ((TreeView)sender).GetNodeAt(pt);

            if (destinationNode is EditorItem)
                MoveNode((EditorItem)destinationNode, e);
            else
                MoveNode(destinationNode, e);
        }

        private void MoveNode(TreeNode destinationNode, DragEventArgs e) {
            if (destinationNode.Name == "MAP") {
                EditorItem NewNode;

                if (e.Data.GetDataPresent("Editor.EditorItem", false)) {
                    NewNode = (EditorItem)e.Data.GetData("Editor.EditorItem");

                    if (NewNode.IsProvider) {
                        _database.Root.SubProviders.Add(NewNode.ProviderObject);
                        NewNode.ParentProviderObject.SubProviders.Remove(NewNode.ProviderObject);
                        NewNode.ParentProviderObject = _database.Root;
                        NewNode.Remove();
                        destinationNode.Nodes.Add(NewNode);
                        //RefreshTree(NewNode.FullPath);
                    } else {
                        _database.Root.Medias.Add(NewNode.MediaObject);
                        NewNode.ProviderObject.Medias.Remove(NewNode.MediaObject);
                        NewNode.Remove();
                        NewNode.ProviderObject = _database.Root;
                        destinationNode.Nodes.Add(NewNode);
                        //RefreshTree(NewNode.FullPath);
                    }
                }
            }
        }

        private void MoveNode(EditorItem destNode, DragEventArgs e) {
            EditorItem NewNode;

            if (e.Data.GetDataPresent("Editor.EditorItem", false)) {
                NewNode = (EditorItem)e.Data.GetData("Editor.EditorItem");

                if (destNode == NewNode)
                    return;

                if (NewNode.IsProvider) {
                    destNode.ProviderObject.SubProviders.Add(NewNode.ProviderObject);
                    NewNode.ParentProviderObject.SubProviders.Remove(NewNode.ProviderObject);
                    NewNode.ParentProviderObject = destNode.ProviderObject;
                    NewNode.Remove();
                    destNode.Nodes.Add(NewNode);
                    //RefreshTree(NewNode.FullPath);
                } else {
                    if (destNode.IsProvider) {
                        destNode.ProviderObject.Medias.Add(NewNode.MediaObject);
                        NewNode.ProviderObject.Medias.Remove(NewNode.MediaObject);
                        NewNode.Remove();
                        NewNode.ProviderObject = destNode.ProviderObject;
                        destNode.Nodes.Add(NewNode);
                    }
                    //RefreshTree(NewNode.FullPath);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            RefreshTree(treeView1.SelectedNode != null ? treeView1.SelectedNode.FullPath : null);
        }

        internal void saveDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Text == "New File") {
                var fsd = new SaveFileDialog {
                    Filter = "StreamDesk Binary Database (*.sdb)|*.sdb|StreamDesk XML Database (*.sdx)|*.sdx"
                };

                if (fsd.ShowDialog() == DialogResult.OK) {
                    if (Path.GetExtension(fsd.FileName) == ".sdb")
                        _database.SaveBinaryDatabase(fsd.FileName);
                    else
                        _database.SaveXMLDatabase(fsd.FileName);

                    Text = fsd.FileName;
                }
            } else {
                if (Path.GetExtension(Text) == ".sdb")
                    _database.SaveBinaryDatabase(Text);
                else
                    _database.SaveXMLDatabase(Text);
            }
        }

        private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e) {
            if (e.Node is IObjectDatabaseTag || e.Node.Tag is StreamEmbed)
                e.CancelEdit = false;
            else
                e.CancelEdit = true;
        }

        private void providerToolStripMenuItem_Click(object sender, EventArgs e) {
            EditorItem selectedNode = null;
            if (treeView1.SelectedNode is EditorItem)
                selectedNode = (EditorItem)treeView1.SelectedNode;

            Provider provider = selectedNode != null ? selectedNode.ProviderObject : _database.Root;

            var newProvider = new Provider();
            var nop = new NewObject(newProvider);
            if (nop.ShowDialog() != DialogResult.OK)
                return;
            newProvider.Name = nop.ObjectName;
            provider.SubProviders.Add(newProvider);
            treeView1.SelectedNode.Nodes.Add(_database.GenerateObjectDatabaseTag<EditorItem>(provider, newProvider));
            //RefreshTree(null);
        }

        private void streamToolStripMenuItem_Click(object sender, EventArgs e) {
            EditorItem selectedNode = null;
            if (treeView1.SelectedNode is EditorItem)
                selectedNode = (EditorItem)treeView1.SelectedNode;

            Provider provider = selectedNode != null ? selectedNode.ProviderObject : _database.Root;

            var newStream = new Media();
            var nop = new NewObject(newStream);
            if (nop.ShowDialog() != DialogResult.OK)
                return;
            newStream.Name = nop.ObjectName;
            provider.Medias.Add(newStream);
            treeView1.SelectedNode.Nodes.Add(_database.GenerateObjectDatabaseTag<EditorItem>(provider, newStream));
            //RefreshTree(null);
        }

        private void chatEmbedToolStripMenuItem_Click(object sender, EventArgs e) {
            var embed = new ChatEmbed();
            var nop = new NewObject(embed);
            if (nop.ShowDialog() != DialogResult.OK)
                return;
            embed.Name = nop.ObjectName;
            _database.ChatEmbeds.Add(embed);
            treeView1.Nodes["ChatEmbeds"].Nodes.Add(new TreeNode(embed.Name, 3, 3) {
                Tag = embed
            });
            //RefreshTree(null);
        }

        private void streamEmbedToolStripMenuItem_Click(object sender, EventArgs e) {
            var embed = new StreamEmbed();
            var nop = new NewObject(embed);
            if (nop.ShowDialog() != DialogResult.OK)
                return;
            embed.Name = nop.ObjectName;
            _database.StreamEmbeds.Add(embed);
            treeView1.Nodes["StreamEmbeds"].Nodes.Add(new TreeNode(embed.Name, 3, 3) {
                Tag = embed
            });
            //RefreshTree(null);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            var fsd = new SaveFileDialog {
                Filter = "StreamDesk Binary Database (*.sdb)|*.sdb|StreamDesk XML Database (*.sdx)|*.sdx"
            };

            if (fsd.ShowDialog() == DialogResult.OK) {
                if (Path.GetExtension(fsd.FileName) == ".sdb")
                    _database.SaveBinaryDatabase(fsd.FileName);
                else
                    _database.SaveXMLDatabase(fsd.FileName);

                Text = fsd.FileName;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e) {
            if (treeView1.SelectedNode == null)
                return;
            if (!(treeView1.SelectedNode is EditorItem))
                return;
            var delObj = (EditorItem)treeView1.SelectedNode;
            if (delObj.IsProvider)
                delObj.ParentProviderObject.SubProviders.Remove(delObj.ProviderObject);
            else
                delObj.ProviderObject.Medias.Remove(delObj.MediaObject);
            delObj.Remove();
        }

        private void toolStripButton3_Click(object sender, EventArgs e) {
            toolStripButton3.Checked = !toolStripButton3.Checked;

            treeView1.CheckBoxes = toolStripButton3.Checked;
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e) {
            if (!(e.Node is EditorItem))
                e.Cancel = true;
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e) {
            var node = (EditorItem)e.Node;
            if (node.IsProvider) {
                foreach (EditorItem i in node.Nodes)
                    i.Checked = !i.Checked;
            } else {
                List<object> list = propertyGrid1.SelectedObjects.ToList();
                if (node.Checked)
                    list.Add(node.MediaObject);
                else
                    list.Remove(node.MediaObject);
                propertyGrid1.SelectedObjects = list.ToArray();
            }
        }

        private void regenerateStreamGUIDsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (
                MessageBox.Show(
                    "WARNING: Regenerating Stream GUID's will invalidate features that use the Stream GUID to identify a specific stream (ex. Favorites). This is used in case a regeneration is needed, for example previous databases that didnt have the GUID Field. Are you sure you want to regenerate the Stream GUID's?",
                    "StreamDesk Database Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                _database.RegenerateGUIDS();
                MessageBox.Show("GUID's Regenerated.", "StreamDesk Database Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void autoTagBlankStreamTagsToolStripMenuItem_Click(object sender, EventArgs e) {
            _database.FillTags();
        }
    }
}
