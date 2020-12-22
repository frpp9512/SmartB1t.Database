using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartB1t.Database.ClassGeneratorUtil.Forms;

namespace SmartB1t.Database.ClassGeneratorUtil
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GlobalData.GlobalProject = new SmartB1tGenerationClasses.SmartB1tCSProject("");
            Application.Run(new frmMain());
        }
    }
}
