using DbMaintenanceWPF.Infrastructure.Commands.Factories;
using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models.ItemModels;
using DbMaintenanceWPF.Models.PrintModels;
using DbMaintenanceWPF.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace DbMaintenanceWPF.Models
{
    public static class Registrator
    {
        public static IServiceCollection RegisterModels(this IServiceCollection services)
        {
            services.AddSingleton<Database>(provider =>
            {
                var iniManager = provider.GetRequiredService<IINIManager>();
                return new Database(new MySqlConnection(iniManager.GetPrivateString("main", "StringConnection")));
            });

            services.AddSingleton<ICommandFactory, CommandFactory>();

            services.AddSingleton<BrandM>();
            services.AddSingleton<CategoryM>();
            services.AddSingleton<DepartmentM>();
            services.AddSingleton<EmployeeM>();
            services.AddSingleton<GiveDetailM>();
            services.AddSingleton<GiveM>();
            services.AddSingleton<PostM>();
            services.AddSingleton<ProductM>();
            services.AddSingleton<ProviderM>();
            services.AddSingleton<PurchaseM>();
            services.AddSingleton<UnitM>();
            services.AddSingleton<UserM>();
            services.AddSingleton<LoginM>();
            services.AddSingleton<ConnectionM>();
            services.AddSingleton<CopyM>();


            services.AddSingleton<MaterialStatementM>();



            return services;
        }
    }
}
