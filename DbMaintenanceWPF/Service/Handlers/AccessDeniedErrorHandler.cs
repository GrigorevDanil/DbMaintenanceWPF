using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Service.Interface;
using System.IO;

namespace DbMaintenanceWPF.Service.Handlers
{
    class AccessDeniedErrorHandler(IUserDialogService userDialog) : IHandlerDatabase
    {
        readonly IUserDialogService UserDialog = userDialog;

        public void ShowError() => UserDialog.ShowWarning("В доступе отказано!", "У вас недостаточно прав для выполнения действия");
    }
}
