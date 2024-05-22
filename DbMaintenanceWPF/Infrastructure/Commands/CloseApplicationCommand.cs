using System.Windows;

namespace DbMaintenanceWPF.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Base.Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
