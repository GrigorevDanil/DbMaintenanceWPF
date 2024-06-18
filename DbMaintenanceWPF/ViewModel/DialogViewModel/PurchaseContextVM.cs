using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    public class PurchaseContextVM : ViewModelBase
    {
        public PurchaseContextVM()
        {
            Products = App.Host.Services.GetRequiredService<ICreaterEntity<Product>>().GetList();
            Providers = App.Host.Services.GetRequiredService<ICreaterEntity<Provider>>().GetList();

            SelectedProduct = Products.FirstOrDefault();
            SelectedProvider = Providers.FirstOrDefault();
            DateProvide = DateTime.Now;
        }

        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        IEnumerable<Product> products;
        public IEnumerable<Product> Products { get => products; set => Set(ref products, value); }

        IEnumerable<Provider> providers;
        public IEnumerable<Provider> Providers { get => providers; set => Set(ref providers, value); }


        Product selectedProduct;
        public Product SelectedProduct { get => selectedProduct; set { Set(ref selectedProduct, value); CanCommitCommandExecute(null); } }

        Provider selectedProvider;
        public Provider SelectedProvider { get => selectedProvider; set { Set(ref selectedProvider, value); CanCommitCommandExecute(null); } }


        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        string textCountProduct;
        public string TextCountProduct { get => textCountProduct; set { Set(ref textCountProduct, value); CanCommitCommandExecute(null); } }

        string textPrice;
        public string TextPrice { get => textPrice; set { Set(ref textPrice, value); CanCommitCommandExecute(null); } }

        DateTime? dateProvide;
        public DateTime? DateProvide { get => dateProvide; set => Set(ref dateProvide, value); }

        bool flagDateProvide;
        public bool FlagDateProvide { get => flagDateProvide; set => Set(ref flagDateProvide, value); }


        #endregion

        #region Команды

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) =>
            !string.IsNullOrEmpty(TextCountProduct) && (int.Parse(TextCountProduct) >= 0) &&
            !string.IsNullOrEmpty(TextPrice) && (int.Parse(TextPrice) >= 0);

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
