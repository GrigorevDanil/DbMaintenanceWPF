using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class PurchaseVM : Base.ViewModelBase
    {
        #region Свойства

        readonly PurchaseM Model;
        readonly IUserDialogService UserDialog;
        public IEnumerable<Purchase> Purchases => Model.PublicListPurchases;
        public IEnumerable<Provider> Providers => Model.PublicListProviders;
        public IEnumerable<Product> Products => Model.PublicListProducts;


        bool flagProvider;
        public bool FlagProvider { get => flagProvider; set { Set(ref flagProvider, value); SelectedProvider = null; } }

        bool flagProduct;
        public bool FlagProduct { get => flagProduct; set { Set(ref flagProduct, value); SelectedProduct = null; } }

        Provider selectedProvider;
        public Provider SelectedProvider { get => selectedProvider; set => Set(ref selectedProvider, value);   }

        Product selectedProduct;
        public Product SelectedProduct { get => selectedProduct; set => Set(ref selectedProduct, value); }

        #endregion

        #region Команды

        #region AddCommand - Добавление поставки

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => p is Purchase;

        private void OnAddCommandExecuted(object p)
        {
            var purchase = (Purchase)p;
            if (!UserDialog.Edit(purchase, "Добавление поставки")) OnPropertyChanged(nameof(Purchases));
        }

        #endregion

        #region EditCommand - Редактирование поставки

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Purchase;

        private void OnEditCommandExecuted(object p)
        {
            Purchase purchase = (Purchase)p;
            if (!UserDialog.Edit(purchase, "Редактирование поставки"))
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

        #endregion

        public PurchaseVM(PurchaseM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }
    }
}
