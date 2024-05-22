using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    class EmployeeContextVM(EmployeeVM viewModel) : ViewModelBase
    {
        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        public IEnumerable<Department> Departments => viewModel.Departments;
        public IEnumerable<Post> Posts => viewModel.Posts;

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

        #endregion


        #region Команды

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => true ;
        private void OnCommitCommandExecuted(object p)
        {
            if (CanCancelCommandExecute(p)) Complete?.Invoke(this, true);
        }

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
