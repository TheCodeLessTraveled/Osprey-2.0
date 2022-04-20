using System;
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
        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {

            this.Text = String.Format("parent x={0} y={1}", MdiParent.Location.X, MdiParent.Location.Y);



        }

        private void Cfg_XmlRepBrowseButton_Click(object sender, EventArgs e)
        {
            
            using (FolderBrowserDialog_XmlFiles)
            {
                
                FolderBrowserDialog_XmlFiles.Description = "Browse for and set the XML configuration repository";

                if (FolderBrowserDialog_XmlFiles.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(FolderBrowserDialog_XmlFiles.SelectedPath))
                    {
                        cfg_XmlRep_TextBox.Text = FolderBrowserDialog_XmlFiles.SelectedPath;

                        Cfg_Message_TextBox.ForeColor = Color.DarkGreen;

                        Cfg_Message_TextBox.Text = String.Format("Press <OK> to save for default XML repository location. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);

                        cfg_OK_Button.Focus();
                    }
                    else
                    {
                        Cfg_Message_TextBox.ForeColor = Color.DarkRed;

                        Cfg_Message_TextBox.Text = String.Format("This folder does not exist. Select a valid folder. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);
                    }
                    
                    if (System.IO.Directory.Exists(FolderBrowserDialog_XmlFiles.SelectedPath))
                    {
                        Properties.Settings.Default.AltOspreyDataFolder = FolderBrowserDialog_XmlFiles.SelectedPath;
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
                FolderBrowserDialog_XmlFiles.SelectedPath = cfg_XmlRep_TextBox.Text;
                
                
                if (System.IO.Directory.Exists(FolderBrowserDialog_XmlFiles.SelectedPath))
                {

                    Cfg_XmlRepBrowseButton_Click(new object(), new EventArgs());

                    //Properties.Settings.Default.AltOspreyDataFolder = FolderBrowserDialog_XmlFiles.SelectedPath;

                    //Cfg_Message_TextBox.ForeColor = Color.DarkGreen;

                    //Cfg_Message_TextBox.Text = String.Format("New folder saved for default XML repository location. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);

                    //cfg_OK_Button.Focus();

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

        private void cfg_OK_Button_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AltOspreyDataFolder = FolderBrowserDialog_XmlFiles.SelectedPath;

            Cfg_Message_TextBox.Text = String.Format("Folder path saved as default location. {0}", FolderBrowserDialog_XmlFiles.SelectedPath);

            


        }
    }
}
