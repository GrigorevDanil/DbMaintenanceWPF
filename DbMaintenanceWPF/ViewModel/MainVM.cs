using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlTypes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DbMaintenanceWPF.ViewModel
{
    public class MainVM : Base.ViewModelBase
    {
        #region Свойства

        readonly IUserDialogService DialogService;
        

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

        string nameUser;
        public string NameUser { get => nameUser; set => Set(ref nameUser, value); }

        string textTime;
        public string TextTime { get => textTime; set => Set(ref textTime, value); }

        Visibility adminVisible;
        public Visibility AdminVisible { get => adminVisible; set => Set(ref adminVisible, value); }




        #endregion

        public MainVM(IUserDialogService dialogService, object curUser)
        {
            TextTime = $"Дата: {DateTime.Now:dd.MM.yyyy} Время: {DateTime.Now:HH:mm}";

            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            DialogService = dialogService;
            CurrentUser = curUser;

            
        }





        #region Команды

        private void Timer_Tick(object sender, EventArgs e) => TextTime = $"Дата: {DateTime.Now:dd.MM.yyyy} Время: {DateTime.Now:HH:mm}";

        #region ChangeUserCommand - Смена пользователя

        private ICommand сhangeUserCommand;
        public ICommand ChangeUserCommand => сhangeUserCommand ??= new RelayCommand(OnChangeUserCommandExecuted, CanChangeUserCommandExecute);
        private static bool CanChangeUserCommandExecute(object p) => true;

        private void OnChangeUserCommandExecuted(object p) => DialogService.ShowAuthentication();

        #endregion

        #region ShowSettingConnectionCommand - Открытие окна настройки соединения

        private ICommand showSettingConnectionCommand;
        public ICommand ShowSettingConnectionCommand => showSettingConnectionCommand ??= new RelayCommand(OnShowSettingConnectionCommandExecuted, CanShowSettingConnectionCommandExecute);
        private static bool CanShowSettingConnectionCommandExecute(object p) => true;

        private void OnShowSettingConnectionCommandExecuted(object p) => DialogService.ShowSettingConnection();

        #endregion

        #region ShowInfoAuthorCommand - Открытие информации об авторе

        private ICommand showInfoAuthorCommand;
        public ICommand ShowInfoAuthorCommand => showInfoAuthorCommand ??= new RelayCommand(OnShowInfoAuthorCommandExecuted, CanShowInfoAuthorCommandExecute);
        private static bool CanShowInfoAuthorCommandExecute(object p) => true;

        private void OnShowInfoAuthorCommandExecuted(object p) => DialogService.ShowQuestion("Информация об авторе","Данную программу разработал студент курса ИП-3 Григорьев Д.В.");

        #endregion

        #region OnUserChanged - Событие изменения пользователя

        void OnUserChanged()
        {
            switch (CurrentUser)
            {
                case User user:
                    {
                        ImageUser = user.Image;
                        NameUser = user.Name + " " + user.Surname;
                        if (user.Role != "Администратор") AdminVisible = Visibility.Collapsed;
                        else AdminVisible = Visibility.Visible;
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
                        NameUser = userServer.Login;

                        
                        AdminVisible = Visibility.Collapsed;
                    }
                    break;
            }
        }

        #endregion

        #region Печати

        #region ShowMaterialStatementCommand - Открытие окна печати выдачи материальной ценности

        private ICommand showMaterialStatementCommand;
        public ICommand ShowMaterialStatementCommand => showMaterialStatementCommand ??= new RelayCommand(OnShowMaterialStatementCommandExecuted, CanShowMaterialStatementCommandExecute);
        private static bool CanShowMaterialStatementCommandExecute(object p) => true;

        private void OnShowMaterialStatementCommandExecuted(object p) => DialogService.ShowPrint("MaterialStatement");

        #endregion

        #endregion

        #endregion

    }
}
