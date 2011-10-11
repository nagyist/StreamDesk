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
            Control.CheckForIllegalCrossThreadCalls = false;
            this.isSilent = silent;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cbDoStartup_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.GetStreamsAtStartup = this.cbDoStartup.Checked;
        }

        private void DownloadCompleteCallback(byte[] dataDownloaded)
        {
            if (!this.pbDownload.Visible)
            {
                this.pbDownload.Minimum = 0;
                this.pbDownload.Value = this.pbDownload.Maximum = 1;
            }
            this.lblTitle.Text = "Stream directory updated.";
            if (this.isSilent)
            {
                base.Close();
            }
            else
            {
                this.btnClose.Enabled = true;
            }
        }

        private void DownloadProgressCallback(int bytesSoFar, int totalBytes)
        {
            if (totalBytes != -1)
            {
                this.pbDownload.Style = ProgressBarStyle.Blocks;
                this.pbDownload.Minimum = 0;
                this.pbDownload.Maximum = totalBytes;
                this.pbDownload.Value = bytesSoFar;
            }
            else
            {
                this.pbDownload.Style = ProgressBarStyle.Marquee;
            }
        }

        private void frmUpdateStreamList_Load(object sender, EventArgs e)
        {
            this.pbDownload.Minimum = 0;
            this.pbDownload.Maximum = 0;
            this.pbDownload.Value = 0;
            this.cbDoStartup.Checked = Settings.Default.GetStreamsAtStartup;
            DownloadThread thread = new DownloadThread();
            thread.DownloadUrl = Settings.Default.StreamUpdateURL;
            thread.CompleteCallback += new DownloadCompleteHandler(this.DownloadCompleteCallback);
            thread.ProgressCallback += new DownloadProgressHandler(this.DownloadProgressCallback);
            try
            {
                new Thread(new ThreadStart(thread.Download)).Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Update failed: " + Environment.NewLine + Environment.NewLine + exception.Message, "Stream dictionary update error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void frmUpdateStreamList_Shown(object sender, EventArgs e)
        {
            bool isSilent = this.isSilent;
        }
    }
}

