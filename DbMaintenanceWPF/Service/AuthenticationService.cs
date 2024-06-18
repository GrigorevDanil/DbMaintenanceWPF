using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using MySqlConnector;
using System;
using System.Data;

namespace DbMaintenanceWPF.Service
{
    class AuthenticationService(IConnectionDatabase connectionDatabase, UserRepository userR, ISHA256Helper SHA256helper, ILoginAttemp loginAttempService, Database database) : IAuthentication
    {
        #region Свойства

        readonly IConnectionDatabase ConnectionDatabase = connectionDatabase;
        readonly UserRepository UserR = userR;
        readonly ISHA256Helper SHA256Helper = SHA256helper;
        readonly ILoginAttemp LoginAttempService = loginAttempService;
        readonly Database Database = database;
        #endregion

        public (bool,string,object) ValidateUserDatabase(string login, string password)
        {
            User currentUser = UserR.GetUserByLogin(login);

            if (currentUser == null) return (false, "Пользователь не найден", null);

            if (currentUser.IsLock) return (false, "Пользователь заблокирован", null);

            if (currentUser.DateLock != null)
            {
                TimeSpan? timeLeft = currentUser.DateLock?.ToLocalTime().AddMinutes(1) - DateTime.Now;

                if (currentUser.DateLock?.AddMinutes(1) > DateTime.Now) return (false, $"Пользователь заблокирован. Повтор через {timeLeft?.Seconds} с.", null);
                else LoginAttempService.UnlockUser(currentUser);
            }

            if (currentUser.PasswordHash != SHA256Helper.HashPassword(password, currentUser.Salt))
            {
                if (currentUser.CountAttemp == 0)
                {
                    LoginAttempService.LockUser(currentUser);
                    return (false, "Пользователь заблокирован на 1 мин", null);
                }

                LoginAttempService.DownAttempUser(currentUser);
                return (false, $"Неверный пароль! Осталось {currentUser.CountAttemp} попытки", null);
            }
            return (true, "Успешная авторизация", currentUser);
        }

        public (bool, string, object) ValidateUserServer(string username, string password, string host)
        {
            Database checkedDatabase = new(new MySqlConnection($"server={host}; user = {username}; password = {password}; database = {Database.connection.Database}"));

            if (ConnectionDatabase.CheckConnection(checkedDatabase)) return (true, "Успешная авторизация", new UserServer() { Login = username, Host = host });
            else return (false, "В доступе отказано / Неверный пароль", null);

        }
    }
}
