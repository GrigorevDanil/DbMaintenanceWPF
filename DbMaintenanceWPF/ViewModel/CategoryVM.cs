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
    public class CategoryVM(CategoryM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {
        #region Свойства

        readonly CategoryM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<Category> Categories => Model.PublicListCategories;

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
        public void UpdateList() => OnPropertyChanged(nameof(Categories));

        #region AddCommand - Добавление категории

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var category = new Category();
            if (UserDialog.Edit(category, "Добавление категории"))
            {
                Model.Create(category);
                OnPropertyChanged(nameof(Categories));
            }
        }

        #endregion

        #region EditCommand - Редактирование категории

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Category;

        private void OnEditCommandExecuted(object p)
        {
            Category category = (Category)p;
            if (UserDialog.Edit(category, "Редактирование категории"))
            {
                Model.Update(category);
                OnPropertyChanged(nameof(Categories));
            }
        }

        #endregion

        #region RemoveCommand - Удаление категории

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Category;

        private void OnRemoveCommandExecuted(object p)
        {
            Category category = (Category)p;
            if (UserDialog.ShowConfirm("Удаление категории","Удалить выбранную категори?"))
            {
                Model.Remove(category);
                OnPropertyChanged(nameof(Categories));
            }
        }

        #endregion

        #region MultiplyRemoveCommand - Множественное удаление категорий

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление категорий", "Удалить выбранные категории?"))
            {
                foreach (Category category in Categories) if (category.IsSelected) Model.Remove(category);
                OnPropertyChanged(nameof(Categories));
            }
        }

        #endregion
        #endregion
    }
}
