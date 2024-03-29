﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;


namespace CodeLessTraveled.Osprey
{

    
    public struct XmlFileCollectionPath
    {
        public string CollectionPath;
    }
    public struct FileScopeData
    {
        private string               m_LastViewedFolderGroup;
        private int                  m_FileColorArgb;

        public int FileColorArgb
        {
            get { return m_FileColorArgb; }
            set { m_FileColorArgb = value; }
        }

        public string LastViewedFolderGroup
        {
            get { return m_LastViewedFolderGroup; }
            set { m_LastViewedFolderGroup = value; }
        }
    }


    public partial class Form1 : Form
    {

        #region _class members initial settings

        /*   
        ╔═══════════════════════════════════════════════════════════════════════════════════════════════════╗
        ║                                 Set member variables                                              ║
        ╚═══════════════════════════════════════════════════════════════════════════════════════════════════╝
        */
        private int m_explorer_window_count = 0;
        string HelpPath = System.IO.Path.Combine(Application.StartupPath,"osprey help.chm");
        private List<ChildExplorer> arrayChildExplorer              = new List<ChildExplorer>();
        
        // variables used for data files and their xml contents.
        private XmlDocument             m_CurrentXml;
        private string                  m_CurrentXmlFilename        = null;
        private string                  m_DefaultOspreyDataXml      = "OspreyData.xml";
        private List<string>            m_ArrayDataXmlFiles         = new List<string>();


        private string m_XmlFileCollectionPath = null; // set after Initialize();

        private string[]                m_CurrentFolderGroup        = new string[2] { "", "" };   // this array holds to two elements [0]= folder team name, [1]=folder team display name.
        private const int               m_idxFTeamNodeName          = 0;                
        private const int               m_idxFTeamDisplayName       = 1;

        private string                  m_CurrentXmlfullpath;
        private System.Drawing.Point    m_SavedLocation ;
        private string                  m_LogFilePath; 

        // variables used to track and control the user interface's state.
        private bool                    m_UI_STATE_HasFiles          = false;
        private bool                    m_UI_STATE_FileIsOpen        = false;
        private bool                    m_UI_STATE_HasChildren       = false;
        private bool                    m_UI_STATE_HasFolderGroups   = false;
        private bool                    m_UI_STATE_FolderGroupIsOpen = false;

        // variables used for displaying messages in the lblStausMessage control.
        private System.Drawing.Color    status_DefaultBackColor ;
        private System.Drawing.Color    status_defaultforecolor;
        
        private string                  status_message;
        private System.Drawing.Color    status_backcolor;
        private System.Drawing.Color    status_forecolor;
        
        private string                  status_message2;
        private System.Drawing.Color    status_backcolor2;
        private System.Drawing.Color    status_forecolor2;

        private int                     status_TimerCount1  = 0;
        private bool                    b_ShowMessages      = true; // add this to settings upon exit.
        private int                     status_childload_count     = 0;    // as child windows are loaded, track the count to display in the status bar.
        private int                     status_child_count         = 0;    // as child windows are loaded, track the count to display in the status bar.
        private string                  m_AutoResizeToolTip = "Dynamically auto size child windows";
        enum NodeChangeType {Rename, Save, SaveAs, Default, NewGroup, LastViewedFTeam, FileScopeChange}
        enum SaveResults {Cancelled, Error, Initialize, Saved, Skip}
        #endregion

        FileScopeData m_FileScopeData = new FileScopeData();

        public Form1()
        {
            InitializeComponent();

            string exeLocation = System.Reflection.Assembly.GetEntryAssembly().Location;

            string exeDirectory = System.IO.Path.GetDirectoryName(exeLocation);

            string testRepoPath = System.IO.Path.Combine(exeDirectory, "XmlRepository");

            

            DirectoryInfo di_XmlFileCol;

            if (!Directory.Exists(testRepoPath))
            {
                di_XmlFileCol = Directory.CreateDirectory(testRepoPath);
            }
            else
            {
                di_XmlFileCol = new DirectoryInfo(testRepoPath);
            }

            m_XmlFileCollectionPath = di_XmlFileCol.FullName;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Form1Location   = this.Location;
            Properties.Settings.Default.Form1Size       = this.Size;
            Properties.Settings.Default.PreferAutoResize = this.Menu_AutoResize.Checked;

            //  Save the folderteam last viewed to the current xml file before exiting the program.
            SaveMain(NodeChangeType.LastViewedFTeam, m_CurrentFolderGroup[m_idxFTeamDisplayName]);
            FileScopeData fsd =   SaveFileScopeSettings();

            
            if (!m_CurrentXmlFilename.EndsWith(".xml"))
            {
                m_CurrentXmlFilename += ".xml";
            }

            Properties.Settings.Default.LastXmlFileName = m_CurrentXmlFilename;

            Properties.Settings.Default.Save();
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            /* 
             ╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
             ║                                                                                                                       ║
             ║ Determine what to load:                                                                                               ║
             ║   1. is there a setting for a saved data file?                                                                        ║          
             ║        a. yes. look for the file.                                                                                     ║
             ║              i)   if file found on disk, load the saved file.                                                         ║
             ║              ii)  if file not found, look for the default ospreydata.xml and load it.                                 ║
             ║              iii) if the default ospreydata.xml is not found, create an empty default ospreydata.xml and load it.     ║
             ║                                                                                                                       ║
             ║        b. no - is there a a default ospreydata.xml?                                                                   ║
             ║              iii) if the default ospreydata.xml is not found, create an empty default ospreydata.xml and load it.     ║
             ║                                                                                                                       ║
             ║  2.  load the combobox with a list of files and set the combobox text to the saved xml data file.                     ║
             ║      this triggers the event handler to load the list of folder teams from the selected file in the combobox.text.    ║
             ║                                                                                                                       ║
             ║  3.  Set the folderteam list text to the saved FolderGroup which will trigger the event to load the child windows     ║
             ║      of FolderGroups.                                                                                                 ║
             ║                                                                                                                       ║
             ╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
             */

            m_LogFilePath = System.IO.Path.Combine(m_XmlFileCollectionPath);
            helpProvider1.HelpNamespace    = this.HelpPath;


            #region _region: Set controls per Properties.Settings 
            // set form size and location per coordinates saved from the last session .
            if (Properties.Settings.Default.Form1Size.Width == 0 || Properties.Settings.Default.Form1Size.Height == 0)
                {
                    System.Drawing.Size initsize = new System.Drawing.Size(500, 500);
                    this.Size = initsize;
                }
                else
                {
                    int savedwidth    = Properties.Settings.Default.Form1Size.Width;
                    int savedheight   = Properties.Settings.Default.Form1Size.Height ;
                    System.Drawing.Size savedsize = new System.Drawing.Size(savedwidth, savedheight);
                    this.Size         = savedsize;
                }
                int savedleft     = Properties.Settings.Default.Form1Location.X;
                int savedtop      = Properties.Settings.Default.Form1Location.Y;
                m_SavedLocation   = new System.Drawing.Point(savedleft, savedtop);
                this.Location     = m_SavedLocation;

            
            string AltXmlCol = Properties.Settings.Default.AltXmlRepository;

            Menu_AutoResize.Checked = Properties.Settings.Default.PreferAutoResize;

            Menu_AutoResize_Click(new object(), new EventArgs());

            #endregion

            m_XmlFileCollectionPath = String.IsNullOrEmpty(AltXmlCol) ? m_XmlFileCollectionPath : AltXmlCol;

            #region determine which xml file (by file name) to load.
               /*  collect a list of existing xml files located in osprey's xml file repository. determine which one to load
                       a) look to opsrey's cache for a saved filename setting, load it.
                           - xor -
                       b) if no file name is cached, load the default osprydata.xml
                           - xor -
                       c) if ospreydata.xml does not exist, create it and load it.
               */
                DirectoryInfo di_DataFolder     = new System.IO.DirectoryInfo(m_XmlFileCollectionPath);
                FileInfo[] arrFilesOnDisk       = di_DataFolder.GetFiles("*.xml");
                bool b_FoundXmlFileToLoad       = false;
               
                string saved_xmlfilename = Properties.Settings.Default.LastXmlFileName.Trim();

                if(saved_xmlfilename != null)
                {
                    b_FoundXmlFileToLoad = arrFilesOnDisk.Any(f => f.Name == saved_xmlfilename);

                    if (b_FoundXmlFileToLoad)
                    {
                        m_CurrentXmlFilename = saved_xmlfilename;
                    }
                }

                
                if (b_FoundXmlFileToLoad == false) 
                {
                    // the saved xml file is not found on disk so try the default ospreydata.xml
                    // if the default file is not found, create it.
                    
                    b_FoundXmlFileToLoad = arrFilesOnDisk.Any(f => f.Name == m_DefaultOspreyDataXml);

                    if (b_FoundXmlFileToLoad == false)
                    {
                        util_CreateDataXmlFile(m_DefaultOspreyDataXml, true); 
                    }

                    m_CurrentXmlFilename = m_DefaultOspreyDataXml;

           
                    
                    m_UI_STATE_HasFiles = true;
                }
                #endregion

                //  at this point the xml file to be loaded has been determined. populate the combobox and pre-select to cascade events for folder group combobox.
                util_PopulateFileListCboBox(m_XmlFileCollectionPath);
             
            if (!String.IsNullOrEmpty(m_CurrentXmlFilename))
                {
                    Menu_File_OpenDataFile_CboBox.Text = m_CurrentXmlFilename;  // triggers the event, Menu_File_OpenDataFile_CboBox_selectedindexchange.
                }
                else
                {
                    util_SetControlsPerSelectedXml();                           // this function is called in the event, Menu_File_Newdatafile_textbox_keyup. 
                }                                                               // only call it from here - form_load() - if Menu_File_Newdatafile_textbox_keyup is not triggered from form_load().

            
            Menu_FolderGroup_ComboBox_0.Text = util_GetLastViewedFolderTeam();  // !set m_CurrentFolderGroup[m_idxFTeamDisplayName] by reading the current xml.

            FileScopeData fsd =  util_GetFileScopeSettings(m_CurrentXml);       // Not in use yet. Future implentation.
        }

