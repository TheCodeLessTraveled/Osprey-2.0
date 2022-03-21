using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;



namespace CodeLessTraveled.Osprey
{

    public partial class Form1 : Form
    {
        #region _Class members initial settings
        
        string HelpPath = System.IO.Path.Combine(Application.StartupPath,"Osprey Help.chm");
        private List<ChildExplorer> ArrayChildExplorer = new List<ChildExplorer>();
        // Variables used for data files and their XML contents.
        private XmlDocument             m_OspreyDataXml;
        private string                  m_CurrentXmlFilename        = null;
        private string                  m_DefaultOspreyDataXml      = "OspreyData.xml";

        private List<string>            m_ArrayDataXmlFiles         = new List<string>();
        private string                  m_XmlFileCollectionPath     = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "The Code Less Traveled", "Osprey");
        private string[]                m_arrSelectedFolderTeamName = new string[2] { "", "" };   // This array holds to two elements [0]= folder team name, [1]=folder team display name.
        private const int               m_idxFTeamNodeName          = 0;                
        private const int               m_idxFTeamDisplayName       = 1;                
        
        
        private string                 m_OspreyDataXmlFullPath;
        private System.Drawing.Point   m_SavedLocation ;
        private string                 m_LogFilePath; 

        // Variables used to track and control the User Interface's state.
        private bool                   m_UI_STATE_HasFiles          = false;
        private bool                   m_UI_STATE_FileIsOpen        = false;
        private bool                   m_UI_STATE_HasChildren       = false;
        private bool                   m_UI_STATE_HasFolderGroups   = false;
        private bool                   m_UI_STATE_FolderGroupIsOpen = false;

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

        private int                     Status_TimerCount1= 0;
        private bool                    b_ShowMessages   = true; // add this to settings upon exit.
        
