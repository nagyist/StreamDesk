namespace StreamDesk.AppCore
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class DownloadThread
    {
        public string _downloadUrl = "";

        public event DownloadCompleteHandler CompleteCallback;

        public event DownloadProgressHandler ProgressCallback;

        public void Download()
        {
            if ((this.CompleteCallback != null) && (this.DownloadUrl != ""))
            {
                byte[] dataDownloaded = new WebDownload().Download(this.DownloadUrl, this.ProgressCallback);
                this.CompleteCallback(dataDownloaded);
                FileStream output = File.Create(Application.UserAppDataPath + @"\streamlist.xml");
                new BinaryWriter(output).Write(dataDownloaded);
                output.Close();
            }
        }

        public string DownloadUrl
        {
            get
            {
                return this._downloadUrl;
            }
            set
            {
                this._downloadUrl = value;
            }
        }
    }
}

