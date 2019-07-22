using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            LoadDatabase();
        }

        static void LoadDatabase()
        {
            var local = Environment.SpecialFolder.LocalApplicationData;
            string path = Environment.GetFolderPath(local) + "\\ADAMS\\ADAMS.db";

            string connection = $"Data Source={path};";

            //if (!File.Exists(path)) connection += "New=True;";

            ADAMSContext ADAMS = new ADAMSContext(connection);

            var fields = typeof(ADAMSContext).GetFields();

            ADAMS.Database.EnsureCreated();
            ADAMS.Database.CloseConnection();
            
        }
    }
}
