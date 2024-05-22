using DbMaintenanceWPF.ViewModel.DialogViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DbMaintenanceWPF.ViewModel
{
    internal static class Registrator
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddTransient<BrandVM>();
            services.AddTransient<CategoryVM>();
            services.AddTransient<DepartmentVM>();
            services.AddTransient<EmployeeVM>();
            services.AddTransient<GiveDetailVM>();
            services.AddTransient<GiveVM>();
            services.AddTransient<PostVM>();
            services.AddTransient<ProductVM>();
            services.AddTransient<ProviderVM>();
            services.AddTransient<PurchaseVM>();
            services.AddTransient<UnitVM>();
            services.AddTransient<UserVM>();

            services.AddTransient<BrandContextVM>();
            services.AddTransient<CategoryContextVM>();
            services.AddTransient<DepartmentContextVM>();

            services.AddTransient<SplashScreenVM>();
            services.AddTransient<LoginVM>();
            services.AddTransient<ConnectionVM>();
            services.AddTransient<MainVM>();



            return services;
        }

    }
}
