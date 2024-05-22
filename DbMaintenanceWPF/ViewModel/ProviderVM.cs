using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class ProviderVM : Base.ViewModelBase
    {
        #region Свойства

        readonly ProviderM Model;
        readonly IUserDialogService UserDialog;
        public IEnumerable<Provider> Providers => Model.PublicListProviders;

        #endregion

        #region Команды

        #region AddCommand - Добавление поставщика

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => p is Provider;

        private void OnAddCommandExecuted(object p)
        {
            var provider = (Provider)p;
            if (!UserDialog.Edit(provider, "Добавление поставщика")) OnPropertyChanged(nameof(Providers));
        }

        #endregion

        #region EditCommand - Редактирование поставщика

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Provider;

        private void OnEditCommandExecuted(object p)
        {
            Provider provider = (Provider)p;
            if (!UserDialog.Edit(provider, "Редактирование поставщика"))
            {
                Model.Update(provider);
                OnPropertyChanged(nameof(Providers));
            }
        }

        #endregion

        #region RemoveCommand - Удаление поставщика

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Provider;

        private void OnRemoveCommandExecuted(object p)
        {
            Provider provider = (Provider)p;
            if (UserDialog.ShowConfirm("Удаление поставщика", "Удалить выбранного поставщика?"))
            {
                Model.Remove(provider);
                OnPropertyChanged(nameof(Providers));
            }
        }

        #endregion

        #endregion

        public ProviderVM(ProviderM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }

    }
}
