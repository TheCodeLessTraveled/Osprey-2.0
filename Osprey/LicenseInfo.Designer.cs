namespace CodeLessTraveled.Osprey
{
    partial class frmLicenseInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLicenseInfo));
            this.HelpLicSplitContainer = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.HelpLicSplitContainer)).BeginInit();
            this.HelpLicSplitContainer.Panel1.SuspendLayout();
            this.HelpLicSplitContainer.Panel2.SuspendLayout();
            this.HelpLicSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // HelpLicSplitContainer
            // 
            this.HelpLicSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HelpLicSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.HelpLicSplitContainer.Name = "HelpLicSplitContainer";
            this.HelpLicSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // HelpLicSplitContainer.Panel1
            // 
            this.HelpLicSplitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.HelpLicSplitContainer.Panel1.Controls.Add(this.label4);
            // 
            // HelpLicSplitContainer.Panel2
            // 
            this.HelpLicSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.HelpLicSplitContainer.Panel2.Controls.Add(this.btnOK);
            this.HelpLicSplitContainer.Panel2.Controls.Add(this.textBox1);
            this.HelpLicSplitContainer.Size = new System.Drawing.Size(587, 338);
            this.HelpLicSplitContainer.SplitterDistance = 26;
            this.HelpLicSplitContainer.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "License Information";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(492, 276);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(25, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(542, 232);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // frmLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 338);
            this.ControlBox = false;
            this.Controls.Add(this.HelpLicSplitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmLicenseInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.HelpLicSplitContainer.Panel1.ResumeLayout(false);
            this.HelpLicSplitContainer.Panel1.PerformLayout();
            this.HelpLicSplitContainer.Panel2.ResumeLayout(false);
            this.HelpLicSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HelpLicSplitContainer)).EndInit();
            this.HelpLicSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer HelpLicSplitContainer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label4;
    }
}