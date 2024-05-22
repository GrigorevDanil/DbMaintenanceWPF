using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class ProductM
    {
        readonly ProductRepository ProductR;
        readonly CategoryRepository CategoryR;
        readonly BrandRepository BrandR;
        readonly UnitRepository UnitR;
        public ReadOnlyObservableCollection<Product> PublicListProducts => new(new ObservableCollection<Product>(ProductR.GetAll()));
        public ReadOnlyObservableCollection<Category> PublicListCategories => new(new ObservableCollection<Category>(CategoryR.GetAll()));
        public ReadOnlyObservableCollection<Brand> PublicListBrands => new(new ObservableCollection<Brand>(BrandR.GetAll()));
        public ReadOnlyObservableCollection<Unit> PublicListUnits => new(new ObservableCollection<Unit>(UnitR.GetAll()));

        public ProductM(ProductRepository productR, CategoryRepository categoryR, BrandRepository brandR, UnitRepository unitR)
        {
            ProductR = productR;
            CategoryR = categoryR;
            BrandR = brandR;
            UnitR = unitR;
        }

        public void Update(Product product) => ProductR.Update(product.Id, product);
        public void Remove(Product product) => ProductR.Remove(product);
    }
}
