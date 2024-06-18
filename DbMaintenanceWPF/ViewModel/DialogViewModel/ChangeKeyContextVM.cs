using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Service;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel.DialogViewModel
{
    public class ChangeKeyContextVM : ViewModelBase
    {
        public ChangeKeyContextVM(ISHA256Helper sHA256Helper,IUserDialogService dialogService, string installedPasswordHash, string installedSalt)
        {
            TextWindow = "Смена пароля";
            TextPassword1 = "Текущий пароль*:";
            TextPassword2 = "Новый пароль*:";
            SHA256Helper = sHA256Helper;
            DialogService = dialogService;
            InstalledPasswordHash = installedPasswordHash;
            InstalledSalt = installedSalt;
        }

        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        readonly ISHA256Helper SHA256Helper;
        readonly IUserDialogService DialogService;
        readonly string InstalledPasswordHash;
        readonly string InstalledSalt;

        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        string textPassword1;
        public string TextPassword1 { get => textPassword1; set => Set(ref textPassword1, value); }

        string textPassword2;
        public string TextPassword2 { get => textPassword2; set => Set(ref textPassword2, value); }

        string currentPassword;
        public string CurrentPassword { get => currentPassword; set => Set(ref currentPassword, value); }

        string repeatPassword;
        public string RepeatPassword { get => repeatPassword; set => Set(ref repeatPassword, value); }

        string textError;
        public string TextError { get => textError; set => Set(ref textError, value); }

        Visibility visibilityEror;
        public Visibility VisibilityEror { get => visibilityEror; set => Set(ref visibilityEror, value); }

        public string ResultHash;

        #endregion

        #region Команды

        #region ShowError - Показать ошибку

        #region ShowEror - Вывод ошибки
        void ShowEror(string textEror)
        {
            TextError = textEror;
            VisibilityEror = Visibility.Visible;
            Task.Delay(200).ContinueWith(t => VisibilityEror = Visibility.Hidden);
        }

        #endregion

        #endregion

        #region PasswordRequirementCommand - Требования к паролю

        private ICommand _PasswordRequirementCommand;

        public ICommand PasswordRequirementCommand => _PasswordRequirementCommand
            ??= new RelayCommand(OnPasswordRequirementCommandExecuted, CanPasswordRequirementCommandExecute);

        private bool CanPasswordRequirementCommandExecute(object p) => true;
        private void OnPasswordRequirementCommandExecuted(object p) => 
            DialogService.ShowQuestion("Требования к паролю", 
                "1. Должен содержать как минимум 8 символов." +
                "\r\n2. Должен включать как минимум одну заглавную букву." +
                "\r\n3. Должен включать как минимум одну строчную букву." +
                "\r\n4. Должен содержать как минимум одну цифру." +
                "\r\n5. Должен содержать как минимум один специальный символ (например, !, @, #, и т.д.).");

        #endregion

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => true;
        private void OnCommitCommandExecuted(object p)
        {
            ResultHash = SHA256Helper.HashPassword(CurrentPassword, InstalledSalt);
            if (ResultHash != InstalledPasswordHash)
            {
                ShowEror("Набранный пароль не совпадает с текущим");
                return;
            }
            if (!Regex.IsMatch(RepeatPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$"))
            {
                ShowEror("Набранный пароль не соответствует требованиям");
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
