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
    public partial class AddDatabaseUrl : Form
    {
        public AddDatabaseUrl()
        {
            InitializeComponent();
        }

        public string Url { get { return textBox1.Text; } }
    }
}
