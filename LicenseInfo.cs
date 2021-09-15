using System;
using System.Windows.Forms;

namespace CodeLessTraveled.Osprey
{
    public partial class frmLicenseInfo : Form
    {
        public frmLicenseInfo()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
