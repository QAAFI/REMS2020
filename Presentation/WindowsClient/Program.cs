using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Rems.Application;
using Rems.Persistence;

using System;
using System.Windows.Forms;

namespace WindowsClient
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

            IServiceProvider ServiceProvider = new ServiceCollection()
                .AddPersistence()
                .AddApplication()                
                .BuildServiceProvider();

            Application.Run(new REMSClient(ServiceProvider));
        }
    }
}
