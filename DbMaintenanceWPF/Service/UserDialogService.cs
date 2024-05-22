using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.View.Windows;
using DbMaintenanceWPF.View.Windows.DialogWindows;
using DbMaintenanceWPF.ViewModel;
using DbMaintenanceWPF.ViewModel.DialogViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DbMaintenanceWPF.Service
{
    class UserDialogService : IUserDialogService
    {

        public bool Edit(object item, string titleWindow, object viewModel = null)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            switch (item)
            {
                default: throw new NotSupportedException($"Редактирование объекта типа {item.GetType().Name} не поддерживается");
                case Category category:
                    return EditCategory(category, titleWindow);
                case Brand brand:
                    return EditBrand(brand, titleWindow);
                case Department department:
                    return EditDepartment(department, titleWindow);
                case Employee employee:
                    return EditEmployee(employee, titleWindow, viewModel as EmployeeVM);
            }
        }

        #region DialogWindows
        private static bool EditCategory(Category category,string titleWindow)
        {
            var view_model = new CategoryContextVM()
            {
                TextWindow = titleWindow,
                TextCategory = category.Title,
            };
            var view = new CategoryContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                category.Title = view_model.TextCategory;

                view.Close();
            };

            

            return view.ShowDialog() ?? false;
        }
        private static bool EditBrand(Brand brand, string titleWindow)
        {
            var view_model = new BrandContextVM()
            {
                TextWindow = titleWindow,
                TextBrand = brand.Title,
            };
            var view = new BrandContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                brand.Title = view_model.TextBrand;

                view.Close();
            };


            return view.ShowDialog() ?? false;
        }
        private static bool EditDepartment(Department department, string titleWindow)
        {
            var view_model = new DepartmentContextVM()
            {
                TextWindow = titleWindow,
                TextDepartment = department.Title,
            };
            var view = new DepartmentContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                department.Title = view_model.TextDepartment;

                view.Close();
            };


            return view.ShowDialog() ?? false;
        }

        private static bool EditEmployee(Employee employee, string titleWindow, EmployeeVM viewModel)
        {
            var view_model = new EmployeeContextVM(viewModel)
            {
                TextWindow = titleWindow,
            };
            var view = new EmployeeContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;


                view.Close();
            };


            return view.ShowDialog() ?? false;
        }

        #endregion

        #region ShowWindows

        public bool ShowSettingConnection(ConnectionM model, Database database, ICommandFactory commandFactory)
        {
            var view_model = new ConnectionVM(model, database, commandFactory);
            var view = new ContextConnection
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;
                view.Close();
            };

            return view.ShowDialog() ?? false;
        }

        public bool ShowAuthentication(LoginM model, IUserDialogService dialogService)
        {
            var view_model = new LoginVM(model, dialogService);
            var view = new LoginContext
            {
                DataContext = view_model,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            App.CurrentWindow.Hide();

            return view.ShowDialog() ?? false;
        }

        public bool ShowMain(object user)
        {
            var view_model = new MainVM(user);
            var view = new MainForm
            {
                DataContext = view_model,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            App.CurrentWindow.Hide();

            return view.ShowDialog() ?? false;
        }

        #endregion

        #region ShowMessages
        public bool ShowConfirm(string title, string text, bool exclamation = false) =>
            new UserMessageWindow()
            {
                VisibleButtonOk = Visibility.Visible,
                MessageTitle = title,
                MessageText = text,
                MessageImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/IconQuestion.png", UriKind.RelativeOrAbsolute))
            }.ShowDialog() == true;

        public void ShowQuestion(string title, string text)
        {
            new UserMessageWindow()
            {
                VisibleButtonOk = Visibility.Collapsed,
                MessageTitle = title,
                MessageText = text,
                MessageImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/IconQuestion.png", UriKind.RelativeOrAbsolute))
            }.ShowDialog();
        }

        public void ShowWarning(string title, string text)
        {
            new UserMessageWindow()
            {
                VisibleButtonOk = Visibility.Collapsed,
                MessageTitle = title,
                MessageText = text,
                MessageImage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/IconWarning.png", UriKind.RelativeOrAbsolute))
            }.ShowDialog();
        }

        #endregion
    }
}
