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
    class UnableToConnectToHostErrorHandler(IUserDialogService userDialog, ICommandFactory commandFactory) : IHandlerDatabase
    {
        readonly IUserDialogService UserDialog = userDialog;
        readonly ICommandFactory CommandFactory = commandFactory;

        public void ShowError()
        {
            if (UserDialog.ShowConfirm("Отсутствует соединение с сервером MySql!", "Перейти к настройкам подключения с базой данных?")) UserDialog.ShowSettingConnection();
            else CommandFactory.CreateCloseApplicationCommand().Execute(null);
        }
    }
}
