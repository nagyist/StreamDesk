namespace StreamDesk
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using NasuTek.XUL.Runtime;
    using System.IO;
    using StreamDesk.AppCore;

    public partial class frmChat : Form
    {
        GeckoWebBrowser webBrowser = new GeckoWebBrowser();
        string[] data;

        public frmChat(string chatServer, string chatChannel)
        {
            InitializeComponent();
            webBrowser.Dock = DockStyle.Fill;
            wbWebIRC.Controls.Add(webBrowser);
            data = new string[] { chatServer, chatChannel };
        }

        private void frmChat_Load(object sender, EventArgs e)
        {
            webBrowser.Navigate(String.Format("http://127.0.0.1:9898/+chat/{0}/{1}", data[0], data[1]));
        }
    }
}

