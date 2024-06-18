using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Infrastructure.Commands.Factories;
using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.ItemModels;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DbMaintenanceWPF.ViewModel
{
    public class AccountVM : ViewModelBase
    {
        public AccountVM(IStorageViewModel storageViewModel, IUserDialogService dialogService, ICommandFactory commandFactory, UserM model)
        {
            StorageViewModel = storageViewModel;
            DialogService = dialogService;
            CommandFactory = commandFactory;
            Model = model;
            CurrentUser = (StorageViewModel.GetViewModel(nameof(MainVM)) as MainVM).CurrentUser;
        }


        #region Свойства

        readonly UserM Model;
        readonly IUserDialogService DialogService;
        readonly IStorageViewModel StorageViewModel;
        readonly ICommandFactory CommandFactory;

        object currentUser;
        public object CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                OnUserChanged();
            }
        }

        ImageSource imageUser;
        public ImageSource ImageUser { get => imageUser; set => Set(ref imageUser, value); }

        string textUsername;
        public string TextUsername { get => textUsername; set => Set(ref textUsername, value); }

        string textRole;
        public string TextRole { get => textRole; set => Set(ref textRole, value); }

        string textTypeUser;
        public string TextTypeUser { get => textTypeUser; set => Set(ref textTypeUser, value); }

        #endregion

        #region Команды

        #region OnUserChanged -  Событие изменения пользователя
        private void OnUserChanged()
        {
            switch (currentUser)
            {
                case User user:
                    {
                        ImageUser = user.Image;
                        TextUsername = user.Surname + " " + user.Name;
                        TextTypeUser = "Программный пользователь";
                        TextRole = user.Role;
                    }
                    break;
                case UserServer userServer:
                    {
                        BitmapImage bitmap;
                        bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.UriSource = new Uri("ImageEmployee.png", UriKind.Relative);
                        bitmap.EndInit();
                        ImageUser = bitmap;

                        TextUsername = userServer.Login;
                        TextTypeUser = "Серверный пользователь";

                    }
                    break;
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
            if (DialogService.Edit(user, "Редактирование пользователя"))
            {
                (StorageViewModel.GetViewModel(nameof(MainVM)) as MainVM).CurrentUser = user;
                Model.Update(user);
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
            if (DialogService.ShowConfirm("Удаление пользователя", "Удалить текущего пользователя? Учтите что удаление текущего пользователя приведет к перезапуску программы"))
            {
                Model.Remove(user);
                CommandFactory.CreateRestartApplicationCommand().Execute(null);
            }
        }

        #endregion

        #endregion
    }
}
