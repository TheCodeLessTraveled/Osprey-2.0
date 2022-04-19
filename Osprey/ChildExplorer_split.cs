using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace CodeLessTraveled.Osprey
{
    public partial class ChildExplorer : Form
    {
        private ChildExplorerConfig m_ChildConfig;

        private int m_TS_TexboxUriIntWidth = 330;
        private int m_FormIntWidth= 600;


        private string  m_trans_label           = null;
        private string  m_trans_uri             = null;
        private int     m_trans_ColorArgbInt    = -1;
        private int     m_trans_WindowOrder     = -1;

        private string m_Track_Window_Sequence;


    
        public ChildExplorer()
        {
            InitializeComponent();
            m_ChildConfig = new ChildExplorerConfig();
            

        }

        public ChildExplorer(ChildExplorerConfig config)
        {

            InitializeComponent();
            //this.Show();

            m_ChildConfig = config;

            SetConfigOptions(m_ChildConfig);
            
            
        }



        public ChildExplorerConfig ChildConfig
        {   get 
            {
                int int_windowSequence = 0;
               
                bool IS_INTEGER = int.TryParse(this.TS_OrderTextbox.Text, out int_windowSequence);
                
                m_ChildConfig.WindowOrder   = int_windowSequence;
                m_ChildConfig.ColorArgbInt  = this.TS_ButtonEditColor.BackColor.ToArgb();
                m_ChildConfig.uri           = this.webBrowser1.Url.LocalPath;
                return m_ChildConfig;

            }    
        }


        private void SetConfigOptions(ChildExplorerConfig configs)
        {
            m_ChildConfig.label         = configs.label;
            m_ChildConfig.uri           = configs.uri;
            m_ChildConfig.ColorArgbInt  = configs.ColorArgbInt;
            m_ChildConfig.WindowOrder   = configs.WindowOrder;

            Opt_Title_Textbox.Text = m_ChildConfig.label;
            this.Text = m_ChildConfig.label;

            this.SetBrowserUrl(this.m_ChildConfig.uri);
            TS_TextboxUri.Text = m_ChildConfig.uri;

            if (m_ChildConfig.ColorArgbInt != 0)
            {
                Opt_ColorDialog.Color        = System.Drawing.Color.FromArgb(m_ChildConfig.ColorArgbInt); 
                TS_ButtonEditColor.BackColor = Opt_ColorDialog.Color;
            }
            
            Opt_SortOrder_Textbox.Text  = m_ChildConfig.WindowOrder.ToString();
            TS_OrderTextbox.Text        = m_ChildConfig.WindowOrder.ToString();
        }




        private void ChildExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            int Int_WindowOrder = -1;

            bool Is_Integer = int.TryParse(this.TS_OrderTextbox.Text, out Int_WindowOrder);

            this.m_ChildConfig.WindowOrder = Int_WindowOrder;
        }

        private void ChildExplorer_Load(object sender, EventArgs e)
        {
            TS_ButtonBack.Text = "\u2190";
            TS_ButtonForward.Text = "\u2192";
            TS_ButtonUp.Text = "\u2191";
           
            splitContainer1.Panel1Collapsed = true;
         
        }
        
        private void ChildExplorer_ResizeEnd(object sender, EventArgs e)
        {
            // the TextboxUri width is 200 to 400

            // the init form is 600. The init textboxuri width is 330
            //
            // if frm width > 670 then textbox width = 400.
            //      330 : 600
            //      400-330 = 70 , 600 + 70 = 670
            //      textbox width = (frmWidth )
            //
            // If Frm width < 470 then textboxUri width = 200.
            //      330 : 600
            //      330-200 = 130, 600-130 = 470
            //


            //// frmWidth - 600

            //int MaxTbxWidth = 500;
            //int MinTbxWidth = 200;

            //if (this.Width > 800) 
            //{
            //    TS_TextboxUri.Width = MaxTbxWidth;  // 400
            //}
            //else if (this.Width < 400)
            //{
            //    TS_TextboxUri.Width = MinTbxWidth;
            //}
            //else 
            //{
            //    TS_TextboxUri.Width = this.Width - 300;
            //}

            //StatusMessage.Text = String.Format("textbox Width: {0} | form Width: {1}", TS_TextboxUri.Width, this.Width);
        }


        //private void ChildExplorer_Resize(object sender, EventArgs e)
        //{
        //    int status_width = this.Width;
        //    int status_height = this.Height;

        //    StatusMessage.Text = String.Format("w {0} x h {1}", status_width, status_height);

        //    //if (this.Width > 600)
        //    //{

        //    //    TS_TextboxUri.Width = 300 + (this.Width-600);
        //    //}
        //    //if (this.Width < 600)
        //    //{
        //    //    int shrinkage = 600 - this.Width;  //300 typical textbox lenth. but as the form is resized < 600, the textbox must reduce by the difference (600 - the resized width)

        //    //    TS_TextboxUri.Width = 300 - shrinkage;

        //    //}


        //    //if (this.Width > 300 && this.Width < 600) 
        //    //{
        //    //    TS_TextboxUri.Width = this.Width - 200;
        //    //}
        //}


        //public string ChildLabel
        //{
        //    get { return this.m_ChildLabel; }
        //    set
        //    {
        //        this.m_ChildLabel = value;
        //        this.Text = m_ChildLabel;
        //        string x = "";
        //    }
        //}


        private void btnOpen_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (!String.IsNullOrEmpty(TS_TextboxUri.Text))
                {
                    // test to see if the entry is a folder.
                    // this.m_TxtboxFolderPath = TS_TextboxUri.Text;

                    if (System.IO.Directory.Exists(TS_TextboxUri.Text))
                    {
                        fbd.SelectedPath = TS_TextboxUri.Text;// this.m_TxtboxFolderPath;
                    }
                }

                fbd.Description = "Browse for Folder";
                
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    
                    //this.m_TxtboxFolderPath = fbd.SelectedPath;
                    this.m_ChildConfig.uri = fbd.SelectedPath;

                    webBrowser1.Url = new Uri(this.m_ChildConfig.uri);
            
                    TS_TextboxUri.Text = this.m_ChildConfig.uri;

                    //string FormTitle = this.m_ChildConfig.uri;
                    //if (FormTitle.Length > 50)
                    //{
                    //    string startChars = this.m_ChildConfig.uri.Substring(0, 20);
                    //    int pos1 = this.m_ChildConfig.uri.Length - 20;
                    //    string EndChars = this.m_ChildConfig.uri.Substring(pos1);
                    //    FormTitle = startChars + "..." + EndChars;
                    //}
                    //this.Text = FormTitle;
                }
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


        private void TS_ButtonBack_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
            TS_TextboxUri.Text = webBrowser1.Url.LocalPath;
        }


        private void TS_ButtonForward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
            TS_TextboxUri.Text = webBrowser1.Url.LocalPath;
        }


        private void TS_ButtonUp_Click(object sender, EventArgs e)
        {
            string startUri = webBrowser1.Url.LocalPath;

            DirectoryInfo di_Parent = System.IO.Directory.GetParent(startUri);

            if (di_Parent == null)
            {
                StatusMessage.Text = String.Format("Folder path is not valid. \"{0}\"", startUri);
      
                StatusMessage.ForeColor = System.Drawing.Color.DarkRed;
            }
            else 
            { 
                webBrowser1.Url = new Uri(di_Parent.FullName);

                TS_TextboxUri.Text = di_Parent.FullName;
            }

            string x = "";
        }


        private void TS_ButtonEditColor_Click(object sender, EventArgs e)
        {
            //ChildConfig childConfig = new ChildConfig();
            //childConfig.MdiParent = this;
            //childConfig.Show();


            using (Opt_ColorDialog)
            {
                if (Opt_ColorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    System.Drawing.Color newColor = Opt_ColorDialog.Color;

                    m_ChildConfig.ColorArgbInt = newColor.ToArgb();

                    TS_ButtonEditColor.BackColor = newColor;


                    string x = "";
                }

            }

        }



        //private void TS_TextboxUri_KeyUp_1(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (!System.IO.Directory.Exists(this.TS_TextboxUri.Text))
        //        {
        //            StatusMessage.Text = "Folder path is not valid.";
        //            StatusMessage.ForeColor = System.Drawing.Color.DarkRed;
        //        }
        //        else
        //        {
        //            SetBrowserUrl(this.TS_TextboxUri.Text);
        //            this.m_ChildConfig.uri = this.TS_TextboxUri.Text;
        //            this.Text = m_ChildConfig.uri;
        //        }
        //    }
        //}



        private void TS_TextboxUri_KeyUp(object sender, KeyEventArgs e)
        {
         
           if (e.KeyCode == Keys.Enter)
            {
                if (!System.IO.Directory.Exists(this.TS_TextboxUri.Text))
                {
                    StatusMessage.Text = "Folder path is not valid.";
                    StatusMessage.ForeColor = System.Drawing.Color.DarkRed;
                }
                else
                {
                    StatusMessage.Text = "";
                    SetBrowserUrl(TS_TextboxUri.Text);
                    this.m_ChildConfig.uri = TS_TextboxUri.Text;
                }
            }
        }


        private void TS_TextboxUri_LocationChanged(object sender, EventArgs e)
        {
           // StatusMessage.Text = "Good Locations";
        }

        private void TS_OrderTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            //m_Track_Window_Sequence

            
            int WindowOrderInt = -1;

            bool KeyStroke_Is_Numeric = false;

            string TheKeyStroke = e.KeyCode.ToString();

            if (
                TheKeyStroke.StartsWith("D")                                        // is the keystroke a marked with a "D" (for digit)
                && TheKeyStroke.Length == 2                                         // is the keystroke 2 characters long.
                && "0123456789".IndexOf(TheKeyStroke.Substring(1, 1)) >= 0          // is the second/last character a match for a number (1-9)
               )
            {
            }


            {
                KeyStroke_Is_Numeric = int.TryParse(TS_OrderTextbox.Text, out WindowOrderInt);

                if (KeyStroke_Is_Numeric)
                {
                    m_ChildConfig.WindowOrder = WindowOrderInt;
                    this.webBrowser1.Focus();
                }

                else
                {
                    string errMsg = String.Format("The value entered, '{0}', is not numeric. Please enter numbers only with a max of 2 digits. ", TS_OrderTextbox);
                    StatusMessage.Text = errMsg;
                }

                string x = "";

            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //    KeyStroke_Is_Numeric = int.TryParse(TS_OrderTextbox.Text, out WindowOrderInt);

            //    if (KeyStroke_Is_Numeric)
            //    {
            //        m_ChildConfig.WindowOrder = WindowOrderInt;
            //        this.webBrowser1.Focus();
            //    }

            //    else
            //    {
            //        string errMsg = String.Format("The value entered, '{0}', is not numeric. Please enter numbers only with a max of 2 digits. ", TS_OrderTextbox);
            //        StatusMessage.Text = errMsg;
            //    }

            //    string x = "";

            //}
        }


        private void TS_Options_Button_Click(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 150;
            splitContainer1.Panel1Collapsed = false;
        }


        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            TS_TextboxUri.Text = webBrowser1.Url.LocalPath;
            this.m_ChildConfig.uri = TS_TextboxUri.Text;
            if (String.IsNullOrEmpty(this.m_ChildConfig.label))
            {
                this.m_ChildConfig.label = TS_TextboxUri.Text;
            }
        }


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Opt_OK_button_Click(object sender, EventArgs e)
        {
            string error_msg = "";


            // Before the use clicked <OK>, the selections are transient.
            // The use could <Cancel> so you cannot save options until <OK> is clicked.
            // Now, take the transient selected values and save them to the config object that will be passed back to the MDI parent to save in the XML.


            m_trans_WindowOrder = -1;

            bool Test_Is_Number = int.TryParse( Opt_SortOrder_Textbox.Text, out m_trans_WindowOrder);

            if (Test_Is_Number)
            {
                m_ChildConfig.WindowOrder = m_trans_WindowOrder;
            }
            else
            {
                error_msg += String.Format("'{0}' is not a number. Must be a number and maximum of 2 digits.{1}", Opt_SortOrder_Textbox.Text,Environment.NewLine);
            }

            m_trans_label = Opt_Title_Textbox.Text;
            m_trans_ColorArgbInt = Opt_ColorDialog.Color.ToArgb();

            if (String.IsNullOrEmpty(error_msg))
            {
                splitContainer1.Panel1Collapsed = true;

             ////    SET THE CURRENT VALUES (to be saved) TO THE TRANSIENT VALUES SELECTED BY THE USER (but not saved).
              
              //m_ChildConfig.WindowOrder  = This is set above in the "if" block.
                m_ChildConfig.label         = m_trans_label;
                m_ChildConfig.ColorArgbInt  = m_trans_ColorArgbInt;



                //// SET THE UI CONTROLS (for config options) PER USER'S SELECTION
                // Window order
                TS_OrderTextbox.Text            = m_ChildConfig.WindowOrder.ToString();
                Opt_SortOrder_Textbox.Text      = m_ChildConfig.WindowOrder.ToString();
                Opt_SortOrder_Textbox.BackColor = System.Drawing.Color.White;
                
                // Form label
                this.Text                   = m_ChildConfig.label;
                Opt_Title_Textbox.Text      = m_ChildConfig.label;

                // Color
                TS_ButtonEditColor.BackColor = System.Drawing.Color.FromArgb(m_ChildConfig.ColorArgbInt);
                Opt_ColorDialog.Color = System.Drawing.Color.FromArgb(m_ChildConfig.ColorArgbInt);

                // Other
                Opt_Error_Textbox.Visible   = false;
                Opt_Error_Textbox.Text      = "";
            }
            else
            {
                Opt_Error_Textbox.Text = error_msg;
                Opt_Error_Textbox.Visible = true;
                Opt_SortOrder_Textbox.BackColor = System.Drawing.Color.Yellow;
            }
        }

       
        private void Opt_Cancel_button_Click(object sender, EventArgs e)
        {
            // splitContainer1.SplitterDistance = 0;

            
            Opt_SortOrder_Textbox.Text  = m_ChildConfig.WindowOrder.ToString();
            Opt_Title_Textbox.Text      = m_ChildConfig.label;
            Opt_ColorDialog.Color       = System.Drawing.Color.FromArgb(m_ChildConfig.ColorArgbInt);

            splitContainer1.Panel1Collapsed = true;

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TS_Button_Options_Click(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 150;
            splitContainer1.Panel1Collapsed = false;
        }

        private void TS_ButtonEditColor_Click_1(object sender, EventArgs e)
        {
            using (Opt_ColorDialog)
            {
                if (Opt_ColorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    System.Drawing.Color newColor = Opt_ColorDialog.Color;

                    m_ChildConfig.ColorArgbInt = newColor.ToArgb();

                    TS_ButtonEditColor.BackColor = newColor;


                    string x = "";
                }

            }
        }

        private void TS_TextboxUri_Click(object sender, EventArgs e)
        {

        }

        private void textBox_Title_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_ChildConfig.label = Opt_Title_Textbox.Text;
                this.Text = m_ChildConfig.label;
            }
        }

        private void Opt_Color_Button_Click(object sender, EventArgs e)
        {
            //Opt_ColorDialog.Color = System.Drawing.Color.FromArgb(m_ChildConfig.ColorArgbInt);

            using (Opt_ColorDialog)
            {
                if (Opt_ColorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_trans_ColorArgbInt = Opt_ColorDialog.Color.ToArgb();

                }

            }
        }


        private void Opt_ColorDefault_Click(object sender, EventArgs e)
        {
            m_trans_ColorArgbInt = System.Drawing.Color.LightGray.ToArgb();
            

        }




        private void Opt_Title_Textbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Opt_SortOrder_Textbox.Focus();
            }
        }

    }

    
}
