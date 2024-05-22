using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DbMaintenanceWPF.Infrastructure.Commands
{
    internal class RestartApplicationCommand : Base.Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            ProcessStartInfo restart = new ProcessStartInfo(Process.GetCurrentProcess().MainModule.FileName);
            Process.Start(restart);
            Application.Current.Shutdown();
        }
    }
}
