﻿#region Licensing Information
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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Managed;
using StreamDesk.Managed.Database;

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
        
        private void aboutStreamDeskDatabaseEditorToolStripMenuItem_Click(object sender, EventArgs e) {
            new AboutForm().ShowDialog();
        }

        private void openDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
            var openDialog = new OpenFileDialog {
                Filter = StreamDeskCore.FormatterEngine.ReturnFilter
            };
            if (openDialog.ShowDialog() == DialogResult.OK) {
                new StreamDatabaseEditor(StreamDeskDatabase.OpenDatabase(openDialog.FileName))
                {
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

        private void loadExtentionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "StreamDesk Plugin (*.dll)|*.dll";
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                var retTurple = StreamDeskCore.FormatterEngine.LoadFormatterDll(openFileDialog.FileName);
                if (!retTurple.Item1)
                    MessageBox.Show(retTurple.Item2.Message, "StreamDesk Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
