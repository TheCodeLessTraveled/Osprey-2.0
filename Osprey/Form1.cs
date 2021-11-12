using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.XPath;
using System.Xml;
using System.Linq;
using System.IO;



namespace CodeLessTraveled.Osprey
{

    public partial class Form1 : Form
    {
        string HelpPath = System.IO.Path.Combine(Application.StartupPath,"Osprey Help.chm");
        private List<ChildExplorer> ArrayChildExplorer = new List<ChildExplorer>();
        // Variables used for data files and their XML contents.
        private XmlDocument m_OspreyDataXml;
        private List<string> m_ArrayDataXmlFiles = new List<string>();
        private string m_OspreyDataXmlFolderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "The Code Less Traveled", "Osprey");
        private string m_XmlFilename = "OspreyData.xml";
        private string m_OspreyDataXmlFullPath;
        private System.Drawing.Point m_SavedLocation ;
                

        // Variables used for displaying messages in the lblStausMessage control.
        private System.Drawing.Color    Status_DefaultBackColor ;
        private System.Drawing.Color    Status_DefaultForeColor;
        private System.Drawing.Font     Status_DefaultFont;
        
        private string                  Status_Message;
        private System.Drawing.Color    Status_BackColor;
        private System.Drawing.Color    Status_ForeColor;
        
        private string                  Status_Message2;
        private System.Drawing.Color    Status_BackColor2;
        private System.Drawing.Color    Status_ForeColor2;

        private int                     Status_TimerCount= 0;
        private bool                    b_ShowMessages   = true; // add this to settings upon exit.
        
        private bool m_bOnLoad         = false;
       
        private string[]  m_arrSelectedFolderTeamName    = new string[2] { "", ""};      // This array holds to two elements [0]= folder team name, [1]=folder team display name.
        private const int idx_FTeamNodeName         = 0;            // <- as seen here.     You want the user to create a display name in whatever convenient upper/lower case combination he/she wants. But, XML is case-sensative.
        private const int idx_FTeamDisplayName      = 1;            // <- and here          So, to prevent duplication of nodes, Force lowercase node name while allowing a display name of their choice. For nodes with the same name spelling and different cases,
                                                                    //                      these are conceptually same, but, technically they are different XML nodes. Force lowercase XML node names 
                                                                    //                      to prevent duplicate folder teams.
        
       
        enum NodeChangeType{Rename, Save, SaveAs, Default, NewGroup}
        enum OpenType {FromXML, New }
        enum SaveResults {Cancelled, Error, Initialize, Saved, Skip}

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Form1Location   = this.Location;
            Properties.Settings.Default.Form1Size       = this.Size;

            if (m_arrSelectedFolderTeamName == null)
            {
                Properties.Settings.Default.LastFolderTeamName = "";
                Properties.Settings.Default.LastFolderTeamDisplayName = "";
            }
            else
            {
                Properties.Settings.Default.LastFolderTeamName = m_arrSelectedFolderTeamName[idx_FTeamNodeName];
                Properties.Settings.Default.LastFolderTeamDisplayName = m_arrSelectedFolderTeamName[idx_FTeamDisplayName];
            }

            if (!m_XmlFilename.EndsWith(".xml"))
            {
                m_XmlFilename += ".xml";
            }

            Properties.Settings.Default.LastXmlFileName = m_XmlFilename;

            Properties.Settings.Default.Save();

        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            /*  1.  Are there any settings saved from the last session?
             *      - Form size and location?
             *      - XML data file?
             *      - FolderTeam? 
             *      
             *  2.  Load the ComboBox with a list of files and set the comboBox text to the saved Xml data file.
             *      This triggers the event handler to load the list of folder teams from the selected file in the ComboBox.Text.
             *     
             * 3.   Set the FolderTeam list text to the saved FolderGroup which will trigger the event to load the child windows of FolderGroups.
             */

            m_bOnLoad = true;

            helpProvider1.HelpNamespace    = this.HelpPath;
            this.Status_DefaultBackColor  = this.lblStausMessage.BackColor;
            this.Status_DefaultForeColor  = this.lblStausMessage.ForeColor;
            this.Status_DefaultFont       = this.lblStausMessage.Font;
            m_arrSelectedFolderTeamName = new string[2] { "", "" };
            string x = "";

            // Set Form size and location per coordinates saved from the last sessionto and recorded in the settings.
                if (Properties.Settings.Default.Form1Size.Width == 0 || Properties.Settings.Default.Form1Size.Height == 0)
                {
                    System.Drawing.Size initSize = new System.Drawing.Size(500, 500);
                    this.Size = initSize;
                }
                else
                {
                    int SavedWidth  = Properties.Settings.Default.Form1Size.Width;
                    int SavedHeight = Properties.Settings.Default.Form1Size.Height ;
                    System.Drawing.Size SavedSize = new System.Drawing.Size(SavedWidth, SavedHeight);
                    this.Size       = SavedSize;
                }
                int SavedLeft           = Properties.Settings.Default.Form1Location.X;
                int SavedTop            = Properties.Settings.Default.Form1Location.Y;
                m_SavedLocation         = new System.Drawing.Point(SavedLeft, SavedTop);
                this.Location           = m_SavedLocation;

