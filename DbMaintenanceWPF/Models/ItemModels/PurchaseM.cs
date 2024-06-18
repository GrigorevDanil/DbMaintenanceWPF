using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class PurchaseM(PurchaseRepository purchaseR, ProductRepository productR, ProviderRepository providerR)
    {
        readonly PurchaseRepository PurchaseR = purchaseR;
        readonly ProductRepository ProductR = productR;
        readonly ProviderRepository ProviderR = providerR;
        public ReadOnlyObservableCollection<Purchase> PublicListPurchases => new(new ObservableCollection<Purchase>(PurchaseR.GetAll()));
        public ReadOnlyObservableCollection<Product> PublicListProducts => new(new ObservableCollection<Product>(ProductR.GetAll()));
        public ReadOnlyObservableCollection<Provider> PublicListProviders => new(new ObservableCollection<Provider>(ProviderR.GetAll()));

        public void Create(Purchase purchase) => PurchaseR.Add(purchase);
        public void Update(Purchase purchase) => PurchaseR.Update(purchase.Id, purchase);
        public void Remove(Purchase purchase) => PurchaseR.Remove(purchase);

    }
}
