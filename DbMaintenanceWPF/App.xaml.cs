using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using System;
using System.Windows;
using System.IO;
using DbMaintenanceWPF.Service;
using DbMaintenanceWPF.ViewModel;
using DbMaintenanceWPF.Models;
using System.Linq;

namespace DbMaintenanceWPF
{
    public partial class App
    {
        public static bool IsDesignMode { get; private set; } = true;
        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        public static Window ActiveWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window CurrentWindow => FocusedWindow ?? ActiveWindow ?? Current.MainWindow;

        private static IHost __Host;

        public static IHost Host => __Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();



        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
               .RegisterServices()
               .RegisterViewModels()
               .RegisterModels();

        public static string CurrentDirectory => IsDesignMode
           ? Path.GetDirectoryName(GetSourceCodePath())
           : Environment.CurrentDirectory;

        private static string GetSourceCodePath([CallerFilePath] string Path = null) => Path;


    }
}
