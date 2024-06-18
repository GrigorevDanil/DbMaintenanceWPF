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
    public class PurchaseVM(PurchaseM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {
        #region Свойства

        readonly PurchaseM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<Purchase> Purchases => Model.PublicListPurchases;
        public IEnumerable<Provider> Providers => Model.PublicListProviders;
        public IEnumerable<Product> Products => Model.PublicListProducts;

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

        public void UpdateList() => OnPropertyChanged(nameof(Purchases));

        #region AddCommand - Добавление поставки

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var purchase = new Purchase();
            if (UserDialog.Edit(purchase, "Добавление поставки"))
            {
                Model.Create(purchase);
                OnPropertyChanged(nameof(Purchases));
            }
        }

        #endregion

        #region EditCommand - Редактирование поставки

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Purchase;

        private void OnEditCommandExecuted(object p)
        {
            Purchase purchase = (Purchase)p;
            if (UserDialog.Edit(purchase, "Редактирование поставки"))
            {
                Model.Update(purchase);
                OnPropertyChanged(nameof(Purchases));
            }
        }

        #endregion

        #region RemoveCommand - Удаление поставки

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Purchase;

        private void OnRemoveCommandExecuted(object p)
        {
            Purchase purchase = (Purchase)p;
            if (UserDialog.ShowConfirm("Удаление поставки", "Удалить выбранную поставку?"))
            {
                Model.Remove(purchase);
                OnPropertyChanged(nameof(Purchases));
            }
        }

        #endregion

        #region MultiplyRemoveCommand - Множественное удаление поставок

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление поставок", "Удалить выбранные поставки?"))
            {
                foreach (Purchase purchase in Purchases) if (purchase.IsSelected) Model.Remove(purchase);
                OnPropertyChanged(nameof(Purchases));
            }
        }

        #endregion
        #endregion
    }
}
