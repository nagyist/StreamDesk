namespace StreamDesk
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class frmChat
    {
        private string chatHTML = "<strong>Please wait...</strong>";
        private IContainer components;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChat));
            this.wbWebIRC = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // wbWebIRC
            // 
            this.wbWebIRC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbWebIRC.Location = new System.Drawing.Point(0, 0);
            this.wbWebIRC.Name = "wbWebIRC";
            this.wbWebIRC.Size = new System.Drawing.Size(601, 326);
            this.wbWebIRC.TabIndex = 0;
            // 
            // frmChat
            // 
            this.ClientSize = new System.Drawing.Size(601, 326);
            this.Controls.Add(this.wbWebIRC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChat";
            this.Text = "StreamDesk Chat";
            this.Load += new System.EventHandler(this.frmChat_Load);
            this.ResumeLayout(false);

        }

        private Panel wbWebIRC;
    }
}

