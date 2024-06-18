using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.ItemModels;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    public class DepartmentVM(DepartmentM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {
        #region Свойства

        readonly DepartmentM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<Department> Departments => Model.PublicListDepartments;

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

        public void UpdateList() => OnPropertyChanged(nameof(Departments));

        #region AddCommand - Добавление отдела

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var department = new Department();
            if (UserDialog.Edit(department, "Добавление отдела"))
            {
                Model.Create(department);
                OnPropertyChanged(nameof(Departments));
            }
        }

        #endregion

        #region EditCommand - Редактирование отдела

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Department;

        private void OnEditCommandExecuted(object p)
        {
            Department department = (Department)p;
            if (UserDialog.Edit(department, "Редактирование отдела"))
            {
                Model.Update(department);
                OnPropertyChanged(nameof(Departments));
            }
        }

        #endregion

        #region RemoveCommand - Удаление отдела

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Department;

        private void OnRemoveCommandExecuted(object p)
        {
            Department department = (Department)p;
            if (UserDialog.ShowConfirm("Удаление отдела", "Удалить выбранный отдел?"))
            {
                Model.Remove(department);
                OnPropertyChanged(nameof(Departments));
            }
        }

        #endregion

        #region MultiplyRemoveCommand - Множественное удаление отделов

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление отделов", "Удалить выбранные отделы?"))
            {
                foreach (Department department in Departments) if (department.IsSelected) Model.Remove(department);
                OnPropertyChanged(nameof(Departments));
            }
        }

        #endregion
        #endregion
    }
}