        public void ChangeMenuStrip1BackColor(System.Drawing.Color MenuStripBackColor)
        {
            // this function is called from the child, FormConfig, when the user clicked the <Set> button.
            this.menuStrip1.BackColor = MenuStripBackColor;
        }


        public void Menu_Edit_Config_Enable(bool Is_Enabled )
        {
            this.Menu_Edit_Config.Enabled = Is_Enabled;
        }
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            
            string[] arrFormats = e.Data.GetFormats();

            DirectoryInfo DragDirInfo = null;
            string DragDirPath;

            // determine if the object being drag-dropped is a folder or a file.
            // when it's a folder use the folders address to create the child explorer window.
            // but, when it's a file, use the file's parent directory to create the child explorer window.

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] arrFileDropItems = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string ObjPath in arrFileDropItems)
                {
                    //var ObjPath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                    if (Directory.Exists(ObjPath))
                    {
                        DragDirInfo = new DirectoryInfo(ObjPath);
                    }
                    else if (File.Exists(ObjPath))
                    {
                        DragDirInfo = new DirectoryInfo(System.IO.Path.GetDirectoryName(ObjPath));
                    }

                    DragDirPath = DragDirInfo.FullName;

                    util_AddChildWindow(DragDirPath);
                }
            }

        }


        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }


        private void ClearAllChildWindows()
        {
            foreach (ChildExplorer ChildForm in this.MdiChildren)
            {
                ChildForm.Close();
            }
        }
        
        
        private void Menu_Edit_DeleteFolderGroup_DoDelete_Click(object sender, EventArgs e)
        {
            string xpathfg    = String.Format("//Osprey/FolderTeam[@Name='{0}']", m_CurrentFolderGroup[m_idxFTeamNodeName]);
    
            XmlNode FGroupDelete= m_CurrentXml.SelectSingleNode(xpathfg); // really, there should only be one folder group with this name.

            string FGNameDelete  = (FGroupDelete.Attributes["Name"]).Value;

            if (null!=FGroupDelete)
            {
                FGroupDelete.ParentNode.RemoveChild(FGroupDelete);

                m_CurrentXml.Save(m_CurrentXmlfullpath);

                util_SetCurrentFolderGroupName(null);

                ClearAllChildWindows();

                util_ShowStatusMessage(String.Format("Deleted {0}.", FGNameDelete), System.Drawing.Color.DarkRed, status_DefaultBackColor);
                
                util_PopulateFolderGroupList();
            }
        }

        
        private void Menu_Edit_OspreyDataXml_OpenLocation_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();

            psi.FileName= System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe");

            string args = "";

            if (m_CurrentXmlfullpath != null)
            {
                args = "\"" + m_CurrentXmlfullpath + "\"";
                psi.Arguments = String.Format(@"/select," + args);
                psi.WorkingDirectory = m_CurrentXmlfullpath;
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


        private void Menu_File_NewFolderGroup_Textbox_KeyUp(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Enter)
                {
                    /*  save
                     *  clear the list
                     *  clear child windows
                     *  
                     */
                    string FolderGroupName = Menu_File_NewFolderGroup_Textbox.Text;

                    if (!String.IsNullOrEmpty(FolderGroupName))
                    {
                        util_SetCurrentFolderGroupName(FolderGroupName);

                        m_UI_STATE_HasChildren = false;

                        SaveResults savevalue = SaveMain(NodeChangeType.NewGroup, FolderGroupName);

                        Menu_File.HideDropDown();

                        util_PopulateFolderGroupList();

                        Menu_File_NewFolderGroup_Textbox.Text = "";

                        Menu_FolderGroup_ComboBox_0.Text = FolderGroupName;
                    }
                }
        }


        private void Menu_File_NewFolderGroup_Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }

        }


        private void Menu_Edit_Rename_FG_Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress= true;

            }
        }


        private void Menu_Edit_Rename_FG_Textbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newname = Menu_Edit_Rename_FG_Textbox.Text;

                SaveResults returnresults = SaveMain(NodeChangeType.Rename, newname);

                if (returnresults == SaveResults.Saved)
                {
                    util_SetCurrentFolderGroupName(newname); //this.m_CurrentFolderGroup = newname;
                }

                util_PopulateFolderGroupList();

                util_SetControlsPerSelectedXml();

                Menu_Edit.HideDropDown();
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
            // the workflow for creating a new xml data file would be
            // 1.   create the file
            // 2.   open the file.
            // 3.   add a folder group(s) to the xml file
            // 4.   add child node paths. 
            // 5.   save file.
            // this routine executes steps 1, but prepares the ui for step 2. 
            // see Menu_File_OpenDataFile_CboBox_SelectedIndexChanged() for step 2.
