using System;
using System.Windows.Forms;

namespace CodeLessTraveled.Osprey
{
    public partial class ChildExplorer : Form
    {
        private string m_TxtboxFolderPath;
        private string m_ChildLabel;
        private int m_panel1Height ;

        public string FolderPathText
        {
            get { return this.m_TxtboxFolderPath; }
            set { 
                    this.m_TxtboxFolderPath = value;
                    this.txbFolderPath1.Text = this.m_TxtboxFolderPath;
                   
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



        public ChildExplorer()
        {
            InitializeComponent();
            m_panel1Height = this.splitContainer1.Panel1.Height; 
        }

   

        private void btnOpen_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (!String.IsNullOrEmpty(txbFolderPath1.Text))
                {
                    //test to see if the entry is a folder.
                    this.m_TxtboxFolderPath = txbFolderPath1.Text;

                    if (System.IO.Directory.Exists(this.m_TxtboxFolderPath))
                    {
                        fbd.SelectedPath = this.m_TxtboxFolderPath;
                    }
                }

                fbd.Description = "Browse for Folder";
                
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.m_TxtboxFolderPath = fbd.SelectedPath;
                    webBrowser1.Url = new Uri(this.m_TxtboxFolderPath);
                    txbFolderPath1.Text = this.m_TxtboxFolderPath;

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



        public void SetBrowserUrl(string BrowserUrl)
        {
            try
            {
                this.webBrowser1.Url = new Uri(BrowserUrl);
                this.txbFolderPath1.Text = BrowserUrl;
            }
            catch (System.UriFormatException eUri)
            {
                this.Text = "Invalid uri! ";
            }

        }

        private void txbFolderPath1_KeyUp(object sender, KeyEventArgs e)
        {
            if (lblError.Visible == true)
            {
                lblError.Visible = false;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (!System.IO.Directory.Exists(this.txbFolderPath1.Text))
                {
                    lblError.Visible = true;
                    lblError.Text = "Folder path is not valid.";
                    //MessageBox.Show("Error");
                }
                else
                {
                    SetBrowserUrl(txbFolderPath1.Text);
                    this.m_TxtboxFolderPath = txbFolderPath1.Text;
                }
            }
        }

        private void txbFolderPath1_TextChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void ChildExplorer_Load(object sender, EventArgs e)
        {

        }

        private void ChildExplorer_Resize(object sender, EventArgs e)
        {
            this.splitContainer1.Width = this.Width - 20;
            this.splitContainer1.Height = this.Height - m_panel1Height + 15;

            


        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            txbFolderPath1.Text = webBrowser1.Url.LocalPath;
            this.FolderPathText = txbFolderPath1.Text;
            if (String.IsNullOrEmpty(this.ChildLabel))
            {
                this.ChildLabel = txbFolderPath1.Text;
            }
        }

    
     
       

       
    }
}
