using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    interface ISHA256Helper
    {
        string HashPassword(string password, string salt);
        string CreateSalt();
    }
}
