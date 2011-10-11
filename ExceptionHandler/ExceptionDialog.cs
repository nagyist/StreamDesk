using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ExceptionHandler {
    internal partial class ExceptionDialog : Form {
        public ExceptionDialog(Exception exception) {
            InitializeComponent();
            label1.Text = String.Format(label1.Text, exception.Message);
            textBox1.Text = exception.ToString();
            textBox1.Select(0, 0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://ndn.nasutek.com/bugzilla/");
        }

        private void button1_Click(object sender, EventArgs e) {
            Process.GetCurrentProcess().Kill();
        }
    }
}
