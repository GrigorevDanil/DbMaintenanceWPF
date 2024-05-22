using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class BrandVM(BrandM model, IUserDialogService userDialog) : Base.ViewModelBase
    { 
        #region Свойства

        readonly BrandM Model = model;
        readonly IUserDialogService UserDialog = userDialog;

        public ReadOnlyObservableCollection<Brand> Brands => Model.PublicListBrands;


        #endregion

        #region Команды

        #region AddCommand - Добавление бренда

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var brand = new Brand();
            if (UserDialog.Edit(brand,"Добавление бренда"))
            {
                Model.Create(brand);
                OnPropertyChanged(nameof(Brands));
            }
        }

        #endregion

        #region EditCommand - Редактирование бренда

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Brand;

        private void OnEditCommandExecuted(object p)
        {
            Brand brand = (Brand)p;
            if (UserDialog.Edit(brand, "Редактирование бренда"))
            {
                Model.Update(brand);
                OnPropertyChanged(nameof(Brands));
            }
        }

        #endregion

        #region RemoveCommand - Удаление бренда

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Brand;

        private void OnRemoveCommandExecuted(object p)
        {
            Brand brand = (Brand)p;
            if (UserDialog.ShowConfirm("Удаление бренда", "Удалить выбранный бренд?"))
            {
                Model.Remove(brand);
                OnPropertyChanged(nameof(Brands));
            }
        }

        #endregion

        #endregion

    }
}
