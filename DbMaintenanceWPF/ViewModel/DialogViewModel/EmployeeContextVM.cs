using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    public class EmployeeContextVM : ViewModelBase
    {
        public EmployeeContextVM()
        {
            Departments = App.Host.Services.GetRequiredService<ICreaterEntity<Department>>().GetList();
            Posts = App.Host.Services.GetRequiredService<ICreaterEntity<Post>>().GetList();

            SelectedDepartment = Departments.FirstOrDefault();
            SelectedPost = Posts.FirstOrDefault();

        }

        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        IEnumerable<Department> departments;
        public IEnumerable<Department> Departments { get => departments; set => Set(ref departments, value); }

        IEnumerable<Post> posts;
        public IEnumerable<Post> Posts { get => posts; set => Set(ref posts, value); }

        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        Department selectedDepartment;
        public Department SelectedDepartment { get => selectedDepartment; set { Set(ref selectedDepartment, value); CanCommitCommandExecute(null); } }

        Post selectedPost;
        public Post SelectedPost { get => selectedPost; set { Set(ref selectedPost, value); CanCommitCommandExecute(null); } }

        string textSurname;
        public string TextSurname { get => textSurname; set { Set(ref textSurname, value); CanCommitCommandExecute(null); } }

        string textName;
        public string TextName { get => textName; set { Set(ref textName, value); CanCommitCommandExecute(null); } }

        string textLastname;
        public string TextLastname { get => textLastname; set { Set(ref textLastname, value); CanCommitCommandExecute(null); } }

        string textEmail;
        public string TextEmail { get => textEmail; set { Set(ref textEmail, value); CanCommitCommandExecute(null); } }

        string textNumPhone;
        public string TextNumPhone { get => textNumPhone; set { Set(ref textNumPhone, value); CanCommitCommandExecute(null); } }

        string textAddress;
        public string TextAddress { get => textAddress; set { Set(ref textAddress, value); CanCommitCommandExecute(null); } }

        bool flagLastname;
        public bool FlagLastname { get => flagLastname; 
            set 
            { 
                Set(ref flagLastname, value);
                CanCommitCommandExecute(null);
                if (!flagLastname) TextLastname = "";
            } 
        }

        bool flagEmail;
        public bool FlagEmail { get => flagEmail; 
            set 
            {
                Set(ref flagEmail, value);
                CanCommitCommandExecute(null);
                if (!flagEmail) TextEmail = "";
            } 
        }

        #endregion

        #region Команды

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) =>
            !string.IsNullOrEmpty(TextSurname) &&
            !string.IsNullOrEmpty(TextName) &&
            !string.IsNullOrEmpty(TextNumPhone) &&
             Regex.IsMatch(TextNumPhone, @"^\+7 \(\d{3}\) \d{3}-\d{4}$") &&
            !string.IsNullOrEmpty(TextAddress) &&
            (!FlagLastname || !string.IsNullOrEmpty(TextLastname)) &&
            (!FlagEmail || !string.IsNullOrEmpty(TextEmail));

        private void OnCommitCommandExecuted(object p) => Complete?.Invoke(this, true);

        #endregion

        #region CancelCommand - Отменить изменения

        private ICommand _CancelCommand;

        public ICommand CancelCommand => _CancelCommand
            ??= new RelayCommand(OnCancelCommandExecuted, CanCancelCommandExecute);

        private bool CanCancelCommandExecute(object p) => true;

        private void OnCancelCommandExecuted(object p) => Complete?.Invoke(this, false);

        #endregion

        #endregion
    }
}
