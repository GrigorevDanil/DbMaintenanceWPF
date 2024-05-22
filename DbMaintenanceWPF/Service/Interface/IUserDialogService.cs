using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;

namespace DbMaintenanceWPF.Service.Interface
{
    interface IUserDialogService
    {
        bool Edit(object item, string titleWindow, object viewModel = null);
        void ShowWarning(string title, string text);
        void ShowQuestion(string title, string text);
        bool ShowConfirm(string title, string text, bool exclamation = false);
        bool ShowSettingConnection(ConnectionM model, Database database, ICommandFactory commandFactory);
        bool ShowAuthentication(LoginM model, IUserDialogService dialogService);
        bool ShowMain(object user);

    }
}
