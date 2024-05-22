using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models
{
    public class Database
    {
        public MySqlConnection connection;
        public MySqlConnectionStringBuilder stringConnection;
        public Database(MySqlConnection connection)
        {
            this.connection = connection;
            stringConnection = new MySqlConnectionStringBuilder(connection.ConnectionString);
        }

        public Database(string server, int port, string nameDb, string userId, string pass)
        {
            connection = new MySqlConnection($"Server={server};Port={port};User ID={userId};Database={nameDb};pass={pass};Allow Zero DateTime=True");
            stringConnection = new MySqlConnectionStringBuilder(connection.ConnectionString);

        }
    }
}
