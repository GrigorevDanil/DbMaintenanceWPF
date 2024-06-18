using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Interface.Factories;
using DbMaintenanceWPF.Service.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public BrandRepository CreateBrandRepository() => App.Host.Services.GetRequiredService<BrandRepository>();

        public CategoryRepository CreateCategoryRepository() => App.Host.Services.GetRequiredService<CategoryRepository>();

        public DepartmentRepository CreateDepartmentRepository() => App.Host.Services.GetRequiredService<DepartmentRepository>();

        public EmployeeRepository CreateEmployeeRepository() => App.Host.Services.GetRequiredService<EmployeeRepository>();

        public GiveDetailRepository CreateGiveDetailRepository() => App.Host.Services.GetRequiredService<GiveDetailRepository>();
        public GiveRepository CreateGiveRepository() => App.Host.Services.GetRequiredService<GiveRepository>();

        public PostRepository CreatePostRepository() => App.Host.Services.GetRequiredService<PostRepository>();

        public ProductRepository CreateProductRepository() => App.Host.Services.GetRequiredService<ProductRepository>();

        public ProviderRepository CreateProviderRepository() => App.Host.Services.GetRequiredService<ProviderRepository>();

        public PurchaseRepository CreatePurchaseRepository() => App.Host.Services.GetRequiredService<PurchaseRepository>();

        public UnitRepository CreateUnitRepository() => App.Host.Services.GetRequiredService<UnitRepository>();

        public UserRepository CreateUserRepository() => App.Host.Services.GetRequiredService<UserRepository>();
    }
}
