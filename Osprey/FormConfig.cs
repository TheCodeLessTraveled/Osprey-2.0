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
        private string  m_trans_Cfg_XmlRepositoryPath;
        private bool    m_trans_Cfg_XmlRepBrowse_Button_Enabled;
        private bool    m_trans_Cfg_XmlRep_TextBox_Enabled;
        private string  m_trans_Cfg_Message_Textbox_Value;
        private bool    m_trans_Cfg_UseDefault_Chkbox_Value;

        //   private bool m_trans_Cfg_OK_Enabled;


        //private bool m_Is_Use_Cfg_Default;
        //private string m_XmlRepositoryPath;
        //public string XmlRepositoryPath
        //{
        //    get{return m_XmlRepositoryPath; }
        //    set{ m_XmlRepositoryPath = value; }
        //}
        //protected string XmlRepositoryPath_protected
        //{
        //    get { return m_XmlRepositoryPath; }
        //    set { m_XmlRepositoryPath = value; }
        //}

        public FormConfig(bool UseDefaultDataFilePath)
        {
          // m_Is_Use_Cfg_Default = UseDefaultDataFilePath;

            //////////// temp test //////////////////////
            //m_Is_Use_Cfg_Default = true;
            /////////////////////////////////////////////
            InitializeComponent();
        }

        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {

            this.Text = String.Format("parent x={0} y={1}", MdiParent.Location.X, MdiParent.Location.Y);


            // Initial settings for the Config UI controls based on the saved value of the alternative xml repo path.

            string XmlSavedRepoPath = Properties.Settings.Default.AltXmlRepository.Trim();

            if (String.IsNullOrEmpty(XmlSavedRepoPath))
            {
                // Use the default setting which is set on for form_load of the parent form, "Form1.cs".
                Cfg_UseDefault_ChkBox.Checked = true;
                Cfg_XmlRepo_TextBox.Text = "";
                Cfg_XmlRepo_TextBox.Enabled = false;
                Cfg_XmlRepoBrowse_Button.Enabled = false;
            }
            else
            {
                Cfg_UseDefault_ChkBox.Checked = false;
                Cfg_XmlRepo_TextBox.Text = XmlSavedRepoPath;
                Cfg_XmlRepo_TextBox.Enabled = true;
                Cfg_XmlRepoBrowse_Button.Enabled = true;
            }

            Cfg_UseDefault_ChkBox.Focus();

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

                        Cfg_Message_TextBox.Text = String.Format("Press <OK> to save for default XML repository location. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);

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

        private void cfg_XmlRep_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void cfg_Save_Button_Click(object sender, EventArgs e)
        {
            if (Cfg_UseDefault_ChkBox.Checked == false)
            {
                if (!System.IO.Directory.Exists(Cfg_XmlRepo_TextBox.Text))
                {
                    Cfg_Message_TextBox.ForeColor = Color.DarkRed;

                    Cfg_Message_TextBox.Text = String.Format("The saved path, \"{0}\", does not exist. Correct the path before saving.", Cfg_XmlRepo_TextBox.Text);
                }
                else
                {
                    Properties.Settings.Default.AltXmlRepository = FolderBrowserDialog_XmlFiles.SelectedPath;

                    Cfg_Message_TextBox.Text = String.Format("Folder path saved as default location. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);
                    Cfg_Message_TextBox.Text += Environment.NewLine + "Close and restart the Osprey application.";
                }
            }
            else
            {
                Cfg_Message_TextBox.Text = "Default path.";

                Properties.Settings.Default.AltXmlRepository ="";
            }
        }

        private void Cfg_UseDefault_ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            string x = "";

            m_trans_Cfg_XmlRepositoryPath           = Cfg_XmlRepo_TextBox.Text;
            m_trans_Cfg_XmlRepBrowse_Button_Enabled = Cfg_XmlRepoBrowse_Button.Enabled;
            m_trans_Cfg_XmlRep_TextBox_Enabled      = Cfg_XmlRepo_TextBox.Enabled;
            m_trans_Cfg_Message_Textbox_Value       = Cfg_Message_TextBox.Text;
            m_trans_Cfg_UseDefault_Chkbox_Value     = !Cfg_UseDefault_ChkBox.Checked;   // at this point the checkbox was selected and the previous state was opposite of what is evaluated here.
                                                                                        // if it was checked before the user unchecked it, then we want to capture the "checked" state.
            Cfg_Message_TextBox.Text = "";

            if (Cfg_UseDefault_ChkBox.Checked == true)
            {
                // Use the default setting which is set on for form_load of the parent form, "Form1.cs".
                Cfg_XmlRepo_TextBox.Text        = "";

                Cfg_XmlRepo_TextBox.Enabled     = false;

                Cfg_XmlRepoBrowse_Button.Enabled = false;
            }
            else
            {
                Cfg_UseDefault_ChkBox.Checked   = false;

                Cfg_XmlRepo_TextBox.Enabled     = true;

                Cfg_XmlRepoBrowse_Button.Enabled = true;

                string AltXmlRepoPath = Properties.Settings.Default.AltXmlRepository.Trim();

                if (!System.IO.Directory.Exists(AltXmlRepoPath))
                {
                    Cfg_Message_TextBox.ForeColor = Color.DarkRed;
                
                    Cfg_Message_TextBox.Text = String.Format("The saved path, \"{0}\", does not exist. Correct the path before saving.", AltXmlRepoPath);
                }

                Cfg_XmlRepo_TextBox.Text = AltXmlRepoPath;
            }


        }

      

        private void Cfg_Cancel_Button_Click(object sender, EventArgs e)
        {
            Cfg_XmlRepo_TextBox.Text            = m_trans_Cfg_XmlRepositoryPath;
            Cfg_XmlRepoBrowse_Button.Enabled    = m_trans_Cfg_XmlRepBrowse_Button_Enabled;
            Cfg_XmlRepo_TextBox.Enabled         = m_trans_Cfg_XmlRep_TextBox_Enabled;
            Cfg_UseDefault_ChkBox.Checked       = m_trans_Cfg_UseDefault_Chkbox_Value;

            this.Close();
        }
    }
}