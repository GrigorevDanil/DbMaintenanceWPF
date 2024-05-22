using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class GiveVM : Base.ViewModelBase
    {

        #region Свойства

        readonly GiveM Model;
        readonly IUserDialogService UserDialog;
        public IEnumerable<Give> Gives => Model.PublicListGives;
        public IEnumerable<Employee> Employees => Model.PublicListEmployees;


        bool flagEmployee;
        public bool FlagEmployee { get => flagEmployee; set { Set(ref flagEmployee, value); SelectedEmployee = null; } }

        Employee selectedEmployee;
        public Employee SelectedEmployee { get => selectedEmployee; set => Set(ref selectedEmployee, value); }
        #endregion

        #region Команды

        #region AddCommand - Добавление выдачи

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => p is Give;

        private void OnAddCommandExecuted(object p)
        {
            var give = (Give)p;
            if (!UserDialog.Edit(give, "Добавление выдачи")) OnPropertyChanged(nameof(Gives));
        }

        #endregion

        #region EditCommand - Редактирование выдачи

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Give;

        private void OnEditCommandExecuted(object p)
        {
            Give give = (Give)p;
            if (!UserDialog.Edit(give, "Редактирование выдачи"))
            {
                Model.Update(give);
                OnPropertyChanged(nameof(Gives));
            }
        }

        #endregion

        #region RemoveCommand - Удаление выдачи

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Give;

        private void OnRemoveCommandExecuted(object p)
        {
            Give give = (Give)p;
            if (UserDialog.ShowConfirm("Удаление выдачи", "Удалить выбранную выдачу?"))
            {
                Model.Remove(give);
                OnPropertyChanged(nameof(Gives));
            }
        }

        #endregion

        #endregion

        public GiveVM(GiveM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }
    }
}
