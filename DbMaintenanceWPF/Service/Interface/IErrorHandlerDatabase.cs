using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IErrorHandlerDatabase
    {
        void ProcessError(MySqlException exp);
    }
}
