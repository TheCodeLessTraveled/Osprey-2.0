using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeLessTraveled.Osprey
{
    public partial class frmHelpAbout : Form
    {
        public frmHelpAbout()
        {
            InitializeComponent();

            System.Text.StringBuilder sb = new StringBuilder();

            Form Parent = null;
    
            foreach ( Form testForm in Application.OpenForms)
            {
                if (testForm.IsMdiContainer == true)
                {
                    Parent = testForm;
                    break;
                }
            }

            Parent = Application.OpenForms[Parent.Name];

            this.StartPosition = FormStartPosition.Manual;

            int ParW = Parent.Width;
            int ParH = Parent.Height;

            int ChW = this.Width;
            int ChH = this.Height;


            this.Left = (ParW - ChW)/2;
            this.Top = (ParH - ChH)/2;
            
            //sb.AppendLine(Parent.Name + Environment.NewLine);
            //sb.AppendLine("Parent Width" + Parent.Width.ToString() + Environment.NewLine);
            //sb.AppendLine("Parent Height" + Parent.Height.ToString() + Environment.NewLine);

            //sb.AppendLine("Child Width" + this.Width.ToString() + Environment.NewLine);
            //sb.AppendLine("Child  Height" + this.Height.ToString() + Environment.NewLine);
            
            //sb.AppendLine("x = " + this.Location.X + Environment.NewLine);
            //sb.AppendLine("y = " + this.Location.Y + Environment.NewLine);

           // MessageBox.Show(sb.ToString());

            


            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
