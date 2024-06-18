
using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    public class ConnectionVM : ViewModelBase
    {
        public ConnectionVM(ConnectionM model, Database database, ICommandFactory commandFactory)
        {
            Model = model;
            Database = database;
            CommandFactory = commandFactory;

            TextServer = Database.stringConnection.Server;
            TextPort = Database.stringConnection.Port.ToString();
            TextDB = Database.stringConnection.Database;
            TextUser = Database.stringConnection.UserID;
            TextPassword = Database.stringConnection.Password;
        }

        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        readonly ConnectionM Model;
        readonly Database Database;
        readonly ICommandFactory CommandFactory;

        string textServer;
        public string TextServer { get => textServer; set => Set(ref textServer, value); }

        string textPort;
        public string TextPort { get => textPort; set => Set(ref textPort, value); }

        string textDB;
        public string TextDB { get => textDB; set => Set(ref textDB, value); }

        string textUser;
        public string TextUser { get => textUser; set => Set(ref textUser,value); }

        string textPassword;
        public string TextPassword { get => textPassword; set => Set(ref textPassword, value); }

        #endregion

        #region Команды


        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => 
            !string.IsNullOrEmpty(TextServer) &&
            !string.IsNullOrEmpty(TextPort) &&
            !string.IsNullOrEmpty(TextDB) &&
            !string.IsNullOrEmpty(TextUser);
        private void OnCommitCommandExecuted(object p)
        {
            Model.SaveNewConnection(TextServer, TextPort, TextDB, TextUser, TextPassword);
            CommandFactory.CreateRestartApplicationCommand().Execute(null); 
            Complete?.Invoke(this, true);
        }

        #endregion

        #region ResetCommand - Команда сброса соединение на по умолчанию

        private ICommand resetCommand;
        public ICommand ResetCommand => resetCommand ??= new RelayCommand(OnResetCommandExecuted, CanResetCommandExecute);
        private static bool CanResetCommandExecute(object p) => true;

        private void OnResetCommandExecuted(object p)
        {
            Model.ResetConnectionToDefault();
            Model.ResetDatabaseToDefault();
            CommandFactory.CreateRestartApplicationCommand().Execute(null); ;
        }

        #endregion

        #region CancelCommand - Отменить изменения

        private ICommand _CancelCommand;

        public ICommand CancelCommand => _CancelCommand
            ??= new RelayCommand(OnCancelCommandExecuted, CanCancelCommandExecute);

        private bool CanCancelCommandExecute(object p) => true;

        private void OnCancelCommandExecuted(object p) => Complete?.Invoke(this, false);

        #endregion

        #endregion
    }
}
