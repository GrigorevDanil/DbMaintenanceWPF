using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.ItemModels;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    public class GiveVM(GiveM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {

        #region Свойства

        readonly GiveM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<Give> Gives => Model.PublicListGives;
        public IEnumerable<Employee> Employees => Model.PublicListEmployees;

        public Visibility VisibleComponent
        {
            get
            {
                var user = (storageViewModel.GetViewModel(nameof(MainVM)) as MainVM)?.CurrentUser as User;
                return user?.Role == "Пользователь" ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        #endregion

        #region Команды

        public void UpdateList() => OnPropertyChanged(nameof(Gives));

        #region AddCommand - Добавление выдачи

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var give = new Give();
            if (UserDialog.Edit(give, "Добавление выдачи"))
            {
                Model.Create(give);
                OnPropertyChanged(nameof(Gives));
            }
        }

        #endregion

        #region EditCommand - Редактирование выдачи

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Give;

        private void OnEditCommandExecuted(object p)
        {
            Give give = (Give)p;
            if (UserDialog.Edit(give, "Редактирование выдачи"))
            {
                Model.Update(give);
                OnPropertyChanged(nameof(Gives));
            }
        }

        #endregion

        #region RemoveCommand - Удаление выдачи

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Give;

        private void OnRemoveCommandExecuted(object p)
        {
            Give give = (Give)p;
            if (UserDialog.ShowConfirm("Удаление выдачи", "Удалить выбранную выдачу?"))
            {
                Model.Remove(give);
                OnPropertyChanged(nameof(Gives));
            }
        }

        #endregion

        #region MultiplyRemoveCommand - Множественное удаление выдач

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление выдач", "Удалить выбранные выдачи?"))
            {
                foreach (Give give in Gives) if (give.IsSelected) Model.Remove(give);
                OnPropertyChanged(nameof(Gives));
            }
        }

        #endregion
        #endregion
    }
}
