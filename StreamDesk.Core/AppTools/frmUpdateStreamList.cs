namespace StreamDesk.AppTools
{
    using StreamDesk.AppCore;
    using StreamDesk.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public partial class frmUpdateStreamList : Form
    {
        public frmUpdateStreamList(bool silent)
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cbDoStartup_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.GetStreamsAtStartup = this.cbDoStartup.Checked;
        }

        private void frmUpdateStreamList_Load(object sender, EventArgs e)
        {

        }

        private void frmUpdateStreamList_Shown(object sender, EventArgs e)
        {
            bool isSilent = this.isSilent;
        }
    }
}

