using DbMaintenanceWPF.Utilities;
using DbMaintenanceWPF.View;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Collections.ObjectModel;
using DbMaintenanceWPF.Model;
using DbMaintenanceWPF.Items;
using System.Data;
using System.Windows.Media.Imaging;

namespace DbMaintenanceWPF.ViewModel
{
    class LoginVM : Utilities.ViewModelBase
    {
        List<string> 
            namesHost;

        int 
            selectedIndexUser;

        string 
            stringConnection,
            currentPassword,
            selectedHost, 
            textConnection,
            currentLogin,
            textEror;

        User 
            currentUser;

        Visibility
            visEror;

        Brush 
            colorConnection;

        bool 
            flagChangeUserServer,
            flagServer;


        RelayCommand
            inputCommand,
            helpCommand,
            updateCommand,
            exitCommand,
            selectionChangedUserCommand,
            visiblePasswordChangeCommand;

        #region Property
        public ObservableCollection<string> Users { get; set; }
        public string StringConnection
        {
            get { return stringConnection; }
            set { stringConnection = value; OnPropertyChanged(nameof(StringConnection)); }
        }
        public string CurrentPassword
        {
            get { return currentPassword; }
            set { currentPassword = value; OnPropertyChanged(nameof(CurrentPassword)); }
        }
        public int SelectedIndexUser
        {
            get { return selectedIndexUser; }
            set { selectedIndexUser = value; OnPropertyChanged(nameof(SelectedIndexUser)); }
        }
        public string SelectedHost
        {
            get { return selectedHost; }
            set { selectedHost = value; OnPropertyChanged(nameof(SelectedHost)); }
        }
        public string CurrentLogin
        {
            get { return currentLogin; }
            set { currentLogin = value; OnPropertyChanged(nameof(CurrentLogin)); }
        }
        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }
        public List<string> NamesHost
        {
            get { return namesHost; }
            set { namesHost = value; OnPropertyChanged(nameof(NamesHost)); }
        }

        public Brush ColorConnection
        {
            get { return colorConnection; }
            set { colorConnection = value; OnPropertyChanged(nameof(ColorConnection)); }
        }

        public string TextConnection
        {
            get { return textConnection; }
            set { textConnection = value; OnPropertyChanged(nameof(TextConnection)); }
        }
        public bool FlagChangeUserServer
        {
            get { return flagChangeUserServer; }
            set { flagChangeUserServer = value; OnPropertyChanged(nameof(FlagChangeUserServer)); }
        }

        public bool FlagServer
        {
            get { return flagServer; }
            set { flagServer = value; OnPropertyChanged(nameof(FlagServer)); }
        }

        public Visibility VisEror
        {
            get { return visEror; }
            set { visEror = value; OnPropertyChanged(nameof(VisEror)); }
        }
        public string TextEror
        {
            get { return textEror; }
            set { textEror = value; OnPropertyChanged(nameof(TextEror)); }
        }


        #endregion

