using DbMaintenanceWPF.Service;
using DbMaintenanceWPF.Service.Interface;
using System;
using System.IO;

namespace DbMaintenanceWPF.Service
{
    class XAMPPHelper : IXAMPPHelper
    {
        #region Свойства

        readonly IINIManager INIManager;
        readonly IUserDialogService UserDialog;

        #endregion

        #region Команды

        public string FindXamppMysqlBin()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.IsReady || drive.DriveType != DriveType.Fixed) continue;

                var binPath = FindXamppMysqlBinInDirectory(drive.RootDirectory);
                if (binPath != null)
                {
                    return binPath;
                }
            }

            return null;

            string FindXamppMysqlBinInDirectory(DirectoryInfo directoryInfo)
            {
                try
                {
                    foreach (var directory in directoryInfo.EnumerateDirectories())
                    {
                        if (directory.Name.Equals("bin", StringComparison.OrdinalIgnoreCase))
                        {
                            if (directory.Parent != null && directory.Parent.Name.Equals("mysql", StringComparison.OrdinalIgnoreCase))
                            {
                                if (directory.Parent.Parent != null && directory.Parent.Parent.Name.Equals("xampp", StringComparison.OrdinalIgnoreCase))
                                {
                                    return directory.FullName;
                                }
                            }
                        }

                        var binPath = FindXamppMysqlBinInDirectory(directory);
                        if (binPath != null)
                        {
                            return binPath;
                        }
                    }
                }
                catch (UnauthorizedAccessException) { }
                catch (SystemException) { }

                return null;
            }
        }

        public bool CheckExistXAMPP(string pathXampp)
        {
            if (!Directory.Exists(pathXampp))
            {
                pathXampp = FindXamppMysqlBin();

                if (pathXampp == null)
                {
                    UserDialog.ShowWarning("XAMPP не найден?", "Приложение XAMPP не установленн на компьютере!");
                    return false;
                }

                INIManager.WritePrivateString("main", "PathXampp", pathXampp);
                return true;
            }
            else return true;
        }


        #endregion

        public XAMPPHelper(IINIManager iNIManager, IUserDialogService userDialog)
        {
            INIManager = iNIManager;
            UserDialog = userDialog;
        }

    }
}
