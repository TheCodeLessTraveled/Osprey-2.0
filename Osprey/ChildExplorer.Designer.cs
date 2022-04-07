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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TS_ButtonEditColor = new System.Windows.Forms.ToolStripButton();
            this.TS_OrderTextbox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TS_ButtonBack = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonForward = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TS_TextboxUri = new System.Windows.Forms.ToolStripTextBox();
            this.TS_ButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.TS_Options_Button = new System.Windows.Forms.ToolStripButton();
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
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TS_ButtonEditColor,
            this.TS_OrderTextbox,
            this.toolStripSeparator1,
            this.TS_ButtonBack,
            this.TS_ButtonForward,
            this.TS_ButtonUp,
            this.toolStripSeparator2,
            this.TS_TextboxUri,
            this.TS_ButtonOpen,
            this.TS_Options_Button});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(778, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
             // 
            // TS_ButtonEditColor
            // 
            this.TS_ButtonEditColor.AutoSize = false;
            this.TS_ButtonEditColor.BackColor = System.Drawing.Color.LightSteelBlue;
            this.TS_ButtonEditColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.TS_ButtonEditColor.Margin = new System.Windows.Forms.Padding(6, 1, 6, 2);
            this.TS_ButtonEditColor.Name = "TS_ButtonEditColor";
            this.TS_ButtonEditColor.Size = new System.Drawing.Size(16, 16);
            this.TS_ButtonEditColor.ToolTipText = "Select a color for this child window.";
            this.TS_ButtonEditColor.Click += new System.EventHandler(this.TS_ButtonEditColor_Click);
            // 
            // TS_OrderTextbox
            // 
            this.TS_OrderTextbox.AutoSize = false;
            this.TS_OrderTextbox.Font = new System.Drawing.Font("Consolas", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS_OrderTextbox.Margin = new System.Windows.Forms.Padding(1, 0, 6, 0);
            this.TS_OrderTextbox.MaxLength = 2;
            this.TS_OrderTextbox.Name = "TS_OrderTextbox";
            this.TS_OrderTextbox.Size = new System.Drawing.Size(20, 18);
            this.TS_OrderTextbox.ToolTipText = "Child window order. Max 2 digits.";
            this.TS_OrderTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TS_OrderTextbox_KeyUp);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // TS_ButtonBack
            // 
            this.TS_ButtonBack.BackColor = System.Drawing.SystemColors.Control;
            this.TS_ButtonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_ButtonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS_ButtonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.TS_ButtonBack.Name = "TS_ButtonBack";
            this.TS_ButtonBack.Size = new System.Drawing.Size(37, 25);
            this.TS_ButtonBack.Text = "ArrBk";
            this.TS_ButtonBack.ToolTipText = "Back";
            this.TS_ButtonBack.Click += new System.EventHandler(this.TS_ButtonBack_Click);
            // 
            // TS_ButtonForward
            // 
            this.TS_ButtonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_ButtonForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS_ButtonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonForward.Margin = new System.Windows.Forms.Padding(0);
            this.TS_ButtonForward.Name = "TS_ButtonForward";
            this.TS_ButtonForward.Size = new System.Drawing.Size(44, 25);
            this.TS_ButtonForward.Text = "ArrFwd";
            this.TS_ButtonForward.Click += new System.EventHandler(this.TS_ButtonForward_Click);
            // 
            // TS_ButtonUp
            // 
            this.TS_ButtonUp.BackColor = System.Drawing.SystemColors.Control;
            this.TS_ButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_ButtonUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS_ButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonUp.Margin = new System.Windows.Forms.Padding(0);
            this.TS_ButtonUp.Name = "TS_ButtonUp";
            this.TS_ButtonUp.Size = new System.Drawing.Size(38, 25);
            this.TS_ButtonUp.Text = "ArrUp";
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
            this.StatusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(778, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusMessage
            // 
            this.StatusMessage.Name = "StatusMessage";
            this.StatusMessage.Size = new System.Drawing.Size(0, 17);
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
            // TS_Options_Button
            // 
            this.TS_Options_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_Options_Button.Image = ((System.Drawing.Image)(resources.GetObject("TS_Options_Button.Image")));
            this.TS_Options_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_Options_Button.Name = "TS_Options_Button";
            this.TS_Options_Button.Size = new System.Drawing.Size(47, 22);
            this.TS_Options_Button.Text = "Options";
            this.TS_Options_Button.Click += new System.EventHandler(this.TS_Options_Button_Click);
            // 
            // ChildExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChildExplorer";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Select a path";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChildExplorer_FormClosing);
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TS_ButtonBack;
        private System.Windows.Forms.ToolStripButton TS_ButtonUp;
        private System.Windows.Forms.ToolStripTextBox TS_TextboxUri;
        private System.Windows.Forms.ToolStripButton TS_ButtonOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton TS_ButtonForward;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton TS_ButtonEditColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripStatusLabel StatusMessage;
        private System.Windows.Forms.ToolStripTextBox TS_OrderTextbox;
        private System.Windows.Forms.ToolStripButton TS_Options_Button;
    }
}