using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.View.Windows;
using DbMaintenanceWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service
{
    public class StorageViewModel : IStorageViewModel
    {
        MainVM MainViewModel { get; set; }

        public object GetViewModel(string nameVM)
        {
            switch (nameVM)
            {
                default: throw new NotSupportedException("Данная ViewModel не может быть выдана");
                case nameof(MainVM): return MainViewModel;
            }

        }

        public void SetViewModel(object viewModel)
        {
            switch (viewModel)
            {
                default: throw new NotSupportedException("Данная ViewModel не может быть выдана");
                case MainVM: { MainViewModel = viewModel as MainVM; } break;
            }
        }
    }
}
