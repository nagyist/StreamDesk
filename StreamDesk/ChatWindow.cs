﻿#region Licensing Information
//----------------------------------------------------------------------------------
// <copyright file="ChatWindow.cs" company="Developers of the StreamDesk Project">
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
//      Chat Window
// </summary>
//----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StreamDesk {
    public partial class ChatWindow : Form {
        public ChatWindow(string html, string title) {
            InitializeComponent();

            webBrowser1.DocumentText = html;
            Text = String.Format(Text, title);
        }

        private void ChatWindow_Load(object sender, EventArgs e) {}
    }
}