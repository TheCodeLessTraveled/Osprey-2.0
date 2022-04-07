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
            this.TS_ButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.TS_TextboxUri = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_ColorPicker = new System.Windows.Forms.Label();
            this.label_SortOrder = new System.Windows.Forms.Label();
            this.label_FormText = new System.Windows.Forms.Label();
            this.Textbox_SortOrder = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_Title = new System.Windows.Forms.TextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.TS_Button_Options = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TS_ButtonEditColor = new System.Windows.Forms.ToolStripButton();
            this.TS_OrderTextbox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TS_ButtonBack = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonForward = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
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
            this.webBrowser1.Size = new System.Drawing.Size(1101, 263);
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowItemReorder = true;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TS_ButtonOpen,
            this.TS_TextboxUri});
            this.toolStrip1.Location = new System.Drawing.Point(195, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(878, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // TS_ButtonOpen
            // 
            this.TS_ButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_ButtonOpen.Image = global::CodeLessTraveled.Osprey.Properties.Resources.openHS;
            this.TS_ButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonOpen.Name = "TS_ButtonOpen";
            this.TS_ButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.TS_ButtonOpen.Text = "&Open";
            this.TS_ButtonOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // TS_TextboxUri
            // 
            this.TS_TextboxUri.AutoSize = false;
            this.TS_TextboxUri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TS_TextboxUri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TS_TextboxUri.Name = "TS_TextboxUri";
            this.TS_TextboxUri.Size = new System.Drawing.Size(400, 23);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1101, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusMessage
            // 
            this.StatusMessage.Name = "StatusMessage";
            this.StatusMessage.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.button_Cancel);
            this.splitContainer1.Panel1.Controls.Add(this.label_ColorPicker);
            this.splitContainer1.Panel1.Controls.Add(this.label_SortOrder);
            this.splitContainer1.Panel1.Controls.Add(this.label_FormText);
            this.splitContainer1.Panel1.Controls.Add(this.Textbox_SortOrder);
            this.splitContainer1.Panel1.Controls.Add(this.button_OK);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_Title);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer1.Size = new System.Drawing.Size(1101, 514);
            this.splitContainer1.SplitterDistance = 247;
            this.splitContainer1.TabIndex = 8;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(105, 98);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 7;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label_ColorPicker
            // 
            this.label_ColorPicker.AutoSize = true;
            this.label_ColorPicker.Location = new System.Drawing.Point(174, 48);
            this.label_ColorPicker.Name = "label_ColorPicker";
            this.label_ColorPicker.Size = new System.Drawing.Size(63, 13);
            this.label_ColorPicker.TabIndex = 6;
            this.label_ColorPicker.Text = "Pick a color";
            // 
            // label_SortOrder
            // 
            this.label_SortOrder.AutoSize = true;
            this.label_SortOrder.Location = new System.Drawing.Point(10, 44);
            this.label_SortOrder.Name = "label_SortOrder";
            this.label_SortOrder.Size = new System.Drawing.Size(65, 13);
            this.label_SortOrder.TabIndex = 5;
            this.label_SortOrder.Text = "Sort Order #";
            // 
            // label_FormText
            // 
            this.label_FormText.AutoSize = true;
            this.label_FormText.Location = new System.Drawing.Point(28, 15);
            this.label_FormText.Name = "label_FormText";
            this.label_FormText.Size = new System.Drawing.Size(47, 13);
            this.label_FormText.TabIndex = 4;
            this.label_FormText.Text = "Title text";
            // 
            // Textbox_SortOrder
            // 
            this.Textbox_SortOrder.Location = new System.Drawing.Point(89, 41);
            this.Textbox_SortOrder.Name = "Textbox_SortOrder";
            this.Textbox_SortOrder.Size = new System.Drawing.Size(42, 20);
            this.Textbox_SortOrder.TabIndex = 3;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(13, 98);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(243, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_Title
            // 
            this.textBox_Title.Location = new System.Drawing.Point(89, 15);
            this.textBox_Title.Name = "textBox_Title";
            this.textBox_Title.Size = new System.Drawing.Size(270, 20);
            this.textBox_Title.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AllowItemReorder = true;
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TS_Button_Options,
            this.toolStripSeparator3,
            this.TS_ButtonEditColor,
            this.TS_OrderTextbox,
            this.toolStripSeparator4,
            this.TS_ButtonBack,
            this.TS_ButtonForward,
            this.TS_ButtonUp});
            this.toolStrip2.Location = new System.Drawing.Point(3, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(187, 25);
            this.toolStrip2.TabIndex = 9;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // TS_Button_Options
            // 
            this.TS_Button_Options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_Button_Options.Image = global::CodeLessTraveled.Osprey.Properties.Resources.gear_32xMD;
            this.TS_Button_Options.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_Button_Options.Name = "TS_Button_Options";
            this.TS_Button_Options.Size = new System.Drawing.Size(23, 22);
            this.TS_Button_Options.Text = "toolStripButton2";
            this.TS_Button_Options.Click += new System.EventHandler(this.TS_Button_Options_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // TS_ButtonEditColor
            // 
            this.TS_ButtonEditColor.AutoSize = false;
            this.TS_ButtonEditColor.BackColor = System.Drawing.Color.LightSteelBlue;
            this.TS_ButtonEditColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.TS_ButtonEditColor.Image = ((System.Drawing.Image)(resources.GetObject("TS_ButtonEditColor.Image")));
            this.TS_ButtonEditColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonEditColor.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.TS_ButtonEditColor.Name = "TS_ButtonEditColor";
            this.TS_ButtonEditColor.Size = new System.Drawing.Size(13, 13);
            this.TS_ButtonEditColor.Text = "toolStripButton1";
            this.TS_ButtonEditColor.ToolTipText = "Select a color for this child window.";
            this.TS_ButtonEditColor.Click += new System.EventHandler(this.TS_ButtonEditColor_Click_1);
            // 
            // TS_OrderTextbox
            // 
            this.TS_OrderTextbox.AutoSize = false;
            this.TS_OrderTextbox.AutoToolTip = true;
            this.TS_OrderTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TS_OrderTextbox.Font = new System.Drawing.Font("Consolas", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS_OrderTextbox.Margin = new System.Windows.Forms.Padding(1, 0, 6, 0);
            this.TS_OrderTextbox.MaxLength = 2;
            this.TS_OrderTextbox.Name = "TS_OrderTextbox";
            this.TS_OrderTextbox.Size = new System.Drawing.Size(20, 18);
            this.TS_OrderTextbox.ToolTipText = "Child window order. Max 2 digits.";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // TS_ButtonBack
            // 
            this.TS_ButtonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_ButtonBack.Image = ((System.Drawing.Image)(resources.GetObject("TS_ButtonBack.Image")));
            this.TS_ButtonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonBack.Name = "TS_ButtonBack";
            this.TS_ButtonBack.Size = new System.Drawing.Size(45, 22);
            this.TS_ButtonBack.Text = "ArwBk";
            this.TS_ButtonBack.Click += new System.EventHandler(this.TS_ButtonBack_Click);
            // 
            // TS_ButtonForward
            // 
            this.TS_ButtonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_ButtonForward.Image = ((System.Drawing.Image)(resources.GetObject("TS_ButtonForward.Image")));
            this.TS_ButtonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonForward.Name = "TS_ButtonForward";
            this.TS_ButtonForward.Size = new System.Drawing.Size(54, 19);
            this.TS_ButtonForward.Text = "ArwFwd";
            this.TS_ButtonForward.Click += new System.EventHandler(this.TS_ButtonForward_Click);
            // 
            // TS_ButtonUp
            // 
            this.TS_ButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_ButtonUp.Image = ((System.Drawing.Image)(resources.GetObject("TS_ButtonUp.Image")));
            this.TS_ButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonUp.Name = "TS_ButtonUp";
            this.TS_ButtonUp.Size = new System.Drawing.Size(47, 19);
            this.TS_ButtonUp.Text = "ArwUp";
            this.TS_ButtonUp.Click += new System.EventHandler(this.TS_ButtonUp_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1101, 514);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(1101, 539);
            this.toolStripContainer1.TabIndex = 10;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // ChildExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 561);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChildExplorer";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "ChildExplorer_Split";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChildExplorer_FormClosing);
            this.Load += new System.EventHandler(this.ChildExplorer_Load);
            this.Resize += new System.EventHandler(this.ChildExplorer_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox TS_TextboxUri;
        private System.Windows.Forms.ToolStripButton TS_ButtonOpen;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripStatusLabel StatusMessage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_Title;
        private System.Windows.Forms.Label label_SortOrder;
        private System.Windows.Forms.Label label_FormText;
        private System.Windows.Forms.TextBox Textbox_SortOrder;
        private System.Windows.Forms.Label label_ColorPicker;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton TS_Button_Options;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton TS_ButtonBack;
        private System.Windows.Forms.ToolStripButton TS_ButtonForward;
        private System.Windows.Forms.ToolStripButton TS_ButtonUp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton TS_ButtonEditColor;
        private System.Windows.Forms.ToolStripTextBox TS_OrderTextbox;
    }
}