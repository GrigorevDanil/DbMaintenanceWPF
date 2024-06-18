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
    public class EmployeeVM(EmployeeM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {

        #region Свойства

        readonly EmployeeM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<Employee> Employees => Model.PublicListEmployees;
        public IEnumerable<Department> Departments => Model.PublicListDepartments;
        public IEnumerable<Post> Posts => Model.PublicListPosts;

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

        public void UpdateList() => OnPropertyChanged(nameof(Employees));

        #region AddCommand - Добавление сотрудника

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var employee = new Employee();
            if (UserDialog.Edit(employee, "Добавление сотрудника"))
            {
                Model.Create(employee);
                OnPropertyChanged(nameof(Employees));
            }
        }

        #endregion

        #region EditCommand - Редактирование сотрудника

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Employee;

        private void OnEditCommandExecuted(object p)
        {
            Employee employee = (Employee)p;
            if (UserDialog.Edit(employee, "Редактирование сотрудника"))
            {
                Model.Update(employee);
                OnPropertyChanged(nameof(Employees));
            }
        }

        #endregion

        #region RemoveCommand - Удаление сотрудника

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Employee;

        private void OnRemoveCommandExecuted(object p)
        {
            Employee employee = (Employee)p;
            if (UserDialog.ShowConfirm("Удаление сотрудника", "Удалить выбранного сотрудника?"))
            {
                Model.Remove(employee);
                OnPropertyChanged(nameof(Employees));
            }
        }

        #endregion

        #region MultiplyRemoveCommand - Множественное удаление сотрудников

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление сотрудников", "Удалить выбранных сотрудников?"))
            {
                foreach (Employee employee in Employees) if (employee.IsSelected) Model.Remove(employee);
                OnPropertyChanged(nameof(Employees));
            }
        }

        #endregion
        #endregion
    }
}
