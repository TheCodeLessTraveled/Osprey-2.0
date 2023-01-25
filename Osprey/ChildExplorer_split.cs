 using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace CodeLessTraveled.Osprey
{
    public partial class ChildExplorer : Form
    {
        public int child_window_count;

        private ChildExplorerConfig m_ChildConfig;
        
        private int     m_TS_TexboxUriIntWidth      = 330;
        private int     m_FormIntWidth              = 600;

        private string  m_trans_label               = null;
        private string  m_trans_uri                 = null;
        private int     m_trans_ColorArgbInt        = -1;
        private int     m_trans_WindowOrder         = -1;
        private string  m_Track_Window_Sequence;

        private int     m_DEFAULT_COLOR = System.Drawing.Color.LightGray.ToArgb();
        private bool    m_USE_DEFAULT_COLOR         = false;
        
        private System.Drawing.Color m_Def_StatusLabel_BgColor = System.Drawing.SystemColors.Control;

        public ChildExplorer()
        {
            InitializeComponent();
            m_ChildConfig = new ChildExplorerConfig();
        }

        public ChildExplorer(ChildExplorerConfig config)
        {
            InitializeComponent();

            m_ChildConfig = config;

            SetConfigOptions(m_ChildConfig);
        
            System.Drawing.Color m_Def_StatusLabel_BgColor = System.Drawing.SystemColors.Control;


    }



    public ChildExplorerConfig ChildConfig
        {   get 
            {
                int int_windowSequence = 0;
               
                bool IS_INTEGER = int.TryParse(this.TS_OrderTextbox.Text, out int_windowSequence);
                
                m_ChildConfig.WindowOrder   = int_windowSequence;
           
                m_ChildConfig.ColorArgbInt  = this.TS_ButtonEditColor.BackColor.ToArgb();

                System.Uri webUrl = this.webBrowser1.Url;

                //if (!String.IsNullOrEmpty(this.webBrowser1.Url.LocalPath))
                if (webUrl != null)
                {
                    m_ChildConfig.uri = this.webBrowser1.Url.LocalPath;
                }

                
                return m_ChildConfig;
            }    
        }

        public void RefreshChildWindows()
        {
            this.webBrowser1.Refresh();
        }

        public void ResizeTS_Textbox() 
        {

            this.TS_TextboxUri.Width = this.Width-200;
        }

        private void SetConfigOptions(ChildExplorerConfig configs)
        {
           
            m_ChildConfig.uri               = configs.uri;
                TS_TextboxUri.Text          = m_ChildConfig.uri;
                if (!String.IsNullOrEmpty(this.m_ChildConfig.uri))
                {
                    WebBrowerSetUrl(this.m_ChildConfig.uri);
                }

            m_ChildConfig.WindowOrder       = configs.WindowOrder;
                Opt_SortOrder_Textbox.Text  = m_ChildConfig.WindowOrder.ToString();
                TS_OrderTextbox.Text        = m_ChildConfig.WindowOrder.ToString();

            m_ChildConfig.label             = configs.label;
                Opt_Title_Textbox.Text      = m_ChildConfig.label;
                this.Text                   = m_ChildConfig.label;


           
            m_ChildConfig.ColorArgbInt = configs.ColorArgbInt;

                if (m_ChildConfig.b_USE_DEFAULT_COLOR == true)
                {
                    m_USE_DEFAULT_COLOR             = true;
                    
                    Opt_Color_Button.Enabled        = false;

                    TS_ButtonEditColor.BackColor    = m_ChildConfig.DefaultColor;

                    Opt_ColorDialog.Color           = m_ChildConfig.DefaultColor;

                    Opt_UseDefaultColor_Checkbox.Checked = true;
                }
                else
                {
                    m_USE_DEFAULT_COLOR             = false;

                    Opt_ColorDialog.Color           = System.Drawing.Color.FromArgb(m_ChildConfig.ColorArgbInt);

                    TS_ButtonEditColor.BackColor    = Opt_ColorDialog.Color;

                    Opt_Color_Button.Enabled        = true;
                    
                    Opt_UseDefaultColor_Checkbox.Checked = false;
                }
        }



        private void ChildExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            int Int_WindowOrder = -1;

            bool Is_Integer = int.TryParse(this.TS_OrderTextbox.Text, out Int_WindowOrder);

            this.m_ChildConfig.WindowOrder = Int_WindowOrder;
        }


        private void ChildExplorer_Load(object sender, EventArgs e)
        {
            TS_ButtonBack.Text      = "\u2190";
            
            TS_ButtonForward.Text   = "\u2192";
            
            TS_ButtonUp.Text        = "\u2191";
           
            splitContainer1.Panel1Collapsed = true;
           
            this.child_window_count = this.child_window_count + 1;

           //MessageBox.Show("window_count= " + child_window_count.ToString() );
        }


        private void ChildExplorer_ResizeEnd(object sender, EventArgs e)
        {
            if (sender == this)
            {
                //string InstruMsg;
                //InstruMsg = String.Format("form width: {0} x form height: {1}|", this.Width, this.Height) + Environment.NewLine;
                //InstruMsg += String.Format("spcon width: {0} x spcon height: {1}", splitContainer1.Width, splitContainer1.Height) + Environment.NewLine;
                //InstruMsg += String.Format("panel 1 width: {0} x panel 1 height: {1}", splitContainer1.Panel1.Width, splitContainer1.Panel1.Width) + Environment.NewLine;
                //InstruMsg += String.Format("panel 2 width: {0} x panel 2 height: {1}", splitContainer1.Panel2.Width, splitContainer1.Panel2.Width) + Environment.NewLine;

                //MessageBox.Show(InstruMsg);

//                TS_TextboxUri.Width = this.Width - 200;  // 200 is an approximation of of the width of the toolbar icons. 
            }

            TS_TextboxUri.Width = this.Width - 200;  // 200 is an approximation of of the width of the toolbar icons. 

        }




        private void Opt_Cancel_button_Click(object sender, EventArgs e)
        {
            Opt_SortOrder_Textbox.Text = m_ChildConfig.WindowOrder.ToString();

            Opt_Title_Textbox.Text = m_ChildConfig.label;

            Opt_ColorDialog.Color = System.Drawing.Color.FromArgb(m_ChildConfig.ColorArgbInt);

            splitContainer1.Panel1Collapsed = true;

        }


        private void Opt_Color_Button_Click(object sender, EventArgs e)
        {
            using (Opt_ColorDialog)
            {
                if (Opt_ColorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_trans_ColorArgbInt = Opt_ColorDialog.Color.ToArgb();

                }

            }
        }


        private void Opt_OK_button_Click(object sender, EventArgs e)
        {
            string error_msg = "";


            // Before the use clicked <OK>, the selections are transient.
            // The use could <Cancel> so you cannot save options until <OK> is clicked.
            // Now, take the transient selected values and save them to the config object that will be passed back to the MDI parent to save in the XML.

            m_trans_label = Opt_Title_Textbox.Text;

            m_trans_WindowOrder = -1;

            bool Test_Is_Number = int.TryParse(Opt_SortOrder_Textbox.Text, out m_trans_WindowOrder);

            if (Test_Is_Number)
            {
                m_ChildConfig.WindowOrder = m_trans_WindowOrder;
            }
            else
            {
                error_msg += String.Format("'{0}' is not a number. Must be a number and maximum of 2 digits.{1}", Opt_SortOrder_Textbox.Text, Environment.NewLine);
            }


            if (m_USE_DEFAULT_COLOR == true)
            {
                m_trans_ColorArgbInt = m_DEFAULT_COLOR;

                m_USE_DEFAULT_COLOR = true;
            }
            else
            {
                m_trans_ColorArgbInt = Opt_ColorDialog.Color.ToArgb();

                m_USE_DEFAULT_COLOR = false;
            }


            if (String.IsNullOrEmpty(error_msg))
            {
                splitContainer1.Panel1Collapsed = true;

                ////    SET THE CURRENT VALUES (to be saved) TO THE TRANSIENT VALUES SELECTED BY THE USER (but not saved).


                /// SAVE COLOR. SET THE UI CONTROLS RELATED TO COLOR
                m_ChildConfig.ColorArgbInt = m_trans_ColorArgbInt;  // this is m_DEFAULT_COLOR when using the default value.

                TS_ButtonEditColor.BackColor = System.Drawing.Color.FromArgb(m_ChildConfig.ColorArgbInt);

                Opt_ColorDialog.Color = System.Drawing.Color.FromArgb(m_ChildConfig.ColorArgbInt);


                /// SET THE UI CONTROLS RELATED TO WINDOW ORDER.  m_ChildConfig.WindowOrder  = This is set above in the "if" block.
                TS_OrderTextbox.Text = m_ChildConfig.WindowOrder.ToString();

                Opt_SortOrder_Textbox.Text = m_ChildConfig.WindowOrder.ToString();

                // Form label
                m_ChildConfig.label = m_trans_label;

                this.Text = m_ChildConfig.label;

                Opt_Title_Textbox.Text = m_ChildConfig.label;

                // Other
                Opt_Error_Textbox.Visible = false;

                Opt_Error_Textbox.Text = "";



            }
            else
            {
                Opt_Error_Textbox.Text = error_msg;

                Opt_Error_Textbox.Visible = true;

                Opt_SortOrder_Textbox.BackColor = System.Drawing.Color.Yellow;
            }
        }


        private void Opt_UseDefaultColor_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (Opt_UseDefaultColor_Checkbox.Checked == true)
            {
                m_USE_DEFAULT_COLOR = true;

                Opt_Color_Button.Enabled = false;
            }
            else // false
            {
                m_USE_DEFAULT_COLOR = false;

                Opt_Color_Button.Enabled = true;
            }


        }


        private void Opt_Title_Textbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Opt_SortOrder_Textbox.Focus();
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



        private void TS_ButtonOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (!String.IsNullOrEmpty(TS_TextboxUri.Text))
                {
                    // test to see if the entry is a folder.

                    if (System.IO.Directory.Exists(TS_TextboxUri.Text))
                    {
                        fbd.SelectedPath = TS_TextboxUri.Text;// this.m_TxtboxFolderPath;
                    }
                }

                fbd.Description = "Browse for Folder";

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.m_ChildConfig.uri = fbd.SelectedPath;

                    webBrowser1.Url = new Uri(this.m_ChildConfig.uri);

                    TS_TextboxUri.Text = this.m_ChildConfig.uri;
                }
            }
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

    

        public void WebBrowerSetUrl(string BrowserUrl)
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


        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            TS_TextboxUri.Text = webBrowser1.Url.LocalPath;

            this.m_ChildConfig.uri = TS_TextboxUri.Text;

            if (String.IsNullOrEmpty(this.m_ChildConfig.label))
            {
                this.m_ChildConfig.label = TS_TextboxUri.Text;
            }
        }

        private void TS_TextboxUri_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!System.IO.Directory.Exists(this.TS_TextboxUri.Text))
                {
                    StatusMessage.Text      = "Folder path is not valid.";
                    StatusMessage.ForeColor = System.Drawing.Color.DarkRed;
                    StatusMessage.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    this.m_ChildConfig.uri = this.TS_TextboxUri.Text;
    
                    webBrowser1.Url = new Uri(TS_TextboxUri.Text);

                    StatusMessage.Text      = "";
                    StatusMessage.ForeColor = System.Drawing.Color.Black;
                    StatusMessage.BackColor = m_Def_StatusLabel_BgColor;
                }
            }
            
        }

        private void ChildExplorer_Layout(object sender, LayoutEventArgs e)
        {
            string x = "";

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ChildExplorer_Resize(object sender, EventArgs e)
        {
            string x = "";
        }

        private void ChildExplorer_SizeChanged(object sender, EventArgs e)
        {
            //string x = "";

            //TS_TextboxUri.Width = this.Width - 200;
        }
    }


}

