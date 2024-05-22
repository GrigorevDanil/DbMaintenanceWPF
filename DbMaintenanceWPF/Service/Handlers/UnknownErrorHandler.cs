using DbMaintenanceWPF.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Handlers
{
    class UnknownErrorHandler(string expMessage) : IHandlerDatabase
    {
        readonly string ExpMessage = expMessage;
        public void ShowError()
        {
            throw new Exception($"Данная ошибка не обработана. Ошибка: {ExpMessage}");
        }
    }
}
