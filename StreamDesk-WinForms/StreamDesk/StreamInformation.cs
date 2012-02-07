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
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Managed;
using StreamDesk.Managed.Database;

namespace StreamDesk {
    public partial class StreamInformation : Form {
        public StreamInformation(Stream stream) {
            InitializeComponent();
            textBox1.Text = stream.Name;
            textBox2.Text = stream.Tags;
            linkLabel1.Text = stream.Web;
            textBox3.Text = stream.Description;
        }

        private void StreamInformation_Load(object sender, EventArgs e) {}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(linkLabel1.Text);
        }

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
