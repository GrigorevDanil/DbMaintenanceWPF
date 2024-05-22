using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Service.Interface;
using System.IO;
using System.Windows.Input;

namespace DbMaintenanceWPF.Service.Handlers
{
    class DatabaseNotFoundErrorHandler(IUserDialogService userDialog, IBackupManagerDatabase backupManager, ICommandFactory commandFactory) : IHandlerDatabase
    {
        readonly IUserDialogService UserDialog = userDialog;
        readonly IBackupManagerDatabase BackupManager = backupManager;
        readonly ICommandFactory CommandFactory = commandFactory;

        public void ShowError()
        {
            if (UserDialog.ShowConfirm("Отсутствует база данных!", "База данных не найдена. Создать базу данных по умолчанию?"))
            {
                BackupManager.SetBackup("DefaultBackup.sql", Directory.GetCurrentDirectory());
                CommandFactory.CreateRestartApplicationCommand().Execute(null);
            }
        }
    }
}
