namespace StreamDesk.AppTools
{
    using StreamDesk.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class frmSettings
    {
        private Button btnOK;
        private CheckBox cbAlwaysOnTop;
        private CheckBox cbResize;
        private CheckBox cbUpdateOnStartup;
        private IContainer components;
        private Label label1;
        private Label label4;
        private Label label5;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.cbUpdateOnStartup = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbResize = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbUpdateOnStartup
            // 
            this.cbUpdateOnStartup.AutoSize = true;
            this.cbUpdateOnStartup.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUpdateOnStartup.Location = new System.Drawing.Point(12, 10);
            this.cbUpdateOnStartup.Name = "cbUpdateOnStartup";
            this.cbUpdateOnStartup.Size = new System.Drawing.Size(173, 19);
            this.cbUpdateOnStartup.TabIndex = 14;
            this.cbUpdateOnStartup.Text = "Update streams on startup";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Automatically update the stream database on launch of application.";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(345, 143);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbAlwaysOnTop
            // 
            this.cbAlwaysOnTop.AutoSize = true;
            this.cbAlwaysOnTop.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAlwaysOnTop.Location = new System.Drawing.Point(13, 50);
            this.cbAlwaysOnTop.Name = "cbAlwaysOnTop";
            this.cbAlwaysOnTop.Size = new System.Drawing.Size(173, 19);
            this.cbAlwaysOnTop.TabIndex = 8;
            this.cbAlwaysOnTop.Text = "Keep video window on top";
            this.cbAlwaysOnTop.UseVisualStyleBackColor = true;
            this.cbAlwaysOnTop.CheckedChanged += new System.EventHandler(this.cbAlwaysOnTop_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(276, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Keep the application on top of all other windows.";
            // 
            // cbResize
            // 
            this.cbResize.AutoSize = true;
            this.cbResize.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbResize.Location = new System.Drawing.Point(14, 91);
            this.cbResize.Name = "cbResize";
            this.cbResize.Size = new System.Drawing.Size(157, 19);
            this.cbResize.TabIndex = 10;
            this.cbResize.Text = "Auto-size video window";
            this.cbResize.UseVisualStyleBackColor = true;
            this.cbResize.CheckedChanged += new System.EventHandler(this.cbResize_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(341, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Automatically resize windows to fit provider\'s player control.";
            // 
            // frmSettings
            // 
            this.ClientSize = new System.Drawing.Size(429, 176);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbResize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbAlwaysOnTop);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbUpdateOnStartup);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

