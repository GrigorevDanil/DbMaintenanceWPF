using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Interface.Factories
{
    public interface IDatabaseErrorHandlerFactory
    {
        IHandlerDatabase CreateDatabaseNotFoundErrorHandler();
        IHandlerDatabase CreateAccessDeniedErrorHandler();
        IHandlerDatabase CreateUnknownErrorHandler(string expMessage);
        IHandlerDatabase CreateUnableToConnectToHostErrorHandler();
    }
}