        enum NodeChangeType{Rename, Save, SaveAs, Default, NewGroup, LastViewedFTeam}
        enum OpenType {FromXML, New }
        enum SaveResults {Cancelled, Error, Initialize, Saved, Skip}
        #endregion

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
                Properties.Settings.Default.LastFolderTeamName          = "";
                Properties.Settings.Default.LastFolderTeamDisplayName   = "";
            }
            else
            {
                //  Save the FolderTeam last viewed to the current xml file before exiting the program.
                SaveMain(NodeChangeType.LastViewedFTeam, null);
            }

            if (!m_CurrentXmlFilename.EndsWith(".xml"))
            {
                m_CurrentXmlFilename += ".xml";
            }

            Properties.Settings.Default.LastXmlFileName = m_CurrentXmlFilename;

            Properties.Settings.Default.Save();

        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            /* Determine what to load:
             *   1. Is there a setting for a saved data file?
             *        a. Yes. Look for the file. 
             *              i)   If file found on disk, load the saved file.
             *              ii)  If file not found, look for the default OspreyData.xml and load it.
             *              iii) If the default OspreyData.xml is not found, create an empty default OspreyData.xml and load it.
             *              
             *        b. No - Is there a a default OspreyData.xml?
             *              iii) If the default OspreyData.xml is not found, create an empty default OspreyData.xml and load it.
             *               
             *  2.  Load the ComboBox with a list of files and set the comboBox text to the saved Xml data file.
             *      This triggers the event handler to load the list of folder teams from the selected file in the ComboBox.Text.
             *     
             *  3.   Set the FolderTeam list text to the saved FolderGroup which will trigger the event to load the child windows of FolderGroups.
             */

            m_LogFilePath                  = System.IO.Path.Combine(m_XmlFileCollectionPath);
            helpProvider1.HelpNamespace    = this.HelpPath;
            this.Status_DefaultBackColor   = this.lblStausMessage.BackColor;
            this.Status_DefaultForeColor   = this.lblStausMessage.ForeColor;
            this.Status_DefaultFont        = this.lblStausMessage.Font;
 
            #region _region: Form size and location settings
            // Set Form size and location per coordinates saved from the last sessionto and recorded in the settings.
                if (Properties.Settings.Default.Form1Size.Width == 0 || Properties.Settings.Default.Form1Size.Height == 0)
                {
                    System.Drawing.Size initSize = new System.Drawing.Size(500, 500);
                    this.Size = initSize;
                }
                else
                {
                    int SavedWidth    = Properties.Settings.Default.Form1Size.Width;
                    int SavedHeight   = Properties.Settings.Default.Form1Size.Height ;
                    System.Drawing.Size SavedSize = new System.Drawing.Size(SavedWidth, SavedHeight);
                    this.Size         = SavedSize;
                }
                int SavedLeft     = Properties.Settings.Default.Form1Location.X;
                int SavedTop      = Properties.Settings.Default.Form1Location.Y;
                m_SavedLocation   = new System.Drawing.Point(SavedLeft, SavedTop);
                this.Location     = m_SavedLocation;
            #endregion


    
                #region Determine which xml file to load.
                /*  Collect a list of existing xml files available to loading. Determine what to load
                        A) If there is a saved filename setting, load it.
                            - XOR -
                        B) Load the default OspryData.xml
                            i)   If no file was saved, load the default OspreyData.xml. 
                            ii)  If OspreyData.xml does not exist, create it and load it.
                 */
                DirectoryInfo   di_DataFolder       = new System.IO.DirectoryInfo(m_XmlFileCollectionPath);
                FileInfo[]      arrFilesOnDisk      = di_DataFolder.GetFiles("*.xml");
                bool            b_XmlFileToLoad     = false;
                string          saved_XmlFileName   = Properties.Settings.Default.LastXmlFileName.Trim();

                if(saved_XmlFileName != null)
                {
                    b_XmlFileToLoad = arrFilesOnDisk.Any(f => f.Name == saved_XmlFileName);

                    if (b_XmlFileToLoad)
                    {
                        m_CurrentXmlFilename = saved_XmlFileName;
                    }
                }

                
                if (b_XmlFileToLoad == false) 
                {
                    // The saved xml file is not found on disk so try the default OspreyData.xml
                    // If the default file is not found, create it.
                    
                    b_XmlFileToLoad = arrFilesOnDisk.Any(f => f.Name == m_DefaultOspreyDataXml);

                    if (b_XmlFileToLoad == false)
                    {
                        util_CreateDataXMLFile(m_DefaultOspreyDataXml, true); 
                    }

                    m_CurrentXmlFilename = m_DefaultOspreyDataXml;

                    m_UI_STATE_HasFiles = true;
                }
                #endregion

                //  At this point the xml file to be loaded has been determined. Populate the comboBox and pre-select to cascade events for folder group ComboBox.
                util_PopulateFileListCboBox(m_XmlFileCollectionPath);  

                Menu_File_OpenDataFile_CboBox.Text = m_CurrentXmlFilename; 
            
                util_SetControlsPerSelectedXml();  
        }


        private void Form1_DragDrop(object sender, DragEventArgs e)
        {

            string[] arrFormats = e.Data.GetFormats();

            DirectoryInfo DragDirInfo = null;
            string DragDirPath;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] arrFileDropItems = (string[])e.Data.GetData(DataFormats.FileDrop);

                var objPath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                if (Directory.Exists(objPath))
                {
                    DragDirInfo = new DirectoryInfo(objPath);
                }
                else if (File.Exists(objPath))
                {
                    DragDirInfo = new DirectoryInfo(System.IO.Path.GetDirectoryName(objPath));
                }
            }


            DragDirPath = DragDirInfo.FullName;

            util_AddChildWindow(DragDirPath);

        }


        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Show();
            }

            Menu_View_Vertical_Click(new object(), new EventArgs());
        }


        private void btn_AcceptButton_Click(object sender, EventArgs e)
        {
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
            string xPathFolderTeams    = String.Format("//Osprey/FolderTeam[@Name='{0}']", m_arrSelectedFolderTeamName[m_idxFTeamNodeName]);
    
            XmlNodeList FolderTeamList = m_OspreyDataXml.SelectNodes(xPathFolderTeams);

            int PreExistingNodeCount   = FolderTeamList.Count;

            if (PreExistingNodeCount > 0)
            {
                XmlNode OspreyNode = m_OspreyDataXml.SelectSingleNode("//Osprey");

                foreach (XmlNode FolderTeam in FolderTeamList)
                {
                    OspreyNode.RemoveChild(FolderTeam);
                }

                m_OspreyDataXml.Save(m_OspreyDataXmlFullPath);
               
                string DeleteTeamName = this.m_arrSelectedFolderTeamName[m_idxFTeamNodeName];
                
                util_SetCurrentFolderGroupName(null);
                
                util_ShowStatusMessage(String.Format("Deleted {0}.", DeleteTeamName), System.Drawing.Color.DarkRed, Status_DefaultBackColor);
                
                ClearAllChildWindows();
                
                util_SetControlsPerSelectedXml();

                util_PopulateFolderGroupList();
                
            }


        }

        
        private void Menu_Edit_OspreyDataXml_OpenLocation_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();

            psi.FileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe");

            string args = "";

            if (m_OspreyDataXmlFullPath != null)
            {
                args = "\"" + m_OspreyDataXmlFullPath + "\"";
                psi.Arguments = String.Format(@"/select," + args);
                psi.WorkingDirectory = m_OspreyDataXmlFullPath;
            }
            else
            {
                args = "\"" + m_XmlFileCollectionPath + "\"";
                psi.Arguments = String.Format(args);
                psi.WorkingDirectory = m_XmlFileCollectionPath;
            }

            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            System.Diagnostics.Process.Start(psi);
        }


        private void Menu_Edit_AddFolderGroup_Textbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }


        private void Menu_Edit_AddFolderGroup_Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                /*  save
                 *  clear the list
                 *  clear child windows
                 *  
                 */
                string FolderGroupName = Menu_Edit_AddFolderGroup_Textbox.Text;

                if (!String.IsNullOrEmpty(FolderGroupName))
                {
                    util_SetCurrentFolderGroupName(FolderGroupName);

                    m_UI_STATE_HasChildren = false;

                    SaveResults SaveValue = SaveMain(NodeChangeType.NewGroup, FolderGroupName);

                    Menu_File.HideDropDown();

                    Menu_Edit.HideDropDown();
                    util_PopulateFolderGroupList();
                }
            }
        }


        private void Menu_Edit_RenameFolderGroup_Tbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

            }
        }


        private void Menu_Edit_RenameFolderGroup_Tbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string NewName = Menu_Edit_RenameFG_Textbox.Text;

                SaveResults ReturnResults = SaveMain(NodeChangeType.Rename, NewName);

                if (ReturnResults == SaveResults.Saved)
                {
                    util_SetCurrentFolderGroupName(NewName); //this.m_arrSelectedFolderTeamName = NewName;
                }

                util_PopulateFolderGroupList();

                util_SetControlsPerSelectedXml();

                Menu_File.HideDropDown();
            }

        }


        private void Menu_File_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            // The workflow for creating a new XML data file would be
            // 1.   Create the file
            // 2.   Open the file.
            // 3.   Add a Folder Group(s) to the XML file
            // 4.   Add child node paths. 
            // 5.   Save file.
            // This routine executes steps 1, but prepares the UI for step 2. 
            // See Menu_File_OpenDataFile_CboBox_SelectedIndexChanged() for step 2.
            m_UI_STATE_HasFiles = false;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string NewFileName              = this.Menu_File_NewDataFile_TextBox.Text;
                    
                    FileInfo fi_NewFile             = util_CreateDataXMLFile(NewFileName,false);  //create xml file and initializes xml document nodes.
                    
                    this.m_OspreyDataXmlFullPath    = fi_NewFile.FullName;
                    
                    this.m_CurrentXmlFilename              = fi_NewFile.Name;
                    
                    this.m_OspreyDataXml            = util_GetOspreyDataXml(m_OspreyDataXmlFullPath);

                    
                    
                    util_PopulateFileListCboBox(m_XmlFileCollectionPath); // count data files,  clear the CboBox, add file list to CboBox.
                    
                    this.Menu_File_OpenDataFile_CboBox.Text = fi_NewFile.Name;  // Set m_OspreyDataXml XmlDocument.
                       
                    m_UI_STATE_HasFiles             = true;
                    
                    m_UI_STATE_FolderGroupIsOpen    = false;
                   
                    m_UI_STATE_HasChildren          = false;
                    
                    m_UI_STATE_HasFolderGroups      = false;

                    Menu_File_OpenDataFile.Select();
                    
                    m_UI_STATE_FileIsOpen           = true;
                    

                    util_SetControlsPerSelectedXml();  //  Disable/Enable menu controls, control.Text=""

                }
                catch (Exception ex)
                {
                    util_LogError ("Error creating new data file. Error is logged.", ex.ToString());
                }
            }
            
        }

                
        private void Menu_File_NewFileExplorer_Click(object sender, EventArgs e)
        {
            // are there any entries in the OspreyData.xml
            //  node count is 0, then nothing has been saved before. initial save requires a name and requires a name.

            ChildExplorer newFileExplorer = new ChildExplorer();
            newFileExplorer.MdiParent = this;
            newFileExplorer.Show();
            ArrayChildExplorer.Add(newFileExplorer);
        }


        private void Menu_File_NewFolderGroup_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }


        private void Menu_FolderGroup_Click(object sender, EventArgs e)
        {
            //Menu_FolderGroup_ComboBox.ComboBox.DroppedDown = true;
        }


        private void Menu_FolderGroup_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            util_FolderGroupSelected(Menu_FolderGroup_ComboBox.Text);

            Menu_FolderGroup.HideDropDown();
        }
  
        
        private void Menu_File_OpenDataFile_CboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

  
        private void Menu_File_OpenDataFile_CboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //!!!!!!!!!!  This is the only place to set the m_OspreyDataXML  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // This event occurs when a new data file is selected from the combobox dropdown list.
            // Once the user selects a file the list of folder teams (groups of folder) fill the folder list combobox (Menu_FolderGroup_ComboBox)
            // give focus to the that downstream control and expand it to visually queue the user and hint at the next action.
       
            ClearAllChildWindows();
      
            Menu_File.HideDropDown();
      
            if (null != m_OspreyDataXml)
            {
                SaveResults result = SaveMain(NodeChangeType.LastViewedFTeam, null);
            }
            
            string testPath = System.IO.Path.Combine(m_XmlFileCollectionPath, Menu_File_OpenDataFile_CboBox.Text);


            if (File.Exists(testPath))                      // load the newly selected file. Check if there is a LastViewedFolderTeam node value.
            {
                util_SetCurrentFolderGroupName(null);
               
                m_CurrentXmlFilename           = Menu_File_OpenDataFile_CboBox.Text;

                m_OspreyDataXmlFullPath = testPath;
               
                m_OspreyDataXml         = util_GetOspreyDataXml(m_OspreyDataXmlFullPath);

                util_PopulateFolderGroupList();             // This will use the m_OspreyDataXml that was set above to populate the folder group ComboBox?

                util_SetFolderLastViewedFolderTeam();

                
                m_UI_STATE_FileIsOpen = true;

                m_UI_STATE_FolderGroupIsOpen = true;    // set to true after the folder is selected - either by the use or when the folder team combobox is populated.

                util_SetControlsPerSelectedXml();
               
            }
            else
            {
                string Msg01 = String.Format("File not found: {0}. Select one from list.", Menu_File_OpenDataFile_CboBox.Text);
                util_ShowStatusMessage(Msg01, System.Drawing.Color.DarkRed, DefaultBackColor);
            }
            
         }

        
        private void Menu_File_Save_Click_1(object sender, EventArgs e)
        {
            // assume there is something already loaded from the XML configuration file, OspreyData.xml.
            SaveMain(NodeChangeType.Save, m_arrSelectedFolderTeamName[m_idxFTeamNodeName]);
     
            util_SetControlsPerSelectedXml();
        }


        private void Menu_File_SaveAs_Texbox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Menu_File.HideDropDown();
                SaveMain(NodeChangeType.SaveAs, Menu_File_SaveAs_Texbox1.Text.Trim());
                
                util_SetCurrentFolderGroupName(Menu_File_SaveAs_Texbox1.Text);//m_arrSelectedFolderTeamName = Menu_File_SaveAs_Texbox1.Text;
    
             if (File.Exists(System.IO.Path.Combine(m_XmlFileCollectionPath, Menu_File_OpenDataFile_CboBox.Text)))
            {
                m_CurrentXmlFilename = Menu_File_OpenDataFile_CboBox.Text;

                m_OspreyDataXmlFullPath = System.IO.Path.Combine(m_XmlFileCollectionPath, m_CurrentXmlFilename );

                m_OspreyDataXml= util_GetOspreyDataXml(m_OspreyDataXmlFullPath);

                util_PopulateFolderGroupList(); 


                bool b_LastTeamSaved = !String.IsNullOrEmpty(m_arrSelectedFolderTeamName[m_idxFTeamDisplayName]) && !String.IsNullOrEmpty(m_arrSelectedFolderTeamName[m_idxFTeamNodeName]);

                if (b_LastTeamSaved)
                {
                    this.Menu_FolderGroup_ComboBox.Text = m_arrSelectedFolderTeamName[m_idxFTeamDisplayName];
                }
            }
            else
            {
                string Msg01 = String.Format("File not found: {0}. Select one from list.", Menu_File_OpenDataFile_CboBox.Text);
                util_ShowStatusMessage(Msg01, System.Drawing.Color.DarkRed, DefaultBackColor);
            }

             m_UI_STATE_HasFolderGroups = true;

             util_SetControlsPerSelectedXml();
            
            }
        }


        private void Menu_File_SaveAs_Texbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
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


        private void Menu_View_ClearAll_Click(object sender, EventArgs e)
        {
            ClearAllChildWindows();

            util_SetCurrentFolderGroupName(null);

            util_SetControlsPerSelectedXml();
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


        private void ospreyHelpChmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.TableOfContents);
        }


        private SaveResults SaveMain(NodeChangeType ChangeType, string NewNodeName)
        {
            // There are 2 main types of saves. 
            // 1)   Saving the last viewed folder group. This occurs every time a new xml data file is selected.  
            // 2)   Most of the logic in this function is for changes to FolderTeam nodes (new FolderTeam, changes to existing FolderTeam and the child collection).


            if (null != m_OspreyDataXml && ChangeType == NodeChangeType.LastViewedFTeam)
            {
                string xPathLastViewedTeamName = "//Osprey/LastViewedFolderTeam";      //<LastViewedFolderTeam>Triple H</LastViewedFolderTeam>
                
                XmlNode nodeLastViewedFTeam = m_OspreyDataXml.SelectSingleNode(xPathLastViewedTeamName);

                if (null == nodeLastViewedFTeam)
                {
                    // create the node.
                    XmlNode FirstFT= m_OspreyDataXml.SelectSingleNode("//Osprey/FolderTeam[1]");

                    nodeLastViewedFTeam = m_OspreyDataXml.CreateElement("LastViewedFolderTeam");
                    m_OspreyDataXml.DocumentElement.InsertBefore(nodeLastViewedFTeam, FirstFT);
                }

                nodeLastViewedFTeam.InnerText = m_arrSelectedFolderTeamName[m_idxFTeamDisplayName];
                
                m_OspreyDataXml.Save(this.m_OspreyDataXmlFullPath);

                return SaveResults.Saved;
            }


#region _ Scenario #2: The node change is related an edit of the current FolderTeam

            NewNodeName = NewNodeName.Trim();
           
            SaveResults ReturnValue = SaveResults.Initialize;
            
            string          xPathNewTeamName        = String.Format("//Osprey/FolderTeam[@Name='{0}']", NewNodeName.ToLower().Trim());
            string          xPathCurrentTeamName    = String.Format("//Osprey/FolderTeam[@Name='{0}']", m_arrSelectedFolderTeamName[m_idxFTeamNodeName]);
           
            XmlNodeList     FolderTeamList          = m_OspreyDataXml.SelectNodes(xPathNewTeamName);
            int             NodeCount               = FolderTeamList.Count;                 // depends on the type of save.

            XmlNode         FolderTeamNode          = null;
            XmlAttribute    attName                 = null;
            XmlAttribute    attDisplayName          = null;
          
      
            bool b_DupFound          = false;
            bool b_CreateNode        = false;
            bool b_AddChildNodes     = false;
            bool b_DelChildNodes     = false;
            bool b_Rename            = false;
          
            string ErrMsg01          = String.Empty;

            // The objective here is to identify if the use is doing something wrong, like, is he/she trying to save a a folder team with the same name as an existing node.
            if (NodeCount > 1)                                // This indecates an attempt to save a duplicate name. We can't have more than one <FolderTeam> node with the same name.
            {                                                 // This later entry will simple overwrite the the existing, rather than try to warn the use of the conflict.
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

#endregion _ Edit to currently active FolderTeam

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
                        string FolderPathText = childForm.ChildConfig.Uri;
                          
                        if (!String.IsNullOrEmpty(FolderPathText) && !String.IsNullOrWhiteSpace(FolderPathText))
                            {
                            XmlNode nChildFileExplorerUri = m_OspreyDataXml.CreateNode(XmlNodeType.Element, "ChildExplorer", "");

                            XmlAttribute atColorArgb = m_OspreyDataXml.CreateAttribute("colorargb");
                            atColorArgb.Value = childForm.ChildConfig.ColorArgbInt.ToString();
                            nChildFileExplorerUri.Attributes.Append(atColorArgb);

                            XmlAttribute atLabel = m_OspreyDataXml.CreateAttribute("label");
                            atLabel.Value = childForm.ChildConfig.Label; 
                            nChildFileExplorerUri.Attributes.Append(atLabel);

                            XmlAttribute atUri = m_OspreyDataXml.CreateAttribute("uri");
                            atUri.Value = FolderPathText;
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
 
                util_ShowStatusMessage(SaveMessage, this.Status_ForeColor, this.Status_BackColor);
 
                ReturnValue = SaveResults.Saved;
               
            }
            else
            {
                util_ShowStatusMessage(ErrMsg01, System.Drawing.Color.Black, System.Drawing.Color.Orange, ErrMsg01, System.Drawing.Color.DarkOrange, DefaultBackColor);
 
                ReturnValue = SaveResults.Error;
            }

               
            return ReturnValue;
            
        }


        private void util_AddChildWindow(string FolderPath)
        {

            ChildExplorerConfig newConfig = new ChildExplorerConfig();
            newConfig.Uri = FolderPath;
            newConfig.ColorArgbInt = 0;
            newConfig.Label = "";


            ChildExplorer newFileExplorer = new ChildExplorer(newConfig);

            newFileExplorer.MdiParent = this;
       
            ArrayChildExplorer.Add(newFileExplorer);

            newFileExplorer.Show();

        }


        private FileInfo util_CreateDataXMLFile(string FileName, bool CreateSampleNode)
        {
            if (!FileName.ToLower().EndsWith(".xml"))
            {
                FileName += ".xml";
            }

            string FileFullPath = System.IO.Path.Combine(m_XmlFileCollectionPath, FileName);

            System.Text.StringBuilder sbXML = new System.Text.StringBuilder();

            sbXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            
            sbXML.AppendLine("<Osprey>");
            
            sbXML.AppendLine("<LastViewedFolderTeam></LastViewedFolderTeam>");
           
            if (CreateSampleNode)
            {
                sbXML.AppendLine("<FolderTeam Name=\"sample\" DisplayName=\"Sample\"></FolderTeam>");
            }
            
            sbXML.AppendLine("</Osprey>");
            
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileFullPath, false, System.Text.Encoding.UTF8);
            
            sw.WriteLine(sbXML.ToString());
            
            sw.Flush();
            
            sw.Close();

            return new FileInfo(FileFullPath);

        }


        private void util_FolderGroupSelected(string SelectedFolderGroupName)
        {

            Menu_File.HideDropDown();

            m_UI_STATE_FolderGroupIsOpen = false;

            util_SetCurrentFolderGroupName(SelectedFolderGroupName); //This method sets the two values in the array, m_arrSelectedFolderTeamName

            ClearAllChildWindows();

            util_LoadChildWindows(m_arrSelectedFolderTeamName[m_idxFTeamNodeName]);

            m_UI_STATE_FolderGroupIsOpen = true;

            m_UI_STATE_HasFolderGroups = true;

            util_SetControlsPerSelectedXml();

        }


        private XmlDocument util_GetOspreyDataXml(string OspreyDataXmlPath)
        {
            XmlDocument xDocOspreyDataXml = new XmlDocument();

            xDocOspreyDataXml.Load(OspreyDataXmlPath);

            m_UI_STATE_FileIsOpen = true;

            return xDocOspreyDataXml;
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
                        m_UI_STATE_HasChildren = false;

                        if (this.b_ShowMessages)
                        {
                            string Warning01 = "Empty url list for this folder group.";
                
                            util_ShowStatusMessage(Warning01, System.Drawing.Color.Black, System.Drawing.Color.Orange, Warning01, System.Drawing.Color.DarkOrange, DefaultBackColor);
                        }
                    }
                    else
                    {
                        m_UI_STATE_HasChildren = true;

                        if (SelectedFolderTeam != null)
                        {
                            foreach (XmlNode nChildExplorer in SelectedFolderTeam.ChildNodes)
                            {
                                string uri          = nChildExplorer.Attributes["uri"].Value;
                                
                                string label        = nChildExplorer.Attributes["label"].Value;

                                string ColorTest    = null;
                               
                                int ColorARGB       = 0;
                                
                                XmlAttribute ColorAttrib = nChildExplorer.Attributes["colorargb"];
                                
                                if (ColorAttrib != null)
                                {
                                    ColorTest = nChildExplorer.Attributes["colorargb"].Value;

                                    if (ColorTest != null)
                                    {
                                        int.TryParse(ColorTest, out ColorARGB);
                                    }

                                }
                                
                                ChildExplorerConfig childConfig = new ChildExplorerConfig();

                                if (ColorARGB != 0)
                                {
                                    childConfig.ColorArgbInt = ColorARGB;
                                }
                                
                                childConfig.Label   = label;
                               
                                childConfig.Uri     = uri;
                                
                                ChildExplorer newFileExplorer = new ChildExplorer(childConfig);

                                newFileExplorer.MdiParent = this;
                                
                                newFileExplorer.Show();
                               
                            }
                        }
                    }

                    Menu_View_Vertical_Click(new object(), new EventArgs());  //This will force the windows to tile
            }
        }


        private void util_LogError(string StatusMessage, string LogMessage)
        {


            util_ShowStatusMessage(StatusMessage, System.Drawing.Color.DarkRed, Status_DefaultBackColor);

            string  LogText      = System.Environment.NewLine;
                    LogText     += String.Format("-----     {0:yyyy.MM.dd_hhmmss}     -----", System.DateTime.Today) + System.Environment.NewLine;
                    LogText     += StatusMessage + System.Environment.NewLine;
                    LogText     += LogMessage + System.Environment.NewLine;

                

            using (StreamWriter sw_log = new StreamWriter(m_LogFilePath,true) ) 
            {
                sw_log.WriteLine(LogText);    
            }
        }


        private void util_PopulateFileListCboBox(string DataFolderPath)
        {
            List<string> ListFiles = new List<string>();

            System.IO.DirectoryInfo di_DataFolder = new System.IO.DirectoryInfo(DataFolderPath);
            
            System.IO.FileInfo[] fi_files = di_DataFolder.GetFiles("*.xml");

            if (fi_files.Count() > 0)
            {
                var names = from f in fi_files
                            select f.Name;

                Menu_File_OpenDataFile_CboBox.Items.Clear();
                Menu_FolderGroup_ComboBox.Items.Clear();
                ListFiles.AddRange(names);

                this.Menu_File_OpenDataFile_CboBox.Items.AddRange(names.ToArray());
             
                m_UI_STATE_HasFiles = true;
            }
            else
            {
                m_UI_STATE_HasFiles = false;
                if (this.b_ShowMessages)
                {
                    string Warning01 = "No data files exist. Create them.";

                    util_ShowStatusMessage(Warning01, System.Drawing.Color.Black, System.Drawing.Color.Orange, Warning01, System.Drawing.Color.DarkOrange, DefaultBackColor);
                }
            }

            
        }


        private void util_PopulateFolderGroupList()
        {
            // This routine populates the folder group ComboBox. It reacts from selecting an XML data file event from the file ComboBox list. 

            XmlNodeList xmlFolderTeamNodes = m_OspreyDataXml.SelectNodes("//Osprey/FolderTeam");
                
            if (xmlFolderTeamNodes.Count == 0)
            {// - NO nodes:   Notify use the xml has no group nodes.    
                m_UI_STATE_HasFolderGroups = false;
                
                string Warning01 = "No folder groups listed in this data file.";
                util_ShowStatusMessage(Warning01, System.Drawing.Color.Black, System.Drawing.Color.Orange, Warning01, System.Drawing.Color.DarkOrange, DefaultBackColor);
            }
            else
            {// - YES nodes:  Add folder nodes to the folder group ComboBox by 1. create a generic list. Add the entire list range to the combobox Items array.
                
                m_UI_STATE_HasFolderGroups = true;

                var FolderGroupNames = xmlFolderTeamNodes.Cast<XmlNode>().Select(node => node.Attributes["DisplayName"].Value).ToList();

                //var FolderGroupNames = xmlFolderTeamNodes.Cast<XmlNode>().OrderBy(node => node.Attributes["DisplayName"].Value);

                this.Menu_FolderGroup_ComboBox.Text = "";
                
               // this.Menu_File_FolderGroup_CboBox.Items.Clear();
                this.Menu_FolderGroup_ComboBox.Items.Clear();

                FolderGroupNames.Sort();

               // this.Menu_File_FolderGroup_CboBox.Items.AddRange(FolderGroupNames.ToArray());
                this.Menu_FolderGroup_ComboBox.Items.AddRange(FolderGroupNames.ToArray());
            }

            util_SetControlsPerSelectedXml();
        }



        private void util_ResetOspreyDataXml()
        {
            ClearAllChildWindows();

            util_CreateDataXMLFile(m_OspreyDataXmlFullPath, true);
        }



        private void util_SetControlsPerSelectedXml()
        {
            // This routine is mainly responsible for setting the UI control properties for Enabled=TRUE or FALSE
            // As the user operates the various controls, flags are set to capture what was changed. This method 
            // is called after these operations to evaluated the flags enabled/disable UI Controls as needed.
            // This is also how the main parent form's text/title is caluated and set.

            this.Menu_File_NewDataFile.Enabled             = true;
            this.Menu_File_OpenDataFile.Enabled            = false;
            this.Menu_File_Save.Enabled                    = false;
            this.Menu_File_SaveAs.Enabled                  = false;
            this.Menu_Edit_DeleteFolderGroup.Enabled       = false;
            this.Menu_View_ClearAll.Enabled                = false;
            this.ToolStrip_Button_AddFolder.Enabled        = false;
            this.ToolStrip_Button_Save.Enabled             = false;

            if (m_UI_STATE_HasFiles)
            {
                this.Menu_File_OpenDataFile.Enabled = true;
            }


            if (m_UI_STATE_FileIsOpen)
            {
                this.Menu_File_NewDataFile_TextBox.Text    = "";
                this.Menu_File_Save.Enabled                = true;
                this.Menu_File_SaveAs.Enabled              = true;
                this.ToolStrip_Button_Save.Enabled         = true;
                this.Text = String.Format("Osprey  \u2502  {0}", m_CurrentXmlFilename);
            }


            if (m_UI_STATE_FolderGroupIsOpen)
            {
                this.ToolStrip_Button_AddFolder.Enabled    = true;
                this.Menu_FolderGroup_ComboBox.Text        = m_arrSelectedFolderTeamName[m_idxFTeamDisplayName];
                this.Text = String.Format("Osprey  \u2502  {0}  \u2502  {1}", m_CurrentXmlFilename, m_arrSelectedFolderTeamName[m_idxFTeamDisplayName]);
            }   
         

            if (m_UI_STATE_HasFolderGroups)
            {
                this.Menu_Edit_RenameFolderGroup.Enabled   = true;
                this.ToolStrip_Button_Save.Enabled         = true;
            }

            if (m_UI_STATE_HasChildren)
            {
                this.Menu_File_Save.Enabled                = true;
                this.Menu_File_SaveAs.Enabled              = true;
                this.Menu_Edit_RenameFolderGroup.Enabled   = true;
                this.Menu_Edit_DeleteFolderGroup.Enabled   = true;
                this.Menu_View_ClearAll.Enabled = false;
            }
        }


        private void util_SetCurrentFolderGroupName(string DisplayName)
        {
            this.m_arrSelectedFolderTeamName[m_idxFTeamDisplayName] = DisplayName;

            this.m_arrSelectedFolderTeamName[m_idxFTeamNodeName] = String.IsNullOrEmpty(DisplayName) ? null : DisplayName.ToLower().Trim();
        }


        private void util_SetFolderLastViewedFolderTeam()
        {
            string savedFolderTeam;

            XmlNode FolderTeamViewedLast = m_OspreyDataXml.SelectSingleNode("//Osprey/LastViewedFolderTeam");

            if (null != FolderTeamViewedLast)
            {
                savedFolderTeam = FolderTeamViewedLast.InnerText;

                util_SetCurrentFolderGroupName(savedFolderTeam);

                this.Menu_FolderGroup_ComboBox.Text = savedFolderTeam;


            }

        }


        private void util_ShowStatusMessage(string StatusMessage, System.Drawing.Color TextColor, System.Drawing.Color BGColor, string StatusMessage2, System.Drawing.Color TextColor2, System.Drawing.Color BGColor2)
        {
            Status_Message = StatusMessage;
            Status_BackColor = BGColor;
            Status_ForeColor = TextColor;

            Status_Message2 = StatusMessage2;
            Status_BackColor2 = BGColor2;
            Status_ForeColor2 = TextColor2;

            Timer_Message1.Enabled = true;

        }


        private void util_ShowStatusMessage(string StatusMessage, System.Drawing.Color TextColor, System.Drawing.Color BGColor)
        {
            Status_Message = StatusMessage;
            Status_BackColor = BGColor;
            Status_ForeColor = TextColor;

            Timer_Message1.Enabled = true;

        }


        private void Timer01_Tick(object sender, EventArgs e)
        {
            int remainder = 0;
            int even      = 0;
            string strmsg = String.Format("{0}",Status_TimerCount1);


            if (Status_TimerCount1 < 7)         // As the count down progresses the remainder will ether be zero or some positive remander value.
            {                                   // So, this wil toggle from even-odd-even-odd....

                Math.DivRem(Status_TimerCount1, 2, out remainder);

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

                Timer_Message1.Enabled = false;
                lblStausMessage.ForeColor = this.Status_DefaultForeColor;
                lblStausMessage.BackColor = this.Status_DefaultBackColor;

                this.Status_Message = "";
                this.Status_ForeColor = this.Status_DefaultForeColor;
                this.Status_BackColor = this.Status_DefaultBackColor;

                this.Status_Message2 = "";
                this.Status_ForeColor2 = this.Status_DefaultForeColor;
                this.Status_BackColor2 = this.Status_DefaultBackColor;

                Status_TimerCount1 = 0;

                this.Timer_Message2.Enabled = true ;
            }

            Status_TimerCount1++;

        }


        private void Timer_Message2_Tick(object sender, EventArgs e)
        {
            Timer_Message2.Enabled = false;
            lblStausMessage.Text = "";
        }

        
    }



    public struct ChildExplorerConfig
    {
        private int     m_ColorArgbInt;
        private string  m_Label;
        private string  m_Uri;
        

        public int ColorArgbInt
        {
            get { return m_ColorArgbInt; }
            set { m_ColorArgbInt = value; }
        }

        public string Label
        {
            get { return m_Label; }
            set { m_Label = value; }
        }

        public string Uri
        {
            get { return m_Uri; }
            set { m_Uri = value; }
        }

    }

}

