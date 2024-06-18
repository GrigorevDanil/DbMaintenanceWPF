using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.ItemModels;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service;
using DbMaintenanceWPF.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    public class UserVM(UserM model, IUserDialogService userDialog, IStorageViewModel storageViewModel, ICommandFactory commandFactory) : Base.ViewModelBase
    {

        #region Свойства

        readonly UserM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        readonly ICommandFactory CommandFactory = commandFactory;
        readonly IStorageViewModel StorageViewModel = storageViewModel;
        object CurrentUser = (storageViewModel.GetViewModel(nameof(MainVM)) as MainVM).CurrentUser;

        public IEnumerable<User> Users => Model.PublicListUsers;

        public IEnumerable<string> Roles => new List<string>()
        {
            "Администратор",
            "Заведующий",
            "Пользователь"
        };


        #endregion

        #region Команды

        public void UpdateList() => OnPropertyChanged(nameof(Users));

        #region AddCommand - Добавление пользователя

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var user = new User();
            if (UserDialog.Edit(user, "Добавление пользователя"))
            {
                Model.Create(user);
                OnPropertyChanged(nameof(Users));
            }
        }

        #endregion

        #region EditCommand - Редактирование пользователя

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is User;

        private void OnEditCommandExecuted(object p)
        {
            User user = (User)p;
            if (UserDialog.Edit(user, "Редактирование пользователя"))
            {
                if (CurrentUser is User curUser)
                {
                    if (curUser.Id == user.Id) (StorageViewModel.GetViewModel(nameof(MainVM)) as MainVM).CurrentUser = user;
                }
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
                if (CurrentUser is User curUser)
                {
                    if (curUser.Id == user.Id) CommandFactory.CreateRestartApplicationCommand().Execute(null);
                }
                OnPropertyChanged(nameof(Users));
            }
        }

        #endregion

        #region MultiplyRemoveCommand - Множественное удаление пользователей

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление пользователей", "Удалить выбранных пользователей?"))
            {
                foreach (User user in Users) if (user.IsSelected) Model.Remove(user);
                OnPropertyChanged(nameof(Users));
            }
        }

        #endregion
        #endregion

    }
}
