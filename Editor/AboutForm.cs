#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="AboutForm.cs" company="Developers of the StreamDesk Project">
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
//      About Form
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Editor {
    public partial class AboutForm : Form {
        private bool _clicked;
        private string _format = @"StreamDesk Stream Editor
Version {0}
Copyright © 2008-2012 StreamDesk Project";

        public AboutForm() {
            InitializeComponent();
            label1.Text = String.Format(_format, GlobalAssemblyInfo.CleanVersion);
            Text = String.Format(Text, GlobalAssemblyInfo.CleanVersion);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://streamdesk.ca");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://nasutek.com");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://www.apache.org/licenses/LICENSE-2.0");
        }

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }

        private void label1_Click(object sender, EventArgs e) {
            if (!_clicked) {
                label1.Text = String.Format(_format, GlobalAssemblyInfo.FullVersion);
                _clicked = true;
            } else {
                label1.Text = String.Format(_format, GlobalAssemblyInfo.CleanVersion);
                _clicked = false;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            new Credits().ShowDialog();
        }
    }
}
