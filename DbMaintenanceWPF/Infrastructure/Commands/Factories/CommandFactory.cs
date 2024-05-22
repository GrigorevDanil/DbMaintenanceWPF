using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using System.Windows.Input;

namespace DbMaintenanceWPF.Infrastructure.Commands.Factories
{
    internal class CommandFactory : ICommandFactory
    {
        public ICommand CreateCloseApplicationCommand() => new CloseApplicationCommand();

        public ICommand CreateRestartApplicationCommand() => new RestartApplicationCommand();
    }
}
