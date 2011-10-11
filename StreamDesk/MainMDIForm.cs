#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="MainMDIForm.cs" company="Developers of the StreamDesk Project">
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
//      Main Form
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Core;
using StreamDesk.Properties;

namespace StreamDesk {
    public partial class MainMDIForm : Form {
        public MainMDIForm() {
            InitializeComponent();
        }

        private void MainMDIForm_Load(object sender, EventArgs e) {
            new UpdatingStreamDatabase().ShowDialog();
            new MainStreamForm(false) {
                MdiParent = this
            }.Show();

            RefreshMenu();
        }

        private void RefreshMenu() {
            favoritesToolStripMenuItem.DropDownItems.Clear();
            favoritesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                addToolStripMenuItem, manageToolStripMenuItem, toolStripMenuItem2
            });
            RefreshMenu(StreamDeskSettings.Instance.FavoritesRoot, favoritesToolStripMenuItem);
        }

        private void newStreamWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            new MainStreamForm(false) {
                MdiParent = this
            }.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e) {
            new Search().ShowDialog();
        }

        private void aboutStreamDeskToolStripMenuItem_Click(object sender, EventArgs e) {
            new AboutForm().ShowDialog();
        }

        private void newWebBrowserWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            new MainStreamForm(true) {
                MdiParent = this
            }.Show();
        }

        private void updateStreamsDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Running this command will require StreamDesk to close all Stream Windows. Are you sure you want to continue?", "StreamDesk", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes) {
                CloseAllWindows();
                new UpdatingStreamDatabase().ShowDialog();
                new MainStreamForm(false) {
                    MdiParent = this
                }.Show();
            }
        }

        private void CloseAllWindows() {
            try {
                foreach (Form mdiChild in MdiChildren)
                    mdiChild.Close();
            } catch {
                CloseAllWindows();
            }
        }

        private void streamDeskHomeToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start("http://streamdesk.ca");
        }

        private void nasuTekHomeToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start("http://nasutek.com");
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHorizontallyToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticallyToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e) {
            new ManageFavorites().ShowDialog();
            RefreshMenu();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e) {
            if (ActiveMdiChild is MainStreamForm) {
                var frm = (MainStreamForm)ActiveMdiChild;

                if (frm.ActiveMediaObject != null) {
                    new AddFavorite(frm.ActiveMediaObject).ShowDialog();
                    RefreshMenu();
                }
            }
        }

        private void RefreshMenu(FavoritesFolder folder, ToolStripMenuItem menuItem) {
            foreach (FavoritesFolder favoritesFolder in folder.SubFolders) {
                var newMenuItem = new ToolStripMenuItem(favoritesFolder.Name) {
                    Image = Resources.folder_heart
                };
                RefreshMenu(favoritesFolder, newMenuItem);
                menuItem.DropDownItems.Add(newMenuItem);
            }

            foreach (Favorite favorite in folder.Favorites) {
                var newMenuItem = new ToolStripMenuItem(favorite.Name) {
                    Tag = favorite,
                    Image = Resources.webcam
                };
                newMenuItem.Click += newMenuItem_Click;
                menuItem.DropDownItems.Add(newMenuItem);
            }
        }

        private void newMenuItem_Click(object sender, EventArgs e) {
            MainStreamForm mainForm;
            if (ActiveMdiChild is MainStreamForm)
                mainForm = (MainStreamForm)ActiveMdiChild;
            else {
                mainForm = new MainStreamForm(false) {
                    MdiParent = this
                };
                mainForm.Show();
            }
            Guid guid = ((Favorite)((ToolStripMenuItem)sender).Tag).Id;
            Media stream = Program.Database.GetMediaObject(guid);
            if (stream != null)
                mainForm.NavigateToStream(stream);
            else
                MessageBox.Show("Stream no longer exists.", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start("http://streamdesk.ca/pages/support.php");
        }
    }
}
