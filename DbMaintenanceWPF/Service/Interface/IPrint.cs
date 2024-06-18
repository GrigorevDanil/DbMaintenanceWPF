using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IPrint
    {
        bool Print(string pattern, string path, string nameDocument, string[] namesVariable, object[] paramets);
    }
}
