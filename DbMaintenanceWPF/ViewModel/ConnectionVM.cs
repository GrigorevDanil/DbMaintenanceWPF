
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
    class ConnectionVM(ConnectionM model, Database database, ICommandFactory commandFactory) : ViewModelBase
    {
        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;
        private readonly Dictionary<string, object> _Values = new();

        readonly ConnectionM Model = model;
        readonly Database Database = database;
        readonly ICommandFactory CommandFactory = commandFactory;

        public string TextServer { get => GetValue(Database.stringConnection.Server); set => SetValue(value); }

        public string TextPort { get => GetValue(Database.stringConnection.Port.ToString()); set => SetValue(value); }

        public string TextDB { get => GetValue(Database.stringConnection.Database); set => SetValue(value); }

        public string TextUser { get => GetValue(Database.stringConnection.UserID); set => SetValue(value); }

        public string TextPassword { get => GetValue(Database.stringConnection.Password); set => SetValue(value); }

        #endregion

        #region Команды


        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => true;
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
            CommandFactory.CreateRestartApplicationCommand().Execute(null); ;
        }

        #endregion

        #region CancelCommand - Отменить изменения

        private ICommand _CancelCommand;

        public ICommand CancelCommand => _CancelCommand
            ??= new RelayCommand(OnCancelCommandExecuted, CanCancelCommandExecute);

        private bool CanCancelCommandExecute(object p) => true;

        private void OnCancelCommandExecuted(object p)
        {
            CommandFactory.CreateCloseApplicationCommand().Execute(null); ;
        }

        #endregion

        protected virtual bool SetValue(object value, [CallerMemberName] string Property = null)
        {
            if (_Values.TryGetValue(Property!, out var old_value) && Equals(value, old_value))
                return false;
            _Values[Property] = value;
            OnPropertyChanged(Property);
            return true;
        }
        protected virtual T GetValue<T>(T Default, [CallerMemberName] string Property = null)
        {
            if (_Values.TryGetValue(Property!, out var value))
                return (T)value;
            return Default;
        }

        #endregion
    }
}
