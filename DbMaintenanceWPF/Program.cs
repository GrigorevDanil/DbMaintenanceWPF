﻿using DbMaintenanceWPF.Models;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Hosting;

namespace DbMaintenanceWPF
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] Args) =>
            Host.CreateDefaultBuilder(Args)
               .UseContentRoot(App.CurrentDirectory)
               .ConfigureAppConfiguration((host, cfg) => cfg
                   .SetBasePath(App.CurrentDirectory)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
               .ConfigureServices(App.ConfigureServices);
    }
}
