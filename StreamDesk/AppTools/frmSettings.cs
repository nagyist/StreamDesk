namespace StreamDesk.AppTools
{
    using StreamDesk.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            base.Close();
        }

        private void cbAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.VideoTopMost = this.cbAlwaysOnTop.Checked;
        }

        private void cbMinSize_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MinSize = this.cbMinSize.Checked;
        }

        private void cbResize_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.VideoResize = this.cbResize.Checked;
        }

        private void cbSystemIRC_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.UseSystemIRC = this.cbSystemIRC.Checked;
        }

        private void cbUpdateOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.GetStreamsAtStartup = this.cbUpdateOnStartup.Checked;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.cbUpdateOnStartup.Checked = Settings.Default.GetStreamsAtStartup;
            this.cbSystemIRC.Checked = Settings.Default.UseSystemIRC;
            this.cbAlwaysOnTop.Checked = Settings.Default.VideoTopMost;
            this.cbResize.Checked = Settings.Default.VideoResize;
            this.cbMinSize.Checked = Settings.Default.MinSize;
        }

        private void btnResetDict_Click(object sender, EventArgs e)
        {

        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {

        }
    }
}

