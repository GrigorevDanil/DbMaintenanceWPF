using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class EmployeeVM : Base.ViewModelBase
    {

        #region Свойства

        readonly EmployeeM Model;
        readonly IUserDialogService UserDialog;
        public IEnumerable<Employee> Employees => Model.PublicListEmployees;
        public IEnumerable<Department> Departments => Model.PublicListDepartments;
        public IEnumerable<Post> Posts => Model.PublicListPosts;

        #endregion

        #region Команды

        #region AddCommand - Добавление сотрудника

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var employee = new Employee();
            if (!UserDialog.Edit(employee, "Добавление сотрудника", this)) OnPropertyChanged(nameof(Employees));
        }

        #endregion

        #region EditCommand - Редактирование сотрудника

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Employee;

        private void OnEditCommandExecuted(object p)
        {
            Employee employee = (Employee)p;
            if (!UserDialog.Edit(employee, "Редактирование сотрудника", this))
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

        #endregion

        public EmployeeVM(EmployeeM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }
    }
}
