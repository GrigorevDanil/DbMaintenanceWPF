using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class CategoryVM : Base.ViewModelBase
    {
        #region Свойства

        readonly CategoryM Model;
        readonly IUserDialogService UserDialog;
        public ReadOnlyObservableCollection<Category> Categories => Model.PublicListCategories;

        #endregion

        #region Команды

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

        #endregion

        public CategoryVM(CategoryM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }
    }
}
