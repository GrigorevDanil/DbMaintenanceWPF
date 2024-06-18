using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    public class PostContextVM : ViewModelBase
    {
        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        string textPost;
        public string TextPost { get => textPost; set { Set(ref textPost, value); CanCommitCommandExecute(null); } }
        #endregion

        #region Команды

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => !string.IsNullOrEmpty(TextPost);
        private void OnCommitCommandExecuted(object p) => Complete?.Invoke(this, true);

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
