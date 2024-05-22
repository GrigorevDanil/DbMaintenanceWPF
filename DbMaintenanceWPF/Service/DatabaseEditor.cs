using MySqlConnector;
using System;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service
{
    public class DatabaseEditor : IEditorDatabase
    {
        readonly IConnectionDatabase ConnectionDatabase;

        public DatabaseEditor(IConnectionDatabase connectionDatabase) => ConnectionDatabase = connectionDatabase;
       
       
        public long? OperationOnRecord(string com, string[] values)
        {
            string[] parametrs = CommandParameterExtractor.FindParametrsInCommand(com);
            ConnectionDatabase.OpenConnection();
            using (MySqlCommand command = new MySqlCommand(com, ConnectionDatabase.GetConnection()))
            {

                for (int i = 0; i < parametrs.Length; i++) command.Parameters.AddWithValue(parametrs[i], values[i]);
                try { command.ExecuteNonQuery(); ConnectionDatabase.CloseConnection(); return command.LastInsertedId; }
                catch (Exception exp)
                {
                    throw new Exception(exp.Message);
                }
            }

        }

    }
}
