using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    public class ProviderContextVM : ViewModelBase
    {
        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;


        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        string textCompany;
        public string TextCompany { get => textCompany; set { Set(ref textCompany, value); CanCommitCommandExecute(null); } }

        string textContactName;
        public string TextContactName { get => textContactName; set { Set(ref textContactName, value); CanCommitCommandExecute(null); } }

        string textAddress;
        public string TextAddress { get => textAddress; set { Set(ref textAddress, value); CanCommitCommandExecute(null); } }

        string textNumPhone;
        public string TextNumPhone { get => textNumPhone; set { Set(ref textNumPhone, value); CanCommitCommandExecute(null); } }

        string textEmail;
        public string TextEmail { get => textEmail; set { Set(ref textEmail, value); CanCommitCommandExecute(null); } }

        bool flagCompany;
        public bool FlagCompany
        {
            get => flagCompany;
            set
            {
                Set(ref flagCompany, value);
                CanCommitCommandExecute(null);
                if (!flagCompany) TextCompany = "";
            }
        }

        bool flagEmail;
        public bool FlagEmail
        {
            get => flagEmail;
            set
            {
                Set(ref flagEmail, value);
                CanCommitCommandExecute(null);
                if (!flagEmail) TextCompany = "";
            }
        }


        #endregion

        #region Команды

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) =>
            !string.IsNullOrEmpty(TextContactName) &&
            !string.IsNullOrEmpty(TextAddress) &&
             !string.IsNullOrEmpty(TextNumPhone) &&
             Regex.IsMatch(TextNumPhone, @"^\+7 \(\d{3}\) \d{3}-\d{4}$") &&
            (!FlagCompany || !string.IsNullOrEmpty(TextCompany)) &&
            (!FlagEmail || !string.IsNullOrEmpty(TextEmail));

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
