using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class BrandM(BrandRepository brandR)
    {
        readonly BrandRepository BrandR = brandR;

        public ReadOnlyObservableCollection<Brand> PublicListBrands => new(new ObservableCollection<Brand>(BrandR.GetAll()));

        public void Create(Brand brand) => BrandR.Add(brand);
        public void Update(Brand brand) => BrandR.Update(brand.Id, brand);
        public void Remove(Brand brand) => BrandR.Remove(brand);
    }
}
