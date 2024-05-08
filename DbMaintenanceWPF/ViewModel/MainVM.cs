using DbMaintenanceWPF.Items;
using DbMaintenanceWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.ViewModel
{
    class MainVM : Utilities.ViewModelBase
    {
        User curUser;
        public User CurrentUser
        {
            get { return curUser; }
            set { curUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }
        public MainVM(User curUser)
        {
            CurrentUser = curUser;
        }
    }
}
