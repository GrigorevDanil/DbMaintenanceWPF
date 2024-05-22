using DbMaintenanceWPF.ViewModel.DialogViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace DbMaintenanceWPF.ViewModel
{
    class ViewModelLocator
    {
        public SplashScreenVM SplashScreenModel => App.Host.Services.GetRequiredService<SplashScreenVM>();
        public LoginVM LoginModel => App.Host.Services.GetRequiredService<LoginVM>();
        public ConnectionVM ConnectionModel => App.Host.Services.GetRequiredService<ConnectionVM>();
        public MainVM MainModel => App.Host.Services.GetRequiredService<MainVM>();


        public BrandVM BrandModel => App.Host.Services.GetRequiredService<BrandVM>();
        public CategoryVM CategoryModel => App.Host.Services.GetRequiredService<CategoryVM>();
        public DepartmentVM DepartmentModel => App.Host.Services.GetRequiredService<DepartmentVM>();
        public EmployeeVM EmployeeModel => App.Host.Services.GetRequiredService<EmployeeVM>();
        public GiveDetailVM GiveDetailModel => App.Host.Services.GetRequiredService<GiveDetailVM>();
        public GiveVM GiveModel => App.Host.Services.GetRequiredService<GiveVM>();
        public PostVM PostModel => App.Host.Services.GetRequiredService<PostVM>();
        public ProductVM ProductModel => App.Host.Services.GetRequiredService<ProductVM>();
        public ProviderVM ProviderModel => App.Host.Services.GetRequiredService<ProviderVM>();
        public PurchaseVM PurchaseModel => App.Host.Services.GetRequiredService<PurchaseVM>();
        public UnitVM UnitModel => App.Host.Services.GetRequiredService<UnitVM>();
        public UserVM UserModel => App.Host.Services.GetRequiredService<UserVM>();



        public BrandContextVM BrandContextModel => App.Host.Services.GetRequiredService<BrandContextVM>();
        public CategoryContextVM CategoryContextModel => App.Host.Services.GetRequiredService<CategoryContextVM>();
        public DepartmentContextVM DepartmentContextModel => App.Host.Services.GetRequiredService<DepartmentContextVM>();
        public EmployeeContextVM EmployeeContextModel => App.Host.Services.GetRequiredService<EmployeeContextVM>();

    }
}

