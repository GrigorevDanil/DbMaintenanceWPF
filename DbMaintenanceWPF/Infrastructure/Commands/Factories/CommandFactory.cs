using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using System.Windows.Input;

namespace DbMaintenanceWPF.Infrastructure.Commands.Factories
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand CreateCloseApplicationCommand() => new CloseApplicationCommand();

        public ICommand CreateCloseWindowCommand() => new CloseWindowCommand();

        public ICommand CreateRestartApplicationCommand() => new RestartApplicationCommand();
    }
}
