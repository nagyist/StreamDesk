using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StreamDesk
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) {
            new EnabledDatabases().ShowDialog();
        }
    }
}
