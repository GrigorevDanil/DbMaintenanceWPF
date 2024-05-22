using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Factories;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Interface.Factories;
using DbMaintenanceWPF.Service.ManageDatabase;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.Service
{
    internal static class Registrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IINIManager, INIManager>(provider => new INIManager("./Config.ini"));

            services.AddSingleton<BrandRepository>();
            services.AddSingleton<CategoryRepository>();
            services.AddSingleton<DepartmentRepository>();
            services.AddSingleton<EmployeeRepository>();
            services.AddSingleton<GiveDetailRepository>();
            services.AddSingleton<GiveRepository>();
            services.AddSingleton<PostRepository>();
            services.AddSingleton<ProductRepository>();
            services.AddSingleton<ProviderRepository>();
            services.AddSingleton<PurchaseRepository>();
            services.AddSingleton<UnitRepository>();
            services.AddSingleton<UserRepository>();

            services.AddSingleton<ICreaterListItems<Brand>, CreaterListBrand>();
            services.AddSingleton<ICreaterListItems<Category>, CreaterListCategory>();
            services.AddSingleton<ICreaterListItems<Department>, CreaterListDepartment>();
            services.AddSingleton<ICreaterListItems<Employee>, CreaterListEmployee>();
            services.AddSingleton<ICreaterListItems<GiveDetail>, CreaterListGiveDetail>();
            services.AddSingleton<ICreaterListItems<Give>, CreaterListGive>();
            services.AddSingleton<ICreaterListItems<Post>, CreaterListPost>();
            services.AddSingleton<ICreaterListItems<Product>, CreaterListProduct>();
            services.AddSingleton<ICreaterListItems<Provider>, CreaterListProvider>();
            services.AddSingleton<ICreaterListItems<Purchase>, CreaterListPurchase>();
            services.AddSingleton<ICreaterListItems<Unit>, CreaterListUnit>();
            services.AddSingleton<ICreaterListItems<User>, CreaterListUser>();
            services.AddSingleton<ICreaterListItems<UserServer>, CreaterListUserServer>();



            services.AddTransient<IUserDialogService, UserDialogService>();
            services.AddTransient<IConnectionDatabase, DatabaseConnection>();
            services.AddTransient<IReaderDatabase, DatabaseReader>();
            services.AddTransient<IEditorDatabase, DatabaseEditor>();
            services.AddTransient<IAuthentication, AuthenticationService>();

            services.AddSingleton<IImageHelper, ImageSourceHelper>();
            services.AddSingleton<ISHA256Helper, SHA256Helper>();
            services.AddSingleton<ILoginAttemp, LoginAttempService>();
            services.AddSingleton<IErrorHandlerDatabase, DatabaseErrorHandler>();
            services.AddSingleton<IDatabaseErrorHandlerFactory, DatabaseErrorHandlerFactory>();
            services.AddSingleton<IBackupManagerDatabase, DatabaseBackupManager>();
            services.AddSingleton<IXAMPPHelper, XAMPPHelper>();



            return services;
        }
    }
}
