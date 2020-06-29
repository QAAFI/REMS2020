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

            ConfigureServices();

            Application.Run(new REMSClient(ServiceProvider));            
        }

        public static IServiceProvider ServiceProvider { get; set; }

        static void ConfigureServices()
        {
            ServiceProvider = new ServiceCollection()
                .AddPersistence()
                .AddApplication()               
                .BuildServiceProvider();            
        }
    }
}
