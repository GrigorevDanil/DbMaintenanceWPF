using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Interface.Factories;
using MySqlConnector;

namespace DbMaintenanceWPF.Service
{
    class DatabaseErrorHandler(IDatabaseErrorHandlerFactory errorHandlerFactory) : IErrorHandlerDatabase
    {
        readonly IDatabaseErrorHandlerFactory ErrorHandlerFactory = errorHandlerFactory;

        #region Команды

        public void ProcessError(MySqlException exp)
        {
            switch (exp.ErrorCode)
            {
                case MySqlErrorCode.UnknownDatabase:
                    {
                        ErrorHandlerFactory.CreateDatabaseNotFoundErrorHandler().ShowError();
                        ErrorHandlerFactory.CreateUnableToConnectToHostErrorHandler().ShowError();
                    } break;
                    
                case MySqlErrorCode.AccessDenied:
                    {
                        ErrorHandlerFactory.CreateAccessDeniedErrorHandler().ShowError();
                    }
                    break;
                case MySqlErrorCode.UnableToConnectToHost:
                    {
                        ErrorHandlerFactory.CreateUnableToConnectToHostErrorHandler().ShowError();
                    }
                    break;
                default: ErrorHandlerFactory.CreateUnknownErrorHandler(exp.Message).ShowError(); break;
            }
        }

        #endregion
    }

}
