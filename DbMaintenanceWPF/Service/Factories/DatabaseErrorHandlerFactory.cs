using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Service.Handlers;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Interface.Factories;
using System.Windows.Input;

namespace DbMaintenanceWPF.Service.Factories
{
    class DatabaseErrorHandlerFactory(IUserDialogService userDialog, IBackupManagerDatabase backupManager, ICommandFactory commandFactory,ConnectionM connectionM, Database database) : IDatabaseErrorHandlerFactory
    {
        readonly IUserDialogService UserDialog = userDialog;
        readonly IBackupManagerDatabase BackupManager = backupManager;
        readonly ICommandFactory CommandFactory = commandFactory;
        readonly ConnectionM ConnectionModel = connectionM;
        readonly Database Database = database;

        public IHandlerDatabase CreateAccessDeniedErrorHandler() => new AccessDeniedErrorHandler(UserDialog);
        public IHandlerDatabase CreateDatabaseNotFoundErrorHandler() => new DatabaseNotFoundErrorHandler(UserDialog,BackupManager, CommandFactory);
        public IHandlerDatabase CreateUnableToConnectToHostErrorHandler() => new UnableToConnectToHostErrorHandler(UserDialog, ConnectionModel, Database, CommandFactory);
        public IHandlerDatabase CreateUnknownErrorHandler(string expMessage) => new UnknownErrorHandler(expMessage);
    }
}
