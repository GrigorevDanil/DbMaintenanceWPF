using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IBackupManagerDatabase
    {

        bool SetBackup(string fileName, string path);
        bool CreateBackup(string fileName, string path);
        bool SetDefaultBackup();
    }
}
