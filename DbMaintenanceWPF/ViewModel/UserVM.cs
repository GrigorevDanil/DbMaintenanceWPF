using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class UserVM : Base.ViewModelBase
    {

        #region Свойства

        readonly UserM Model;
        readonly IUserDialogService UserDialog;
        public IEnumerable<User> Users => Model.PublicListUsers;


        bool flagRole;
        public bool FlagRole { get => flagRole; set { Set(ref flagRole, value); SelectedRole = null; } }

        string selectedRole;
        public string SelectedRole { get => selectedRole; set => Set(ref selectedRole, value); }


        #endregion

        #region Команды

        #region AddCommand - Добавление пользователя

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => p is User;

        private void OnAddCommandExecuted(object p)
        {
            var unit = (Unit)p;
            if (!UserDialog.Edit(unit, "Добавление пользователя")) OnPropertyChanged(nameof(Users));
        }

        #endregion

        #region EditCommand - Редактирование пользователя

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is User;

        private void OnEditCommandExecuted(object p)
        {
            User user = (User)p;
            if (!UserDialog.Edit(user, "Редактирование пользователя"))
            {
                Model.Update(user);
                OnPropertyChanged(nameof(Users));
            }
        }

        #endregion

        #region RemoveCommand - Удаление пользователя

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is User;

        private void OnRemoveCommandExecuted(object p)
        {
            User user = (User)p;
            if (UserDialog.ShowConfirm("Удаление пользователя", "Удалить выбранного пользователя?"))
            {
                Model.Remove(user);
                OnPropertyChanged(nameof(user));
            }
        }

        #endregion

        #endregion

        public UserVM(UserM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }

    }
}
