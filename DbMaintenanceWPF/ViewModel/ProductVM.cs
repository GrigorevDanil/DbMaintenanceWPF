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
    public class ProductVM(ProductM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {

        #region Свойства

        readonly ProductM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<Product> Products => Model.PublicListProducts;
        public IEnumerable<Category> Categories => Model.PublicListCategories;
        public IEnumerable<Brand> Brands => Model.PublicListBrands;
        public IEnumerable<Unit> Units => Model.PublicListUnits;

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

        public void UpdateList() => OnPropertyChanged(nameof(Products));

        #region AddCommand - Добавление товара

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var product = new Product();
            if (UserDialog.Edit(product, "Добавление товара"))
            {
                Model.Create(product);
                OnPropertyChanged(nameof(Products));
            }
        }

        #endregion

        #region EditCommand - Редактирование товара

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Product;

        private void OnEditCommandExecuted(object p)
        {
            Product product = (Product)p;
            if (UserDialog.Edit(product, "Редактирование товара"))
            {
                Model.Update(product);
                OnPropertyChanged(nameof(Products));
            }
        }

        #endregion

        #region RemoveCommand - Удаление товара

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Product;

        private void OnRemoveCommandExecuted(object p)
        {
            Product product = (Product)p;
            if (UserDialog.ShowConfirm("Удаление товара", "Удалить выбранный товар?"))
            {
                Model.Remove(product);
                OnPropertyChanged(nameof(Products));
            }
        }

        #endregion

        #region MultiplyRemoveCommand - Множественное удаление товаров

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление товаров", "Удалить выбранные товары?"))
            {
                foreach (Product product in Products) if (product.IsSelected) Model.Remove(product);
                OnPropertyChanged(nameof(Products));
            }
        }

        #endregion
        #endregion


    }
}
