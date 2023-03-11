﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeLessTraveled.Osprey
{

    public partial class FormConfig : Form
    {
        private string m_trans_Cfg_XmlRepositoryPath;
        private bool m_trans_Cfg_XmlRepBrowse_Button_Enabled;
        private bool m_trans_Cfg_XmlRep_TextBox_Enabled;
        private string m_trans_Cfg_Message_Textbox_Value;
        private bool m_restore_val_Cfg_UseDefPath_Chkbox;
        private SystemColors m_DefaultXmlFileColor;

        private System.Drawing.Color m_trans_Cfg_Color;
        private bool m_trans_cfg_ColorButton_Enabled;
        private bool m_trans_cfg_ColorDialog_Enabled;
        private bool m_restore_val_Cfg_UseDefColor_Chkbox;
        private Form m_ParentMDI;
        private int m_fileColorArgb = 0;

        private int m_Top = -1;
        private int m_Left = -1;

        public int FileColorArgb
            {   get{ return m_fileColorArgb; }
                set{ m_fileColorArgb = value; }
            }
        public bool Use_Default_FileColor
        {
            get { return Cfg_UseDefaultColor_ChkBox.Checked; }
            set { Cfg_UseDefaultColor_ChkBox.Checked = value; }
        }

        //public int Top
        //{
        //    get { return m_Top; }
        //    set { m_Top = value; }
        //}
        //public int Left
        //{
        //    get { return m_Left; }
        //    set { m_Left = value; }
        //}


        public int LogEvents
        {
            get { return m_fileColorArgb; }
            set { m_fileColorArgb = value; }
        }


        public FormConfig()
        {
            InitializeComponent();
            var DefaultXmlFileColor =  SystemColors.Control;

        }

       

        private void FormConfig_Load(object sender, EventArgs e)
        {
            // Initial settings for the Config UI controls based on the saved value of the alternative xml repo path.

            string XmlSavedRepoPath = Properties.Settings.Default.AltXmlRepository.Trim();
          
                if (String.IsNullOrEmpty(XmlSavedRepoPath))
                {
                    // Use the default setting which is set on for form_load of the parent form, "Form1.cs".
                    Cfg_UseDefPath_ChkBox.Checked       = true;
                    Cfg_XmlRepo_TextBox.Text            = "";
                    Cfg_XmlRepo_TextBox.Enabled         = false;
                    Cfg_XmlRepoBrowse_Button.Enabled    = false;
                }
                else
                {
                    Cfg_UseDefPath_ChkBox.Checked       = false;
                    Cfg_XmlRepo_TextBox.Text            = XmlSavedRepoPath;
                    Cfg_XmlRepo_TextBox.Enabled         = true;
                    Cfg_XmlRepoBrowse_Button.Enabled    = true;
                }

                if (m_fileColorArgb == SystemColors.Control.ToArgb())
                {
                    // user default color
                    Cfg_UseDefaultColor_ChkBox.Checked  = true;
                    Cfg_MainMenuColor_Btn.Enabled       = false;
                }
                else 
                {
                    Cfg_UseDefaultColor_ChkBox.Checked  = false;
                    Cfg_ColorDialog_FileColor.Color      = System.Drawing.Color.FromArgb(m_fileColorArgb);
                    Cfg_MainMenuColor_Btn.Enabled       = true;
                }

            Cfg_UseDefPath_ChkBox.Focus();

        }


        private void Cfg_XmlRepBrowseButton_Click(object sender, EventArgs e)
        {
            string UserInputPath = Cfg_XmlRepo_TextBox.Text.Replace('"',' ').Trim();
            
            using (FolderBrowserDialog_XmlFiles)
            {


                if (!String.IsNullOrEmpty(UserInputPath) && System.IO.Directory.Exists(UserInputPath))
                {
                    FolderBrowserDialog_XmlFiles.SelectedPath = UserInputPath;
                }


                FolderBrowserDialog_XmlFiles.Description = "Browse for and set the XML configuration repository";

                if (FolderBrowserDialog_XmlFiles.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(FolderBrowserDialog_XmlFiles.SelectedPath))
                    {
                        Cfg_XmlRepo_TextBox.Text = FolderBrowserDialog_XmlFiles.SelectedPath;

                        Cfg_Message_TextBox.ForeColor = Color.DarkGreen;

                        string  saveLocMsg  = String.Format("Pressing <Save> will move files from the current location to this new location");
                            //    saveLocMsg += String.Format(" From: {0}", Properties.Settings.Default);
                              //  saveLocMsg += String.Format(" T0:   {0}", FolderBrowserDialog_XmlFiles.SelectedPath);



                        Cfg_Message_TextBox.Text = saveLocMsg;

                        cfg_Save_Button.Focus();
                    }
                    else
                    {
                        Cfg_Message_TextBox.ForeColor = Color.DarkRed;

                        Cfg_Message_TextBox.Text = String.Format("This folder does not exist. Select a valid folder. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);
                    }
                    
                    if (System.IO.Directory.Exists(FolderBrowserDialog_XmlFiles.SelectedPath))
                    {
                        Properties.Settings.Default.AltXmlRepository = FolderBrowserDialog_XmlFiles.SelectedPath;
                    }
                    else
                    {
                        Cfg_Message_TextBox.Text = String.Format("This folder does not exist. Select a valid folder. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);
                    }
                }
            }

        }

        private void cfg_XmlRep_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FolderBrowserDialog_XmlFiles.SelectedPath = Cfg_XmlRepo_TextBox.Text;
                
                
                if (System.IO.Directory.Exists(FolderBrowserDialog_XmlFiles.SelectedPath))
                {

                    Cfg_XmlRepBrowseButton_Click(new object(), new EventArgs());

                    //Properties.Settings.Default.AltXmlRepository = FolderBrowserDialog_XmlFiles.SelectedPath;

                    //Cfg_Message_TextBox.ForeColor = Color.DarkGreen;

                    //Cfg_Message_TextBox.Text = String.Format("New folder saved for default XML repository location. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);

                    //cfg_Save_Button.Focus();

                }
                else
                {
                    Cfg_Message_TextBox.ForeColor = Color.DarkRed;

                    Cfg_Message_TextBox.Text = String.Format("This folder does not exist. Select a valid folder. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);
                }

            }
        }



        private void cfg_Save_Button_Click(object sender, EventArgs e)
        {
            ////////// Evaluate for default File Color  //////////////////////////////////////////////////////
            if (Cfg_UseDefaultColor_ChkBox.Checked == false)
            {
                this.FileColorArgb = m_trans_Cfg_Color.ToArgb();
            }
            else
            {
                this.FileColorArgb = System.Drawing.SystemColors.Control.ToArgb();
            }
            ////////// DEFAULT path /////////////////////////////////////////////////////////////////////////
            if (Cfg_UseDefPath_ChkBox.Checked == false)
            {   
                if (!System.IO.Directory.Exists(Cfg_XmlRepo_TextBox.Text))
                {
                    Cfg_Message_TextBox.ForeColor = Color.DarkRed;

                    Cfg_Message_TextBox.Text = String.Format("The saved path, \"{0}\", does not exist. Correct the path before saving.", Cfg_XmlRepo_TextBox.Text);

                    Properties.Settings.Default.UseDefaultXmlRepository = true;

                    Properties.Settings.Default.AltXmlRepository = null;
                }
                else
                {
                    Properties.Settings.Default.AltXmlRepository = Cfg_XmlRepo_TextBox.Text;

                    Properties.Settings.Default.UseDefaultXmlRepository = false;

                    Cfg_UseDefPath_ChkBox.Checked = false;

                    Cfg_Message_TextBox.Text = String.Format("Folder path saved as default location. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);

                    Cfg_Message_TextBox.Text += Environment.NewLine + "Close and restart the Osprey application.";
                }
            }
            else  // Use the default XmlRepository path
            {
                Cfg_Message_TextBox.Text = "Use Ospry's default Xml repository path.";

                Properties.Settings.Default.AltXmlRepository = "";
            }
            ////////// Log events to file /////////////////////////////////////////////////////////////////// 
            if (Cfg_LogEvents_ChkBox.Checked == false)
            {
                Properties.Settings.Default.LogEventsToFile = false;
            }
            else
            {
                Properties.Settings.Default.LogEventsToFile = true;
            }
          
            this.Close();
        }



        private void Cfg_UseDefaultColor_ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            string x = "";

            m_trans_Cfg_Color                     = Cfg_ColorDialog_FileColor.Color;
            m_trans_cfg_ColorButton_Enabled       = Cfg_MainMenuColor_Btn.Enabled;
            m_restore_val_Cfg_UseDefColor_Chkbox  = !Cfg_UseDefaultColor_ChkBox.Checked;  // at this point we have a transition from one checked state to another.
                                                                                         // to capture the previous state i must apply a "not" logic.
           if (Cfg_UseDefaultColor_ChkBox.Checked == true)                              // if the state now is CHECKED, the restore value is NOT CHECKED which is the value before the click.
            {
                Cfg_MainMenuColor_Btn.Enabled = false;
                m_trans_Cfg_Color = SystemColors.Control;

            }
            else
            {
                Cfg_MainMenuColor_Btn.Enabled = true;
            }

        }

        
        
        private void Cfg_UseDefault_ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            string x = "";

            m_trans_Cfg_XmlRepositoryPath           = Cfg_XmlRepo_TextBox.Text;
            m_trans_Cfg_XmlRepBrowse_Button_Enabled = Cfg_XmlRepoBrowse_Button.Enabled;
            m_trans_Cfg_XmlRep_TextBox_Enabled      = Cfg_XmlRepo_TextBox.Enabled;
            m_trans_Cfg_Message_Textbox_Value       = Cfg_Message_TextBox.Text;
            m_restore_val_Cfg_UseDefPath_Chkbox     = !Cfg_UseDefPath_ChkBox.Checked;   // at this point the checkbox was selected and the previous state (potentially to be restored to) was opposite of new value.
                                                                                        // if it was "CHECKED" before the user clicked it, then we want to capture the "CHECKED" state.
            Cfg_Message_TextBox.Text = "";

            if (Cfg_UseDefPath_ChkBox.Checked == true)
            {
                // this initially is set based on the Properties.Settings value.
                Properties.Settings.Default.UseDefaultXmlRepository = true;

                Cfg_XmlRepo_TextBox.Text        = "";

                Cfg_XmlRepo_TextBox.Enabled     = false;

                Cfg_XmlRepoBrowse_Button.Enabled = false;
            }
            else
            {
                Properties.Settings.Default.UseDefaultXmlRepository = false;

                Cfg_UseDefPath_ChkBox.Checked   = false;

                Cfg_XmlRepo_TextBox.Enabled     = true;

                Cfg_XmlRepoBrowse_Button.Enabled = true;

                string AltXmlRepoPath = Properties.Settings.Default.AltXmlRepository.Trim();

                if (!System.IO.Directory.Exists(AltXmlRepoPath))
                {
                    Cfg_Message_TextBox.ForeColor = Color.DarkRed;
                    string repoMsg   =  "Are you selecting a custom path for your Xml repository? Be sure to move your current";
                           repoMsg  +=  " Xml files to your new location to make them available." + Environment.NewLine;
                           repoMsg  +=  "Restart Osprey to load the new Xml files.";

                    Cfg_Message_TextBox.Text = repoMsg ;
                }

                Cfg_XmlRepo_TextBox.Text = AltXmlRepoPath;
            }


        }

      

        private void Cfg_Cancel_Button_Click(object sender, EventArgs e)
        {
            Cfg_XmlRepo_TextBox.Text            = m_trans_Cfg_XmlRepositoryPath;
            Cfg_XmlRepoBrowse_Button.Enabled    = m_trans_Cfg_XmlRepBrowse_Button_Enabled;
            Cfg_XmlRepo_TextBox.Enabled         = m_trans_Cfg_XmlRep_TextBox_Enabled;
            Cfg_UseDefPath_ChkBox.Checked       = m_restore_val_Cfg_UseDefPath_Chkbox;

            this.Close();
        }

        private void Cfg_MainMenuColor_Btn_Click(object sender, EventArgs e)
        {
            using (Cfg_ColorDialog_FileColor)
            {
                if (Cfg_ColorDialog_FileColor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_trans_Cfg_Color = Cfg_ColorDialog_FileColor.Color;
                }
            }
        }

        private void Cfg_LogEvents_ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            //Form parentFrm = this.MdiParent;

            //MenuStrip menuStrip = (MenuStrip)ParentForm.Controls["menuStrip1"];
            //ToolStripMenuItem mi_LogEvents = (ToolStripMenuItem)menuStrip.Items["Menu_LogEvents"];

            //if (Cfg_LogEvents_ChkBox.Checked == true)
            //{
            //    mi_LogEvents.Visible = true;
            //    Properties.Settings.Default.LogEventsToFile = true;
            //}
            //else
            //{
            //    mi_LogEvents.Visible = false;
            //    Properties.Settings.Default.LogEventsToFile = false;
            //}
        }

        private void FormConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
           // ((Form1)this.MdiParent).Menu_Edit_Config_Enable(true);

            
            //MdiParent.Menu_Edit_Config.Enabled = true;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormConfig_Activated(object sender, EventArgs e)
        {
            //if (m_Top > -1) 
            //{
            //    this.Top = m_Top;
            //}
            //if (m_Left> -1) 
            //{
            //    this.m_Left = m_Left;
            //}
        }
    }
}