//
//
//
//          m_UI_STATE_HasFiles = false;

            if (e.KeyCode == Keys.Enter)
            {
                //ClearAllChildWindows();

                //Menu_File.DropDown.Hide();
                
                try
                {
                    string newfilename  = this.Menu_File_NewDataFile_TextBox.Text;
                
                    string PathTest     = System.IO.Path.Combine(m_XmlFileCollectionPath, newfilename);

                    PathTest = System.IO.Path.ChangeExtension(PathTest, "xml");

                    if (File.Exists(PathTest))
                    { 
                        string note = "The file name, '"+ newfilename +"', already exist.";
                      
                        util_ShowStatusMessage(note, System.Drawing.Color.Black, System.Drawing.Color.Orange);

                        Menu_File_NewDataFile_TextBox.Text = "";
                    }
                    else
                    {
                        FileInfo fi_newfile         = util_CreateDataXmlFile(newfilename, true);  //Creates a new xml data file, add a "Sample" folder group node.
                    
                        this.m_CurrentXmlfullpath   = fi_newfile.FullName;
                    
                        this.m_CurrentXmlFilename   = fi_newfile.Name;
                    
                        this.m_CurrentXml           = util_GetOspreyDataXml(m_CurrentXmlfullpath);

            
                        util_PopulateFileListCboBox(m_XmlFileCollectionPath);                   // count data files,  clear the cbobox, add file list to cbobox.
                    
                        this.Menu_File_OpenDataFile_CboBox.Text = fi_newfile.Name;              // set m_CurrentXml XmlDocument.

                        ClearAllChildWindows();

  
                        m_UI_STATE_HasFiles = true;
                    
                        m_UI_STATE_FolderGroupIsOpen    = true; 
                   
                        m_UI_STATE_HasChildren          = true;

                        m_UI_STATE_HasFolderGroups      = true; 

                        Menu_File_OpenDataFile.Select();
                    
                        m_UI_STATE_FileIsOpen           = true;
                    

                        util_SetControlsPerSelectedXml();  //  disable/enable menu controls, control.text=""
                    }

                    Menu_File.DropDown.Hide();

                }
                catch (Exception ex)
                {
                    util_LogError ("error creating new data file. error is logged.", ex.ToString());
                }
            }
            
        }

                
        private void Menu_File_NewFileExplorer_Click(object sender, EventArgs e)
        {   // This function is called by the main form menu strip's Open Folder icon.
            // are there any entries in the ospreydata.xml
            //  node count is 0, then nothing has been saved before. initial save requires a name and requires a name.

            util_AddChildWindow(null);
          
        }

        private void Menu_FolderGroup_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check to see if the user opts to create a new folder group. 
            int selectedIndex = Menu_FolderGroup_ComboBox_0.SelectedIndex;

            if (selectedIndex == 0 && Menu_FolderGroup_ComboBox_0.Text=="<New Folder Group>")
            {
                // Reveal the dropdown menu items that expose the Menu_FolderGroup_Textbox where and how  a new folder group is created.
                this.Menu_File.ShowDropDown();
                this.Menu_File_New.ShowDropDown();
                this.Menu_File_NewFolderGroup.ShowDropDown();
                this.Menu_File_NewFolderGroup_Textbox.Focus();
            }
            else
            {
                util_LoadSelectedFolderGroup(Menu_FolderGroup_ComboBox_0.Text);
            };
        }
  
        
        private void Menu_File_OpenDataFile_CboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }



        private void Menu_File_OpenDataFile_CboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*  this is the only place to set the m_CurrentXml.
             *  this is the reaction to selecting an xml data file
             *  this is where the <LastViewedFolderTeam/> value is read.
             *  once the file is selected, the folder group combobox (Menu_FolderGroup_ComboBox) is populated
             *  give focus to the that downstream control and expand it to visually queue the user and hint at the next action.
             */
            
            ClearAllChildWindows();

            Menu_File.HideDropDown();
            

            if (!String.IsNullOrEmpty(Menu_File_OpenDataFile_CboBox.Text))
            {
                string testpath = System.IO.Path.Combine(m_XmlFileCollectionPath, Menu_File_OpenDataFile_CboBox.Text);

                if (File.Exists(testpath))                      // load the newly selected file. check if there is a LastViewedFolderTeam node value.
                {
                    m_CurrentXmlFilename        = Menu_File_OpenDataFile_CboBox.Text;

                    m_CurrentXmlfullpath        = testpath;
                                                                                               // these 2 function must be called in this sequence.
                    m_CurrentXml                = util_GetOspreyDataXml(m_CurrentXmlfullpath); // 1.  the m_CurrentXml must be populated

                    string savedFolderGroupName = util_GetLastViewedFolderTeam();              // 2.   before a node can be retrieved the XmlDocument must be defined in step #1.


                    m_FileScopeData = util_GetFileScopeSettings(m_CurrentXml); // for  future implementation
                    
                    if (!String.IsNullOrEmpty(savedFolderGroupName))
                    {
                        util_SetCurrentFolderGroupName(savedFolderGroupName);
                    }

                    util_PopulateFolderGroupList();                                             // this will use the m_CurrentXml that was set above to populate the folder group combobox?
                                                                                                // maybe the best place to do this is when the folder group is selected or automatically invoked.
                    m_UI_STATE_FileIsOpen = true;                                               // !set m_CurrentFolderGroup[m_idxFTeamDisplayName] by reading the current xml.
                                                                                                // set to true after folder group is selected - either by the use or when the folder team combobox is populated.
                    util_SetControlsPerSelectedXml();                                           // this activates the block in util_SetControlsPerSelectedXml();

                    // set the file's config color.
                    string xPathFileColor  = "//Osprey/FileColor";
                    XmlNode NodeFileColor  = m_CurrentXml.SelectSingleNode(xPathFileColor);
                    string xml_color_value = NodeFileColor.InnerText;
                    
                    int color_int = 0;
                    bool is_color_int = Int32.TryParse(xml_color_value, out color_int);

                    if (is_color_int)
                    {
                        menuStrip1.BackColor = System.Drawing.Color.FromArgb(color_int);
                    }
                }
                else
                {
                    string msg01 = String.Format("file not found: {0}. select one from list.", Menu_File_OpenDataFile_CboBox.Text);
                    util_ShowStatusMessage(msg01, System.Drawing.Color.DarkRed, DefaultBackColor);
                }
            }
        }

        
        private void Menu_File_Save_Click_1(object sender, EventArgs e)
        {
            // assume there is something already loaded from the xml configuration file, ospreydata.xml.
            SaveMain(NodeChangeType.Save, m_CurrentFolderGroup[m_idxFTeamNodeName]);
     
            util_SetControlsPerSelectedXml();
        }


        private void Menu_File_SaveAs_Texbox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // first save the newly copied noded to the xml
                // second, save the current node to m_CurrentFolderGroup[m_idxFTeamDisplayName].
                // save the last viewed folder team to the xml. so, there are 2 save actions to the xml in this one function.

                Menu_File.HideDropDown();

                string newFolderGroupName = Menu_File_SaveAs_Texbox1.Text.Trim();

                if (!String.IsNullOrEmpty(newFolderGroupName))
                {
                    SaveMain(NodeChangeType.SaveAs, newFolderGroupName);

                    util_SetCurrentFolderGroupName(newFolderGroupName);

                    SaveResults result = SaveMain(NodeChangeType.LastViewedFTeam, newFolderGroupName);

                    m_UI_STATE_HasFolderGroups = true;

                    Menu_FolderGroup_ComboBox_0.Text = newFolderGroupName;

                    m_UI_STATE_HasFolderGroups = true;

                    m_UI_STATE_FolderGroupIsOpen = true;

                    util_PopulateFolderGroupList();

                    util_SetControlsPerSelectedXml();

                }
                else
                {
                    //log error here and status
                    util_ShowStatusMessage("no folder group was selected. create a folder group if none exists", this.status_forecolor, this.status_backcolor);
                }
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
            frmHelpAbout frmabout = new frmHelpAbout();
            frmabout.MdiParent = this;
            frmabout.Show();
         
        }


        private void Menu_Help_LicenseInfo_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frmlicinfo = new frmLicenseInfo();
            frmlicinfo.MdiParent = this;
            frmlicinfo.Show();
        }



        private void Menu_View_Cascade_Click(object sender, EventArgs e)
        {
            this.Menu_View_Vertical.Checked = false;
            this.Menu_View_Horizontal.Checked = false;
            this.Menu_View_CascadeAll.Checked = true;

            if (this.MdiChildren.Count() > 0)
            {
                this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
            }
        }


        private void Menu_View_ClearAll_Click(object sender, EventArgs e)
        {
            
            if (this.MdiChildren.Count() > 0)
            {
                ClearAllChildWindows();
                m_UI_STATE_HasChildren = false;
                util_SetCurrentFolderGroupName(null);
                util_SetControlsPerSelectedXml();
            }
        }


        private void Menu_View_Horizontal_Click(object sender, EventArgs e)
        {
            this.Menu_View_Vertical.Checked = false;
            this.Menu_View_Horizontal.Checked = true;
            this.Menu_View_CascadeAll.Checked = false;

            if (this.MdiChildren.Count() > 0)
            {
                this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);

                foreach (Form child in this.MdiChildren)
                {
                   child.Height = 290;      // This height solves a UI issue where the right side of the ChildExplorer window does not render correctly.
               }                            // and somehow setting the height to this resolves the issue.
                this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
            }
        }


        private void Menu_View_Vertical_Click(object sender, EventArgs e)
        {
            this.Menu_View_Vertical.Checked = true;
            this.Menu_View_Horizontal.Checked = false;
            this.Menu_View_CascadeAll.Checked = false;

            if (this.MdiChildren.Count() > 0)
            {
                this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
            }
        }


        private void ospreyHelpChmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace, HelpNavigator.TableOfContents);
        }


        public FileScopeData SaveFileScopeSettings()
        {
            // for backwards compatibility, if the node does not exist, create it.

            string xPathLastViewedTeamName = "//Osprey/LastViewedFolderTeam";

            XmlNode NodeLastViewedFTeam = m_CurrentXml.SelectSingleNode(xPathLastViewedTeamName);

                if (null == NodeLastViewedFTeam)
                {
                    // create the node.
                    XmlNode NodeInsertPoint = m_CurrentXml.SelectSingleNode("//Osprey/FolderTeam[1]");

                    NodeLastViewedFTeam = m_CurrentXml.CreateElement("LastViewedFolderTeam");

                    m_CurrentXml.DocumentElement.InsertBefore(NodeLastViewedFTeam, NodeInsertPoint);
            }

            string xPathFileColor = "//Osprey/FileColor";

            XmlNode NodeFileColor = m_CurrentXml.SelectSingleNode(xPathFileColor);

            if (null == NodeFileColor)
            {
                // create the node.
                XmlNode NodeInsertPoint = m_CurrentXml.SelectSingleNode("//Osprey/FolderTeam[1]");

                NodeFileColor = m_CurrentXml.CreateElement("FileColor");

                m_CurrentXml.DocumentElement.InsertBefore(NodeFileColor, NodeInsertPoint);
            }

            NodeLastViewedFTeam.InnerText   = m_CurrentFolderGroup[m_idxFTeamDisplayName];
            NodeFileColor.InnerText         = this.menuStrip1.BackColor.ToArgb().ToString();

            m_CurrentXml.Save(this.m_CurrentXmlfullpath);

            m_FileScopeData.FileColorArgb         = this.menuStrip1.BackColor.ToArgb();
            m_FileScopeData.LastViewedFolderGroup = NodeLastViewedFTeam.InnerText;

            return m_FileScopeData;
        }

        private SaveResults SaveMain(NodeChangeType ChangeType, string GroupName)
        {
            // There are 2 main types of saves. 
            // 1)   saving the last viewed folder group. this occurs every time a new xml data file is selected.  
            // 2)   most of the logic in this function is for changes to folderteam nodes (new folderteam, changes to existing folderteam and the child collection).
            // This is backwards compatible with earlier versions that don't have this node. if the node does not exist, created it, set it, save it. now,it's the file is upgraded.

            if (null != m_CurrentXml && ChangeType == NodeChangeType.LastViewedFTeam)
            {
                string xPathLastViewedTeamName = "//Osprey/LastViewedFolderTeam";

                XmlNode NodeLastViewedFTeam = m_CurrentXml.SelectSingleNode(xPathLastViewedTeamName);

                // for backwards compatibility, if the node does not exist, create it.
                if (null == NodeLastViewedFTeam)
                {
                    // create the node.
                    XmlNode FirstNodeFolderTeam = m_CurrentXml.SelectSingleNode("//Osprey/FolderTeam[1]");

                    NodeLastViewedFTeam = m_CurrentXml.CreateElement("LastViewedFolderTeam");

                    m_CurrentXml.DocumentElement.InsertBefore(NodeLastViewedFTeam, FirstNodeFolderTeam);
                }

                NodeLastViewedFTeam.InnerText = GroupName;
            
                m_CurrentXml.Save(this.m_CurrentXmlfullpath);

                ////// for future implementation ////////
                m_FileScopeData.LastViewedFolderGroup = NodeLastViewedFTeam.InnerText;

                m_CurrentXml.Save(this.m_CurrentXmlfullpath);

                return SaveResults.Saved;
            }

            // Save file color association 
            string xPathFileColor = "//Osprey/FileColor";

            XmlNode NodeFileColor = m_CurrentXml.SelectSingleNode(xPathFileColor);

            if (null == NodeFileColor)
            {
                // create the node.
                XmlNode NodeInsertPoint = m_CurrentXml.SelectSingleNode("//Osprey/FolderTeam[1]");

                NodeFileColor = m_CurrentXml.CreateElement("FileColor");

                m_CurrentXml.DocumentElement.InsertBefore(NodeFileColor, NodeInsertPoint);
            }

            NodeFileColor.InnerText = this.menuStrip1.BackColor.ToArgb().ToString();

            m_FileScopeData.FileColorArgb = this.menuStrip1.BackColor.ToArgb();             ////// for future implementation ////////

            m_CurrentXml.Save(this.m_CurrentXmlfullpath);

            

            #region _ scenario #2: the node change is related an edit of the current folderteam

            GroupName = GroupName.Trim();
           
            SaveResults returnvalue = SaveResults.Initialize;
            
            string          xPathNewTeamName        = String.Format("//Osprey/FolderTeam[@Name='{0}']", GroupName.ToLower().Trim());
            string          xPathCurrentTeamName    = String.Format("//Osprey/FolderTeam[@Name='{0}']", m_CurrentFolderGroup[m_idxFTeamNodeName]);
           
            XmlNodeList     folderteamlist          = m_CurrentXml.SelectNodes(xPathNewTeamName);
            int             nodecount               = folderteamlist.Count;                 // depends on the type of save.

            XmlNode         FolderTeamNode          = null;
            XmlAttribute    attName                 = null;
            XmlAttribute    attDisplayName          = null;
          
      
            bool b_dupfound          = false;
            bool b_createnode        = false;
            bool b_addchildnodes     = false;
            bool b_delchildnodes     = false;
            bool b_rename            = false;
           
            string errmsg01          = string.Empty;

            // the objective here is to identify if the user is doing something wrong, like, is he/she trying to save a a folder team with the same name as an existing node.
            if (nodecount > 1)                                // this indecates an attempt to save a duplicate name. we can't have more than one <folderteam> node with the same name.
            {                                                 // this later entry will simple overwrite the the existing, rather than try to warn the use of the conflict.
                b_dupfound    = true;
                errmsg01      += String.Format("\"{0}\", has {1} duplicate xml nodes.", GroupName,nodecount);
            }
            else if (nodecount==1)
            {
                switch(ChangeType)
                {
                    case NodeChangeType.NewGroup:              // can't create the new node because one with the same name already exists.
                        b_dupfound       = true;
                        errmsg01        += String.Format("\"{0}\", already exists.",GroupName);
                        break;

                    case NodeChangeType.SaveAs:
                        b_dupfound       = true;
                        errmsg01        += String.Format("\"{0}\", already exists.",GroupName);
                        break;

                    case NodeChangeType.Save:                 // essentially, overwrite the child nodes by first deleting them and adding the new ones currently displayed in the parent window.
                        b_delchildnodes  = true;
                        b_addchildnodes  = true;
                        FolderTeamNode   = folderteamlist[0]; // this is the only scenario where we are expecting an existing node to save changes to.
                        break;

                    case NodeChangeType.Rename:               // can't rename it because a same-named node already exist in the xml doc.
                        b_dupfound       = true;
                        errmsg01        += String.Format("\"{0}\", already exists.", GroupName);
                        break;

                }
            }
            else if (nodecount == 0)
            {
              
                switch(ChangeType)
                {
                    case NodeChangeType.NewGroup:
                        b_createnode    = true;
                        FolderTeamNode  = m_CurrentXml.CreateNode(XmlNodeType.Element, "FolderTeam", "");
           
                        break;
                    
                    case NodeChangeType.SaveAs:
                        b_createnode    = true;
                        b_addchildnodes = true;
                        FolderTeamNode  = m_CurrentXml.CreateNode(XmlNodeType.Element, "FolderTeam", "");

                        break;
                    
                    case NodeChangeType.Rename:
                        b_rename        = true;
                        FolderTeamNode  = m_CurrentXml.SelectSingleNode(xPathCurrentTeamName);
                        break;
                }

            }

#endregion _ edit to currently active folderteam

            if (String.IsNullOrEmpty(errmsg01))
            {
                if (b_createnode) // all scenarios include this step. the "if" block is not really necessary.
                {
                    attName = m_CurrentXml.CreateAttribute("Name");
                    attName.Value = GroupName.ToLower().Trim();

                    attDisplayName = m_CurrentXml.CreateAttribute("DisplayName");
                    attDisplayName.Value = GroupName.Trim();

                    FolderTeamNode.Attributes.Append(attName);
                    FolderTeamNode.Attributes.Append(attDisplayName);

                }

                if (b_delchildnodes)
                {
                    FolderTeamNode.InnerXml = "";
                }

                if (b_addchildnodes)
                {
                    foreach (ChildExplorer ChildForm in this.MdiChildren)
                    {
                        string FolderPathText = ChildForm.ChildConfig.uri;

               
                        if (!String.IsNullOrEmpty(FolderPathText) && !String.IsNullOrWhiteSpace(FolderPathText))
                            {
                            XmlNode nChildFileeEplorerUri = m_CurrentXml.CreateNode(XmlNodeType.Element, "ChildExplorer", "");

                            XmlAttribute atColorArgb = m_CurrentXml.CreateAttribute("colorargb");
                            atColorArgb.Value = ChildForm.ChildConfig.ColorArgbInt.ToString();
                            nChildFileeEplorerUri.Attributes.Append(atColorArgb);

                            XmlAttribute atLabel = m_CurrentXml.CreateAttribute("label");
                            atLabel.Value = ChildForm.ChildConfig.label; 
                            nChildFileeEplorerUri.Attributes.Append(atLabel);

                            XmlAttribute atUri = m_CurrentXml.CreateAttribute("uri");
                            atUri.Value = FolderPathText;
                            nChildFileeEplorerUri.Attributes.Append(atUri);

                            XmlAttribute atOrder = m_CurrentXml.CreateAttribute("WindowOrder");
                            atOrder.Value = ChildForm.ChildConfig.WindowOrder.ToString();
                            nChildFileeEplorerUri.Attributes.Append(atOrder);

                            FolderTeamNode.AppendChild(nChildFileeEplorerUri);
                        }
                    }
                }

                if (b_rename)
                {
                    FolderTeamNode.Attributes["Name"].Value = GroupName.ToLower().Trim();
                    FolderTeamNode.Attributes["DisplayName"].Value = GroupName;
                }

                if (b_createnode)
                {
                    XmlNode OspreyNode = m_CurrentXml.SelectSingleNode("//Osprey");

                    OspreyNode.AppendChild(FolderTeamNode);
                }

                m_CurrentXml.Save(this.m_CurrentXmlfullpath);

                string savemessage = String.Format("Saved \"{0}\"",GroupName);
 
                util_ShowStatusMessage(savemessage, this.status_forecolor, this.status_backcolor);
 
                returnvalue = SaveResults.Saved;
               
            }
            else
            {
                util_ShowStatusMessage(errmsg01, System.Drawing.Color.Black, System.Drawing.Color.Orange, errmsg01, System.Drawing.Color.DarkOrange, DefaultBackColor);
 
                returnvalue = SaveResults.Error;
            }

               
            return returnvalue;
            
        }


        private void util_AddChildWindow(string folderpath)
        {
            m_UI_STATE_HasChildren = true;

            Menu_View_ClearAll.Enabled = true;

            ChildExplorerConfig newconfig = new ChildExplorerConfig();
            newconfig.ColorArgbInt = 0;
            newconfig.label = folderpath;
            newconfig.uri = folderpath;
            newconfig.b_USE_DEFAULT_COLOR = true;
            
            ChildExplorer NewFileExplorer = new ChildExplorer(newconfig);

            NewFileExplorer.child_window_count = m_explorer_window_count++;
            
            NewFileExplorer.Width = this.Width / 2;

            NewFileExplorer.Height = this.Height / 2;
    
            NewFileExplorer.MdiParent = this;

            arrayChildExplorer.Add(NewFileExplorer);

            NewFileExplorer.Show();

        }


        private FileInfo util_CreateDataXmlFile(string FileName, bool CreateSampleNode)
        {
            /*   This function creates the initial xml data files and a sample folder group node. 
             *   Upon the initial launch of the application, there will be no xml data files. The Form_Load will determine if there are any xml data files saved to disk. 
             *   If not, it creates the OspreyData.xml file with a sample folder group. This allows the user to start using the app immediately without the need for creating 
             *   an initial xml file and folder group.
             */
            
            if (!FileName.ToLower().EndsWith(".xml"))
            {
                FileName += ".xml";
            }

            string FileFullPath = System.IO.Path.Combine(m_XmlFileCollectionPath, FileName);

            System.Text.StringBuilder sbXml= new System.Text.StringBuilder();

            sbXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            
            sbXml.AppendLine("<Osprey>");
            
           
            if (CreateSampleNode)
            {
                sbXml.AppendLine("<LastViewedFolderTeam>Sample</LastViewedFolderTeam>");
                sbXml.AppendLine("<FolderTeam Name=\"sample\" DisplayName=\"Sample\"></FolderTeam>");
            }
            
            sbXml.AppendLine("</Osprey>");
            
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileFullPath, false, System.Text.Encoding.UTF8);
            
            sw.WriteLine(sbXml.ToString());
            
            sw.Flush();
            
            sw.Close();

            return new FileInfo(FileFullPath);

        }

  
        private XmlDocument util_GetOspreyDataXml(string OspreyDataXmlPath)
        {
            XmlDocument xDocOspreyDataXml = new XmlDocument();

            xDocOspreyDataXml.Load(OspreyDataXmlPath);

            m_UI_STATE_FileIsOpen = true;

            return xDocOspreyDataXml;
        }

        private string util_GetLastViewedFolderTeam()
        {
            string xpath = "//Osprey/LastViewedFolderTeam";      
             
            XmlNode NodeSavedFolderTeam = m_CurrentXml.SelectSingleNode(xpath);

                // for backwards compatibility, if the node deos not exist, create it.
            if (null == NodeSavedFolderTeam)
            {   // create the node.
                XmlNode FolderTeamNode = m_CurrentXml.SelectSingleNode("//Osprey/FolderTeam[1]");

                NodeSavedFolderTeam = m_CurrentXml.CreateElement("LastViewedFolderTeam");

                m_CurrentXml.DocumentElement.InsertBefore(NodeSavedFolderTeam, FolderTeamNode);

                NodeSavedFolderTeam.InnerText = "";

                m_CurrentXml.Save(this.m_CurrentXmlfullpath);
            }

            return NodeSavedFolderTeam.InnerText;
        }

        private FileScopeData util_GetFileScopeSettings(XmlDocument xmlDataFile)
        {
            XmlNode FolderTeamGroup = xmlDataFile.SelectSingleNode("//Osprey/FolderTeam[1]");

            bool Do_Save = false;

            // for backwards compatibility, if the node deos not exist, create it.
            string xpath_LastViewedFolderGroup = "//Osprey/LastViewedFolderTeam";
            XmlNode NodeLastViewedGroup = xmlDataFile.SelectSingleNode(xpath_LastViewedFolderGroup);
               
            // Determine if the last saved viewed group saved to the file is valid. 
                if (null == NodeLastViewedGroup)
                    {   // create the node.

                        Do_Save = true;
                        
                        NodeLastViewedGroup = xmlDataFile.CreateElement("LastViewedFolderTeam");

                        xmlDataFile.DocumentElement.InsertBefore(NodeLastViewedGroup, FolderTeamGroup);

                        NodeLastViewedGroup.InnerText = "";
                    }

            // Determine if the color value saved to the file is valid. 
                string xpath_FileColor = "//Osprey/FileColor";
                XmlNode NodeFileColor = xmlDataFile.SelectSingleNode(xpath_FileColor);
            
                if (null == NodeFileColor)
                {   // create the node.

                    Do_Save = true;
                    
                    NodeFileColor = xmlDataFile.CreateElement("FileColor");

                    xmlDataFile.DocumentElement.InsertBefore(NodeFileColor, FolderTeamGroup);

                    NodeFileColor.InnerText = "";

                }
                // convert xml color value from string to int.
                int int_ColorArgb = 0;

                bool Is_Argb = Int32.TryParse(NodeFileColor.InnerText, out int_ColorArgb);


            // Put the read values into this struct and retun the data.
            FileScopeData fileScopeData         = new FileScopeData();
            fileScopeData.LastViewedFolderGroup = NodeLastViewedGroup.InnerText;
            fileScopeData.FileColorArgb         = int_ColorArgb;
           
            if (Do_Save)
            {
                xmlDataFile.Save(this.m_CurrentXmlfullpath);
            }

            return fileScopeData;
        }

        private void util_ReadScopeSettings(XmlDocument SelectedDataFile)
        {
            m_FileScopeData.FileColorArgb = 0;
            m_FileScopeData.LastViewedFolderGroup = "";
        }
        private void util_LoadChildWindows(string folderteamname)
        {
            // load a group of folders per parameter, "folderteamname".
            // this method is triggered when the user selectsfrom the folder group combobox.
            // read the child nodes and set the ChildExplorer windows, per xml data.

            // every time a node is loaded, ui controls must show what the user selected.
            // change the form's text property to show this.
            // default windows to show tiled vertically.
            status_childload_count = 0;

            string xpath = String.Format("//Osprey/FolderTeam[@Name='{0}']", folderteamname);

            XmlNode xml_selected_foldergroup  = m_CurrentXml.SelectSingleNode(xpath);

            // Only continue if the node has child nodes. otherwise, there are no child windows to load.
            this.status_child_count = xml_selected_foldergroup.SelectNodes("ChildExplorer").Count;
            
            if (xml_selected_foldergroup.SelectNodes("ChildExplorer").Count > 0)
            {
                  
                if (xml_selected_foldergroup != null)
                {
                    // Ensure each child node has a WindowOrder assigned to it. Force "0" if not exists.
                    // Sort the List
                    foreach (XmlNode xml_ChildExplorer in xml_selected_foldergroup.SelectNodes("ChildExplorer"))
                    {
                        XmlAttribute attWindowOrder;

                        attWindowOrder = xml_ChildExplorer.Attributes["WindowOrder"];

                        if (attWindowOrder == null)
                        {
                            attWindowOrder = m_CurrentXml.CreateAttribute("WindowOrder");

                            attWindowOrder.Value = "0";

                            xml_ChildExplorer.Attributes.Append(attWindowOrder);
                        }
                   }


                    var ExplorerChildren = xml_selected_foldergroup.ChildNodes.Cast<XmlNode>().ToList();

                    var orderedExplorerChildren = xml_selected_foldergroup.ChildNodes.Cast<XmlNode>().OrderByDescending(node => Convert.ToInt32(node.Attributes["WindowOrder"].Value)).ToList();

                    if (orderedExplorerChildren.Count == 0)
                    {
                        m_UI_STATE_HasChildren = false;

                        if (this.b_ShowMessages)
                        {
                            string warning01 = "this folder group is empty. add new folder groups.";

                            util_ShowStatusMessage(warning01, System.Drawing.Color.Black, System.Drawing.Color.Orange, warning01, System.Drawing.Color.DarkOrange, DefaultBackColor);
                        }
                    }
                    else
                    {
                        m_UI_STATE_HasChildren = true;

                        if (orderedExplorerChildren != null)
                        {
                            XmlNode[] OrderedExplorerChildXmlNodes = orderedExplorerChildren.ToArray();

                            foreach (XmlNode nChildExplorer in OrderedExplorerChildXmlNodes)
                            {
                                string xml_uri              = nChildExplorer.Attributes["uri"].Value;

                                string xml_label            = nChildExplorer.Attributes["label"].Value;



                                //  Two steps are needed to retrieve the color value from the xml. Otherwise, if a null is evaluated the app will crash.
                                //  1. Does the attribute exist  2. Is the color value a valid integer. 

                                XmlAttribute xml_ColorAttribName = nChildExplorer.Attributes["colorargb"];

                                string xml_color_value_test = null;

                                int    xml_colorargb        = 0;

                                if (xml_ColorAttribName != null)
                                {
                                    xml_color_value_test = nChildExplorer.Attributes["colorargb"].Value;

                                    if (!String.IsNullOrEmpty(xml_color_value_test))
                                    {
                                        int.TryParse(xml_color_value_test, out xml_colorargb);
                                    }

                                }


                                //  Two steps are needed to retrieve the child-window-order-value from the xml. Otherwise, if a null is evaluated the app will crash.
                                //  1. Does the attribute exist  2. Is the value a valid integer. 
                                
                                XmlAttribute xml_orderAttrib    = nChildExplorer.Attributes["WindowOrder"];

                                string xml_order_value_test     = null;

                                int xml_orderInt                = 0;

                                if (xml_orderAttrib != null)
                                {
                                    xml_order_value_test = nChildExplorer.Attributes["WindowOrder"].Value;

                                    if (xml_order_value_test != null)
                                    {
                                        int.TryParse(xml_order_value_test, out xml_orderInt);
                                    }

                                }

                                ChildExplorerConfig childconfig = new ChildExplorerConfig();

                                if (xml_colorargb == childconfig.DefaultColorArgbInt)
                                {
                                    childconfig.ColorArgbInt = childconfig.DefaultColorArgbInt;

                                    childconfig.b_USE_DEFAULT_COLOR = true;
                                }
                                else 
                                {
                                    childconfig.ColorArgbInt = xml_colorargb;
                                
                                    childconfig.b_USE_DEFAULT_COLOR = false;
                                }

                                childconfig.label               = xml_label;

                                childconfig.uri                 = xml_uri;

                                childconfig.WindowOrder         = xml_orderInt;

                                ChildExplorer NewFileExplorer   = new ChildExplorer(childconfig);

                                NewFileExplorer.MdiParent       = this;

                                NewFileExplorer.Width           = (this.Width / 2);

                                NewFileExplorer.Height          = (this.Height / 2);

                                NewFileExplorer.Show();

                                status_childload_count++;
                            }
                        }
                    }

                    

                    Menu_View_Vertical_Click(new object(), new EventArgs());  //this will force the windows to tile
                }
            }
        }


        private void util_LoadSelectedFolderGroup(string SelectedFolderGroupName)
        {
            // presumably, the value for SelectedFolderGroupName results from the user selecting from the Menu_FolderGroup_ComboBox.
            // this implies it existed in the current xml file and was a valid selection from the xml.

            Menu_File.HideDropDown();
            
            ClearAllChildWindows();
            
            util_SetCurrentFolderGroupName(SelectedFolderGroupName); //this method sets the two values in the array, m_CurrentFolderGroup

            if (!String.IsNullOrEmpty(SelectedFolderGroupName.Trim()))
            {
                util_LoadChildWindows(m_CurrentFolderGroup[m_idxFTeamNodeName]);
                
                m_UI_STATE_FolderGroupIsOpen    = true;

                m_UI_STATE_HasFolderGroups      = true;
            }
            else
            {
                m_UI_STATE_FolderGroupIsOpen    = false;

                m_UI_STATE_HasFolderGroups      = false;
            }

            util_SetControlsPerSelectedXml();
        }


        private void util_LogError(string statusmessage, string logmessage)
        {
            util_ShowStatusMessage(statusmessage, System.Drawing.Color.DarkRed, status_DefaultBackColor);

            string  logtext      = System.Environment.NewLine;
                    logtext     += String.Format("-----     {0:yyyy.mm.dd_hhmmss}     -----", System.DateTime.Today) + System.Environment.NewLine;
                    logtext     += statusmessage + System.Environment.NewLine;
                    logtext     += logmessage + System.Environment.NewLine;

            using (StreamWriter sw_log = new StreamWriter(m_LogFilePath,true) ) 
            {
                sw_log.WriteLine(logtext);    
            }
        }


        private void util_PopulateFileListCboBox(string datafolderpath)
        {
            List<string> ListFiles = new List<string>();

            System.IO.DirectoryInfo di_DataFolder = new System.IO.DirectoryInfo(datafolderpath);
            
            System.IO.FileInfo[] fi_files = di_DataFolder.GetFiles("*.xml");

            if (fi_files.Count() > 0)
            {
                var names = from f in fi_files
                            select f.Name;

                Menu_File_OpenDataFile_CboBox.Items.Clear();
                Menu_FolderGroup_ComboBox_0.Items.Clear();
                ListFiles.AddRange(names);

                this.Menu_File_OpenDataFile_CboBox.Items.AddRange(names.ToArray());
             
                m_UI_STATE_HasFiles = true;
            }
            else
            {
                m_UI_STATE_HasFiles = false;
                if (this.b_ShowMessages)
                {
                    string warning01 = "No data files exist. create them.";

                    util_ShowStatusMessage(warning01, System.Drawing.Color.Black, System.Drawing.Color.Orange, warning01, System.Drawing.Color.DarkOrange, DefaultBackColor);
                }
            }

            
        }


        private void util_PopulateFolderGroupList()
        {

            // m_CurrentXml is loaded. this this routine populates the folder group combobox per m_CurrentXml. 
            // the call is a reaction from selecting an xml data file event from the file combobox list. 

            XmlNodeList XmlFolderTeamNodes = m_CurrentXml.SelectNodes("//Osprey/FolderTeam");

            if (XmlFolderTeamNodes.Count == 0)
            {// - no nodes:   notify use the xml has no group nodes.    
                m_UI_STATE_HasFolderGroups = false;
                
                string warning01 = String.Format("The data file, [ {0} ], does not contain any folder groups. You must add folder groups.", m_CurrentXmlFilename);
                util_ShowStatusMessage(warning01, System.Drawing.Color.Black, System.Drawing.Color.Orange, warning01, System.Drawing.Color.DarkOrange, DefaultBackColor);
            }
            else
            {// - yes nodes:  add folder nodes to the folder group combobox by 1. create a generic list. add the entire list range to the combobox items array.
                
                m_UI_STATE_HasFolderGroups = true;

                var FolderGroupNames = XmlFolderTeamNodes.Cast<XmlNode>().Select(node => node.Attributes["DisplayName"].Value).ToList();

                FolderGroupNames.Sort();

                FolderGroupNames.Insert(0, "<New Folder Group>");

                this.Menu_FolderGroup_ComboBox_0.Text = "";
                
                this.Menu_FolderGroup_ComboBox_0.Items.Clear();

                this.Menu_FolderGroup_ComboBox_0.Items.AddRange(FolderGroupNames.ToArray());
            }

        }

        private void util_SetControlsPerSelectedXml()
        {

            // this routine is mainly responsible for setting the ui control properties for enabled=true or false
            // as the user operates the various controls, flags are set to capture what was changed. this method 
            // is called after these operations to evaluated the flags enabled/disable ui controls as needed.
            // this is also how the main parent form's text/title is caluated and set.
            
            this.Menu_File_New.Enabled                      = true;
            
            this.Menu_FolderGroup_ComboBox_0.Enabled        = false;
            this.Menu_File_OpenDataFile.Enabled             = false;
            this.Menu_File_Save.Enabled                     = false;
            this.Menu_File_SaveAs.Enabled                   = false;
            this.Menu_Edit_DeleteFolderGroup.Enabled        = false;
            this.Menu_View_ClearAll.Enabled                 = false;
            this.Menu_AddChildExplorer.Enabled              = false;
            this.Menu_Save.Enabled                          = false;

            if (m_UI_STATE_HasFiles)
            {
                this.Menu_File_OpenDataFile.Enabled = true;
            }


            if (m_UI_STATE_FileIsOpen)
            {
                this.Menu_File_NewDataFile_TextBox.Text     = "";

                this.Menu_Save.Enabled                      = true;
                
                this.Text = String.Format("Osprey  \u2502  {0}", m_CurrentXmlFilename);
            }


            if (m_UI_STATE_FolderGroupIsOpen)
            {
                this.Menu_AddChildExplorer.Enabled = true;

                this.Menu_FolderGroup_ComboBox_0.Text       = m_CurrentFolderGroup[m_idxFTeamDisplayName];

                
                this.Menu_FolderGroup_ComboBox_0.Enabled    = false;
            
                
                if (m_UI_STATE_HasFolderGroups)
                {
                    this.Text = String.Format("Osprey  \u2502  {0}  \u2502  {1}", m_CurrentXmlFilename, m_CurrentFolderGroup[m_idxFTeamDisplayName]);
                }
                else
                {
                    this.Text = String.Format("Osprey  \u2502  {0}", m_CurrentXmlFilename);
                }
            }   
         

            if (m_UI_STATE_HasFolderGroups)
            {
                this.Menu_Edit_DeleteFolderGroup.Enabled    = true;
                this.Menu_Edit_RenameFolderGroup.Enabled    = true;
                this.Menu_File_Save.Enabled                 = true;
                this.Menu_File_SaveAs.Enabled               = true;
                this.Menu_FolderGroup_ComboBox_0.Enabled    = true;
                this.Menu_Save.Enabled                      = true;
            }

            if (m_UI_STATE_HasChildren)
            {
                this.Menu_File_Save.Enabled                = true;
                this.Menu_File_SaveAs.Enabled              = true;
                this.Menu_Edit_RenameFolderGroup.Enabled   = true;
                this.Menu_View_ClearAll.Enabled            = true;
            }
        }


        private void util_SetCurrentFolderGroupName(string displayname)
        {
            this.m_CurrentFolderGroup[m_idxFTeamDisplayName] = displayname;
            this.m_CurrentFolderGroup[m_idxFTeamNodeName]    = String.IsNullOrEmpty(displayname) ? null : displayname.ToLower().Trim();
        }

        
        private void util_ShowStatusMessage(string statusmessage, System.Drawing.Color TextColor, System.Drawing.Color bgcolor, string statusmessage2, System.Drawing.Color TextColor2, System.Drawing.Color bgcolor2)
        {
            status_message = statusmessage;
            status_backcolor = bgcolor;
            status_forecolor = TextColor;

            status_message2 = statusmessage2;
            status_backcolor2 = bgcolor2;
            status_forecolor2 = TextColor2;

            Timer_Message1.Enabled = true;

        }


        private void util_ShowStatusMessage(string StatusMessage, System.Drawing.Color TextColor, System.Drawing.Color bgcolor)
        {
            status_message = StatusMessage;
            status_backcolor = bgcolor;
            status_forecolor = TextColor;

            Timer_Message1.Enabled = true;

        }


        private void Timer01_Tick(object sender, EventArgs e)
        {
            int remainder = 0;
            int even      = 0;
            string strmsg = String.Format("{0}",status_TimerCount1);


            if (status_TimerCount1 < 7)         // as the count down progresses the remainder will ether be zero or some positive remander value.
            {                                   // so, this wil toggle from even-odd-even-odd....

                Math.DivRem(status_TimerCount1, 2, out remainder);

                if (remainder == even) //even
                {
                    StatusLabel.Text            = this.status_message;
                    StatusLabel.ForeColor       = this.status_forecolor;
                    StatusLabel.BackColor       = this.status_backcolor;
                }
                else //odd
                {
                    StatusLabel.Text           = status_message2;
                    StatusLabel.ForeColor      = this.status_forecolor2;
                    StatusLabel.BackColor      = this.status_backcolor2;
                }
                StatusLabel.GetCurrentParent().Refresh();

            }
            else
            {   // when the timer reaches 10 iterations, reset these values to the default settings.  

                Timer_Message1.Enabled = false;

                StatusLabel.ForeColor    = this.status_defaultforecolor;
                StatusLabel.BackColor    = this.status_DefaultBackColor;

                this.status_message = "";
                this.status_forecolor = this.status_defaultforecolor;
                this.status_backcolor = this.status_DefaultBackColor;

                this.status_message2 = "";
                this.status_forecolor2 = this.status_defaultforecolor;
                this.status_backcolor2 = this.status_DefaultBackColor;

                status_TimerCount1 = 0;

                this.Timer_Message2.Enabled = true ;
            }

            status_TimerCount1++;

        }


        private void Timer_Message2_Tick(object sender, EventArgs e)
        {
            Timer_Message2.Enabled  = false;
            StatusLabel.Text        = "";
        }


        private void AdHocTest_Click(object sender, EventArgs e)
        {
            string teststring = "q3";
            int finalValue = -1;

            bool b_IsValidInt = int.TryParse(teststring, out finalValue);

            string x = "";
            
            //statuslabel1.Text = "aaaaaaaaaaaaa";
            //System.threading.thread.sleep(1000);
            //statuslabel1.Text = "";
            //System.threading.thread.sleep(1000);
        }


   
        private void Menu_Edit_Config_Click_1(object sender, EventArgs e)
        {
            Menu_Edit_Config.Enabled = false;

            FormConfig frmConfig = new FormConfig();

            frmConfig.MdiParent = this;

            frmConfig.FileColorArgb = menuStrip1.BackColor.ToArgb();

            frmConfig.Show();



        }

        private void Menu_View_AlwaysTop_Click(object sender, EventArgs e)
        {
            if (Menu_View_AlwaysTop.Checked)
            {
                Menu_View_AlwaysTop.Checked = false;

                this.TopMost = false;
            }
            else
            {
                Menu_View_AlwaysTop.Checked = true;
                
                this.TopMost = true;
            }
        }
        bool b_loadingChildMsgFlag = false;
        private void Timer_LoadChildWindows_Tick(object sender, EventArgs e)
        {
            //if (b_loadingChildMsgFlag == true)
            //{
            //    // display a message
            //    b_loadingChildMsgFlag = false;

            //    Timer_LoadChildWindows.Interval = 1000;
            //    string OnMsg = String.Format("Loading window {0} of {1}", this.status_childload_count, this.status_childload_count);
            //    util_ShowStatusMessage(OnMsg,System.Drawing.Color.Black, System.Drawing.Color.Orange,)
            //}
            //else
            //{ 
            //    //display a blank
            //    b_loadingChildMsgFlag = true;

            //    Timer_LoadChildWindows.Interval = 500;
            //    string offMsg = "";
            //   // util_ShowStatusMessage("")
            //}
        }

      

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Menu_AutoResize.Checked == true)
            { 
                if (Menu_View_Horizontal.Checked)
                {
                    this.Menu_View_Horizontal_Click(new object(), new EventArgs());
                }

                if (Menu_View_Vertical.Checked)
                {
                    this.Menu_View_Vertical_Click(new object(), new EventArgs());
                }

                if (Menu_View_CascadeAll.Checked)
                {
                    this.Menu_View_Cascade_Click(new object(), new EventArgs());
                }

            }


            this.Resize_Child_TS_Textbox();
        }

        private void btn_AcceptButton_Click(object sender, EventArgs e)
        {
            // don't remove this.
        }

        private void Menu_Refresh_Click(object sender, EventArgs e)
        {
            foreach (ChildExplorer child in this.MdiChildren)
            {
                ChildExplorer childExplorer= (ChildExplorer) child;

                childExplorer.RefreshChildWindows();
            }
        }
        private void Resize_Child_TS_Textbox()
        {
            foreach (ChildExplorer child in this.MdiChildren)
            {
                ChildExplorer childExplorer = (ChildExplorer)child;
                
                childExplorer.ResizeTS_Textbox();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            int curW = this.Width;
            int curH = this.Height;
            int curT = this.Top;
            int curL = this.Left;


            if (curW < 200 && curH < 200)
            {
                this.Width = 500;
                this.Height = 500;

                this.Top = 200;
                this.Left = 200;
            }

            

        }

       

        private void Menu_AutoResize_Click(object sender, EventArgs e)
        {
            if (this.Menu_AutoResize.Checked)
            {
                this.Menu_AutoResize.Image = Properties.Resources.AutoFillOn;
                this.Menu_AutoResize.ToolTipText = m_AutoResizeToolTip + " (on)";
                this.Menu_AutoResize.Checked = true;
                Form1_ResizeEnd(new object(), new EventArgs());
            }
            else
            {
                this.Menu_AutoResize.Image = Properties.Resources.AutoFillOff;
                this.Menu_AutoResize.ToolTipText = m_AutoResizeToolTip + " (off)";
                this.Menu_AutoResize.Checked = false;
            }
        }

       
    }



    public struct ChildExplorerConfig
    {
        private int     m_ColorArgbInt;
        private string  m_label;
        private string  m_uri;
        private int     m_WindowOrder;
        private bool    m_Use_Default_Color;


        public int ColorArgbInt
        {
            get { return m_ColorArgbInt; }
            set { m_ColorArgbInt = value; }
        }

        public int DefaultColorArgbInt
        {
            // This must be read-only because it is the DEFAULT which must be treated as a CONSTANT.
            get { return System.Drawing.Color.LightGray.ToArgb(); }
        }
        public System.Drawing.Color DefaultColor
        {
            // This must be read-only because it is the DEFAULT which must be treated as a CONSTANT.
            get { return System.Drawing.Color.LightGray; }
        }

        public bool b_USE_DEFAULT_COLOR
        {
            get { return m_Use_Default_Color; }
            set { m_Use_Default_Color = value; }
        }


        public string label
        {
            get { return m_label; }
            set { m_label = value; }
        }

        public string uri
        {
            get { return m_uri; }
            set { m_uri = value; }
        }

        public int WindowOrder
        {
            get { return m_WindowOrder; }
            set { m_WindowOrder = value; }
        }
    }

}

