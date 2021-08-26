using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CodeLessTraveled.Osprey
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// CL 20210119
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
   
            Application.Run(new Form1());

        }

   
    }
}
