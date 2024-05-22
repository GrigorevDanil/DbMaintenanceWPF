using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class UnitVM : Base.ViewModelBase
    {
        #region Свойства

        readonly UnitM Model;
        readonly IUserDialogService UserDialog;
        public IEnumerable<Unit> Units => Model.PublicListUnits;

        #endregion

        #region Команды

        #region AddCommand - Добавление единицы

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => p is Unit;

        private void OnAddCommandExecuted(object p)
        {
            var unit = (Unit)p;
            if (!UserDialog.Edit(unit, "Добавление единицы")) OnPropertyChanged(nameof(Units));
        }

        #endregion

        #region EditCommand - Редактирование единицы

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Unit;

        private void OnEditCommandExecuted(object p)
        {
            Unit unit = (Unit)p;
            if (!UserDialog.Edit(unit, "Редактирование единицы"))
            {
                Model.Update(unit);
                OnPropertyChanged(nameof(Units));
            }
        }

        #endregion

        #region RemoveCommand - Удаление единицы

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Unit;

        private void OnRemoveCommandExecuted(object p)
        {
            Unit unit = (Unit)p;
            if (UserDialog.ShowConfirm("Удаление единицы", "Удалить выбранную единицу?"))
            {
                Model.Remove(unit);
                OnPropertyChanged(nameof(Units));
            }
        }

        #endregion

        #endregion

        public UnitVM(UnitM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }
    }
}
