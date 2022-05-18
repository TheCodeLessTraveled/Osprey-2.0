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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.Opt_ColorDialog = new System.Windows.Forms.ColorDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Opt_UseDefaultColor_Checkbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Opt_Error_Textbox = new System.Windows.Forms.TextBox();
            this.Opt_Cancel_Button = new System.Windows.Forms.Button();
            this.label_SortOrder = new System.Windows.Forms.Label();
            this.label_FormText = new System.Windows.Forms.Label();
            this.Opt_SortOrder_Textbox = new System.Windows.Forms.TextBox();
            this.Opt_OK_Button = new System.Windows.Forms.Button();
            this.Opt_Color_Button = new System.Windows.Forms.Button();
            this.Opt_Title_Textbox = new System.Windows.Forms.TextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.TS_Button_Options = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonEditColor = new System.Windows.Forms.ToolStripButton();
            this.TS_OrderTextbox = new System.Windows.Forms.ToolStripTextBox();
            this.TS_ButtonBack = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonForward = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonUp = new System.Windows.Forms.ToolStripButton();
            this.TS_ButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.TS_TextboxUri = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
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
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(710, 299);
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 530);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(710, 22);
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FloralWhite;
            this.splitContainer1.Panel1.Controls.Add(this.Opt_UseDefaultColor_Checkbox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.Opt_Error_Textbox);
            this.splitContainer1.Panel1.Controls.Add(this.Opt_Cancel_Button);
            this.splitContainer1.Panel1.Controls.Add(this.label_SortOrder);
            this.splitContainer1.Panel1.Controls.Add(this.label_FormText);
            this.splitContainer1.Panel1.Controls.Add(this.Opt_SortOrder_Textbox);
            this.splitContainer1.Panel1.Controls.Add(this.Opt_OK_Button);
            this.splitContainer1.Panel1.Controls.Add(this.Opt_Color_Button);
            this.splitContainer1.Panel1.Controls.Add(this.Opt_Title_Textbox);
            this.splitContainer1.Panel1MinSize = 170;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer1.Size = new System.Drawing.Size(710, 507);
            this.splitContainer1.SplitterDistance = 204;
            this.splitContainer1.TabIndex = 8;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // Opt_UseDefaultColor_Checkbox
            // 
            this.Opt_UseDefaultColor_Checkbox.AutoSize = true;
            this.Opt_UseDefaultColor_Checkbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Opt_UseDefaultColor_Checkbox.Location = new System.Drawing.Point(178, 59);
            this.Opt_UseDefaultColor_Checkbox.Name = "Opt_UseDefaultColor_Checkbox";
            this.Opt_UseDefaultColor_Checkbox.Size = new System.Drawing.Size(109, 17);
            this.Opt_UseDefaultColor_Checkbox.TabIndex = 102;
            this.Opt_UseDefaultColor_Checkbox.Text = "Use Default Color";
            this.Opt_UseDefaultColor_Checkbox.UseVisualStyleBackColor = true;
            this.Opt_UseDefaultColor_Checkbox.CheckedChanged += new System.EventHandler(this.Opt_UseDefaultColor_Checkbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(110, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 12);
            this.label1.TabIndex = 100;
            this.label1.Text = "200 characters";
            // 
            // Opt_Error_Textbox
            // 
            this.Opt_Error_Textbox.BackColor = System.Drawing.SystemColors.Info;
            this.Opt_Error_Textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Opt_Error_Textbox.ForeColor = System.Drawing.Color.DarkRed;
            this.Opt_Error_Textbox.Location = new System.Drawing.Point(106, 113);
            this.Opt_Error_Textbox.Multiline = true;
            this.Opt_Error_Textbox.Name = "Opt_Error_Textbox";
            this.Opt_Error_Textbox.Size = new System.Drawing.Size(292, 50);
            this.Opt_Error_Textbox.TabIndex = 100;
            this.Opt_Error_Textbox.TabStop = false;
            this.Opt_Error_Textbox.Visible = false;
            // 
            // Opt_Cancel_Button
            // 
            this.Opt_Cancel_Button.Location = new System.Drawing.Point(13, 140);
            this.Opt_Cancel_Button.Name = "Opt_Cancel_Button";
            this.Opt_Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Opt_Cancel_Button.TabIndex = 5;
            this.Opt_Cancel_Button.Text = "Cancel";
            this.Opt_Cancel_Button.UseVisualStyleBackColor = true;
            this.Opt_Cancel_Button.Click += new System.EventHandler(this.Opt_Cancel_button_Click);
            // 
            // label_SortOrder
            // 
            this.label_SortOrder.AutoSize = true;
            this.label_SortOrder.Location = new System.Drawing.Point(22, 57);
            this.label_SortOrder.Name = "label_SortOrder";
            this.label_SortOrder.Size = new System.Drawing.Size(65, 13);
            this.label_SortOrder.TabIndex = 100;
            this.label_SortOrder.Text = "Sort Order #";
            // 
            // label_FormText
            // 
            this.label_FormText.AutoSize = true;
            this.label_FormText.Location = new System.Drawing.Point(40, 30);
            this.label_FormText.Name = "label_FormText";
            this.label_FormText.Size = new System.Drawing.Size(47, 13);
            this.label_FormText.TabIndex = 100;
            this.label_FormText.Text = "Title text";
            // 
            // Opt_SortOrder_Textbox
            // 
            this.Opt_SortOrder_Textbox.Location = new System.Drawing.Point(106, 57);
            this.Opt_SortOrder_Textbox.MaxLength = 2;
            this.Opt_SortOrder_Textbox.Name = "Opt_SortOrder_Textbox";
            this.Opt_SortOrder_Textbox.Size = new System.Drawing.Size(19, 20);
            this.Opt_SortOrder_Textbox.TabIndex = 2;
            // 
            // Opt_OK_Button
            // 
            this.Opt_OK_Button.Location = new System.Drawing.Point(12, 111);
            this.Opt_OK_Button.Name = "Opt_OK_Button";
            this.Opt_OK_Button.Size = new System.Drawing.Size(75, 23);
            this.Opt_OK_Button.TabIndex = 4;
            this.Opt_OK_Button.Text = "OK";
            this.Opt_OK_Button.UseVisualStyleBackColor = true;
            this.Opt_OK_Button.Click += new System.EventHandler(this.Opt_OK_button_Click);
            // 
            // Opt_Color_Button
            // 
            this.Opt_Color_Button.Location = new System.Drawing.Point(314, 59);
            this.Opt_Color_Button.Name = "Opt_Color_Button";
            this.Opt_Color_Button.Size = new System.Drawing.Size(74, 23);
            this.Opt_Color_Button.TabIndex = 3;
            this.Opt_Color_Button.Text = "Pick a Color";
            this.Opt_Color_Button.UseVisualStyleBackColor = true;
            this.Opt_Color_Button.Click += new System.EventHandler(this.Opt_Color_Button_Click);
            // 
            // Opt_Title_Textbox
            // 
            this.Opt_Title_Textbox.Location = new System.Drawing.Point(106, 27);
            this.Opt_Title_Textbox.MaxLength = 200;
            this.Opt_Title_Textbox.Name = "Opt_Title_Textbox";
            this.Opt_Title_Textbox.Size = new System.Drawing.Size(282, 20);
            this.Opt_Title_Textbox.TabIndex = 1;
            this.Opt_Title_Textbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Opt_Title_Textbox_KeyUp);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TS_Button_Options,
            this.TS_ButtonEditColor,
            this.TS_OrderTextbox,
            this.TS_ButtonBack,
            this.TS_ButtonForward,
            this.TS_ButtonUp,
            this.TS_ButtonOpen,
            this.TS_TextboxUri});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip2.Size = new System.Drawing.Size(710, 23);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // TS_Button_Options
            // 
            this.TS_Button_Options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_Button_Options.Image = global::CodeLessTraveled.Osprey.Properties.Resources.gear_32xMD;
            this.TS_Button_Options.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_Button_Options.Name = "TS_Button_Options";
            this.TS_Button_Options.Size = new System.Drawing.Size(23, 20);
            this.TS_Button_Options.ToolTipText = "Settings Options. Customize color hint, window order, title text";
            this.TS_Button_Options.Click += new System.EventHandler(this.TS_Button_Options_Click);
            // 
            // TS_ButtonEditColor
            // 
            this.TS_ButtonEditColor.AutoSize = false;
            this.TS_ButtonEditColor.BackColor = System.Drawing.Color.LightGray;
            this.TS_ButtonEditColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.TS_ButtonEditColor.Enabled = false;
            this.TS_ButtonEditColor.Image = ((System.Drawing.Image)(resources.GetObject("TS_ButtonEditColor.Image")));
            this.TS_ButtonEditColor.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TS_ButtonEditColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonEditColor.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
            this.TS_ButtonEditColor.Name = "TS_ButtonEditColor";
            this.TS_ButtonEditColor.Size = new System.Drawing.Size(23, 4);
            this.TS_ButtonEditColor.Text = "toolStripButton1";
            this.TS_ButtonEditColor.ToolTipText = "Use the Settings Options to customize this color hint.";
            this.TS_ButtonEditColor.Click += new System.EventHandler(this.TS_ButtonEditColor_Click_1);
            // 
            // TS_OrderTextbox
            // 
            this.TS_OrderTextbox.AutoSize = false;
            this.TS_OrderTextbox.AutoToolTip = true;
            this.TS_OrderTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TS_OrderTextbox.Enabled = false;
            this.TS_OrderTextbox.Font = new System.Drawing.Font("Consolas", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS_OrderTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.TS_OrderTextbox.MaxLength = 2;
            this.TS_OrderTextbox.Name = "TS_OrderTextbox";
            this.TS_OrderTextbox.Size = new System.Drawing.Size(18, 18);
            this.TS_OrderTextbox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TS_OrderTextbox.ToolTipText = "Window sort order is set by the configuration gear icon.";
            // 
            // TS_ButtonBack
            // 
            this.TS_ButtonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_ButtonBack.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TS_ButtonBack.Image = ((System.Drawing.Image)(resources.GetObject("TS_ButtonBack.Image")));
            this.TS_ButtonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonBack.Name = "TS_ButtonBack";
            this.TS_ButtonBack.Size = new System.Drawing.Size(23, 17);
            this.TS_ButtonBack.Text = "b";
            this.TS_ButtonBack.ToolTipText = "Navigate back";
            this.TS_ButtonBack.Click += new System.EventHandler(this.TS_ButtonBack_Click);
            // 
            // TS_ButtonForward
            // 
            this.TS_ButtonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_ButtonForward.Image = ((System.Drawing.Image)(resources.GetObject("TS_ButtonForward.Image")));
            this.TS_ButtonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonForward.Name = "TS_ButtonForward";
            this.TS_ButtonForward.Size = new System.Drawing.Size(23, 19);
            this.TS_ButtonForward.Text = "f";
            this.TS_ButtonForward.ToolTipText = "Navigate forward";
            this.TS_ButtonForward.Click += new System.EventHandler(this.TS_ButtonForward_Click);
            // 
            // TS_ButtonUp
            // 
            this.TS_ButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_ButtonUp.Image = ((System.Drawing.Image)(resources.GetObject("TS_ButtonUp.Image")));
            this.TS_ButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonUp.Name = "TS_ButtonUp";
            this.TS_ButtonUp.Size = new System.Drawing.Size(23, 19);
            this.TS_ButtonUp.Text = "u";
            this.TS_ButtonUp.ToolTipText = "Navigate up";
            this.TS_ButtonUp.Click += new System.EventHandler(this.TS_ButtonUp_Click);
            // 
            // TS_ButtonOpen
            // 
            this.TS_ButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_ButtonOpen.Image = global::CodeLessTraveled.Osprey.Properties.Resources.openHS;
            this.TS_ButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_ButtonOpen.Name = "TS_ButtonOpen";
            this.TS_ButtonOpen.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.TS_ButtonOpen.Size = new System.Drawing.Size(30, 20);
            this.TS_ButtonOpen.Text = "toolStripButton1";
            this.TS_ButtonOpen.Click += new System.EventHandler(this.TS_ButtonOpen_Click);
            // 
            // TS_TextboxUri
            // 
            this.TS_TextboxUri.AutoSize = false;
            this.TS_TextboxUri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TS_TextboxUri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TS_TextboxUri.Margin = new System.Windows.Forms.Padding(0);
            this.TS_TextboxUri.Name = "TS_TextboxUri";
            this.TS_TextboxUri.Size = new System.Drawing.Size(200, 23);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(710, 507);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(710, 530);
            this.toolStripContainer1.TabIndex = 10;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            this.toolStripContainer1.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripContainer1.TopToolStripPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // ChildExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 552);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChildExplorer";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "ChildExplorer_Split";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChildExplorer_FormClosing);
            this.Load += new System.EventHandler(this.ChildExplorer_Load);
            this.ResizeEnd += new System.EventHandler(this.ChildExplorer_ResizeEnd);
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
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ColorDialog Opt_ColorDialog;
        private System.Windows.Forms.ToolStripStatusLabel StatusMessage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button Opt_OK_Button;
        private System.Windows.Forms.Button Opt_Color_Button;
        private System.Windows.Forms.TextBox Opt_Title_Textbox;
        private System.Windows.Forms.Label label_SortOrder;
        private System.Windows.Forms.Label label_FormText;
        private System.Windows.Forms.TextBox Opt_SortOrder_Textbox;
        private System.Windows.Forms.Button Opt_Cancel_Button;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton TS_Button_Options;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripButton TS_ButtonBack;
        private System.Windows.Forms.ToolStripButton TS_ButtonForward;
        private System.Windows.Forms.ToolStripButton TS_ButtonUp;
        private System.Windows.Forms.ToolStripButton TS_ButtonEditColor;
        private System.Windows.Forms.ToolStripTextBox TS_OrderTextbox;
        private System.Windows.Forms.TextBox Opt_Error_Textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton TS_ButtonOpen;
        private System.Windows.Forms.ToolStripTextBox TS_TextboxUri;
        private System.Windows.Forms.CheckBox Opt_UseDefaultColor_Checkbox;
    }
}