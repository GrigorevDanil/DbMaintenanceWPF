using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    public class GiveContextVM : ViewModelBase
    {
        public GiveContextVM()
        {
            Employees = App.Host.Services.GetRequiredService<ICreaterEntity<Employee>>().GetList();
            SelectedEmployee = Employees.First();
            DateGive = DateTime.Now;
        }

        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        IEnumerable<Employee> employees;
        public IEnumerable<Employee> Employees { get => employees; set => Set(ref employees, value); }

        Employee selectedEmployee;
        public Employee SelectedEmployee { get => selectedEmployee; set => Set(ref selectedEmployee, value);  }

        DateTime? dateGive;
        public DateTime? DateGive { get => dateGive; set => Set(ref dateGive, value);  }

        bool flagDate;
        public bool FlagDate  { get => flagDate; set =>Set(ref flagDate, value); }

        #endregion

        #region Команды

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => true;

        private void OnCommitCommandExecuted(object p) => Complete?.Invoke(this, true);

        #endregion

        #region CancelCommand - Отменить изменения

        private ICommand _CancelCommand;

        public ICommand CancelCommand => _CancelCommand
            ??= new RelayCommand(OnCancelCommandExecuted, CanCancelCommandExecute);

        private bool CanCancelCommandExecute(object p) => true;

        private void OnCancelCommandExecuted(object p) => Complete?.Invoke(this, false);

        #endregion

        #endregion
    }
}
