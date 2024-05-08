using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using DbMaintenanceWPF.View;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows.Media;

namespace DbMaintenanceWPF.Utilities
{
    public class DatabaseService
    {
        Database database;
        MySqlDataAdapter adapter;
        public DatabaseService(Database database)
        {
            this.database = database;
            adapter = new MySqlDataAdapter();
        }
        public void openConnection() { if (database.connection.State == ConnectionState.Closed) database.connection.Open(); }
        public void closeConnection() { if (database.connection.State == ConnectionState.Open) database.connection.Close(); }
        public MySqlConnection getConnection() { return database.connection; }

        public int CheckConnection()
        {
            try { App.serviceDb.openConnection(); return 1; }
            catch (MySqlException exp)
            {
                //?????
                //База данных не найдена
                if (exp.ErrorCode.ToString() == "UnknownDatabase")
                {
                    if (new View.MessageWindow("Отсутствует база данных!", "База данных не найдена? Создать базу данных по умолчанию?", MessageBoxButton.OKCancel, MessageBoxImage.Error).ShowDialog() == true)
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            RedirectStandardInput = true,
                            UseShellExecute = false
                        };

                        string pathXampp = App.manager.GetPrivateString("main", "PathXampp");

                        if (!Directory.Exists(pathXampp))
                        {
                            pathXampp = App.FindXamppMysqlBin();

                            if (pathXampp == null)
                            {
                                new View.MessageWindow("XAMPP не найден?", "Приложение XAMPP не установленн на компьютере!", MessageBoxButton.OK, MessageBoxImage.Error).ShowDialog();
                                return 0;
                            }

                            App.manager.WritePrivateString("main", "PathXampp", pathXampp);
                        }

                        Process process = new Process { StartInfo = startInfo };

                        process.Start();
                        process.StandardInput.WriteLine($"SetBackup.bat {pathXampp} {Directory.GetCurrentDirectory()} DefaultBackup.sql");
                        process.WaitForExit();

                        App.RestartApp();
                    }
                }
                else if (new View.MessageWindow("Отсутствует соединение с сервером MySql!", "Перейти к настройкам подключения с базой данных?", MessageBoxButton.OKCancel, MessageBoxImage.Error).ShowDialog() == true) new ContextConnection().ShowDialog();
                return 0;
            }
        }


        public T LoadRecordFromServer<T>(string com, Func<MySqlDataReader, T> createItem)
        {
            T item = default(T);
            openConnection();
            using (MySqlCommand command = new MySqlCommand(com, getConnection()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = createItem(reader);
                    }
                }
            }
            closeConnection();
            return item;
        }

        public List<T> LoadListFromServer<T>(string com, Func<MySqlDataReader, T> createItem)
        {
            List<T> list = new List<T>();
            openConnection();
            using (MySqlCommand command = new MySqlCommand(com, getConnection()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T item = createItem(reader);
                        list.Add(item);
                    }
                }
            }
            closeConnection();
            return list;
        }

        public void LoadComboBox(ref ComboBox comboBox, ref List<int> list, string com, int countCol = 1, int startCol = 1)
        {
            comboBox.Items.Clear();
            list.Clear();
            string item;
            openConnection();
            using (MySqlCommand command = new MySqlCommand(com, getConnection()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = "";
                        for (int i = startCol; i <= countCol - 1; i++) item += reader[i].ToString() + " ";
                        item += reader[countCol].ToString();
                        comboBox.Items.Add(item);
                        list.Add(reader.GetInt32(0));
                    }
                }
            }
            closeConnection();
        }


        public void DeleteRecord(string id, string commandDelete)
        {
            try
            {
                openConnection();
                using (MySqlCommand command = new MySqlCommand(commandDelete, getConnection()))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                closeConnection();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
        }

        private string[] FindParametrsInCommand(string com)
        {
            MatchCollection matches = Regex.Matches(com, @"@\w+");
            string[] parametrs = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++) parametrs[i] = matches[i].Value;
            return parametrs;
        }

        public void OperationOnRecord(string com, string[] values)
        {
            string[] parametrs = FindParametrsInCommand(com);
            openConnection();
            using (MySqlCommand command = new MySqlCommand(com, getConnection()))
            {

                for (int i = 0; i < parametrs.Length; i++) command.Parameters.AddWithValue(parametrs[i], values[i]);
                try { command.ExecuteNonQuery(); }
                catch (Exception exp)
                {

                    MessageBox.Show($"При выполнении операции произошла ошибка: {exp.Message}", "Произошла ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            closeConnection();
        }

        public void OperationOnRecord(string com, string[] values, byte[] img)
        {
            string[] parametrs = FindParametrsInCommand(com);
            openConnection();
            using (MySqlCommand command = new MySqlCommand(com, getConnection()))
            {
                command.Parameters.AddWithValue(parametrs[0], img);
                for (int i = 1; i < parametrs.Length; i++) command.Parameters.AddWithValue(parametrs[i], values[i]);
                try { command.ExecuteNonQuery(); }
                catch (Exception exp)
                {

                    MessageBox.Show($"При выполнении операции произошла ошибка: {exp.Message}", "Произошла ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            closeConnection();
        }

        public DataTable OperationSelect(string com, string[] values)
        {
            DataTable table = new DataTable();
            string[] parametrs = FindParametrsInCommand(com);
            using (var command = new MySqlCommand(com, getConnection()))
            {
                for (int i = 0; i < parametrs.Length; i++) command.Parameters.AddWithValue(parametrs[i], values[i]);
                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            return table;
        }

    }
}
