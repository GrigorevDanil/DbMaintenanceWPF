using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.Service.Handlers
{
    class UnableToConnectToHostErrorHandler(IUserDialogService userDialog,ConnectionM connectionM,Database database, ICommandFactory commandFactory) : IHandlerDatabase
    {
        readonly IUserDialogService UserDialog = userDialog;
        readonly ConnectionM ConnectionModel = connectionM;
        readonly Database Database = database;
        readonly ICommandFactory CommandFactory = commandFactory;

        public void ShowError()
        {
            if (UserDialog.ShowConfirm("Отсутствует соединение с сервером MySql!", "Перейти к настройкам подключения с базой данных?")) UserDialog.ShowSettingConnection(ConnectionModel, Database, CommandFactory);
            else CommandFactory.CreateCloseApplicationCommand().Execute(null);
        }
    }
}
