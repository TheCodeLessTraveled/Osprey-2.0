using System;
using System.Windows.Forms;
using System.IO;

namespace CodeLessTraveled.Osprey
{
    public partial class ChildExplorer : Form
    {
        private string m_TxtboxFolderPath;
        private string m_ChildLabel;
    
        public ChildExplorer()
        {
            InitializeComponent();
        }

        private void ChildExplorer_Load(object sender, EventArgs e)
        {
            TS_ButtonBack.Text = "\u2190";
            TS_ButtonUp.Text = "\u00AB";
        }



        private void ChildExplorer_Resize(object sender, EventArgs e)
        {
            int status_width = this.Width;
            int status_height = this.Height;

            Status_SizeLabel.Text = String.Format("w {0} x h {1}", status_width, status_height);

            if (this.Width > 300 && this.Width < 600) 
            {
                TS_TextboxUri.Width = this.Width - 200;
            }
        }



        public string ChildLabel
        {
            get { return this.m_ChildLabel; }
            set
            {
                this.m_ChildLabel = value;
                this.Text = m_ChildLabel;
                string x = "";
            }
        }


        
        private void btnOpen_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (!String.IsNullOrEmpty(TS_TextboxUri.Text))
                {
                    //test to see if the entry is a folder.
                    this.m_TxtboxFolderPath = TS_TextboxUri.Text;

                    if (System.IO.Directory.Exists(this.m_TxtboxFolderPath))
                    {
                        fbd.SelectedPath = TS_TextboxUri.Text;// this.m_TxtboxFolderPath;
                    }
                }

                fbd.Description = "Browse for Folder";
                
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.m_TxtboxFolderPath = fbd.SelectedPath;
                    webBrowser1.Url = new Uri(this.m_TxtboxFolderPath);
                    TS_TextboxUri.Text = this.m_TxtboxFolderPath;

                    string FormTitle = this.m_TxtboxFolderPath;
                    if (FormTitle.Length > 50)
                    {
                        string startChars = this.m_TxtboxFolderPath.Substring(0, 20);
                        int pos1 = this.m_TxtboxFolderPath.Length - 20;
                        string EndChars = this.m_TxtboxFolderPath.Substring(pos1);
                        FormTitle = startChars + "..." + EndChars;
                    }
                    this.Text = FormTitle;
                }
            }
        }
        


        public string FolderPathText
        {
            get { return this.m_TxtboxFolderPath; }
            set
            {
                this.m_TxtboxFolderPath = value;
                this.TS_TextboxUri.Text = this.m_TxtboxFolderPath;

            }
        }



        public void SetBrowserUrl(string BrowserUrl)
        {
            try
            {
                this.webBrowser1.Url = new Uri(BrowserUrl);
                this.TS_TextboxUri.Text = BrowserUrl;
            }
            catch (System.UriFormatException eUri)
            {
                this.Text = "Invalid uri! ";
            }

        }



        private void TS_ButtonUp_Click(object sender, EventArgs e)
        {
            string startUri = webBrowser1.Url.AbsolutePath;

            DirectoryInfo di_Parent = System.IO.Directory.GetParent(startUri);

            webBrowser1.Url = new Uri(di_Parent.FullName);

            TS_TextboxUri.Text = di_Parent.FullName;

            string x = "";
        }



        private void TS_ButtonBack_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
            TS_TextboxUri.Text = webBrowser1.Url.AbsolutePath;
        }

     

        private void TS_TextboxUri_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (lblError.Visible == true)
            {
                lblError.Visible = false;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (!System.IO.Directory.Exists(this.TS_TextboxUri.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Folder path is not valid.";
                }
                else
                {
                    SetBrowserUrl(this.TS_TextboxUri.Text);
                    this.m_TxtboxFolderPath = this.TS_TextboxUri.Text;
                }
            }
        }


        
        private void TS_TextboxUri_KeyUp(object sender, KeyEventArgs e)
        {
            if (lblError.Visible == true)
            {
                lblError.Visible = false;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (!System.IO.Directory.Exists(this.TS_TextboxUri.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Folder path is not valid.";
                    //MessageBox.Show("Error");
                }
                else
                {
                    SetBrowserUrl(TS_TextboxUri.Text);
                    this.m_TxtboxFolderPath = TS_TextboxUri.Text;
                }
            }
        }



        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            TS_TextboxUri.Text = webBrowser1.Url.LocalPath;
            this.FolderPathText = TS_TextboxUri.Text;
            if (String.IsNullOrEmpty(this.ChildLabel))
            {
                this.ChildLabel = TS_TextboxUri.Text;
            }
        }


    }
}
