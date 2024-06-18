using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.ItemModels;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    public class UnitVM(UnitM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {
        #region Свойства

        readonly UnitM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<Unit> Units => Model.PublicListUnits;

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

        public void UpdateList() => OnPropertyChanged(nameof(Units));

        #region AddCommand - Добавление единицы

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var unit = new Unit();
            if (UserDialog.Edit(unit, "Добавление единицы"))
            {
                Model.Create(unit);
                OnPropertyChanged(nameof(Units));
            }
        }

        #endregion

        #region EditCommand - Редактирование единицы

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Unit;

        private void OnEditCommandExecuted(object p)
        {
            Unit unit = (Unit)p;
            if (UserDialog.Edit(unit, "Редактирование единицы"))
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

        #region MultiplyRemoveCommand - Множественное удаление единиц

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление единиц", "Удалить выбранные единицы?"))
            {
                foreach (Unit unit in Units) if (unit.IsSelected) Model.Remove(unit);
                OnPropertyChanged(nameof(Units));
            }
        }

        #endregion
        #endregion
    }
}
