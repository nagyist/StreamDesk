namespace StreamDesk.AppTools
{
    using StreamDesk.AppCore;
    using StreamDesk.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    partial class frmUpdateStreamList
    {
        private Button btnClose;
        private CheckBox cbDoStartup;
        private IContainer components;
        private bool isSilent;
        private Label lblTitle;
        private ProgressBar pbDownload;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbDoStartup = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(227, 19);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Updating the stream directory...";
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(12, 66);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(209, 23);
            this.pbDownload.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(233, 66);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbDoStartup
            // 
            this.cbDoStartup.AutoSize = true;
            this.cbDoStartup.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDoStartup.Location = new System.Drawing.Point(13, 38);
            this.cbDoStartup.Name = "cbDoStartup";
            this.cbDoStartup.Size = new System.Drawing.Size(201, 18);
            this.cbDoStartup.TabIndex = 3;
            this.cbDoStartup.Text = "Do this automatically on startup";
            this.cbDoStartup.UseVisualStyleBackColor = true;
            this.cbDoStartup.CheckedChanged += new System.EventHandler(this.cbDoStartup_CheckedChanged);
            // 
            // frmUpdateStreamList
            // 
            this.ClientSize = new System.Drawing.Size(320, 100);
            this.ControlBox = false;
            this.Controls.Add(this.cbDoStartup);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbDownload);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdateStreamList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stream directory update";
            this.Load += new System.EventHandler(this.frmUpdateStreamList_Load);
            this.Shown += new System.EventHandler(this.frmUpdateStreamList_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

