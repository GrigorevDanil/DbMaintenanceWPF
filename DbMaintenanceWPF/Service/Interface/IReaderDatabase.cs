using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IReaderDatabase
    {
        T LoadRecord<T>(string query, Func<MySqlDataReader, T> createItem);
        List<T> LoadList<T>(string com, Func<MySqlDataReader, T> createItem);
        DataTable OperationSelect(string com, object[] values);
    }
}
