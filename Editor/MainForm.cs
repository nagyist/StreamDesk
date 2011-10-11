#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Developers of the StreamDesk Project">
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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Core;

namespace Editor {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void newStreamDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
            new StreamDatabaseEditor {
                MdiParent = this
            }.Show();
        }

        private void importStreamDeskV21DatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
            var openDialog = new OpenFileDialog();

            foreach (IDatabaseImporter i in Program.Importers) {
                if (openDialog.Filter == "")
                    openDialog.Filter += i.Name + "(*" + i.Extention + ")|*" + i.Extention;
                else
                    openDialog.Filter += "|" + i.Name + "(*" + i.Extention + ")|*" + i.Extention;
            }

            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            try {
                new StreamDatabaseEditor(Program.Importers.Where(v => v.Extention == Path.GetExtension(openDialog.FileName)).First().ImportDatabase(openDialog.FileName)) {
                    MdiParent = this
                }.Show();
            } catch (Exception ex) {
                MessageBox.Show("An error occured on import: " + ex.Message, "StreamDesk Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutStreamDeskDatabaseEditorToolStripMenuItem_Click(object sender, EventArgs e) {
            new AboutForm().ShowDialog();
        }

        private void saveDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {}

        private void openDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
            var openDialog = new OpenFileDialog {
                Filter = "StreamDesk Binary Database (*.sdb)|*.sdb|StreamDesk XML Database (*.sdx)|*.sdx"
            };
            if (openDialog.ShowDialog() == DialogResult.OK) {
                new StreamDatabaseEditor(Path.GetExtension(openDialog.FileName) == ".sdb" ? StreamDeskDatabase.OpenBinaryDatabase(openDialog.FileName) : StreamDeskDatabase.OpenXMLDatabase(openDialog.FileName)) {
                    MdiParent = this, Text = openDialog.FileName
                }.Show();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e) {
            if (ActiveMdiChild != null)
                ((StreamDatabaseEditor)ActiveMdiChild).saveDatabaseToolStripMenuItem_Click(sender, e);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e) {
            LayoutMdi(MdiLayout.Cascade);
        }
    }
}
