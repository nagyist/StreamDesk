#region License Header
// KtecK Lab's StreamDesk
// Code (C) NasuTek-Alliant Enterprises, 2010; David Kellaway, 2008.
// StreamDesk and the StreamDesk logo are copyright (C) KtecK 2007-2010.
// Licensed under the NasuTek Restrictive Development License Version 1.00
#endregion

#region Using Directives
using System.ComponentModel;
using System.Configuration.Install;

#endregion

namespace StreamDesk {
    [RunInstaller (true)] public partial class ProjectInstaller : Installer {
        public ProjectInstaller () {
            InitializeComponent ();
        }
    }
}