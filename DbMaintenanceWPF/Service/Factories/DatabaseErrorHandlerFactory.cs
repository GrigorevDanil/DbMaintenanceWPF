using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Service.Handlers;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Interface.Factories;
using System.Windows.Input;

namespace DbMaintenanceWPF.Service.Factories
{
    class DatabaseErrorHandlerFactory(IUserDialogService userDialog, IBackupManagerDatabase backupManager, ICommandFactory commandFactory) : IDatabaseErrorHandlerFactory
    {
        readonly IUserDialogService UserDialog = userDialog;
        readonly IBackupManagerDatabase BackupManager = backupManager;
        readonly ICommandFactory CommandFactory = commandFactory;

        public IHandlerDatabase CreateAccessDeniedErrorHandler() => new AccessDeniedErrorHandler(UserDialog);
        public IHandlerDatabase CreateDatabaseNotFoundErrorHandler() => new DatabaseNotFoundErrorHandler(UserDialog,BackupManager, CommandFactory);
        public IHandlerDatabase CreateUnableToConnectToHostErrorHandler() => new UnableToConnectToHostErrorHandler(UserDialog, CommandFactory);
        public IHandlerDatabase CreateUnknownErrorHandler(string expMessage) => new UnknownErrorHandler(expMessage);
    }
}
