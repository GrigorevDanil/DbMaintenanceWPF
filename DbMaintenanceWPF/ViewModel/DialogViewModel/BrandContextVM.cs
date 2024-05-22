using DbMaintenanceWPF.Infrastructure.Commands.Factories;
using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    class BrandContextVM : ViewModelBase
    {
        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        string textBrand;
        public string TextBrand { get => textBrand; set { Set(ref textBrand, value); CanCommitCommandExecute(null);}
    }
        #endregion

        #region Команды

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => TextBrand != "" && TextBrand != null;
        private void OnCommitCommandExecuted(object p)
        {
            if (CanCancelCommandExecute(p))  Complete?.Invoke(this, true);
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
