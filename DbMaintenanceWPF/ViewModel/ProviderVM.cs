using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.ItemModels;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    public class ProviderVM(ProviderM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {
        #region Свойства

        readonly ProviderM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<Provider> Providers => Model.PublicListProviders;

        public Visibility VisibleComponent
        {
            get
            {
                var user = (storageViewModel.GetViewModel(nameof(MainVM)) as MainVM)?.CurrentUser as User;
                return user?.Role == "Пользователь" ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        #endregion

        #region Команды

        public void UpdateList() => OnPropertyChanged(nameof(Providers));

        #region AddCommand - Добавление поставщика

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var provider = new Provider();
            if (UserDialog.Edit(provider, "Добавление поставщика"))
            {
                Model.Create(provider);
                OnPropertyChanged(nameof(Providers));
            }
        }

        #endregion

        #region EditCommand - Редактирование поставщика

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Provider;

        private void OnEditCommandExecuted(object p)
        {
            Provider provider = (Provider)p;
            if (UserDialog.Edit(provider, "Редактирование поставщика"))
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

        #region MultiplyRemoveCommand - Множественное удаление поставщиков

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление поставщиков", "Удалить выбранных поставщиков?"))
            {
                foreach (Provider provider in Providers) if (provider.IsSelected) Model.Remove(provider);
                OnPropertyChanged(nameof(Providers));
            }
        }

        #endregion
        #endregion

    }
}
