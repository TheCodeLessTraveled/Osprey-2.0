namespace CodeLessTraveled.Osprey
{
    partial class ChildExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChildExplorer));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.lblError = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TS_ButtonBack = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonForward = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TS_TextboxUri = new System.Windows.Forms.ToolStripTextBox();
            this.TS_ButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status_SizeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(778, 419);
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.DarkOrange;
            this.lblError.Location = new System.Drawing.Point(378, 68);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 2;
            this.lblError.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TS_ButtonBack,
            this.TS_ButtonForward,
            this.TS_ButtonUp,
            this.toolStripSeparator2,
            this.TS_TextboxUri,
            this.TS_ButtonOpen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(778, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TS_ButtonBack
            // 
            this.TS_ButtonBack.BackColor = System.Drawing.SystemColors.Control;
            this.TS_ButtonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_ButtonBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS_ButtonBack.Image = global::CodeLessTraveled.Osprey.Properties.Resources._112_LeftArrowLong_Blue_24x24_72;
            this.TS_ButtonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.TS_ButtonBack.Name = "TS_ButtonBack";
            this.TS_ButtonBack.Size = new System.Drawing.Size(23, 25);
            this.TS_ButtonBack.ToolTipText = "Back";
            this.TS_ButtonBack.Click += new System.EventHandler(this.TS_ButtonBack_Click);
            // 
            // TS_ButtonForward
            // 
            this.TS_ButtonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_ButtonForward.Image = global::CodeLessTraveled.Osprey.Properties.Resources._112_RightArrowLong_Blue_24x24_72;
            this.TS_ButtonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonForward.Margin = new System.Windows.Forms.Padding(0);
            this.TS_ButtonForward.Name = "TS_ButtonForward";
            this.TS_ButtonForward.Size = new System.Drawing.Size(23, 25);
            this.TS_ButtonForward.Text = "toolStripButton1";
            this.TS_ButtonForward.Click += new System.EventHandler(this.TS_ButtonForward_Click);
            // 
            // TS_ButtonUp
            // 
            this.TS_ButtonUp.BackColor = System.Drawing.SystemColors.Control;
            this.TS_ButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_ButtonUp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS_ButtonUp.Image = global::CodeLessTraveled.Osprey.Properties.Resources._112_UpArrowLong_Blue_24x24_72;
            this.TS_ButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonUp.Margin = new System.Windows.Forms.Padding(0);
            this.TS_ButtonUp.Name = "TS_ButtonUp";
            this.TS_ButtonUp.Size = new System.Drawing.Size(23, 25);
            this.TS_ButtonUp.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.TS_ButtonUp.ToolTipText = "Up";
            this.TS_ButtonUp.Click += new System.EventHandler(this.TS_ButtonUp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // TS_TextboxUri
            // 
            this.TS_TextboxUri.AutoSize = false;
            this.TS_TextboxUri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TS_TextboxUri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TS_TextboxUri.Name = "TS_TextboxUri";
            this.TS_TextboxUri.Size = new System.Drawing.Size(400, 23);
            this.TS_TextboxUri.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TS_TextboxUri_KeyUp_1);
            // 
            // TS_ButtonOpen
            // 
            this.TS_ButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_ButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("TS_ButtonOpen.Image")));
            this.TS_ButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonOpen.Name = "TS_ButtonOpen";
            this.TS_ButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.TS_ButtonOpen.Text = "&Open";
            this.TS_ButtonOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status_SizeLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(778, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Status_SizeLabel
            // 
            this.Status_SizeLabel.Name = "Status_SizeLabel";
            this.Status_SizeLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 419);
            this.panel1.TabIndex = 8;
            // 
            // ChildExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChildExplorer";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Select a path";
            this.Load += new System.EventHandler(this.ChildExplorer_Load);
            this.Resize += new System.EventHandler(this.ChildExplorer_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TS_ButtonBack;
        private System.Windows.Forms.ToolStripButton TS_ButtonUp;
        private System.Windows.Forms.ToolStripTextBox TS_TextboxUri;
        private System.Windows.Forms.ToolStripButton TS_ButtonOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status_SizeLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton TS_ButtonForward;
    }
}