using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.ViewModel.Base;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    public class UserMessageWindowVM : ViewModelBase
    {
        #region Свойства
        public event EventHandler<EventArgs<bool>> Complete;

        string messageTitle;
        public string MessageTitle { get => messageTitle; set => Set(ref messageTitle, value); }

        string messageText;
        public string MessageText { get => messageText; set => Set(ref messageText, value); }

        ImageSource imageMessage;
        public ImageSource ImageMessage { get => imageMessage; set => Set(ref imageMessage, value); }

        Visibility visibleButtonOk;
        public Visibility VisibleButtonOk { get => visibleButtonOk; set => Set(ref visibleButtonOk, value); }

        #endregion

        #region Команды

        #region CommitCommand - Принять

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => true;
        private void OnCommitCommandExecuted(object p) => Complete?.Invoke(this, true);

        #endregion

        #region CancelCommand - Отменить

        private ICommand _CancelCommand;

        public ICommand CancelCommand => _CancelCommand
            ??= new RelayCommand(OnCancelCommandExecuted, CanCancelCommandExecute);

        private bool CanCancelCommandExecute(object p) => true;

        private void OnCancelCommandExecuted(object p) => Complete?.Invoke(this, false);

        #endregion

        #endregion
    }
}
