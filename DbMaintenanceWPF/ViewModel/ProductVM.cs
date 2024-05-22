using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class ProductVM : Base.ViewModelBase
    {

        #region Свойства

        readonly ProductM Model;
        readonly IUserDialogService UserDialog;
        public IEnumerable<Product> Products => Model.PublicListProducts;
        public IEnumerable<Category> Categories => Model.PublicListCategories;
        public IEnumerable<Brand> Brands => Model.PublicListBrands;
        public IEnumerable<Unit> Units => Model.PublicListUnits;



        bool flagCategory;
        public bool FlagCategory { get => flagCategory; set { Set(ref flagCategory, value); SelectedCategory = null; } }

        bool flagBrand;
        public bool FlagBrand { get => flagBrand; set { Set(ref flagBrand, value); SelectedBrand = null; } }

        bool flagUnit;
        public bool FlagUnit { get => flagUnit; set { Set(ref flagUnit, value); SelectedUnit = null;  } }

        Category selectedCategory;
        public Category SelectedCategory { get => selectedCategory; set => Set(ref selectedCategory, value); }

        Brand selectedBrand;
        public Brand SelectedBrand { get => selectedBrand; set => Set(ref selectedBrand, value); }

        Unit selectedUnit;
        public Unit SelectedUnit { get => selectedUnit; set => Set(ref selectedUnit, value);  }

        #endregion

        #region Команды

        #region AddCommand - Добавление товара

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => p is Product;

        private void OnAddCommandExecuted(object p)
        {
            var product = (Product)p;
            if (!UserDialog.Edit(product, "Добавление товара")) OnPropertyChanged(nameof(Products));
        }

        #endregion

        #region EditCommand - Редактирование товара

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Product;

        private void OnEditCommandExecuted(object p)
        {
            Product product = (Product)p;
            if (!UserDialog.Edit(product, "Редактирование товара"))
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


        #endregion

        public ProductVM(ProductM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }

        
    }
}
