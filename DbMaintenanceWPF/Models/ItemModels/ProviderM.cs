using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class ProviderM(ProviderRepository providerR)
    {
        readonly ProviderRepository ProviderR = providerR;
        public ReadOnlyObservableCollection<Provider> PublicListProviders => new(new ObservableCollection<Provider>(ProviderR.GetAll()));

        public void Create(Provider provider) => ProviderR.Add(provider);
        public void Update(Provider provider) => ProviderR.Update(provider.Id, provider);
        public void Remove(Provider provider) => ProviderR.Remove(provider);
    }
}
