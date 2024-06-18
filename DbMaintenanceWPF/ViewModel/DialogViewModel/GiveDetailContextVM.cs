using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    public class GiveDetailContextVM : ViewModelBase
    {
        public GiveDetailContextVM()
        {
            Gives = App.Host.Services.GetRequiredService<ICreaterEntity<Give>>().GetList();
            Products = App.Host.Services.GetRequiredService<ICreaterEntity<Product>>().GetList();

            SelectedGive = Gives.FirstOrDefault();
            SelectedProduct = Products.FirstOrDefault();
        }

        #region Свойства
        public event EventHandler<EventArgs<bool>> Complete;

        IEnumerable<Give> gives;
        public IEnumerable<Give> Gives { get => gives; set => Set(ref gives, value); }

        IEnumerable<Product> products;
        public IEnumerable<Product> Products { get => products; set => Set(ref products, value); }

        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        Give selectedGive;
        public Give SelectedGive { get => selectedGive; set { Set(ref selectedGive, value); CanCommitCommandExecute(null); } }

        Product selectedProduct;
        public Product SelectedProduct { get => selectedProduct; set { Set(ref selectedProduct, value); CanCommitCommandExecute(null); } }

        string textCountProduct;
        public string TextCountProduct { get => textCountProduct; set { Set(ref textCountProduct, value); CanCommitCommandExecute(null); } }
        #endregion

        #region Команды

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => !string.IsNullOrEmpty(TextCountProduct)  && (int.Parse(TextCountProduct) >= 0);

        private void OnCommitCommandExecuted(object p) => Complete?.Invoke(this, true);

        #endregion

        #region CancelCommand - Отменить изменения

        private ICommand _CancelCommand;

        public ICommand CancelCommand => _CancelCommand
            ??= new RelayCommand(OnCancelCommandExecuted, CanCancelCommandExecute);

        private bool CanCancelCommandExecute(object p) => true;

        private void OnCancelCommandExecuted(object p) => Complete?.Invoke(this, false);

        #endregion


        #endregion
    }
}
