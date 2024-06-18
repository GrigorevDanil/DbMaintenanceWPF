using DbMaintenanceWPF.Models.Items;

namespace DbMaintenanceWPF.Service.Interface.Factories
{
    public interface ICreaterFactory
    {
        ICreaterEntity<Brand> CreateCreaterBrand();
        ICreaterEntity<Category> CreateCreaterCategory();
        ICreaterEntity<Department> CreateCreaterDepartment();
        ICreaterEntity<Employee> CreateCreaterEmployee();
        ICreaterEntity<Give> CreateCreaterGive();
        ICreaterEntity<GiveDetail> CreateCreaterGiveDetail();
        ICreaterEntity<Post> CreateCreaterPost();
        ICreaterEntity<Product> CreateCreaterProduct();
        ICreaterEntity<Provider> CreateCreaterProvider();
        ICreaterEntity<Purchase> CreateCreaterPurchase();
        ICreaterEntity<Unit> CreateCreaterUnit();
        ICreaterEntity<User> CreateCreaterUser();
    }
}
