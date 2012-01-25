#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="MainStreamForm.cs" company="Developers of the StreamDesk Project">
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
//      Main Stream Form
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Managed;

namespace StreamDesk {
    public partial class MainStreamForm : Form {

        public MainStreamForm(bool webBrowserWindow) {
            InitializeComponent();

            foreach (StreamMenuItem i in Program.Database.GenerateObjectDatabaseTags<StreamMenuItem>())
                streamsToolStripMenuItem.DropDownItems.Add(i);

            webBrowser1.ScrollBarsEnabled = webBrowserWindow;
            toolStrip1.Visible = webBrowserWindow;
        }

        public Stream ActiveStreamObject { get; private set; }

        internal void NavigateToStream(Stream streamObject) {
            if (streamObject.StreamEmbed == "url_browser") {
                webBrowser1.ScrollBarsEnabled = true;
                toolStrip1.Visible = true;
            } else {
                webBrowser1.ScrollBarsEnabled = false;
                toolStrip1.Visible = false;
            }

            viewToolStripMenuItem.Visible = true;
            ActiveStreamObject = streamObject;

            if (streamObject.ChatEmbed != "none" || streamObject.ChatEmbed != null)
                chatToolStripMenuItem.Visible = true;
            else
                chatToolStripMenuItem.Visible = false;

            Text = streamObject.Name + " > " + streamObject.ProviderObject.Name;

            if (streamObject.StreamEmbed == "url_browser" || streamObject.StreamEmbed == "url_custom")
                webBrowser1.Navigate(streamObject.GetStreamEmbedData("URL"));
            else {
                ClientSize = streamObject.Size;
                webBrowser1.DocumentText = Program.Database.GetStream(streamObject);
            }
        }

        private void streamInformationToolStripMenuItem_Click(object sender, EventArgs e) {
            new StreamInformation(ActiveStreamObject).ShowDialog();
        }

        private void webChatToolStripMenuItem_Click(object sender, EventArgs e) {
            new ChatWindow(Program.Database.GetChat(ActiveStreamObject), Text) {
                MdiParent = Program.MainForm
            }.Show();
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
    }
}
