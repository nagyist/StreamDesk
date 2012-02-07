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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Managed;
using StreamDesk.Managed.Database;
using StreamDesk.Properties;

namespace StreamDesk {
    public partial class MainStreamForm : Form {
        private bool mClosed;
        private static int mActiveForms;

        public MainStreamForm() {
            mActiveForms++;

            InitializeComponent();

            RefreshStreamsMenu();
        }

        private void RefreshStreamsMenu()
        {
            streamsToolStripMenuItem.DropDownItems.Clear();

            foreach (var streamDeskDatabase in Program.Database.ActiveDatabases)
            {
                var dbName = new ToolStripMenuItem(streamDeskDatabase.Name, Resources.folder);

                foreach (var i in streamDeskDatabase.GenerateObjectDatabaseTags<StreamMenuItem>(new object[] { this }))
                {
                    dbName.DropDownItems.Add(i);
                }

                streamsToolStripMenuItem.DropDownItems.Add(dbName);
            }
        }

        public Stream ActiveStreamObject { get; private set; }
        public StreamDeskDatabase ActiveDatabase { get; private set; }

        internal void NavigateToStream(Stream streamObject, StreamDeskDatabase db) {
            if (streamObject.StreamEmbed == "url_browser") {
                webBrowser1.ScrollBarsEnabled = true;
                toolStrip1.Visible = true;
            } else {
                webBrowser1.ScrollBarsEnabled = false;
                toolStrip1.Visible = false;
            }

            viewToolStripMenuItem.Visible = true;
            ActiveStreamObject = streamObject;
            ActiveDatabase = db;

            if (streamObject.ChatEmbed != "none" || streamObject.ChatEmbed != null)
                chatToolStripMenuItem.Visible = true;
            else
                chatToolStripMenuItem.Visible = false;

            Text = streamObject.Name + " > " + streamObject.ProviderObject.Name;

            if (streamObject.StreamEmbed == "url_browser" || streamObject.StreamEmbed == "url_custom")
                webBrowser1.Navigate(streamObject.GetStreamEmbedData("URL"));
            else {
                ClientSize = new Size(streamObject.Size.Width, streamObject.Size.Height);
                webBrowser1.DocumentText = db.GetStream(streamObject);
            }
        }

        private void streamInformationToolStripMenuItem_Click(object sender, EventArgs e) {
            new StreamInformation(ActiveStreamObject).ShowDialog();
        }

        private void webChatToolStripMenuItem_Click(object sender, EventArgs e) {
            new ChatWindow(ActiveDatabase.GetChat(ActiveStreamObject), Text).Show();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            if (toolStrip1.Visible) {
                toolStripTextBox1.Text = e.Url.ToString();
                Text = webBrowser1.DocumentTitle;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            webBrowser1.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e) {
            webBrowser1.GoForward();
        }

        private void toolStripButton3_Click(object sender, EventArgs e) {
            webBrowser1.Refresh();
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                webBrowser1.Navigate(toolStripTextBox1.Text);
        }


        private void RefreshMenu()
        {
            favoritesToolStripMenuItem.DropDownItems.Clear();
            favoritesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                addToolStripMenuItem, manageToolStripMenuItem, toolStripMenuItem2
            });
            RefreshMenu(StreamDeskSettings.Instance.FavoritesRoot, favoritesToolStripMenuItem);
        }

        private void newStreamWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MainStreamForm()
            {
            }.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            CloseForm();
        }

        private void CloseForm() {
            mActiveForms--;

            if (mActiveForms == 0)
                Application.Exit();
            else
                if (!mClosed) Close();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Search(this).ShowDialog();
        }

        private void aboutStreamDeskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void newWebBrowserWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MainStreamForm()
            {
                MdiParent = this
            }.Show();
        }

        private void updateStreamsDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Update Databases
        }

        private void streamDeskHomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://streamdesk.ca");
        }

        private void nasuTekHomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://nasutek.com");
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ManageFavorites().ShowDialog();
            RefreshMenu();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e) {
            new AddFavorite(ActiveStreamObject).ShowDialog();
            RefreshMenu();
        }

        private void RefreshMenu(FavoritesFolder folder, ToolStripMenuItem menuItem)
        {
            foreach (FavoritesFolder favoritesFolder in folder.SubFolders)
            {
                var newMenuItem = new ToolStripMenuItem(favoritesFolder.Name)
                {
                    Image = Resources.folder_heart
                };
                RefreshMenu(favoritesFolder, newMenuItem);
                menuItem.DropDownItems.Add(newMenuItem);
            }

            foreach (Favorite favorite in folder.Favorites)
            {
                var newMenuItem = new ToolStripMenuItem(favorite.Name)
                {
                    Tag = favorite,
                    Image = Resources.webcam
                };
                newMenuItem.Click += newMenuItem_Click;
                menuItem.DropDownItems.Add(newMenuItem);
            }
        }

        private void newMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var streamDeskDatabase in Program.Database.ActiveDatabases) {
                Guid guid = ((Favorite)((ToolStripMenuItem)sender).Tag).Id;
                Stream stream = streamDeskDatabase.GetStreamObject(guid);
                if (stream != null)
                    NavigateToStream(stream, streamDeskDatabase);
                else
                    MessageBox.Show("Stream no longer exists.", "StreamDesk", MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://streamdesk.ca/pages/support.php");
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e) {
            new Options().ShowDialog();
            RefreshStreamsMenu();
        }

        private void MainStreamForm_FormClosing(object sender, FormClosingEventArgs e) {
            mClosed = true;
            if(e.CloseReason == CloseReason.UserClosing)
                CloseForm();
        }

        private void MainStreamForm_Load(object sender, EventArgs e) {
            if (Program.Database.FailedDatabases.Count <= 0) return;
            if (MessageBox.Show("Some databases failed to load on launch, do you want to review which ones in the Database Manager?", "StreamDesk", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            new EnabledDatabases().ShowDialog();
            RefreshStreamsMenu();
        }
    }
}
