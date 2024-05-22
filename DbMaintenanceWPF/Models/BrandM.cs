using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class BrandM(BrandRepository brandR)
    {
        readonly BrandRepository BrandR = brandR;

        public ReadOnlyObservableCollection<Brand> PublicListBrands => new(new ObservableCollection<Brand>(BrandR.GetAll()));

        public void Create(Brand brand) => BrandR.Add(brand);
        public void Update(Brand brand) => BrandR.Update(brand.Id, brand);
        public void Remove(Brand brand) => BrandR.Remove(brand);
    }
}
