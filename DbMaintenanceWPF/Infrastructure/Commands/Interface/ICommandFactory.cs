
using System.Windows.Input;

namespace DbMaintenanceWPF.Infrastructure.Commands.Interface
{
    public interface ICommandFactory
    {
        ICommand CreateRestartApplicationCommand();
        ICommand CreateCloseApplicationCommand();
        ICommand CreateCloseWindowCommand();
    }
}
