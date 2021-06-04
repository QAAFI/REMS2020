using Microsoft.Extensions.DependencyInjection;

using Rems.Application;
using Rems.Application.Common.Interfaces;
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
                .AddSingleton<IFileManager, FileManager>(s => FileManager.Instance)
                .AddPersistence()
                .AddApplication()                
                .BuildServiceProvider();

            Application.Run(new REMSClient(ServiceProvider));
        }
    }
}
