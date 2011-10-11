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
        private CheckBox cbSystemIRC;
        private CheckBox cbUpdateOnStartup;
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label label4;
        private CheckBox cbMinSize;
        private Label label3;
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
            this.cbUpdateOnStartup = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSystemIRC = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbResize = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbMinSize = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbUpdateOnStartup
            // 
            this.cbUpdateOnStartup.AutoSize = true;
            this.cbUpdateOnStartup.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUpdateOnStartup.Location = new System.Drawing.Point(12, 12);
            this.cbUpdateOnStartup.Name = "cbUpdateOnStartup";
            this.cbUpdateOnStartup.Size = new System.Drawing.Size(173, 19);
            this.cbUpdateOnStartup.TabIndex = 14;
            this.cbUpdateOnStartup.Text = "Update streams on startup";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "If you enable this setting, the application will download\r\na new list of streams " +
                "every time it starts.";
            // 
            // cbSystemIRC
            // 
            this.cbSystemIRC.AutoSize = true;
            this.cbSystemIRC.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSystemIRC.Location = new System.Drawing.Point(12, 71);
            this.cbSystemIRC.Name = "cbSystemIRC";
            this.cbSystemIRC.Size = new System.Drawing.Size(119, 19);
            this.cbSystemIRC.TabIndex = 2;
            this.cbSystemIRC.Text = "Use my IRC client";
            this.cbSystemIRC.UseVisualStyleBackColor = true;
            this.cbSystemIRC.CheckedChanged += new System.EventHandler(this.cbSystemIRC_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Opens IRC-based chat using your preferred IRC client\r\ninstead of the web chat win" +
                "dow.";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(277, 269);
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
            this.cbAlwaysOnTop.Location = new System.Drawing.Point(13, 131);
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
            this.label4.Location = new System.Drawing.Point(28, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Keeps the video player on top of other windows.";
            // 
            // cbResize
            // 
            this.cbResize.AutoSize = true;
            this.cbResize.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbResize.Location = new System.Drawing.Point(14, 175);
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
            this.label5.Location = new System.Drawing.Point(28, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(324, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Automatically grow the video window to fit high-res video.";
            // 
            // cbMinSize
            // 
            this.cbMinSize.AutoSize = true;
            this.cbMinSize.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMinSize.Location = new System.Drawing.Point(14, 220);
            this.cbMinSize.Name = "cbMinSize";
            this.cbMinSize.Size = new System.Drawing.Size(271, 19);
            this.cbMinSize.TabIndex = 13;
            this.cbMinSize.Text = "Limit the minimum size of the video window";
            this.cbMinSize.UseVisualStyleBackColor = true;
            this.cbMinSize.CheckedChanged += new System.EventHandler(this.cbMinSize_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Enable/Disable the minimum size of the video window";
            // 
            // frmSettings
            // 
            this.ClientSize = new System.Drawing.Size(361, 304);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbMinSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbResize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbAlwaysOnTop);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSystemIRC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbUpdateOnStartup);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StreamDesk Setup";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

