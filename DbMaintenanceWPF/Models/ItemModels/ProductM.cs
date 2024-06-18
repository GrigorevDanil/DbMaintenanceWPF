using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class ProductM(ProductRepository productR, CategoryRepository categoryR, BrandRepository brandR, UnitRepository unitR)
    {
        readonly ProductRepository ProductR = productR;
        readonly CategoryRepository CategoryR = categoryR;
        readonly BrandRepository BrandR = brandR;
        readonly UnitRepository UnitR = unitR;
        public ReadOnlyObservableCollection<Product> PublicListProducts => new(new ObservableCollection<Product>(ProductR.GetAll()));
        public ReadOnlyObservableCollection<Category> PublicListCategories => new(new ObservableCollection<Category>(CategoryR.GetAll()));
        public ReadOnlyObservableCollection<Brand> PublicListBrands => new(new ObservableCollection<Brand>(BrandR.GetAll()));
        public ReadOnlyObservableCollection<Unit> PublicListUnits => new(new ObservableCollection<Unit>(UnitR.GetAll()));

        public void Create(Product product) => ProductR.Add(product);
        public void Update(Product product) => ProductR.Update(product.Id, product);
        public void Remove(Product product) => ProductR.Remove(product);
    }
}
