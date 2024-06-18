using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.DialogViewModel;
using DbMaintenanceWPF.ViewModel.PrintViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DbMaintenanceWPF.ViewModel
{
    public static class Registrator
    {

        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<BrandVM>();
            services.AddSingleton<CategoryVM>();
            services.AddSingleton<DepartmentVM>();
            services.AddSingleton<EmployeeVM>();
            services.AddSingleton<GiveDetailVM>();
            services.AddSingleton<GiveVM>();
            services.AddSingleton<PostVM>();
            services.AddSingleton<ProductVM>();
            services.AddSingleton<ProviderVM>();
            services.AddSingleton<PurchaseVM>();
            services.AddSingleton<UnitVM>();
            services.AddSingleton<UserVM>();
            services.AddTransient<SplashScreenVM>();
            services.AddTransient<LoginVM>();
            services.AddTransient<ConnectionVM>();
            services.AddTransient<MainVM>();
            services.AddTransient<CopyVM>();
            services.AddTransient<AccountVM>();
            services.AddTransient<InfoVM>();

            services.AddTransient<MaterialStatementVM>();

            return services;
        }

    }
}
