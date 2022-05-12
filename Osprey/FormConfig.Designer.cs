
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
            this.FolderBrowserDialog_XmlFiles = new System.Windows.Forms.FolderBrowserDialog();
            this.Cfg_XmlRepo_TextBox = new System.Windows.Forms.TextBox();
            this.label_xmlRepository = new System.Windows.Forms.Label();
            this.Cfg_XmlRepoBrowse_Button = new System.Windows.Forms.Button();
            this.Cfg_Message_TextBox = new System.Windows.Forms.TextBox();
            this.cfg_Save_Button = new System.Windows.Forms.Button();
            this.Cfg_UseDefault_ChkBox = new System.Windows.Forms.CheckBox();
            this.Cfg_Cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Cfg_XmlRepo_TextBox
            // 
            this.Cfg_XmlRepo_TextBox.Enabled = false;
            this.Cfg_XmlRepo_TextBox.Location = new System.Drawing.Point(136, 57);
            this.Cfg_XmlRepo_TextBox.Name = "Cfg_XmlRepo_TextBox";
            this.Cfg_XmlRepo_TextBox.Size = new System.Drawing.Size(503, 20);
            this.Cfg_XmlRepo_TextBox.TabIndex = 0;
            this.Cfg_XmlRepo_TextBox.TextChanged += new System.EventHandler(this.cfg_XmlRep_TextBox_TextChanged);
            this.Cfg_XmlRepo_TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cfg_XmlRep_TextBox_KeyUp);
            // 
            // label_xmlRepository
            // 
            this.label_xmlRepository.Location = new System.Drawing.Point(6, 25);
            this.label_xmlRepository.Name = "label_xmlRepository";
            this.label_xmlRepository.Size = new System.Drawing.Size(86, 29);
            this.label_xmlRepository.TabIndex = 1;
            this.label_xmlRepository.Text = "Xml Config Files Repository";
            // 
            // Cfg_XmlRepoBrowse_Button
            // 
            this.Cfg_XmlRepoBrowse_Button.Enabled = false;
            this.Cfg_XmlRepoBrowse_Button.Image = global::CodeLessTraveled.Osprey.Properties.Resources.openHS;
            this.Cfg_XmlRepoBrowse_Button.Location = new System.Drawing.Point(105, 54);
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
            this.Cfg_Message_TextBox.Location = new System.Drawing.Point(105, 161);
            this.Cfg_Message_TextBox.Multiline = true;
            this.Cfg_Message_TextBox.Name = "Cfg_Message_TextBox";
            this.Cfg_Message_TextBox.ReadOnly = true;
            this.Cfg_Message_TextBox.Size = new System.Drawing.Size(531, 44);
            this.Cfg_Message_TextBox.TabIndex = 3;
            // 
            // cfg_Save_Button
            // 
            this.cfg_Save_Button.Location = new System.Drawing.Point(105, 99);
            this.cfg_Save_Button.Name = "cfg_Save_Button";
            this.cfg_Save_Button.Size = new System.Drawing.Size(75, 23);
            this.cfg_Save_Button.TabIndex = 4;
            this.cfg_Save_Button.Text = "Save";
            this.cfg_Save_Button.UseVisualStyleBackColor = true;
            this.cfg_Save_Button.Click += new System.EventHandler(this.cfg_Save_Button_Click);
            // 
            // Cfg_UseDefault_ChkBox
            // 
            this.Cfg_UseDefault_ChkBox.AutoSize = true;
            this.Cfg_UseDefault_ChkBox.Checked = true;
            this.Cfg_UseDefault_ChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cfg_UseDefault_ChkBox.Location = new System.Drawing.Point(105, 25);
            this.Cfg_UseDefault_ChkBox.Name = "Cfg_UseDefault_ChkBox";
            this.Cfg_UseDefault_ChkBox.Size = new System.Drawing.Size(107, 17);
            this.Cfg_UseDefault_ChkBox.TabIndex = 5;
            this.Cfg_UseDefault_ChkBox.Text = "Use Default Path";
            this.Cfg_UseDefault_ChkBox.UseVisualStyleBackColor = true;
            this.Cfg_UseDefault_ChkBox.CheckedChanged += new System.EventHandler(this.Cfg_UseDefault_ChkBox_CheckedChanged);
            // 
            // Cfg_Cancel_Button
            // 
            this.Cfg_Cancel_Button.Location = new System.Drawing.Point(212, 99);
            this.Cfg_Cancel_Button.Name = "Cfg_Cancel_Button";
            this.Cfg_Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cfg_Cancel_Button.TabIndex = 6;
            this.Cfg_Cancel_Button.Text = "Cancel";
            this.Cfg_Cancel_Button.UseVisualStyleBackColor = true;
            this.Cfg_Cancel_Button.Click += new System.EventHandler(this.Cfg_Cancel_Button_Click);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 242);
            this.Controls.Add(this.Cfg_Cancel_Button);
            this.Controls.Add(this.Cfg_UseDefault_ChkBox);
            this.Controls.Add(this.cfg_Save_Button);
            this.Controls.Add(this.Cfg_Message_TextBox);
            this.Controls.Add(this.Cfg_XmlRepoBrowse_Button);
            this.Controls.Add(this.label_xmlRepository);
            this.Controls.Add(this.Cfg_XmlRepo_TextBox);
            this.Name = "FormConfig";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog_XmlFiles;
        private System.Windows.Forms.TextBox Cfg_XmlRepo_TextBox;
        private System.Windows.Forms.Label label_xmlRepository;
        private System.Windows.Forms.Button Cfg_XmlRepoBrowse_Button;
        private System.Windows.Forms.TextBox Cfg_Message_TextBox;
        private System.Windows.Forms.Button cfg_Save_Button;
        private System.Windows.Forms.CheckBox Cfg_UseDefault_ChkBox;
        private System.Windows.Forms.Button Cfg_Cancel_Button;
    }
}