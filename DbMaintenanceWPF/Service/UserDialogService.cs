using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.View.Windows;
using DbMaintenanceWPF.View.Windows.DialogWindows;
using DbMaintenanceWPF.View.Windows.DialogWindows.PrintWindows;
using DbMaintenanceWPF.ViewModel;
using DbMaintenanceWPF.ViewModel.DialogViewModel;
using DbMaintenanceWPF.ViewModel.PrintViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DbMaintenanceWPF.Service
{
    class UserDialogService(IStorageViewModel storageViewModel) : IUserDialogService
    {
        readonly IStorageViewModel StorageViewModel = storageViewModel;

        public bool ShowPrint(string namePrint)
        {
            switch (namePrint)
            {
                default: throw new NotSupportedException($"Печать типа {namePrint} не поддерживается");
                case "MaterialStatement":
                    return ShowMaterialStatementPrint();
            }
        }

        #region PrintWindow

        private bool ShowMaterialStatementPrint()
        {
            var view_model = App.Host.Services.GetRequiredService<MaterialStatementVM>();
            {
            };
            var view = new PrintMaterialStatementContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            return view.ShowDialog() ?? false;
        }

        #endregion

        public bool Edit(object item, string titleWindow)
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
                    return EditEmployee(employee, titleWindow);
                case GiveDetail giveDetail:
                    return EditGiveDetail(giveDetail, titleWindow);
                case Give give:
                    return EditGive(give, titleWindow);
                case Post post:
                    return EditPost(post, titleWindow);
                case Product product:
                    return EditProduct(product, titleWindow);
                case Provider provider:
                    return EditProvider(provider, titleWindow);
                case Purchase purchase:
                    return EditPurchase(purchase, titleWindow);
                case Unit unit:
                    return EditUnit(unit, titleWindow);
                case User user:
                    return EditUser(user, titleWindow);
            }
        }

        #region DialogWindows
        private bool EditCategory(Category category,string titleWindow)
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

                if (view.DialogResult == true)  category.Title = view_model.TextCategory;

                view.Close();
            };

            

            return view.ShowDialog() ?? false;
        }
        private bool EditBrand(Brand brand, string titleWindow)
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

                if (view.DialogResult == true) brand.Title = view_model.TextBrand;

                view.Close();
            };


            return view.ShowDialog() ?? false;
        }
        private bool EditDepartment(Department department, string titleWindow)
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

                if (view.DialogResult == true) department.Title = view_model.TextDepartment;

                view.Close();
            };


            return view.ShowDialog() ?? false;
        }
        private bool EditEmployee(Employee employee, string titleWindow)
        {
            var view_model = new EmployeeContextVM()
            {
                TextWindow = titleWindow,
                TextSurname = employee.Surname,
                TextName = employee.Name,
                TextNumPhone = employee.NumPhone,
                TextAddress = employee.Address,
            };

            if (employee.Department != null) view_model.SelectedDepartment = employee.Department;
            if (employee.Post != null) view_model.SelectedPost = employee.Post;

            if (!string.IsNullOrEmpty(employee.Lastname))
            {
                view_model.TextLastname = employee.Lastname;
                view_model.FlagLastname = true;
            }
            if (!string.IsNullOrEmpty(employee.Email))
            {
                view_model.TextEmail = employee.Email;
                view_model.FlagEmail = true;
            }

            var view = new EmployeeContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                if (view.DialogResult == true)
                {
                    employee.Department = view_model.SelectedDepartment;
                    employee.Post = view_model.SelectedPost;
                    employee.Surname = view_model.TextSurname;
                    employee.Name = view_model.TextName;
                    if (view_model.FlagLastname) employee.Lastname = view_model.TextLastname;
                    if (view_model.FlagEmail) employee.Lastname = view_model.TextEmail;
                    employee.NumPhone = view_model.TextNumPhone;
                    employee.Address = view_model.TextAddress;
                }

                view.Close();
            };

            return view.ShowDialog() ?? false;
        }
        private bool EditGiveDetail(GiveDetail giveDetail, string titleWindow)
        {
            var view_model = new GiveDetailContextVM()
            {
                TextWindow = titleWindow,
            };

            if (giveDetail.Give != null) view_model.SelectedGive = giveDetail.Give;
            if (giveDetail.Product != null) view_model.SelectedProduct = giveDetail.Product;
            if (giveDetail.CountProduct != null) view_model.TextCountProduct = giveDetail.CountProduct.ToString();


            var view = new GiveDetailContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                if (view.DialogResult == true)
                {
                    giveDetail.Give = view_model.SelectedGive;
                    giveDetail.Product = view_model.SelectedProduct;
                    giveDetail.CountProduct = int.Parse(view_model.TextCountProduct);
                }
                

                view.Close();
            };

            return view.ShowDialog() ?? false;
        }
        private bool EditGive(Give give, string titleWindow)
        {
            var view_model = new GiveContextVM()
            {
                TextWindow = titleWindow,
            };

            if (give.Employee != null) view_model.SelectedEmployee = give.Employee;
            if (give.DateGive != null) view_model.DateGive = give.DateGive;



            var view = new GiveContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                if (view.DialogResult == true)
                {
                    give.Employee = view_model.SelectedEmployee;
                    give.DateGive = view_model.DateGive;
                }


                view.Close();
            };

            return view.ShowDialog() ?? false;
        }
        private bool EditPost(Post post, string titleWindow)
        {
            var view_model = new PostContextVM()
            {
                TextWindow = titleWindow,
                TextPost = post.Title,
            };
            var view = new PostContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                if (view.DialogResult == true) post.Title = view_model.TextPost;

                view.Close();
            };



            return view.ShowDialog() ?? false;
        }
        private bool EditProduct(Product product, string titleWindow)
        {
            var view_model = new ProductContextVM()
            {
                TextWindow = titleWindow,
                TextTitle = product.Title,
                TextModel = product.Model,
                TextOKPD = product.OKPD
            };

            if (product.Category != null) view_model.SelectedCategory = product.Category;
            if (product.Brand != null) view_model.SelectedBrand = product.Brand;
            if (product.Unit != null) view_model.SelectedUnit = product.Unit;
            if (product.CountProduct != null) view_model.TextCountProduct = product.CountProduct.ToString();
            if (product.Price != null) view_model.TextPrice = product.Price.ToString();

            if (!string.IsNullOrEmpty(product.Description))
            {
                view_model.TextDescription = product.Description;
                view_model.FlagDescription = true;
            }

            var view = new ProductContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                if (view.DialogResult == true)
                {
                    product.Category = view_model.SelectedCategory;
                    product.Brand = view_model.SelectedBrand;
                    product.Unit = view_model.SelectedUnit;
                    product.Title = view_model.TextTitle;
                    product.Model = view_model.TextModel;
                    product.CountProduct = int.Parse(view_model.TextCountProduct);
                    product.Price = int.Parse(view_model.TextPrice);
                    product.OKPD = view_model.TextOKPD;
                    if (view_model.FlagDescription) product.Description = view_model.TextDescription;
                }

                view.Close();
            };

            return view.ShowDialog() ?? false;
        }
        private bool EditProvider(Provider provider, string titleWindow)
        {
            var view_model = new ProviderContextVM()
            {
                TextWindow = titleWindow,
                TextContactName = provider.ContactName,
                TextAddress = provider.Address,
                TextNumPhone = provider.NumPhone,
            };

            if (!string.IsNullOrEmpty(provider.TitleCompany))
            {
                view_model.TextCompany = provider.TitleCompany;
                view_model.FlagCompany = true;
            }

            if (!string.IsNullOrEmpty(provider.Email))
            {
                view_model.TextEmail = provider.Email;
                view_model.FlagEmail = true;
            }

            var view = new ProviderContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                if (view.DialogResult == true)
                {;
                    provider.ContactName = view_model.TextContactName;
                    provider.Address = view_model.TextAddress;
                    provider.NumPhone = view_model.TextNumPhone;

                    if (view_model.FlagCompany) provider.TitleCompany = view_model.TextCompany;
                    if (view_model.FlagEmail) provider.Email = view_model.TextEmail;
                }

                view.Close();
            };

            return view.ShowDialog() ?? false;
        }
        private bool EditPurchase(Purchase purchase, string titleWindow)
        {
            var view_model = new PurchaseContextVM()
            {
                TextWindow = titleWindow,
            };

            if (purchase.Product != null) view_model.SelectedProduct = purchase.Product;
            if (purchase.Provider != null) view_model.SelectedProvider = purchase.Provider;
            if (purchase.CountProduct != null) view_model.TextCountProduct = purchase.CountProduct.ToString();
            if (purchase.Price != null) view_model.TextPrice = purchase.Price.ToString();

            if (purchase.DateProvide != null)
            {
                view_model.DateProvide = purchase.DateProvide;
                view_model.FlagDateProvide = true;
            }

            var view = new PurchaseContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                if (view.DialogResult == true)
                {
                    purchase.Product = view_model.SelectedProduct;
                    purchase.Provider = view_model.SelectedProvider;
                    if (view_model.FlagDateProvide) purchase.DateProvide = view_model.DateProvide;
                    purchase.CountProduct = int.Parse(view_model.TextCountProduct);
                    purchase.Price = int.Parse(view_model.TextPrice);
                }

                view.Close();
            };

            return view.ShowDialog() ?? false;
        }
        private bool EditUnit(Unit unit, string titleWindow)
        {
            var view_model = new UnitContextVM()
            {
                TextWindow = titleWindow,
                TextShortName = unit.ShortName,
                TextUnit = unit.Title,
            };

            if (unit.OKEI != null) view_model.TextOKEI = unit.OKEI.ToString();

            var view = new UnitContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                if (view.DialogResult == true)
                {
                    unit.ShortName = view_model.TextShortName;
                    unit.Title = view_model.TextUnit;
                    unit.OKEI = int.Parse(view_model.TextOKEI);
                }

                view.Close();
            };


            return view.ShowDialog() ?? false;
        }
        private bool EditUser(User user, string titleWindow)
        {
            var view_model = new UserContextVM(App.Host.Services.GetRequiredService<IImageHelper>(), App.Host.Services.GetRequiredService<IUserDialogService>())
            {
                TextWindow = titleWindow,
                TextLogin = user.Login,
                TextSurname = user.Surname,
                TextName = user.Name,
                UserPasswordHash = user.PasswordHash,
                UserSalt = user.Salt,
            };

            if (user.PasswordHash != null)
            {
                if (string.IsNullOrEmpty(user.Login))
                {
                    view_model.TextResetPassword = "Сбросить пароль";
                    view_model.VisibleChangePassword = Visibility.Collapsed;
                }
                else
                {
                    view_model.VisibleResetPassword = Visibility.Collapsed;
                    view_model.VisibleLock = Visibility.Collapsed;
                    view_model.VisibleRole = Visibility.Collapsed;
                }

                
            }
            else
            {
                view_model.VisibleChangePassword = Visibility.Collapsed;
                view_model.TextResetPassword = "Установить пароль";
            }

            if (user.Image != null) view_model.ImageUser = user.Image;

            if (user.Role != null) view_model.SelectedRole = user.Role;

            if (user.DateLock != null)
            {
                view_model.DateLock = user.DateLock;
                view_model.FlagLock = true;
            }

            var view = new UserContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;

                if (view.DialogResult == true)
                {
                    user.CountAttemp = 3;
                    user.Image = view_model.ImageUser;
                    user.Login = view_model.TextLogin;
                    user.Surname = view_model.TextSurname;
                    user.Name = view_model.TextName; 
                    user.PasswordHash = view_model.UserPasswordHash;
                    user.Salt = view_model.UserSalt;
                    user.Role = view_model.SelectedRole;

                    if (view_model.FlagLock)
                    {
                        user.IsLock = true;
                        user.DateLock = view_model.DateLock;
                    }
                    else
                    {
                        user.IsLock = false;
                        user.DateLock = null;
                    }
                }

                view.Close();
            };

            return view.ShowDialog() ?? false;
        }

        #endregion

        #region ShowWindows

        public bool ShowSettingConnection()
        {
            ConnectionVM view_model = App.Host.Services.GetRequiredService<ConnectionVM>();
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
        public bool ShowAuthentication()
        {
            LoginVM view_model = App.Host.Services.GetRequiredService<LoginVM>();
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
            var view_model = new MainVM(App.Host.Services.GetRequiredService<IUserDialogService>(), user);
            StorageViewModel.SetViewModel(view_model);
            view_model.CurrentUser = user;
            var view = new MainForm
            {
                DataContext = view_model,
                Owner = App.CurrentWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            App.CurrentWindow.Hide();

            return view.ShowDialog() ?? false;
        }
        public (string, string) ShowResetKey()
        {
            var view_model = new ResetKeyContextVM(App.Host.Services.GetRequiredService<ISHA256Helper>(), App.Host.Services.GetRequiredService<IUserDialogService>());
            var view = new KeyContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;
                view.Close();
            };

            view.ShowDialog();
            return (view_model.ResultHash, view_model.GeneratedSalt);
        }
        public string ShowChangeKey(string resultHash, string generatedSalt)
        {
            var view_model = new ChangeKeyContextVM(App.Host.Services.GetRequiredService<ISHA256Helper>(), App.Host.Services.GetRequiredService<IUserDialogService>(), resultHash, generatedSalt);
            var view = new KeyContext
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;
                view.Close();
            };

            view.ShowDialog();
            return view_model.ResultHash;
        }

        #endregion

        #region ShowMessages
        public bool ShowConfirm(string title, string text)
        {
            var view_model = new UserMessageWindowVM()
            {
                MessageTitle = title,
                MessageText = text,
                ImageMessage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/IconQuestion.png", UriKind.RelativeOrAbsolute)),
                VisibleButtonOk = Visibility.Visible
            };
            var view = new UserMessageWindow
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
        public void ShowQuestion(string title, string text)
        {
            var view_model = new UserMessageWindowVM()
            {
                MessageTitle = title,
                MessageText = text,
                ImageMessage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/IconQuestion.png", UriKind.RelativeOrAbsolute)),
                VisibleButtonOk = Visibility.Collapsed
            };
            var view = new UserMessageWindow
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;
                view.Close();
            };

            view.ShowDialog();
        }
        public void ShowWarning(string title, string text)
        {
            var view_model = new UserMessageWindowVM()
            {
                MessageTitle = title,
                MessageText = text,
                ImageMessage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/IconWarning.png", UriKind.RelativeOrAbsolute)),
                VisibleButtonOk = Visibility.Collapsed
            };
            var view = new UserMessageWindow
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;
                view.Close();
            };

            view.ShowDialog();
        }
        public string ShowInput(string title)
        {
            var view_model = new InputMessageWindowVM()
            {
                MessageTitle = title,
                ImageMessage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/IconInput.png", UriKind.RelativeOrAbsolute)),
                VisibleButtonOk = Visibility.Visible
            };
            
            var view = new InputMessageWindow
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e.Arg;
                view.Close();
            };
            view.ShowDialog();
            return view_model.MessageText;
        }
        public string ShowBrowserCatalog()
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) return dlg.SelectedPath;
            return null;
        }


        #endregion
    }
}
