using DbMaintenanceWPF.Items;
using DbMaintenanceWPF.Model;
using DbMaintenanceWPF.Utilities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace DbMaintenanceWPF.ViewModel
{
    class ConnectionVM : Utilities.ViewModelBase
    {
        private Connection currentConnection;
        public Connection CurrentConnection
        {
            get { return currentConnection; }
            set { currentConnection = value; OnPropertyChanged(nameof(CurrentConnection)); }
        }
        public ObservableCollection<string> Users { get; set; }
        private RelayCommand editCommand, resetCommand;
        

        public ConnectionVM()
        {
            //Установка текущей модели
            CurrentConnection = new Connection
            {
                Server = App.db.stringConnection.Server,
                Port = App.db.stringConnection.Port.ToString(),
                DbName = App.db.stringConnection.Database,
                User = App.db.stringConnection.UserID.ToString(),
                Password = App.db.stringConnection.Password
            };
            Users = new ObservableCollection<string>();

        }

        void UpdateUsers()
        {
            Users.Clear();
            //Взятие всех пользователей на сервере
            using (MySqlCommand command = new MySqlCommand("SELECT User FROM mysql.user", App.serviceDb.getConnection()))
            {
                using (MySqlDataReader reader = command.ExecuteReader()) while (reader.Read()) Users.Add(reader.GetString(0));
            }
        }

        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((o) =>
                  {
                      App.manager.WritePrivateString("main", "StringConnection", $"Server={CurrentConnection.Server};Port={CurrentConnection.Port};User ID={CurrentConnection.User};{(CurrentConnection.Password.Length != 0 ? $"Password= {CurrentConnection.Password}; " : " ")}Database={CurrentConnection.DbName};Allow Zero DateTime=True");
                      ProcessStartInfo startInfo = new ProcessStartInfo(Process.GetCurrentProcess().MainModule.FileName);
                      Process.Start(startInfo);
                      Application.Current.Shutdown();
                  }));
            }
        }
        // команда сброса
        public RelayCommand ResetCommand
        {
            get
            {
                return resetCommand ??
                  (resetCommand = new RelayCommand((o) =>
                  {
                      App.manager.WritePrivateString("main", "StringConnection", $"Server=127.0.0.1;Port=3306;User ID=root;Database=dbhrtime;Allow Zero DateTime=True");
                      ProcessStartInfo startInfo = new ProcessStartInfo(Process.GetCurrentProcess().MainModule.FileName);
                      Process.Start(startInfo);
                      Application.Current.Shutdown();
                  }));
            }
        }
    }
}
