using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

//
//Test - Poncki
//
namespace Ciastko
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.dfg
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
