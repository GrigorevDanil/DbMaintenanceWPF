using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IEditorDatabase
    {
        long? OperationOnRecord(string com, string[] values);
    }
}
