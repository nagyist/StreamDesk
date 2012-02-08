using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using StreamDesk.Managed;
using StreamDesk.Managed.Database;

namespace StreamDesk {
    public partial class LoadDatabases : Form {
        public LoadDatabases() {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Enabled = false;

            foreach (var activeDatabase in StreamDeskSettings.Instance.ActiveDatabases) {
                var wc = new WebClient();
                wc.DownloadDataCompleted += wc_DownloadDataCompleted;
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadDataAsync(new Uri(activeDatabase), activeDatabase);

                while (wc.IsBusy) {
                    Application.DoEvents();
                }
            }

            new MainStreamForm().Show();

            Hide();
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            Invoke(new Action(delegate {
                progressBar1.Value = e.ProgressPercentage;
                if (label2.Text != "Loading " + e.UserState)
                    label2.Text = "Loading " + e.UserState;
            }));
        }

        private void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e) {
            if (e.Error != null)
                Program.Database.FailedDatabases.Add(Tuple.Create((string)e.UserState, e.Error));
            else {
                using (var ms = new System.IO.MemoryStream(e.Result)) {
                    var db = StreamDeskDatabase.OpenDatabase(ms, System.IO.Path.GetExtension((string) e.UserState));
                    db.TagInformation = (string) e.UserState;
                    Program.Database.ActiveDatabases.Add(db);
                }
            }
        }
    }
}
