using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class ProviderM
    {
        readonly ProviderRepository ProviderR;
        public ReadOnlyObservableCollection<Provider> PublicListProviders => new(new ObservableCollection<Provider>(ProviderR.GetAll()));

        public ProviderM(ProviderRepository providerR) => ProviderR = providerR;

        public void Update(Provider provider) => ProviderR.Update(provider.Id, provider);
        public void Remove(Provider provider) => ProviderR.Remove(provider);
    }
}
