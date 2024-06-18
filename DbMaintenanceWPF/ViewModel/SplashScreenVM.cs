using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.View.Windows;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.ViewModel
{
    public class SplashScreenVM : Base.ViewModelBase
    {
        #region Свойства

        readonly IUserDialogService DialogService; 
        readonly IConnectionDatabase DatabaseConnection;  

        private int valueProgress;
        public int ValueProgress
        {
            get => valueProgress;
            set => Set(ref valueProgress, value);
        }

        #endregion

        public SplashScreenVM(IUserDialogService dialogService, IConnectionDatabase databaseConnection)
        {
            ValueProgress = 0;
            DialogService = dialogService;
            DatabaseConnection = databaseConnection;

            FuncWaitWindow(); 
        }

        #region Команды

        private async Task FuncWaitWindow()
        {
            for (int value = 0; value <= 100; value++)
            {
                ValueProgress = value;
                await Task.Delay(15);
            }
            DatabaseConnection.CheckConnection();
            DialogService.ShowAuthentication();
        }

        #endregion


    }
}
