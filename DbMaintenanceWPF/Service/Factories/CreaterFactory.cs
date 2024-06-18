using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Interface.Factories;
using DbMaintenanceWPF.Service.Repositories;
using DbMaintenanceWPF.ViewModel.PrintViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Service.Factories
{
    public class CreaterFactory : ICreaterFactory
    {
        public ICreaterEntity<Brand> CreateCreaterBrand() => App.Host.Services.GetRequiredService<ICreaterEntity<Brand>>();

        public ICreaterEntity<Category> CreateCreaterCategory() => App.Host.Services.GetRequiredService<ICreaterEntity<Category>>();

        public ICreaterEntity<Department> CreateCreaterDepartment() => App.Host.Services.GetRequiredService<ICreaterEntity<Department>>();

        public ICreaterEntity<Employee> CreateCreaterEmployee() => App.Host.Services.GetRequiredService<ICreaterEntity<Employee>>();

        public ICreaterEntity<Give> CreateCreaterGive() => App.Host.Services.GetRequiredService<ICreaterEntity<Give>>();

        public ICreaterEntity<GiveDetail> CreateCreaterGiveDetail() => App.Host.Services.GetRequiredService<ICreaterEntity<GiveDetail>>();

        public ICreaterEntity<Post> CreateCreaterPost() => App.Host.Services.GetRequiredService<ICreaterEntity<Post>>();

        public ICreaterEntity<Product> CreateCreaterProduct() => App.Host.Services.GetRequiredService<ICreaterEntity<Product>>();

        public ICreaterEntity<Provider> CreateCreaterProvider() => App.Host.Services.GetRequiredService<ICreaterEntity<Provider>>();

        public ICreaterEntity<Purchase> CreateCreaterPurchase() => App.Host.Services.GetRequiredService<ICreaterEntity<Purchase>>();

        public ICreaterEntity<Unit> CreateCreaterUnit() => App.Host.Services.GetRequiredService<ICreaterEntity<Unit>>();

        public ICreaterEntity<User> CreateCreaterUser() => App.Host.Services.GetRequiredService<ICreaterEntity<User>>();
    }
}
