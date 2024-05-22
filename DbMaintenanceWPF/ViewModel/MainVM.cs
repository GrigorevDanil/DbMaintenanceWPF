using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System;
using System.Data.SqlTypes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DbMaintenanceWPF.ViewModel
{
    class MainVM : Base.ViewModelBase
    {
        #region Свойства


        private object currentView;
        public object CurrentView { get => currentView; set => Set(ref currentView, value); }

        ImageSource imageUser;
        public ImageSource ImageUser { get => imageUser; set => Set(ref imageUser, value); }

        string nameUser;
        public string NameUser { get => nameUser; set => Set(ref nameUser, value); }

        string textTime;
        public string TextTime { get => textTime; set => Set(ref textTime, value); }


        #endregion

        public MainVM(object CurrentUser)
        {
            switch(CurrentUser)
            {
                case User user:
                    {
                        ImageUser = user.Image;
                        NameUser = user.Name + " " + user.Surname;
                    }
                    break;
                case UserServer userServer:
                    {
                        ImageUser = new BitmapImage(new Uri("ImageEmployee.png", UriKind.Relative)); ;
                        NameUser = userServer.Login;
                    }
                    break;
            }

            TextTime = $"Дата: {DateTime.Now:dd.MM.yyyy} Время: {DateTime.Now:HH:mm}";

            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        #region Команды
        private void Timer_Tick(object sender, EventArgs e) => TextTime = $"Дата: {DateTime.Now:dd.MM.yyyy} Время: {DateTime.Now:HH:mm}";

        #endregion
    }
}
