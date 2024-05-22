using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class PurchaseM
    {
        readonly PurchaseRepository PurchaseR;
        readonly ProductRepository ProductR;
        readonly ProviderRepository ProviderR;
        public ReadOnlyObservableCollection<Purchase> PublicListPurchases => new(new ObservableCollection<Purchase>(PurchaseR.GetAll()));
        public ReadOnlyObservableCollection<Product> PublicListProducts => new(new ObservableCollection<Product>(ProductR.GetAll()));
        public ReadOnlyObservableCollection<Provider> PublicListProviders => new(new ObservableCollection<Provider>(ProviderR.GetAll()));


        public PurchaseM(PurchaseRepository purchaseR, ProductRepository productR, ProviderRepository providerR)
        {
            PurchaseR = purchaseR;
            ProductR = productR;
            ProviderR = providerR;

        }

        public void Update(Purchase purchase) => PurchaseR.Update(purchase.Id, purchase);
        public void Remove(Purchase purchase) => PurchaseR.Remove(purchase);

    }
}