        public LoginVM()
        {
            FlagServer = false;
            CurrentLogin = "";
            CurrentPassword = "";
            NamesHost = new List<string>();
            Users = new ObservableCollection<string>();

            //Инициализация базы данных

            //Взятие строки подключения из файла
            var connection = new MySqlConnection(App.manager.GetPrivateString("main", "StringConnection"));

            //Создание экземпляра базы данных и сервиса к ней
            App.db = new Database(connection, new MySqlConnectionStringBuilder(connection.ConnectionString));
            App.serviceDb = new DatabaseService(App.db);

            UpdateCommand.Execute(null);

            UpdateNamesHost();
            UpdateUsers();

            SelectionChangedUserCommand.Execute(null);

            App.serviceDb.closeConnection();

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

        private void UpdateNamesHost()
        {
            NamesHost.Clear();
            using (MySqlCommand command = new MySqlCommand("SELECT User, Host FROM mysql.user", App.serviceDb.getConnection()))
            {
                using (MySqlDataReader reader = command.ExecuteReader()) while (reader.Read()) NamesHost.Add(reader.GetString(1));
            }
        }


        // команда входа
        public RelayCommand InputCommand
        {
            get
            {
                return inputCommand ??
                  (inputCommand = new RelayCommand((o) =>
                  {
                      App.serviceDb.CheckConnection();
                      App.serviceDb.openConnection();

                      //Проверка на то что пользователь с сервера
                      if (!FlagServer)
                      {
                          if (CurrentLogin == "" || CurrentPassword == "")
                          {
                              ShowEror("Поля пустые!");
                              return;
                          }

                          if (FlagChangeUserServer)
                          {
                              App.db.connection = new MySqlConnection($"server=localhost; user = root; database = {App.db.connection.Database}");
                              FlagChangeUserServer = false;
                          }

                          //Вспомогательная таблица
                          DataTable table;

                          //Попытка найти пользователя в таблице
                          table = App.serviceDb.OperationSelect("SELECT * FROM user WHERE Login = @login;", [CurrentLogin, CurrentPassword]);


                          if (table.Rows.Count > 0)
                          {
                              DataRow row = table.Rows[0];

                              if ((bool)row["IsLock"] == true)
                              {
                                  ShowEror("Пользователь заблокирован");
                                  return;
                              }

                              if (row["DateLock"] != DBNull.Value)
                              {
                                  DateTime tempTimeDB = (DateTime)row["DateLock"];
                                  TimeSpan timeLeft = tempTimeDB.ToLocalTime().AddMinutes(1) - DateTime.Now;

                                  if (tempTimeDB.AddMinutes(1) > DateTime.Now)
                                  {
                                      ShowEror($"Повторная попытка через {timeLeft.Seconds} сек");
                                      return;
                                  }
                                  else
                                  {
                                      //Разблокировка пользователя и установка счетчика на 3 попытки
                                      App.serviceDb.OperationOnRecord("UPDATE user SET DateLock = @date, CountAttemp = @count WHERE Login = @login;", [null, "3", CurrentLogin]);
                                      table = App.serviceDb.OperationSelect("SELECT * FROM user WHERE Login = @login;", [CurrentLogin]);
                                      row = table.Rows[0];
                                  }
                              }
                              //Текущий пользователь
                              CurrentUser = new User();

                              //Получение иконки пользователя или установка обычной аватарки
                              BitmapImage bitmapImage = new BitmapImage();
                              if (row["Image"] != DBNull.Value)
                              {
                                  using (MemoryStream ms = new MemoryStream((byte[])row["Image"]))
                                  {
                                      ms.Position = 0;

                                      bitmapImage.BeginInit();
                                      bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                                      bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                      bitmapImage.UriSource = null;
                                      bitmapImage.StreamSource = ms;
                                      bitmapImage.EndInit();
                                  }
                                  bitmapImage.Freeze();
                                  CurrentUser.Image = bitmapImage;
                              }
                              else CurrentUser.Image = new BitmapImage(new Uri("ImageEmployee.png", UriKind.Relative));


                              CurrentUser.Id = int.Parse(row["IdUser"].ToString());
                              CurrentUser.Login = row["Login"].ToString();
                              CurrentUser.Role = row["Role"].ToString();
                              CurrentUser.Surname = row["Surname"].ToString();
                              CurrentUser.Name = row["Name"].ToString();
                              if (row["DateLock"].ToString() != "") CurrentUser.DateLock = DateTime.Parse(row["DateLock"].ToString()).ToString("dd.MM.yyyy");
                              CurrentUser.IsLock = bool.Parse(row["IsLock"].ToString());
                              CurrentUser.PasswordHash = row["PasswordHash"].ToString();
                              CurrentUser.Salt = row["Salt"].ToString();

                              //Проверка логина и пароля

                              string tempPassHash = App.HashPassword(CurrentPassword, CurrentUser.Salt);

                              int tempCount = int.Parse(row["CountAttemp"].ToString());

                              if (CurrentUser.PasswordHash != tempPassHash)
                              {
                                  if (tempCount == 0)
                                  {
                                      //Блокировка пользователя
                                      App.serviceDb.OperationOnRecord("UPDATE user SET DateLock = @date WHERE IdUser = @idUser;", [DateTime.Now.ToString("yyyy-MM-dd"), CurrentUser.Id.ToString()]);
                                      App.serviceDb.openConnection();
                                      ShowEror("Пользователь заблокирован на 1 мин");
                                      return;
                                  }

                                  //Уменьшение счетчика на 1 при неправильном пароле
                                  App.serviceDb.OperationOnRecord("UPDATE user SET CountAttemp = CountAttemp-1 WHERE IdUser = @idUser;", [CurrentUser.Id.ToString()]);
                                  App.serviceDb.openConnection();
                                  ShowEror($"Неверный пароль! Осталось {tempCount} попытки");
                                  return;
                              }

                              MainForm form = new MainForm();
                              form.Show();
                              CurrentLogin = "";
                              CurrentPassword = "";
                              //this.Hide();
                          }

                      }
                      else
                      {
                          FlagChangeUserServer = true;

                          stringConnection = $"server={SelectedHost}; user = {Users[SelectedIndexUser]}; password = {CurrentPassword}; database = {App.db.connection.Database}";
                          App.db.connection = new MySqlConnection(stringConnection);
                          App.serviceDb.CheckConnection();

                          //Текущий пользователь
                          CurrentUser = new User();
                          CurrentUser.Login = Users[SelectedIndexUser];
                          CurrentUser.Image = new BitmapImage(new Uri("ImageEmployee.png", UriKind.Relative));
                          MainForm form = new MainForm();
                          form.Show();
                          CurrentLogin = "";
                          CurrentPassword = "";
                          //this.Hide();
                      
                      }
                  }));
            }
        }

        void ShowEror(string textEror)
        {
            TextEror = textEror;
            VisEror = Visibility.Visible;
            Task.Delay(800).ContinueWith(t => VisEror = Visibility.Hidden);
        }

        // команда выбора пользователя
        public RelayCommand SelectionChangedUserCommand
        {
            get
            {
                return visiblePasswordChangeCommand ??
                  (visiblePasswordChangeCommand = new RelayCommand((o) =>
                  {
                      if (SelectedIndexUser == -1) return;
                      if (NamesHost[SelectedIndexUser] == "%") SelectedHost = "localhost";
                      else SelectedHost = NamesHost[SelectedIndexUser];
                  }));
            }
        }


        // команда проверки соединения(Обновления)
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                  (updateCommand = new RelayCommand((o) =>
                  {
                      if (App.serviceDb.CheckConnection() == 1)
                      {
                          TextConnection = "Подключение установлено";
                          ColorConnection = Brushes.LightGreen;
                          UpdateNamesHost();
                      }
                      else 
                      {
                          TextConnection = "Отсутствует подключение";
                          ColorConnection = Brushes.Red;
                      }
                  }));
            }
        }

        // команда выхода
        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ??
                  (exitCommand = new RelayCommand((o) =>
                  {
                      Application.Current.Shutdown();
                  }));
            }
        }

        // команда вызова справки
        public RelayCommand HelpCommand
        {
            get
            {
                return helpCommand ??
                  (helpCommand = new RelayCommand((o) =>
                  {
                      Process.Start(Directory.GetCurrentDirectory() + "/Help/help5.chm");
                  }));
            }
        }
    }
}
