
using System.Windows.Input;

namespace DbMaintenanceWPF.Infrastructure.Commands.Interface
{
    interface ICommandFactory
    {
        ICommand CreateRestartApplicationCommand();
        ICommand CreateCloseApplicationCommand();
    }
}
