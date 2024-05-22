using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Models;
using MySqlConnector;
using System.Data;

namespace DbMaintenanceWPF.Service
{
    class DatabaseConnection(Database database, IErrorHandlerDatabase databaseErrorHandler) : IConnectionDatabase
    {
        readonly IErrorHandlerDatabase DatabaseErrorHandler = databaseErrorHandler;

        readonly Database Database = database;

        public void OpenConnection() { if (Database.connection.State == ConnectionState.Closed) Database.connection.Open(); }
        public void CloseConnection() { if (Database.connection.State == ConnectionState.Open) Database.connection.Close(); }
        public MySqlConnection GetConnection() { return Database.connection; }

        public bool CheckConnection()
        {
            try
            {
                OpenConnection();
                return true;
            }
            catch (MySqlException exp)
            {
                DatabaseErrorHandler.ProcessError(exp);
                return false;
            }
        }

        public bool CheckConnection(Database database)
        {
            try { OpenConnection(); return true; }
            catch { return false; }
        }
    }
}
