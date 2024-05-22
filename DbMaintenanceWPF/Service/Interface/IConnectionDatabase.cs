using DbMaintenanceWPF.Models;
using MySqlConnector;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IConnectionDatabase
    {
        void OpenConnection();
        void CloseConnection();
        MySqlConnection GetConnection();
        bool CheckConnection();
        bool CheckConnection(Database database);
    }
}
