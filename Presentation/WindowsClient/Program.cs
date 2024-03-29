﻿using AutoUpdaterDotNET;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;
using NLog.Targets;
using Rems.Application;
using Rems.Application.Common.Interfaces;
using Rems.Infrastructure;

using System;
using System.Reflection;
using System.Windows.Forms;
using WindowsClient.Forms;

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
            // Setup error logging
            ConfigureLogging();
            var log = LogManager.GetLogger("");
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                log.Info("Checking for updates.");
                log.Info($"Current version: {version}");
                AutoUpdater.InstalledVersion = version;
                AutoUpdater.Mandatory = true;
                AutoUpdater.Start("https://raw.githubusercontent.com/QAAFI/REMS2020/master/version.xml");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Load services
                log.Info("Loading services.");
                IServiceProvider ServiceProvider = new ServiceCollection()
                    .AddSingleton<IFileManager, FileManager>(s => FileManager.Instance)
                    .AddPersistence()
                    .AddApplication()
                    .BuildServiceProvider();

                // Prepare the client
                log.Info("Loading fonts.");
                FontManager.LoadFonts();
                
                Application.Run(new REMSClient(ServiceProvider));
            }
            catch (Exception error)
            {
                log.Error(error);
                AlertBox.Show("A fatal error has occurred. Check the error log for details", AlertType.Error);
            }
        }

        static void ConfigureLogging()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget
            {
                AutoFlush = true,
                FileName = "ErrorLog.txt",
                DeleteOldFileOnStartup = true                
            };

            config.AddRuleForAllLevels(target);

            LogManager.Configuration = config;
        }
    }
}
