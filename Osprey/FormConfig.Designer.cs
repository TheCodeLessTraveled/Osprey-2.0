
namespace CodeLessTraveled.Osprey
{
    partial class FormConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.FolderBrowserDialog_XmlFiles = new System.Windows.Forms.FolderBrowserDialog();
            this.Cfg_XmlRepo_TextBox = new System.Windows.Forms.TextBox();
            this.Cfg_XmlRepoBrowse_Button = new System.Windows.Forms.Button();
            this.Cfg_Message_TextBox = new System.Windows.Forms.TextBox();
            this.cfg_Save_Button = new System.Windows.Forms.Button();
            this.Cfg_UseDefPath_ChkBox = new System.Windows.Forms.CheckBox();
            this.Cfg_Cancel_Button = new System.Windows.Forms.Button();
            this.Cfg_UseDefaultColor_ChkBox = new System.Windows.Forms.CheckBox();
            this.Cfg_ColorDialog_FileColor = new System.Windows.Forms.ColorDialog();
            this.Cfg_MainMenuColor_Btn = new System.Windows.Forms.Button();
            this.Cfg_LogEvents_ChkBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cfg_XmlRepo_TextBox
            // 
            this.Cfg_XmlRepo_TextBox.Enabled = false;
            this.Cfg_XmlRepo_TextBox.Location = new System.Drawing.Point(59, 61);
            this.Cfg_XmlRepo_TextBox.Name = "Cfg_XmlRepo_TextBox";
            this.Cfg_XmlRepo_TextBox.Size = new System.Drawing.Size(524, 20);
            this.Cfg_XmlRepo_TextBox.TabIndex = 0;
            this.Cfg_XmlRepo_TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cfg_XmlRep_TextBox_KeyUp);
            // 
            // Cfg_XmlRepoBrowse_Button
            // 
            this.Cfg_XmlRepoBrowse_Button.Enabled = false;
            this.Cfg_XmlRepoBrowse_Button.Image = global::CodeLessTraveled.Osprey.Properties.Resources.openHS;
            this.Cfg_XmlRepoBrowse_Button.Location = new System.Drawing.Point(23, 60);
            this.Cfg_XmlRepoBrowse_Button.Name = "Cfg_XmlRepoBrowse_Button";
            this.Cfg_XmlRepoBrowse_Button.Size = new System.Drawing.Size(25, 25);
            this.Cfg_XmlRepoBrowse_Button.TabIndex = 2;
            this.Cfg_XmlRepoBrowse_Button.UseVisualStyleBackColor = true;
            this.Cfg_XmlRepoBrowse_Button.Click += new System.EventHandler(this.Cfg_XmlRepBrowseButton_Click);
            // 
            // Cfg_Message_TextBox
            // 
            this.Cfg_Message_TextBox.BackColor = System.Drawing.SystemColors.Info;
            this.Cfg_Message_TextBox.ForeColor = System.Drawing.Color.DarkRed;
            this.Cfg_Message_TextBox.Location = new System.Drawing.Point(37, 417);
            this.Cfg_Message_TextBox.Multiline = true;
            this.Cfg_Message_TextBox.Name = "Cfg_Message_TextBox";
            this.Cfg_Message_TextBox.ReadOnly = true;
            this.Cfg_Message_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Cfg_Message_TextBox.Size = new System.Drawing.Size(617, 34);
            this.Cfg_Message_TextBox.TabIndex = 3;
            // 
            // cfg_Save_Button
            // 
            this.cfg_Save_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cfg_Save_Button.Location = new System.Drawing.Point(37, 356);
            this.cfg_Save_Button.Name = "cfg_Save_Button";
            this.cfg_Save_Button.Size = new System.Drawing.Size(75, 23);
            this.cfg_Save_Button.TabIndex = 4;
            this.cfg_Save_Button.Text = "Set";
            this.cfg_Save_Button.UseVisualStyleBackColor = true;
            this.cfg_Save_Button.Click += new System.EventHandler(this.cfg_Save_Button_Click);
            // 
            // Cfg_UseDefPath_ChkBox
            // 
            this.Cfg_UseDefPath_ChkBox.AutoSize = true;
            this.Cfg_UseDefPath_ChkBox.Checked = true;
            this.Cfg_UseDefPath_ChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cfg_UseDefPath_ChkBox.Location = new System.Drawing.Point(30, 35);
            this.Cfg_UseDefPath_ChkBox.Name = "Cfg_UseDefPath_ChkBox";
            this.Cfg_UseDefPath_ChkBox.Size = new System.Drawing.Size(148, 17);
            this.Cfg_UseDefPath_ChkBox.TabIndex = 5;
            this.Cfg_UseDefPath_ChkBox.Text = "Use default Xml repository";
            this.Cfg_UseDefPath_ChkBox.UseVisualStyleBackColor = true;
            this.Cfg_UseDefPath_ChkBox.CheckedChanged += new System.EventHandler(this.Cfg_UseDefault_ChkBox_CheckedChanged);
            // 
            // Cfg_Cancel_Button
            // 
            this.Cfg_Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cfg_Cancel_Button.Location = new System.Drawing.Point(132, 356);
            this.Cfg_Cancel_Button.Name = "Cfg_Cancel_Button";
            this.Cfg_Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cfg_Cancel_Button.TabIndex = 6;
            this.Cfg_Cancel_Button.Text = "Cancel";
            this.Cfg_Cancel_Button.UseVisualStyleBackColor = true;
            this.Cfg_Cancel_Button.Click += new System.EventHandler(this.Cfg_Cancel_Button_Click);
            // 
            // Cfg_UseDefaultColor_ChkBox
            // 
            this.Cfg_UseDefaultColor_ChkBox.AutoSize = true;
            this.Cfg_UseDefaultColor_ChkBox.Location = new System.Drawing.Point(30, 37);
            this.Cfg_UseDefaultColor_ChkBox.Name = "Cfg_UseDefaultColor_ChkBox";
            this.Cfg_UseDefaultColor_ChkBox.Size = new System.Drawing.Size(109, 17);
            this.Cfg_UseDefaultColor_ChkBox.TabIndex = 7;
            this.Cfg_UseDefaultColor_ChkBox.Text = "Use Default Color";
            this.Cfg_UseDefaultColor_ChkBox.UseVisualStyleBackColor = true;
            this.Cfg_UseDefaultColor_ChkBox.CheckedChanged += new System.EventHandler(this.Cfg_UseDefaultColor_ChkBox_CheckedChanged);
            // 
            // Cfg_MainMenuColor_Btn
            // 
            this.Cfg_MainMenuColor_Btn.BackgroundImage = global::CodeLessTraveled.Osprey.Properties.Resources.Color_linecolor;
            this.Cfg_MainMenuColor_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Cfg_MainMenuColor_Btn.Location = new System.Drawing.Point(156, 33);
            this.Cfg_MainMenuColor_Btn.Name = "Cfg_MainMenuColor_Btn";
            this.Cfg_MainMenuColor_Btn.Size = new System.Drawing.Size(33, 23);
            this.Cfg_MainMenuColor_Btn.TabIndex = 8;
            this.Cfg_MainMenuColor_Btn.UseVisualStyleBackColor = true;
            this.Cfg_MainMenuColor_Btn.Click += new System.EventHandler(this.Cfg_MainMenuColor_Btn_Click);
            // 
            // Cfg_LogEvents_ChkBox
            // 
            this.Cfg_LogEvents_ChkBox.AutoSize = true;
            this.Cfg_LogEvents_ChkBox.Enabled = false;
            this.Cfg_LogEvents_ChkBox.Location = new System.Drawing.Point(30, 113);
            this.Cfg_LogEvents_ChkBox.Name = "Cfg_LogEvents_ChkBox";
            this.Cfg_LogEvents_ChkBox.Size = new System.Drawing.Size(111, 17);
            this.Cfg_LogEvents_ChkBox.TabIndex = 9;
            this.Cfg_LogEvents_ChkBox.Text = "Log Events to File";
            this.Cfg_LogEvents_ChkBox.UseVisualStyleBackColor = true;
            this.Cfg_LogEvents_ChkBox.Visible = false;
            this.Cfg_LogEvents_ChkBox.CheckedChanged += new System.EventHandler(this.Cfg_LogEvents_ChkBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(231, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 59);
            this.label1.TabIndex = 10;
            this.label1.Text = "Assign a color for this XML configuration file that will display when the file is" +
    " opened. * Note: Folder groups also have a color property which will override th" +
    "is setting color . ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Cfg_XmlRepo_TextBox);
            this.groupBox1.Controls.Add(this.Cfg_XmlRepoBrowse_Button);
            this.groupBox1.Controls.Add(this.Cfg_LogEvents_ChkBox);
            this.groupBox1.Controls.Add(this.Cfg_UseDefPath_ChkBox);
            this.groupBox1.Location = new System.Drawing.Point(37, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 169);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Cfg_UseDefaultColor_ChkBox);
            this.groupBox2.Controls.Add(this.Cfg_MainMenuColor_Btn);
            this.groupBox2.Location = new System.Drawing.Point(37, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(622, 104);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "XML File Settings";
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 484);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cfg_Cancel_Button);
            this.Controls.Add(this.cfg_Save_Button);
            this.Controls.Add(this.Cfg_Message_TextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.FormConfig_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormConfig_FormClosed);
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog_XmlFiles;
        private System.Windows.Forms.TextBox Cfg_XmlRepo_TextBox;
        private System.Windows.Forms.Button Cfg_XmlRepoBrowse_Button;
        private System.Windows.Forms.TextBox Cfg_Message_TextBox;
        private System.Windows.Forms.Button cfg_Save_Button;
        private System.Windows.Forms.CheckBox Cfg_UseDefPath_ChkBox;
        private System.Windows.Forms.Button Cfg_Cancel_Button;
        private System.Windows.Forms.CheckBox Cfg_UseDefaultColor_ChkBox;
        private System.Windows.Forms.ColorDialog Cfg_ColorDialog_FileColor;
        private System.Windows.Forms.Button Cfg_MainMenuColor_Btn;
        private System.Windows.Forms.CheckBox Cfg_LogEvents_ChkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}