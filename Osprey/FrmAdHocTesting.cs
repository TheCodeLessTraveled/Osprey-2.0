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
    public partial class FrmAdHocTesting : Form
    {
        public FrmAdHocTesting()
        {
            InitializeComponent();
        }

        private void FrmAdHocTesting_Load(object sender, EventArgs e)
        {
            Test1();
        }
        private void Test1()
        {

            this.textBox1.Text = "x";

        
        }
    }
}
