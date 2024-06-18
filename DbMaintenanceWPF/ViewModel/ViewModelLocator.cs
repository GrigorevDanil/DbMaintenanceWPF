using DbMaintenanceWPF.ViewModel.DialogViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using System.Windows;
using DbMaintenanceWPF.ViewModel.Base;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Service.Interface;
using System.Windows.Threading;
using System;
using DbMaintenanceWPF.Service;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Repositories;

namespace DbMaintenanceWPF.ViewModel
{
    public class ViewModelLocator : ViewModelBase
    {
        public ViewModelLocator()
        {
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(NavigatorCurrentViewCommand, NavigatorCurrentViewExecute));
            DispatcherTimer timerWatcher = new() { Interval = TimeSpan.FromSeconds(60) };
            timerWatcher.Tick += TimerWatcher_Tick;
            timerWatcher.Start();

        }

        private void UpdateList()
        {
            BrandModel.UpdateList();
            CategoryModel.UpdateList();
            DepartmentModel.UpdateList();
            EmployeeModel.UpdateList();
            GiveDetailModel.UpdateList();
            GiveModel.UpdateList();
            PostModel.UpdateList();
            ProductModel.UpdateList();
            ProviderModel.UpdateList();
            PurchaseModel.UpdateList();
            UnitModel.UpdateList();
            UserModel.UpdateList();
        }

        #region Свойства

        readonly IWatcherDatabase WatcherService = App.Host.Services.GetRequiredService<IWatcherDatabase>();
        public SplashScreenVM SplashScreenModel => App.Host.Services.GetRequiredService<SplashScreenVM>();
        public BrandVM BrandModel => App.Host.Services.GetRequiredService<BrandVM>();
        public CategoryVM CategoryModel => App.Host.Services.GetRequiredService<CategoryVM>();
        public CopyVM CopyModel => App.Host.Services.GetRequiredService<CopyVM>();
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
        public AccountVM AccountModel => App.Host.Services.GetRequiredService<AccountVM>();
        public InfoVM InfoModel => App.Host.Services.GetRequiredService<InfoVM>();

        private object _currentView;
        public object CurrentView { get => _currentView; set => Set(ref _currentView, value); }

        #endregion

        #region Команды

        private void TimerWatcher_Tick(object sender, EventArgs e)
        {
            WatcherService.SynchronizeRecordings();
            UpdateList();
        }

        public static RoutedCommand NavigatorCurrentViewCommand = new RoutedCommand();
        private void NavigatorCurrentViewExecute(object sender, ExecutedRoutedEventArgs e)
        {
            switch (e.Parameter)
            {
                case nameof(BrandModel): CurrentView = BrandModel; break;
                case nameof(CategoryModel): CurrentView = CategoryModel; break;
                case nameof(CopyModel): CurrentView = CopyModel; break;
                case nameof(DepartmentModel): CurrentView = DepartmentModel; break;
                case nameof(EmployeeModel): CurrentView = EmployeeModel; break;
                case nameof(GiveDetailModel): CurrentView = GiveDetailModel; break;
                case nameof(GiveModel): CurrentView = GiveModel; break;
                case nameof(PostModel): CurrentView = PostModel; break;
                case nameof(ProductModel): CurrentView = ProductModel; break;
                case nameof(ProviderModel): CurrentView = ProviderModel; break;
                case nameof(PurchaseModel): CurrentView = PurchaseModel; break;
                case nameof(UnitModel): CurrentView = UnitModel; break;
                case nameof(UserModel): CurrentView = UserModel; break;
                case nameof(AccountModel): CurrentView = AccountModel; break;
                case nameof(InfoModel): CurrentView = InfoModel; break;
            }
        }

        #endregion

    }
}

