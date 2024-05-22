using DbMaintenanceWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    interface IAuthentication
    {
        (bool, string, object) ValidateUserDatabase(string login, string password);
        (bool, string, object) ValidateUserServer(string username, string password, string host);
    }
}