                // This array contains the folder team from the last session? 
                string savedDisplayText = Properties.Settings.Default.LastFolderTeamDisplayName;
                string savedTeamName = Properties.Settings.Default.LastFolderTeamName;

                m_arrSelectedFolderTeamName[idx_FTeamDisplayName] = savedDisplayText;
                m_arrSelectedFolderTeamName[idx_FTeamNodeName] = savedTeamName;


                util_PopulateFileListCboBox(m_OspreyDataXmlFolderPath);

                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastXmlFileName.Trim()))
                {
                    m_XmlFilename = Properties.Settings.Default.LastXmlFileName;  // overwrite the default value. This is the filename saved from the last session? 
                }

                if (!m_XmlFilename.EndsWith(".xml"))
                {
                    m_XmlFilename += ".xml";
                }

                int XmlFileNameIndex = Menu_File_LoadFile_CboBox.FindStringExact(m_XmlFilename);

                if (XmlFileNameIndex >= 0)
                {
                    this.m_OspreyDataXmlFullPath = System.IO.Path.Combine(m_OspreyDataXmlFolderPath, m_XmlFilename);

                    Menu_File_LoadFile_CboBox.Text = m_XmlFilename;
                }


              //  this.m_OspreyDataXmlFullPath = System.IO.Path.Combine(m_OspreyDataXmlFolderPath, m_XmlFilename);
              
               






                //if (!String.IsNullOrEmpty(Saved_LastFolderTeamName) && !String.IsNullOrEmpty(Saved_LastFolderTeamDisplayName))
                //{
                //    m_arrSelectedFolderTeamName[idx_FTeamDisplayName] = Saved_LastFolderTeamDisplayName;
                //    m_arrSelectedFolderTeamName[idx_FTeamNodeName] = Saved_LastFolderTeamName;

                //    Menu_File_SelectGroup_CboBox.Text = Saved_LastFolderTeamDisplayName;

                //    Menu_File_SelectGroup_CboBox_SelectedIndexChanged(new object(), new EventArgs());
                //    // m_OspreyDataXmlFullPath is blank at util_SetControlsPerSelectedXml()
                //}

            
                //util_PopulateFileListCboBox(m_OspreyDataXmlFolderPath);                                             // #2.  Populate [Menu_File_LoadFile_CboBox]. This will always have the default value from above. 

                //Menu_File_LoadFile_CboBox.Text = Menu_File_LoadFile_CboBox.Text;

                //util_SetControlsPerSelectedXml();                                                               
                
                                                                                                                //      This uses a Directory.GetFiles type of routine to build an array of data file names to
                //if (!String.IsNullOrEmpty(Saved_FileName))                                                      
                //{

                    //if (!Saved_FileName.EndsWith(".xml"))
                    //{
                    //    Saved_FileName += ".xml";
                    //}
                    // m_XmlFilename = Saved_FileName;

                    //if (System.IO.File.Exists(System.IO.Path.Combine(m_OspreyDataXmlFolderPath, Saved_FileName)))
                    //{
                    //    m_XmlFilename = Saved_FileName;
                    //}

                    //if (Menu_File_LoadFile_CboBox.Items.Contains(m_XmlFilename))                         
                    //{                                                                                           //      Among other UI settings, this will enable the[Menu] -> [File] -> [Open] control if the user's OspreyData.xml has entries.
                    //    Menu_File_LoadFile_CboBox.Text = m_XmlFilename;                                  //      Initially, OspreyData.xml is empty. You don't want the first experience of the user to try to use the [Open] control when 
                    //                                                                                            //      there is nothing to open. Instead, leave them with no option except to create new Folder Teams.
                    //    util_SetControlsPerSelectedXml();                                                               
                    //}           
                //}

              

                m_bOnLoad = false;
          
         

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
        }


        private void ClearAllChildWindows()
        {
            foreach (ChildExplorer childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }




        private void Menu_Edit_DeleteFolderGroup_DoDelete_Click(object sender, EventArgs e)
        {
            XmlDocument xDocOspreyDataXml = GetOspreyDataXml(this.m_OspreyDataXmlFullPath);
            string xPathFolderTeamName = String.Format("//Osprey/FolderTeam[@Name='{0}']", m_arrSelectedFolderTeamName[idx_FTeamNodeName]);
    
            XmlNodeList FolderTeamNodeList = xDocOspreyDataXml.SelectNodes(xPathFolderTeamName);

                int PreExistingNodeCount = FolderTeamNodeList.Count;

            if (PreExistingNodeCount > 0)
            {
                XmlNode OspreyNode = xDocOspreyDataXml.SelectSingleNode("//Osprey");

                foreach (XmlNode FolderTeam in FolderTeamNodeList)
                {
                    OspreyNode.RemoveChild(FolderTeam);
                }

                xDocOspreyDataXml.Save(m_OspreyDataXmlFullPath);

                string DeleteTeamName = this.m_arrSelectedFolderTeamName[idx_FTeamNodeName];
                
                util_SetCurrentLoadedFolderTeamName(null);
                
                ShowStatusMessage(String.Format("Deleted {0}.", DeleteTeamName), System.Drawing.Color.DarkRed, Status_DefaultBackColor);
                
                ClearAllChildWindows();
                
                util_SetControlsPerSelectedXml();
                
                
            }


        }

        private void Menu_Edit_OspreyDataXml_OpenLocation_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();

            psi.FileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe");

            string args = "\"" + m_OspreyDataXmlFullPath + "\"";

            psi.Arguments = String.Format(@"/select," + args);

            psi.WorkingDirectory = m_OspreyDataXmlFullPath;

            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            System.Diagnostics.Process.Start(psi);
        }
               
        private void Menu_Edit_ResetXml_DoReset_Click(object sender, EventArgs e)
        {
        //    util_ResetOspreyDataXml();
        //    util_SetCurrentLoadedFolderTeamName("");// m_arrSelectedFolderTeamName = "";
        //    util_SetControlsPerSelectedXml();
        //    string Msg = "OspreyData.xml initialized.";
        //    ShowStatusMessage(Msg, System.Drawing.Color.Black, System.Drawing.Color.Orange, Msg, System.Drawing.Color.Black, DefaultBackColor);

        }

     

        private void Menu_File_NewDataFile_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }


        private void Menu_File_NewDataFile_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                string NewFileName = this.Menu_File_NewDataFile_TextBox.Text;
                FileInfo fi_NewFile = util_CreateDataXMLFile(NewFileName);

                this.m_OspreyDataXmlFullPath = fi_NewFile.FullName;
                this.m_XmlFilename = fi_NewFile.Name;
              
                 //util_SetControlsPerSelectedXml(m_OspreyDataXmlFullPath);

                this.Menu_File_LoadFile_CboBox.Text = fi_NewFile.Name;

               // Menu_File_OpenDataFile_CboBox_SelectedIndexChanged(new object(), new EventArgs());

                //string Msg01 = String.Format("New file create : {0}", m_XmlFilename);
                //ShowStatusMessage(Msg01, Status_DefaultForeColor, Status_DefaultBackColor);
                
            }
        }


        private void Menu_File_NewGroup_TextBox_Click(object sender, EventArgs e)
        {

        }


        private void Menu_File_NewFileExplorer_Click(object sender, EventArgs e)
        {
            // are there any entries in the OspreyData.xml
            //  node count is 0, then nothing has been saved before. initial save requires a name and requires a name.

            ChildExplorer newFileExplorer = new ChildExplorer();
            newFileExplorer.MdiParent = this;
            newFileExplorer.Show();
            ArrayChildExplorer.Add(newFileExplorer);

            if (!String.IsNullOrEmpty(this.m_arrSelectedFolderTeamName[idx_FTeamNodeName]))
            {
                if (!Menu_File_Save.Enabled) 
                { 
                    Menu_File_Save.Enabled = true; 
                }

                if (!this.ToolStrip_Button_Save.Enabled) 
                {
                    ToolStrip_Button_Save.Enabled = true; 
                }
               
            }

            if (!Menu_File_SaveAs.Enabled) { Menu_File_SaveAs.Enabled = true; }

        }
        



        private void Menu_File_NewGroup_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void Menu_File_NewGroup_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(Menu_File_NewGroup_TextBox.Text))
                {
                    SaveResults SaveValue = SaveMain(NodeChangeType.NewGroup, Menu_File_NewGroup_TextBox.Text.Trim());
                    
                    if (SaveValue == SaveResults.Saved)
                    {
                        util_SetCurrentLoadedFolderTeamName(Menu_File_NewGroup_TextBox.Text.Trim()); //this.m_arrSelectedFolderTeamName = Menu_File_NewGroup_TextBox.Text.Trim();

                        ClearAllChildWindows();

                        Menu_File_NewFileExplorer_Click(new object(), new EventArgs());
                    }

                    util_SetControlsPerSelectedXml();

                    Menu_File.HideDropDown();
                }
            }
        }



        private void Menu_File_OpenDataFile_CboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void Menu_File_OpenDataFile_CboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Everytime the form loads, the file list ComboBox will populate. And, if a file name was saved with the last session.
            // The comboBox text will be set to the save file name - which triggers this event.

            // Selecting a new index implies the FolderTeam list much change and update the ComboBox list.
            // Once the FolderTeam list has completed, check for a saved FolderTeam, update the ComboBox Text; trigger it's event.
            if (File.Exists(System.IO.Path.Combine(m_OspreyDataXmlFolderPath, Menu_File_LoadFile_CboBox.Text)))
            {
                m_XmlFilename = Menu_File_LoadFile_CboBox.Text;

                this.Text = String.Format("Osprey  |  {0}", m_XmlFilename);
 
                m_OspreyDataXmlFullPath = System.IO.Path.Combine(m_OspreyDataXmlFolderPath, m_XmlFilename );

                m_OspreyDataXml= GetOspreyDataXml(m_OspreyDataXmlFullPath);

                util_PopulateFolderTeamList(); 


                bool b_LastTeamSaved = !String.IsNullOrEmpty(m_arrSelectedFolderTeamName[idx_FTeamDisplayName]) && !String.IsNullOrEmpty(m_arrSelectedFolderTeamName[idx_FTeamNodeName]);

                if (b_LastTeamSaved)
                {
                    this.Menu_File_SelectGroup_CboBox.Text = m_arrSelectedFolderTeamName[idx_FTeamDisplayName];
                }
            }
            else
            {
                string Msg01 = String.Format("File not found: {0}. Select one from list.", Menu_File_LoadFile_CboBox.Text);
                ShowStatusMessage(Msg01, System.Drawing.Color.DarkRed, DefaultBackColor);
            }

         }

        


        private void Menu_File_Rename_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

            }
        }

        private void Menu_File_Rename_TextBox__KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string NewName = Menu_File_Rename_TextBox.Text;

                SaveResults ReturnResults = SaveMain(NodeChangeType.Rename, NewName);

                if (ReturnResults == SaveResults.Saved)
                {
                    util_SetCurrentLoadedFolderTeamName(NewName); //this.m_arrSelectedFolderTeamName = NewName;
                }

                util_SetControlsPerSelectedXml();

                Menu_File.HideDropDown();
            }
        }


        
        private void Menu_File_Save_Click_1(object sender, EventArgs e)
        {
            // assume there is something already loaded from the XML configuration file, OspreyData.xml.
            SaveMain(NodeChangeType.Save, m_arrSelectedFolderTeamName[idx_FTeamNodeName]);
            util_SetControlsPerSelectedXml();
        }

        private void Menu_File_SaveAs_Texbox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Menu_File.HideDropDown();
                SaveMain(NodeChangeType.SaveAs, Menu_File_SaveAs_Texbox1.Text.Trim());
                
                util_SetCurrentLoadedFolderTeamName(Menu_File_SaveAs_Texbox1.Text);//m_arrSelectedFolderTeamName = Menu_File_SaveAs_Texbox1.Text;
                
                //util_SetControlsPerSelectedXml();

             if (File.Exists(System.IO.Path.Combine(m_OspreyDataXmlFolderPath, Menu_File_LoadFile_CboBox.Text)))
            {
                m_XmlFilename = Menu_File_LoadFile_CboBox.Text;

                m_OspreyDataXmlFullPath = System.IO.Path.Combine(m_OspreyDataXmlFolderPath, m_XmlFilename );

                m_OspreyDataXml= GetOspreyDataXml(m_OspreyDataXmlFullPath);

                util_PopulateFolderTeamList(); 


                bool b_LastTeamSaved = !String.IsNullOrEmpty(m_arrSelectedFolderTeamName[idx_FTeamDisplayName]) && !String.IsNullOrEmpty(m_arrSelectedFolderTeamName[idx_FTeamNodeName]);

                if (b_LastTeamSaved)
                {
                    this.Menu_File_SelectGroup_CboBox.Text = m_arrSelectedFolderTeamName[idx_FTeamDisplayName];
                }
            }
            else
            {
                string Msg01 = String.Format("File not found: {0}. Select one from list.", Menu_File_LoadFile_CboBox.Text);
                ShowStatusMessage(Msg01, System.Drawing.Color.DarkRed, DefaultBackColor);
            }

             this.Text = String.Format("Osprey  |  {0}  |  {1}", m_XmlFilename, this.Menu_File_SelectGroup_CboBox.Text);
 
                /*
                 * 
                 * 
             this.Text = String.Format("Osprey  |  {0}  |  {1}", m_XmlFilename, Menu_File_SelectGroup_CboBox.Text);

            util_SetCurrentLoadedFolderTeamName(Menu_File_SelectGroup_CboBox.Text); //This method sets the two values in the array, m_arrSelectedFolderTeamName
            
            ClearAllChildWindows();
                        
            Menu_File.HideDropDown();

            util_LoadChildWindows(m_arrSelectedFolderTeamName[idx_FTeamNodeName]);
            
           util_SetControlsPerSelectedXml();
                 
                 */

            }
        }

        private void Menu_File_SaveAs_Texbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

            }
        }


        private void Menu_File_SelectGroup_CboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void Menu_File_SelectGroup_CboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Text = String.Format("Osprey  |  {0}  |  {1}", m_XmlFilename, Menu_File_SelectGroup_CboBox.Text);

            util_SetCurrentLoadedFolderTeamName(Menu_File_SelectGroup_CboBox.Text); //This method sets the two values in the array, m_arrSelectedFolderTeamName
            
            ClearAllChildWindows();
                        
            Menu_File.HideDropDown();

            util_LoadChildWindows(m_arrSelectedFolderTeamName[idx_FTeamNodeName]);
            
           util_SetControlsPerSelectedXml();
 

        }


        

        
        private void Menu_Help_About_Click(object sender, EventArgs e)
        {
            frmHelpAbout frmAbout = new frmHelpAbout();
            frmAbout.MdiParent = this;
            frmAbout.Show();

        }

        private void Menu_Help_LicenseInfo_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frmLicInfo = new frmLicenseInfo();
            frmLicInfo.MdiParent = this;
            frmLicInfo.Show();
        }



        private void Menu_View_Refresh()
        {
            Menu_View_CascadeAll.Checked = false;
            Menu_View_Horizontal.Checked = false;
            Menu_View_Vertical.Checked = false;


        }

        private void Menu_View_Cascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
            Menu_View_Refresh();
            Menu_View_CascadeAll.Checked = true;
            
        }


        
        private void Menu_View_Horizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);

            Menu_View_Refresh();
            Menu_View_Horizontal.Checked = true;
        }


        private void Menu_View_Vertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);

            Menu_View_Refresh();
            Menu_View_Vertical.Checked = true;
        }


        private int CheckForDups(string NodeNameName)
        {

            return 0;
        }
       
  

        private SaveResults SaveMain(NodeChangeType ChangeType, string NewNodeName)
        {
            NewNodeName = NewNodeName.Trim();
            SaveResults ReturnValue = SaveResults.Initialize;
            
            //XmlDocument  m_OspreyDataXml     = GetOspreyDataXml(this.m_OspreyDataXmlFullPath);

            string       xPathNewTeamName      = String.Format("//Osprey/FolderTeam[@Name='{0}']", NewNodeName.ToLower().Trim());
            string       xPathCurrentTeamName  = String.Format("//Osprey/FolderTeam[@Name='{0}']", m_arrSelectedFolderTeamName[idx_FTeamNodeName]);

            XmlNodeList  FolderTeamList        = m_OspreyDataXml.SelectNodes(xPathNewTeamName);
            int          NodeCount             = FolderTeamList.Count;  // depends on the type of save.

            XmlNode FolderTeamNode             = null;
            XmlAttribute attName               = null;
            XmlAttribute attDisplayName        = null;
          
      
            bool b_DupFound          = false;
            bool b_CreateNode        = false;
            bool b_AddChildNodes     = false;
            bool b_DelChildNodes     = false;
            bool b_Rename            = false;
          
            string ErrMsg01          = String.Empty;

            if (NodeCount > 1)                                // Should never have more than one <FolderTeam> node with the same name.
            {                                                 // Delete all the related duplicate nodes and overwrite.
                b_DupFound    = true;
                ErrMsg01      += String.Format("\"{0}\", has {1} duplicate XML nodes.", NewNodeName,NodeCount);
            }
            else if (NodeCount==1)
            {
                switch(ChangeType)
                {
                    case NodeChangeType.NewGroup:              // can't create the new node because one with the same name already exists.
                        b_DupFound       = true;
                        ErrMsg01        += String.Format("\"{0}\", already exists.",NewNodeName);
                        break;

                    case NodeChangeType.SaveAs:
                        b_DupFound       = true;
                        ErrMsg01        += String.Format("\"{0}\", already exists.",NewNodeName);
                        break;

                    case NodeChangeType.Save:                 // Essentially, overwrite the child nodes by first deleting them and adding the new ones currently displayed in the parent window.
                        b_DelChildNodes  = true;
                        b_AddChildNodes  = true;
                        FolderTeamNode   = FolderTeamList[0]; // This is the only scenario where we are expecting an existing node to save changes to.
                        break;

                    case NodeChangeType.Rename:               // can't rename it because a same-named node already exist in the XML doc.
                        b_DupFound       = true;
                        ErrMsg01        += string.Format("\"{0}\", already exists.", NewNodeName);
                        break;
                }
            }
            else if (NodeCount == 0)
            {
              
                switch(ChangeType)
                {
                    case NodeChangeType.NewGroup:
                        b_CreateNode    = true;
                        b_AddChildNodes = true;
                        FolderTeamNode  = m_OspreyDataXml.CreateNode(XmlNodeType.Element, "FolderTeam", "");
           
                        break;
                    
                    case NodeChangeType.SaveAs:
                        b_CreateNode    = true;
                        b_AddChildNodes = true;
                        FolderTeamNode  = m_OspreyDataXml.CreateNode(XmlNodeType.Element, "FolderTeam", "");
                        break;
                    
                    case NodeChangeType.Rename:
                        b_Rename        = true;
                        FolderTeamNode  = m_OspreyDataXml.SelectSingleNode(xPathCurrentTeamName);
                        break;
                }

            }

                if (String.IsNullOrEmpty(ErrMsg01))
                {
                    if (b_CreateNode) // All scenarios include this step. The "if" block is not really necessary.
                    {
                        attName = m_OspreyDataXml.CreateAttribute("Name");
                        attName.Value = NewNodeName.ToLower().Trim();

                        attDisplayName = m_OspreyDataXml.CreateAttribute("DisplayName");
                        attDisplayName.Value = NewNodeName.Trim();

                        FolderTeamNode.Attributes.Append(attName);
                        FolderTeamNode.Attributes.Append(attDisplayName);
                    }

                    if (b_DelChildNodes)
                    {
                        FolderTeamNode.InnerXml = "";
                    }

                    if (b_AddChildNodes)
                    {
                        foreach (ChildExplorer childForm in this.MdiChildren)
                        {
                            if (!String.IsNullOrEmpty(childForm.FolderPathText) && !String.IsNullOrWhiteSpace(childForm.FolderPathText))
                            {
                                XmlNode nChildFileExplorerUri = m_OspreyDataXml.CreateNode(XmlNodeType.Element, "ChildExplorer", "");

                                XmlAttribute atLabel = m_OspreyDataXml.CreateAttribute("label");
                                atLabel.Value = ""; 
                                nChildFileExplorerUri.Attributes.Append(atLabel);

                                XmlAttribute atUri = m_OspreyDataXml.CreateAttribute("uri");
                                atUri.Value = childForm.FolderPathText;
                                nChildFileExplorerUri.Attributes.Append(atUri);

                                FolderTeamNode.AppendChild(nChildFileExplorerUri);
                            }
                        }
                    }

                    if (b_Rename)
                    {
                        FolderTeamNode.Attributes["Name"].Value = NewNodeName.ToLower().Trim();
                        FolderTeamNode.Attributes["DisplayName"].Value = NewNodeName;
                    }

                    if (b_CreateNode)
                    {
                        XmlNode OspreyNode = m_OspreyDataXml.SelectSingleNode("//Osprey");

                        OspreyNode.AppendChild(FolderTeamNode);
                    }

                    m_OspreyDataXml.Save(this.m_OspreyDataXmlFullPath);

                    string SaveMessage = String.Format("Saved \"{0}\"",NewNodeName);
 
                    ShowStatusMessage(SaveMessage, this.Status_ForeColor, this.Status_BackColor);
 
                    ReturnValue = SaveResults.Saved;
               
                }
                else
                {
                    ShowStatusMessage(ErrMsg01, System.Drawing.Color.Black, System.Drawing.Color.Orange, ErrMsg01, System.Drawing.Color.DarkOrange, DefaultBackColor);
 
                    ReturnValue = SaveResults.Error;
                }

               
            return ReturnValue;
            
        }

        private XmlDocument GetOspreyDataXml(string OspreyDataXmlPath)
        {
            XmlDocument xDocOspreyDataXml = new XmlDocument();

            xDocOspreyDataXml.Load(OspreyDataXmlPath);

            return xDocOspreyDataXml;
        }



        private FileInfo util_CreateDataXMLFile(string FileName)
        {
            if (!FileName.EndsWith(".xml"))
            {
                FileName += ".xml";
            }

            string FileFullPath = System.IO.Path.Combine(m_OspreyDataXmlFolderPath, FileName);

            System.Text.StringBuilder sbXML = new System.Text.StringBuilder();

            sbXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sbXML.Append("<Osprey></Osprey>");
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileFullPath, false, System.Text.Encoding.UTF8);
            sw.WriteLine(sbXML.ToString());
            sw.Flush();
            sw.Close();

            return new FileInfo(FileFullPath);

        }


     
        private void util_LoadChildWindows(string FolderTeamName)
        {
            // Load a group of folders per parameter, "FolderTeamName".
            // This method is triggered when the user selectsfrom the folder group ComboBox.
            // Read the child nodes and set the ChildExplorer windows, per XML data.

            // Every time a node is loaded, UI controls must show what the user selected.
            // Change the form's text property to show this.
            // Default windows to show tiled vertically.

            
            string xPath                = String.Format("//Osprey/FolderTeam[@Name='{0}']", FolderTeamName);

            XmlNode SelectedFolderTeam  = m_OspreyDataXml.SelectSingleNode(xPath);

            if (SelectedFolderTeam != null)
            {
                XmlNodeList ExplorerChildren = SelectedFolderTeam.ChildNodes;

                    if (ExplorerChildren.Count == 0)
                    {
                        if (this.b_ShowMessages)
                        {
                            string Warning01 = "Empty url list.";
                
                            ShowStatusMessage(Warning01, System.Drawing.Color.Black, System.Drawing.Color.Orange, Warning01, System.Drawing.Color.DarkOrange, DefaultBackColor);
                        }
                    }
                    else
                    {
                        if (SelectedFolderTeam != null)
                        {
                            foreach (XmlNode nChildExplorer in SelectedFolderTeam.ChildNodes)
                            {
                                string uri = nChildExplorer.Attributes["uri"].Value;
                                
                                string label = nChildExplorer.Attributes["label"].Value;

                                ChildExplorer newFileExplorer = new ChildExplorer();

                                newFileExplorer.MdiParent = this;
                       
                                newFileExplorer.Show();
                                
                                newFileExplorer.ChildLabel = label;

                                newFileExplorer.FolderPathText = uri;
                                
                                newFileExplorer.SetBrowserUrl(uri);
                            }
                        }
                    }

                    Menu_View_Vertical_Click(new object(), new EventArgs());  //This will force the windows to tile
            }
        }



        private void util_PopulateFileListCboBox(string DataFolderPath)
        {
            //m_ArrayDataXmlFiles 

            List<string> ListFiles = new List<string>();

            System.IO.DirectoryInfo di_DataFolder = new System.IO.DirectoryInfo(DataFolderPath);
            
            System.IO.FileInfo[] fi_files = di_DataFolder.GetFiles("*.xml");

            var names = from f in fi_files
                        select f.Name;

            ListFiles.AddRange(names);

            this.Menu_File_LoadFile_CboBox.Items.AddRange(names.ToArray());

        }

        
        private void util_PopulateFolderTeamList()
        {
            // This routine populates the folder group ComboBox. It reacts from selecting an XML data file event. 
          
            XmlNodeList xmlFolderTeamNodes = m_OspreyDataXml.SelectNodes("//Osprey/FolderTeam");
        
            System.Collections.Generic.List<string> listNames = new System.Collections.Generic.List<string>();

            this.Menu_File_SelectGroup_CboBox.Items.Clear();
            
            // 1.   Does the XML file have any nodes? 

            foreach (XmlNode xNode in xmlFolderTeamNodes)
            {
                try
                {
                    listNames.Add(xNode.Attributes["DisplayName"].Value);
                }
                catch (System.NullReferenceException)
                {
                    string ErrMsg02 = "Null <TeamFolder> found. Manually remove it.";

                    ShowStatusMessage(ErrMsg02, System.Drawing.Color.Black, System.Drawing.Color.Orange, ErrMsg02, System.Drawing.Color.DarkOrange, DefaultBackColor);
                }
            }

            listNames.Sort();
 
            this.Menu_File_SelectGroup_CboBox.Items.AddRange(listNames.ToArray());
        }



        private void util_SetControlsPerSelectedXml()
        {
            // Add folder group names to the Menu_File_SelectGroup_CboBox.
            // Updates the form title like "Osprey-[SelectedFile][SelectedFolderGroup]"
            // Evaluates m_arrSelectedFolderTeamName[idx_FTeamDisplayName]

            // This routine will set the UI controls according to the current data stored in the XML file.
            // It is necessary to re-read the xml file because this method is called after new entries are saved
            // to the xml file. Becuase there can be multiple threads (application instances), the XML file
            // must be read each time this is called. Now the UI controls correctly reflect the XML file entries.

            //XmlDocument xDocOspreyData                             = GetOspreyDataXml(XmlFullPath);

            XmlNodeList xmlFolderTeamNodes                         = m_OspreyDataXml.SelectNodes("//Osprey/FolderTeam");
            System.Collections.Generic.List<string> listNames      = new System.Collections.Generic.List<string>();

            //this.Menu_File_SelectGroup_CboBox.Items.Clear();
            //this.Menu_File_NewFileExplorer.Enabled                 = true;
            //this.Menu_File_NewGroup_TextBox.Text                   = "";
            //this.Menu_File_Rename_TextBox.Text                     = "";
            //this.Menu_File_SaveAs_Texbox1.Text                     = "";
            
            // 1.   Does the XML file have any nodes? 
            
                // If "No", disable the relevant controls which will be enables once data is saved to the XML data file.
                if (xmlFolderTeamNodes.Count == 0)
                {   // There are no entries in the xml configuration file. So, there is nothing to load and these controls should be disabled.
                    this.Menu_File_SelectGroup.Enabled             = false;
                    this.Menu_File_SelectGroup_CboBox.Enabled      = false;
                    this.Menu_File_Save.Enabled                    = false;
                    this.ToolStrip_Button_Save.Enabled             = false;
                    this.Menu_File_SaveAs.Enabled                  = false;
                    this.Menu_File_Rename.Enabled                  = false;
                    this.Menu_View_ClearAll.Enabled                = false;
                    this.Menu_Edit_DeleteFolderGroup.Enabled       = false;
               
                }
                // If "Yes", enable controls. A TeamFolder node was selected through the UI.
                //else if (xmlFolderTeamNodes.Count > 0)
                //{
                //    // now the xml configuration file is no longer empty and has settings. 
                //    // So, there is data to be loaded. Now, some controls can be enabled, depending on the app's state.

                    //foreach (XmlNode xNode in xmlFolderTeamNodes)
                    //{
                    //    try
                    //    {
                    //        listNames.Add(xNode.Attributes["DisplayName"].Value);
                    //    }
                    //    catch (System.NullReferenceException)
                    //    {
                    //        string ErrMsg02 = "Null <TeamFolder> found. Manually remove it.";
                            
                    //        ShowStatusMessage(ErrMsg02, System.Drawing.Color.Black, System.Drawing.Color.Orange, ErrMsg02, System.Drawing.Color.DarkOrange, DefaultBackColor);
                    //    }
                    //}
   
                    //listNames.Sort();

                    //this.Menu_File_SelectGroup_CboBox.Enabled  = true;
                    //this.Menu_File_SelectGroup_CboBox.Text     = "-- Select --";
                    //this.Menu_File_SelectGroup_CboBox.Items.AddRange(listNames.ToArray());

                    this.Menu_File_SelectGroup.Enabled           = true;
                    this.Menu_File_SelectGroup_CboBox.Enabled    = true;
                    this.Menu_File_Save.Enabled                  = true;
                    this.Menu_File_SaveAs.Enabled                = true;
                    this.Menu_File_Rename.Enabled                = true;
                //}

                
                //if (!String.IsNullOrEmpty(Menu_File_LoadFile_CboBox.Text))
                ////if (!String.IsNullOrEmpty(m_XmlFilename))
                //{
                //    SelectedFileName = String.Format("[{0}]", Menu_File_LoadFile_CboBox.Text.Trim());
                //}

                //if (!String.IsNullOrEmpty(m_arrSelectedFolderTeamName[idx_FTeamDisplayName]))
                //{
                //    SelectedFolderTeam = String.Format("[{0}]", m_arrSelectedFolderTeamName[idx_FTeamDisplayName]);
                //}
                
            if (m_arrSelectedFolderTeamName[idx_FTeamNodeName] == null)
                {
                    //this.Text = "Osprey";
                    this.ToolStrip_Button_Save.Enabled       = false;
                    this.ToolStrip_Button_AddFolder.Enabled  = false;
                    this.Menu_File_NewFileExplorer.Enabled   = false;
                    this.Menu_File_Save.Enabled              = false;
                    this.Menu_File_SaveAs.Enabled            = false;
                    this.Menu_File_Rename.Enabled            = false;
                    this.Menu_Edit_DeleteFolderGroup.Enabled = false;
                    this.Menu_View_ClearAll.Enabled          = false;
               }
                else
                {
                    //this.Text = String.Format("Osprey [{0}]", m_arrSelectedFolderTeamName[idx_FTeamDisplayName]);
                    this.ToolStrip_Button_Save.Enabled       = true;
                    this.ToolStrip_Button_AddFolder.Enabled  = true;
                    this.Menu_View_ClearAll.Enabled          = true;
                    this.Menu_Edit_DeleteFolderGroup.Enabled = true;

                }

            //string FormTextTemplate = "Osprey  {0}  |  {1}";
            //string SelectedFileName = null;
            //string SelectedFolderTeam = null;

            //this.Text = String.Format(FormTextTemplate, SelectedFileName, SelectedFolderTeam);
        }


       

  

        private void util_ResetOspreyDataXml()
        {
            ClearAllChildWindows();

            util_CreateDataXMLFile(m_OspreyDataXmlFullPath);

            //System.Text.StringBuilder sbXML = new System.Text.StringBuilder();

            //sbXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            //sbXML.Append("<Osprey></Osprey>");
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(m_OspreyDataXmlFullPath, false, System.Text.Encoding.UTF8);
            //sw.WriteLine(sbXML.ToString());
            //sw.Flush();
            //sw.Close();
        }

        private void util_SetCurrentLoadedFolderTeamName(string DisplayName)
        {
            this.m_arrSelectedFolderTeamName[idx_FTeamDisplayName] = DisplayName;

            this.m_arrSelectedFolderTeamName[idx_FTeamNodeName] = String.IsNullOrEmpty(DisplayName) ? null : DisplayName.ToLower().Trim();  
        }

        private void btn_AcceptButton_Click(object sender, EventArgs e)
        {
        }

        private void btnClearClearAll_Click(object sender, EventArgs e)
        {
            ClearAllChildWindows();
        }


        private void Timer01_Tick(object sender, EventArgs e)
        {


            int remainder = 0;
            int even      = 0;
            string strmsg = String.Format("{0}",Status_TimerCount);


            if (Status_TimerCount < 10)         // As the count down progresses the remainder will ether be zero or some positive remander value.
            {                                   // So, this wil toggle from even-odd-even-odd....

                Math.DivRem(Status_TimerCount, 2, out remainder);

                if (remainder == even) //even
                {
                    lblStausMessage.Text        = this.Status_Message;
                    lblStausMessage.ForeColor   = this.Status_ForeColor;
                    lblStausMessage.BackColor   = this.Status_BackColor;
                }
                else //odd
                {
                    lblStausMessage.Text = Status_Message2;
                    lblStausMessage.ForeColor = this.Status_ForeColor2;
                    lblStausMessage.BackColor = this.Status_BackColor2;
                }
            }
            else
            {   // When the timer reaches 10 iterations, reset these values to the default settings.  
                Timer01.Enabled = false;
                lblStausMessage.Text = "";
                lblStausMessage.ForeColor = this.Status_DefaultForeColor;
                lblStausMessage.BackColor = this.Status_DefaultBackColor;

                this.Status_Message = "";
                this.Status_ForeColor = this.Status_DefaultForeColor;
                this.Status_BackColor = this.Status_DefaultBackColor;

                this.Status_Message2 = "";
                this.Status_ForeColor2 = this.Status_DefaultForeColor;
                this.Status_BackColor2 = this.Status_DefaultBackColor;

                Status_TimerCount = 0;
          
            }

            Status_TimerCount++;

        }


        private void ShowStatusMessage(string StatusMessage, System.Drawing.Color TextColor, System.Drawing.Color BGColor,string StatusMessage2, System.Drawing.Color TextColor2, System.Drawing.Color BGColor2)
        {
            Status_Message    = StatusMessage;
            Status_BackColor  = BGColor;
            Status_ForeColor  = TextColor;
            
            Status_Message2 =   StatusMessage2;
            Status_BackColor2 = BGColor2;
            Status_ForeColor2 = TextColor2;

        
            Timer01.Enabled = true;

        }

        private void ShowStatusMessage(string StatusMessage, System.Drawing.Color TextColor, System.Drawing.Color BGColor)
        {
            Status_Message   = StatusMessage;
            Status_BackColor = BGColor;
            Status_ForeColor = TextColor;


            Timer01.Enabled = true;

        }

        private void Menu_View_ClearAll_Click(object sender, EventArgs e)
        {
            ClearAllChildWindows();
         
            util_SetCurrentLoadedFolderTeamName(null);

            util_SetControlsPerSelectedXml();
        }

     
        private void Menu_File_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


  


        private void ospreyHelpChmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.TableOfContents);
        }

      

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {
        
        }

        private void Menu_File_SelectGroup_CboBox_MouseLeave(object sender, EventArgs e)
        {

        }

    }



}

