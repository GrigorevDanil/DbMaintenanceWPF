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
    public class GiveDetailVM(GiveDetailM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {

        #region Свойства

        readonly GiveDetailM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<GiveDetail> GiveDetails => Model.PublicListGiveDetails;
        public IEnumerable<Give> Gives => Model.PublicListGives;
        public IEnumerable<Product> Products => Model.PublicListProducts;

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

        public void UpdateList() => OnPropertyChanged(nameof(GiveDetails));

        #region AddCommand - Добавление информации о выдаче

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var giveDetail = new GiveDetail();
            if (UserDialog.Edit(giveDetail, "Добавление информации о выдаче"))
            {
                Model.Create(giveDetail);
                OnPropertyChanged(nameof(GiveDetails));
            }
        }

        #endregion

        #region EditCommand - Редактирование информации о выдаче

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is GiveDetail;

        private void OnEditCommandExecuted(object p)
        {
            var giveDetail = (GiveDetail)p;
            if (UserDialog.Edit(giveDetail, "Редактирование информации о выдаче"))
            {
                Model.Update(giveDetail);
                OnPropertyChanged(nameof(GiveDetails));
            }
        }

        #endregion

        #region RemoveCommand - Удаление информации о выдаче

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is GiveDetail;

        private void OnRemoveCommandExecuted(object p)
        {
            var giveDetail = (GiveDetail)p;
            if (UserDialog.ShowConfirm("Удаление информации о выдаче", "Удалить выбранную информацию о выдаче?"))
            {
                Model.Remove(giveDetail);
                OnPropertyChanged(nameof(GiveDetails));
            }
        }

        #endregion

        #region MultiplyRemoveCommand - Множественное удаление информаций о выдаче

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление информаций о выдаче", "Удалить выбранные информации о выдаче?"))
            {
                foreach (GiveDetail giveDetail in GiveDetails) if (giveDetail.IsSelected) Model.Remove(giveDetail);
                OnPropertyChanged(nameof(GiveDetails));
            }
        }

        #endregion
        #endregion
    }
}
