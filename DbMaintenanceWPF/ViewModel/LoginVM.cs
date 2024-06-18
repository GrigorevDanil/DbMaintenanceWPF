using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Infrastructure.Commands;
using System.Windows.Input;
using System.Linq;
using System;

namespace DbMaintenanceWPF.ViewModel
{
    public class LoginVM : Base.ViewModelBase
    {
        #region Свойства

        readonly LoginM Model;
        readonly IUserDialogService DialogService;
        public IEnumerable<UserServer> UserServers => Model.PublicListUserServers;


        string selectedHost;
        public string SelectedHost { get => selectedHost; set => Set(ref selectedHost, value); }

        bool flagServer;
        public bool FlagServer { get => flagServer; set => Set(ref flagServer, value); }

        string textError;
        public string TextError { get => textError; set => Set(ref textError, value); }

        Visibility visEror;
        public Visibility VisEror { get => visEror; set => Set(ref visEror, value); }

        string currentLogin;
        public string CurrentLogin { get => currentLogin; set => Set(ref currentLogin, value); }

        string currentPassword;
        public string CurrentPassword { get => currentPassword; set => Set(ref currentPassword, value); }

        int selectedIndex;
        public int SelectedIndex{ get => selectedIndex; set => Set(ref selectedIndex, value); }


        #endregion

        #region Команды

        #region ShowEror - Вывод ошибки
        void ShowEror(string textEror)
        {
            TextError = textEror;
            VisEror = Visibility.Visible;
            Task.Delay(800).ContinueWith(t => VisEror = Visibility.Hidden);
        }

        #endregion

        #region AuthenticationCommand - Аутентификация

        private ICommand authenticationCommand;
        public ICommand AuthenticationCommand => authenticationCommand ??= new RelayCommand(OnAuthenticationCommandExecuted, CanAuthenticationCommandExecute);
        private static bool CanAuthenticationCommandExecute(object p) => true;

        private void OnAuthenticationCommandExecuted(object p)
        {
            var (isSuccess, msg, user) = Model.Authentication(FlagServer, FlagServer ? UserServers.ElementAt(SelectedIndex).Login : CurrentLogin, CurrentPassword, SelectedHost);
            if (isSuccess)
            {
                DialogService.ShowMain(user);
            }
            else ShowEror(msg);
        }

        #endregion

        #region SelectionChangedUserCommand - Выбор пользователя в списке

        private ICommand selectionChangedUserCommand;
        public ICommand SelectionChangedUserCommand => selectionChangedUserCommand ??= new RelayCommand(OnSelectionChangedUserCommandExecuted, CanSelectionChangedUserCommandExecute);
        private static bool CanSelectionChangedUserCommandExecute(object p) => true;

        private void OnSelectionChangedUserCommandExecuted(object p)
        {
            if (UserServers.ElementAt(SelectedIndex).Host == "%") SelectedHost = "localhost";
            else SelectedHost = UserServers.ElementAt(SelectedIndex).Host;
        }

        #endregion


        #endregion

        public LoginVM(LoginM model, IUserDialogService dialogService)
        {
            Model = model;
            DialogService = dialogService;

            SelectionChangedUserCommand.Execute(null);
        }

        
    }
}
