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
    public class ProductContextVM : ViewModelBase
    {
        public ProductContextVM()
        {
            Categories = App.Host.Services.GetRequiredService<ICreaterEntity<Category>>().GetList();
            Brands = App.Host.Services.GetRequiredService<ICreaterEntity<Brand>>().GetList();
            Units = App.Host.Services.GetRequiredService<ICreaterEntity<Unit>>().GetList();

            SelectedCategory = Categories.FirstOrDefault();
            SelectedBrand = Brands.FirstOrDefault();
            SelectedUnit = Units.FirstOrDefault();
        }

        #region Свойства

        public event EventHandler<EventArgs<bool>> Complete;

        IEnumerable<Category> categories;
        public IEnumerable<Category> Categories { get => categories; set => Set(ref categories, value); }

        IEnumerable<Brand> brands;
        public IEnumerable<Brand> Brands { get => brands; set => Set(ref brands, value); }

        IEnumerable<Unit> units;
        public IEnumerable<Unit> Units { get => units; set => Set(ref units, value); }

        Category selectedCategory;
        public Category SelectedCategory { get => selectedCategory; set { Set(ref selectedCategory, value); CanCommitCommandExecute(null); } }

        Brand selectedBrand;
        public Brand SelectedBrand { get => selectedBrand; set { Set(ref selectedBrand, value); CanCommitCommandExecute(null); } }

        Unit selectedUnit;
        public Unit SelectedUnit { get => selectedUnit; set { Set(ref selectedUnit, value); CanCommitCommandExecute(null); } }

        string textWindow;
        public string TextWindow { get => textWindow; set => Set(ref textWindow, value); }

        string textTitle;
        public string TextTitle { get => textTitle; set { Set(ref textTitle, value); CanCommitCommandExecute(null); } }

        string textModel;
        public string TextModel { get => textModel; set { Set(ref textModel, value); CanCommitCommandExecute(null); } }

        string textCountProduct;
        public string TextCountProduct { get => textCountProduct; set { Set(ref textCountProduct, value); CanCommitCommandExecute(null); } }

        string textPrice;
        public string TextPrice { get => textPrice; set { Set(ref textPrice, value); CanCommitCommandExecute(null); } }

        string textOKPD;
        public string TextOKPD { get => textOKPD; set { Set(ref textOKPD, value); CanCommitCommandExecute(null); } }

        string textDescription;
        public string TextDescription { get => textDescription; set { Set(ref textDescription, value); CanCommitCommandExecute(null); } }

        bool flagDescription;
        public bool FlagDescription
        {
            get => flagDescription;
            set
            {
                Set(ref flagDescription, value);
                CanCommitCommandExecute(null);
                if (!flagDescription) TextDescription = "";
            }
        }


        #endregion

        #region Команды

        #region CommitCommand - Принять изменения

        private ICommand _CommitCommand;

        public ICommand CommitCommand => _CommitCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) =>
            !string.IsNullOrEmpty(TextTitle) &&
            !string.IsNullOrEmpty(TextModel) &&
            !string.IsNullOrEmpty(TextCountProduct) && (int.Parse(TextCountProduct) >= 0) &&
            !string.IsNullOrEmpty(TextPrice) && (int.Parse(TextPrice) >= 0) &&
            !string.IsNullOrEmpty(TextOKPD) &&
            (!FlagDescription || !string.IsNullOrEmpty(TextDescription));

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
