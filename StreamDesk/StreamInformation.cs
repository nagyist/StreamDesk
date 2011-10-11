#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="StreamInformation.cs" company="Developers of the StreamDesk Project">
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
//      Stream Information Form
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using StreamDesk.Core;

namespace StreamDesk {
    public partial class StreamInformation : Form {
        public StreamInformation(Media media) {
            InitializeComponent();
            textBox1.Text = media.Name;
            textBox2.Text = media.Tags;
            linkLabel1.Text = media.Web;
            textBox3.Text = media.Description;
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
