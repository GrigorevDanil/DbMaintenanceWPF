using DbMaintenanceWPF.Infrastructure.Commands.Base;
using System.Windows;

namespace DbMaintenanceWPF.Infrastructure.Commands
{
    class MinimiziWindowCommand : Command
    {
        public override bool CanExecute(object parameter) => parameter is Window;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            var window = (Window)parameter;
            window.WindowState = WindowState.Minimized;
        }
    }
}
