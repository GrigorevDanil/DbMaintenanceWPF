
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Repositories;

namespace DbMaintenanceWPF.Service.Interface.Factories
{
    public interface IRepositoryFactory
    {
        BrandRepository CreateBrandRepository();
        CategoryRepository CreateCategoryRepository();
        DepartmentRepository CreateDepartmentRepository();
        EmployeeRepository CreateEmployeeRepository();
        GiveDetailRepository CreateGiveDetailRepository();
        GiveRepository CreateGiveRepository();
        PostRepository CreatePostRepository();
        ProductRepository CreateProductRepository();
        ProviderRepository CreateProviderRepository();
        PurchaseRepository CreatePurchaseRepository();
        UnitRepository CreateUnitRepository();
        UserRepository CreateUserRepository();
    }
}
