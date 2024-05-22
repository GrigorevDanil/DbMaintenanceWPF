using DbMaintenanceWPF.Service.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace DbMaintenanceWPF.Service
{
    class DatabaseReader(IConnectionDatabase connectionDatabase) : IReaderDatabase
    {
        readonly IConnectionDatabase ConnectionDatabase = connectionDatabase;

        public T LoadRecord<T>(string com, Func<MySqlDataReader, T> createItem)
        {
            ConnectionDatabase.CheckConnection();
            T item = default;
            ConnectionDatabase.OpenConnection();
            using (MySqlCommand command = new(com, ConnectionDatabase.GetConnection()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = createItem(reader);
                    }
                }
            }
            ConnectionDatabase.CloseConnection();
            return item;
        }

        public List<T> LoadList<T>(string com, Func<MySqlDataReader, T> createItem)
        {
            ConnectionDatabase.CheckConnection();
            List<T> list = new();
            ConnectionDatabase.OpenConnection();
            using (MySqlCommand command = new(com, ConnectionDatabase.GetConnection()))
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
            ConnectionDatabase.CloseConnection();
            return list;
        }

        public DataTable OperationSelect(string com, string[] values)
        {
            MySqlDataAdapter adapter = new();
            DataTable table = new();
            string[] parametrs = CommandParameterExtractor.FindParametrsInCommand(com);
            using (var command = new MySqlCommand(com, ConnectionDatabase.GetConnection()))
            {
                for (int i = 0; i < parametrs.Length; i++) command.Parameters.AddWithValue(parametrs[i], values[i]);
                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            return table;
        }

    }
}
