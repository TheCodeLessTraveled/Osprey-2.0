
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
            this.cfg_XmlRep_TextBox = new System.Windows.Forms.TextBox();
            this.label_xmlRepository = new System.Windows.Forms.Label();
            this.Cfg_XmlRepBrowseButton = new System.Windows.Forms.Button();
            this.Cfg_Message_TextBox = new System.Windows.Forms.TextBox();
            this.cfg_OK_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cfg_XmlRep_TextBox
            // 
            this.cfg_XmlRep_TextBox.Location = new System.Drawing.Point(133, 26);
            this.cfg_XmlRep_TextBox.Name = "cfg_XmlRep_TextBox";
            this.cfg_XmlRep_TextBox.Size = new System.Drawing.Size(503, 20);
            this.cfg_XmlRep_TextBox.TabIndex = 0;
            this.cfg_XmlRep_TextBox.TextChanged += new System.EventHandler(this.cfg_XmlRep_TextBox_TextChanged);
            this.cfg_XmlRep_TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cfg_XmlRep_TextBox_KeyUp);
            // 
            // label_xmlRepository
            // 
            this.label_xmlRepository.Location = new System.Drawing.Point(6, 25);
            this.label_xmlRepository.Name = "label_xmlRepository";
            this.label_xmlRepository.Size = new System.Drawing.Size(86, 29);
            this.label_xmlRepository.TabIndex = 1;
            this.label_xmlRepository.Text = "Xml Config Files Repository";
            // 
            // Cfg_XmlRepBrowseButton
            // 
            this.Cfg_XmlRepBrowseButton.Image = global::CodeLessTraveled.Osprey.Properties.Resources.openHS;
            this.Cfg_XmlRepBrowseButton.Location = new System.Drawing.Point(102, 23);
            this.Cfg_XmlRepBrowseButton.Name = "Cfg_XmlRepBrowseButton";
            this.Cfg_XmlRepBrowseButton.Size = new System.Drawing.Size(25, 25);
            this.Cfg_XmlRepBrowseButton.TabIndex = 2;
            this.Cfg_XmlRepBrowseButton.UseVisualStyleBackColor = true;
            this.Cfg_XmlRepBrowseButton.Click += new System.EventHandler(this.Cfg_XmlRepBrowseButton_Click);
            // 
            // Cfg_Message_TextBox
            // 
            this.Cfg_Message_TextBox.BackColor = System.Drawing.SystemColors.Info;
            this.Cfg_Message_TextBox.ForeColor = System.Drawing.Color.DarkRed;
            this.Cfg_Message_TextBox.Location = new System.Drawing.Point(134, 116);
            this.Cfg_Message_TextBox.Multiline = true;
            this.Cfg_Message_TextBox.Name = "Cfg_Message_TextBox";
            this.Cfg_Message_TextBox.Size = new System.Drawing.Size(502, 60);
            this.Cfg_Message_TextBox.TabIndex = 3;
            // 
            // cfg_OK_Button
            // 
            this.cfg_OK_Button.Location = new System.Drawing.Point(133, 64);
            this.cfg_OK_Button.Name = "cfg_OK_Button";
            this.cfg_OK_Button.Size = new System.Drawing.Size(75, 23);
            this.cfg_OK_Button.TabIndex = 4;
            this.cfg_OK_Button.Text = "OK";
            this.cfg_OK_Button.UseVisualStyleBackColor = true;
            this.cfg_OK_Button.Click += new System.EventHandler(this.cfg_OK_Button_Click);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 226);
            this.Controls.Add(this.cfg_OK_Button);
            this.Controls.Add(this.Cfg_Message_TextBox);
            this.Controls.Add(this.Cfg_XmlRepBrowseButton);
            this.Controls.Add(this.label_xmlRepository);
            this.Controls.Add(this.cfg_XmlRep_TextBox);
            this.Name = "FormConfig";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog_XmlFiles;
        private System.Windows.Forms.TextBox cfg_XmlRep_TextBox;
        private System.Windows.Forms.Label label_xmlRepository;
        private System.Windows.Forms.Button Cfg_XmlRepBrowseButton;
        private System.Windows.Forms.TextBox Cfg_Message_TextBox;
        private System.Windows.Forms.Button cfg_OK_Button;
    }
}