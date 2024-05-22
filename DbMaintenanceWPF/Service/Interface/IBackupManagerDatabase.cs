using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    interface IBackupManagerDatabase
    {
        bool SetBackup(string fileName, string pathSave);
        bool CreateBackup(string fileName, string pathGet);
        bool SetDefaultBackup();
    }
}
