using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Factories;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Interface.Factories;
using DbMaintenanceWPF.Service.Repositories;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddSingleton<ICreaterEntity<Brand>, CreaterEntityBrand>();
            services.AddSingleton<ICreaterEntity<Category>, CreaterEntityCategory>();
            services.AddSingleton<ICreaterEntity<Department>, CreaterEntityDepartment>();
            services.AddSingleton<ICreaterEntity<Employee>, CreaterEntityEmployee>();
            services.AddSingleton<ICreaterEntity<GiveDetail>, CreaterEntityGiveDetail>();
            services.AddSingleton<ICreaterEntity<Give>, CreaterEntityGive>();
            services.AddSingleton<ICreaterEntity<Post>, CreaterEntityPost>();
            services.AddSingleton<ICreaterEntity<Product>, CreaterEntityProduct>();
            services.AddSingleton<ICreaterEntity<Provider>, CreaterEntityProvider>();
            services.AddSingleton<ICreaterEntity<Purchase>, CreaterEntityPurchase>();
            services.AddSingleton<ICreaterEntity<Unit>, CreaterEntityUnit>();
            services.AddSingleton<ICreaterEntity<User>, CreaterEntityUser>();
            services.AddSingleton<ICreaterEntity<UserServer>, CreaterEntityUserServer>();



            services.AddTransient<IUserDialogService, UserDialogService>();
            services.AddTransient<IConnectionDatabase, DatabaseConnection>();
            services.AddTransient<IReaderDatabase, DatabaseReader>();
            services.AddTransient<IEditorDatabase, DatabaseEditor>();
            services.AddTransient<IAuthentication, AuthenticationService>();

            services.AddTransient<IImageHelper, ImageSourceHelper>();
            services.AddTransient<ISHA256Helper, SHA256Helper>();
            services.AddTransient<ILoginAttemp, LoginAttempService>();
            services.AddTransient<IErrorHandlerDatabase, DatabaseErrorHandler>();
            services.AddTransient<IDatabaseErrorHandlerFactory, DatabaseErrorHandlerFactory>();
            services.AddTransient<IBackupManagerDatabase, DatabaseBackupManager>();
            services.AddTransient<IXAMPPHelper, XAMPPHelper>();

            services.AddTransient<IPrint, PrintService>();


            services.AddSingleton<IStorageViewModel, StorageViewModel>();

            services.AddSingleton<IWatcherDatabase, WatcherDatabaseService>();





            return services;
        }
    }
}
