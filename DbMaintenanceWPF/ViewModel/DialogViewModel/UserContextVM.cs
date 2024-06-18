using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    public class UserContextVM : ViewModelBase
    {
        public UserContextVM(IImageHelper imageHelper, IUserDialogService dialogService)
        {
            Roles = new List<string>() { "Администратор", "Заведующий", "Пользователь" };

            SelectedRole = Roles.FirstOrDefault();
            DateLock = DateTime.Now;
            ImageHelper = imageHelper;
            DialogService = dialogService;

            ImageUser = ImageHelper.DefaultImageUser;
        }

        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        readonly IImageHelper ImageHelper;
        readonly IUserDialogService DialogService;

        IEnumerable<string> roles;
        public IEnumerable<string> Roles { get => roles; set => Set(ref roles, value); }

        string selectedRole;
        public string SelectedRole { get => selectedRole; set { Set(ref selectedRole, value); CanCommitCommandExecute(null); } }

        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        string textLogin;
        public string TextLogin { get => textLogin; set { Set(ref textLogin, value); CanCommitCommandExecute(null); } }

        string textSurname;
        public string TextSurname { get => textSurname; set { Set(ref textSurname, value); CanCommitCommandExecute(null); } }

        string textName;
        public string TextName { get => textName; set { Set(ref textName, value); CanCommitCommandExecute(null); } }

        ImageSource imageUser;
        public ImageSource ImageUser { get => imageUser; set => Set(ref imageUser, value); }

        DateTime? dateLock;
        public DateTime? DateLock { get => dateLock; set => Set(ref dateLock, value); }

        bool flagLock;
        public bool FlagLock { get => flagLock; set => Set(ref flagLock, value); }

        Visibility visibleChangePassword;
        public Visibility VisibleChangePassword { get => visibleChangePassword; set => Set(ref visibleChangePassword, value); }

        Visibility visibleResetPassword;
        public Visibility VisibleResetPassword { get => visibleResetPassword; set => Set(ref visibleResetPassword, value); }

        Visibility visibleLock;
        public Visibility VisibleLock { get => visibleLock; set => Set(ref visibleLock, value); }

        Visibility visibleRole;
        public Visibility VisibleRole { get => visibleRole; set => Set(ref visibleRole, value); }

        string textResetPassword;
        public string TextResetPassword { get => textResetPassword; set { Set(ref textResetPassword, value); CanCommitCommandExecute(null); } }


        public string UserPasswordHash { get; set; }
        public string UserSalt { get; set; }

        #endregion

        #region Команды

        #region ChangeImageCommand - Сменить картинку

        private ICommand _ChangeImageCommand;

        public ICommand ChangeImageCommand => _ChangeImageCommand
            ??= new RelayCommand(OnChangeImageCommandExecuted, CanChangeImageCommandExecute);

        private bool CanChangeImageCommandExecute(object p) => true;

        private void OnChangeImageCommandExecuted(object p)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

            if (dlg.ShowDialog() == true) ImageUser = ImageHelper.ReduceBitmapImageSize(new BitmapImage(new Uri(dlg.FileName, UriKind.Relative)));
        }

        #endregion

        #region ResetImageCommand - Сбросить картинку до по умолчанию

        private ICommand _ResetImageCommand;

        public ICommand ResetImageCommand => _ResetImageCommand
            ??= new RelayCommand(OnResetImageCommandExecuted, CanResetImageCommandExecute);

        private bool CanResetImageCommandExecute(object p) => true;

        private void OnResetImageCommandExecuted(object p) => ImageUser = ImageHelper.DefaultImageUser;


        #endregion

        #region ChangePasswordCommand - Сменить пароль

        private ICommand _ChangePasswordCommand;

        public ICommand ChangePasswordCommand => _ChangePasswordCommand
            ??= new RelayCommand(OnChangePasswordCommandExecuted, CanChangePasswordCommandExecute);

        private bool CanChangePasswordCommandExecute(object p) => true;

        private void OnChangePasswordCommandExecuted(object p)
        {
            string hash = DialogService.ShowChangeKey(UserPasswordHash, UserSalt);
            if (!string.IsNullOrEmpty(hash)) UserPasswordHash = hash;   
        }


        #endregion

        #region ResetPasswordCommand - Сбросить пароль

        private ICommand _ResetPasswordCommand;

        public ICommand ResetPasswordCommand => _ResetPasswordCommand
            ??= new RelayCommand(OnResetPasswordCommandExecuted, CanResetPasswordCommandExecute);

        private bool CanResetPasswordCommandExecute(object p) => true;

        private void OnResetPasswordCommandExecuted(object p)
        {
            (string hash, string salt) = DialogService.ShowResetKey();
            (UserPasswordHash, UserSalt) = (hash, salt);
        }


        #endregion

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) =>
            !string.IsNullOrEmpty(TextLogin) &&
            !string.IsNullOrEmpty(TextSurname) &&
            !string.IsNullOrEmpty(TextName);

        private void OnCommitCommandExecuted(object p)
        {
            if (UserPasswordHash == null)
            {
                DialogService.ShowWarning("Ошибка создания", "Вы не установили пароль");
                return;
            }
            Complete?.Invoke(this, true);
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
