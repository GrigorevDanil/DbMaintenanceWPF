using DbMaintenanceWPF.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    public class InfoVM
    {
        #region Команды

        #region SiteCommand - Переход на сайт 

        private ICommand siteCommand;
        public ICommand SiteCommand => siteCommand ??= new RelayCommand(OnSiteCommandExecuted, CanSiteCommandExecute);
        private static bool CanSiteCommandExecute(object p) => true;

        private void OnSiteCommandExecuted(object p) => Process.Start("https://www.gorodbelorechensk.ru/");

        #endregion

        #endregion
    }
}
