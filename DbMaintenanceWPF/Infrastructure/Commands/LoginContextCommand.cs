using DbMaintenanceWPF.Infrastructure.Commands.Base;
using DbMaintenanceWPF.View.Windows;
using System;
using System.Windows;

namespace DbMaintenanceWPF.Infrastructure.Commands
{
    class LoginContextCommand : Command
    {
        private LoginContext _Window;

        public override bool CanExecute(object parameter) => _Window == null;

        public override void Execute(object parameter)
        {
            var window = new LoginContext
            {
                Owner = Application.Current.MainWindow
            };
            _Window = window;
            window.Closed += OnWindowClosed;

            window.ShowDialog();
        }

        private void OnWindowClosed(object Sender, EventArgs E)
        {
            ((Window)Sender).Closed -= OnWindowClosed;
            _Window = null;
        }
    }
}
