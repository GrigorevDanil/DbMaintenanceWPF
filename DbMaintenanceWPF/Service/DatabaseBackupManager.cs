using DbMaintenanceWPF.Service.Interface;
using System.Diagnostics;
using System.IO;

namespace DbMaintenanceWPF.Service
{
    class DatabaseBackupManager(IXAMPPHelper XAMPPhelper, IINIManager INImanager) : IBackupManagerDatabase
    {
        #region Свойства

        readonly IXAMPPHelper XAMPPHelper = XAMPPhelper;
        readonly IINIManager INIManager = INImanager;

        #endregion

        #region Команды
        public bool CreateBackup(string fileName, string pathSave)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false
            };

            string pathXampp = INIManager.GetPrivateString("main", "PathXampp");

            if (XAMPPHelper.CheckExistXAMPP(pathXampp))
            {
                Process process = new Process { StartInfo = startInfo };

                process.Start();
                process.StandardInput.WriteLine($"CreateBackup.bat {pathXampp} {pathSave} {fileName}.sql");
                process.WaitForExit();
                return true;
            }
            else return false;
           
        }

        public bool SetBackup(string fileName, string pathGet)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false
            };

            string pathXampp = INIManager.GetPrivateString("main", "PathXampp");

            if (XAMPPHelper.CheckExistXAMPP(pathXampp))
            {
                Process process = new Process { StartInfo = startInfo };

                process.Start();
                process.StandardInput.WriteLine($"SetBackup.bat {pathXampp} {pathGet} {fileName}.sql");
                process.WaitForExit();

                return true;
            }

            return false;
        }

        public bool SetDefaultBackup()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false
            };

            string pathXampp = INIManager.GetPrivateString("main", "PathXampp");

            if (XAMPPHelper.CheckExistXAMPP(pathXampp))
            {
                Process process = new Process { StartInfo = startInfo };

                process.Start();
                process.StandardInput.WriteLine($"SetBackup.bat {pathXampp} {Directory.GetCurrentDirectory()} {"DefaultBackup.sql"}.sql");
                process.WaitForExit();

                return true;
            }

            return false;
        }

        #endregion

    }
}
