using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class GiveDetailVM : Base.ViewModelBase
    {

        #region Свойства

        readonly GiveDetailM Model;
        readonly IUserDialogService UserDialog;
        public IEnumerable<GiveDetail> GiveDetails => Model.PublicListGiveDetails;
        public IEnumerable<Give> Gives => Model.PublicListGives;
        public IEnumerable<Product> Products => Model.PublicListProducts;


        bool flagGive;
        public bool FlagGive { get => flagGive; set { Set(ref flagGive, value); SelectedGive = null; } }

        bool flagProduct;
        public bool FlagProduct { get => flagProduct; set { Set(ref flagProduct, value); SelectedGive = null; } }

        Give selectedGive;
        public Give SelectedGive { get => selectedGive; set => Set(ref selectedGive, value);  }

        Product selectedProduct;
        public Product SelectedProduct { get => selectedProduct; set => Set(ref selectedProduct, value); }

        #endregion

        #region Команды


        #region AddCommand - Добавление информации о выдаче

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => p is GiveDetail;

        private void OnAddCommandExecuted(object p)
        {
            var giveDetail = (GiveDetail)p;
            if (!UserDialog.Edit(giveDetail, "Добавление информации о выдаче")) OnPropertyChanged(nameof(GiveDetails));
        }

        #endregion

        #region EditCommand - Редактирование информации о выдаче

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is GiveDetail;

        private void OnEditCommandExecuted(object p)
        {
            GiveDetail giveDetail = (GiveDetail)p;
            if (!UserDialog.Edit(giveDetail, "Редактирование информации о выдаче"))
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
            GiveDetail giveDetail = (GiveDetail)p;
            if (UserDialog.ShowConfirm("Удаление информации о выдаче", "Удалить выбранную информацию о выдаче?"))
            {
                Model.Remove(giveDetail);
                OnPropertyChanged(nameof(GiveDetails));
            }
        }

        #endregion

        #endregion

        public GiveDetailVM(GiveDetailM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }
    }
}
