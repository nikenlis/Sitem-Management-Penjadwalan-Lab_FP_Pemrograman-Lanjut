using LaporLab.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaporLab
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

            FrmLogin frmLogin = new FrmLogin();

            if (frmLogin.ShowDialog() == DialogResult.OK) // jika user dan password benar
                Application.Run(new DashboardLapor()); // jalankan form utama
            else
                Application.Exit();

        
        }
    }
}
