
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Models
{
    public class ConnectionM(IBackupManagerDatabase backupManager, IINIManager iNIManager)
    {
        readonly IBackupManagerDatabase BackupManager = backupManager;
        readonly IINIManager INIManager = iNIManager;

        public void ResetDatabaseToDefault() => BackupManager.SetDefaultBackup();
        public void ResetConnectionToDefault() => INIManager.WritePrivateString("main", "StringConnection", "Server=127.0.0.1;Port=3306;User ID=root;Database=dbmaintenance;Allow Zero DateTime=True");
        public void SaveNewConnection(string server,string port, string db, string user, string password) => INIManager.WritePrivateString("main", "StringConnection", $"Server={server};Port={port};User ID={user};{(password != "" || password != null ? $"Password={password};" : "")}Database={db};Allow Zero DateTime=True");
    }
}
