using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    interface IINIManager
    {
        string GetPrivateString(string aSection, string aKey);
        void WritePrivateString(string aSection, string aKey, string aValue);

    }
}
