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
    public partial class ChildOptionsForm : Form
    {
        ChildExplorerConfig m_ChildExplorerConfig;
        public ChildOptionsForm()
        {
            InitializeComponent();
        }

        public ChildOptionsForm(ChildExplorerConfig config)
        {

        }

        public ChildExplorerConfig ChildOptions
        {
            get { return m_ChildExplorerConfig; }
            
        }
    }
}
