using ClosedXML.Report;
using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.ItemModels;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Models.PrintModels;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel.PrintViewModel
{
    public class MaterialStatementVM(MaterialStatementM model, IUserDialogService dialogService, IPrint printService, ICommandFactory commandFactory, IReaderDatabase readerDatabase) : ViewModelBase
    {
        #region Свойства

        readonly MaterialStatementM Model = model;
        readonly IUserDialogService DialogService = dialogService;
        readonly IPrint PrintService = printService;
        readonly ICommandFactory CommandFactory = commandFactory;
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public IEnumerable<Employee> Employees => Model.PublicListEmployees;
        public IEnumerable<Give> Gives => Model.PublicListGives;
        public IEnumerable<Department> Departments => Model.PublicListDepartments;
        public IEnumerable<Give> SelectedGives => Model.PublicSelectedGives;
        public IEnumerable<GiveDetail> IncomingProducts => Model.PublicIncommingProducts;

        Employee selectedMaterialPerson = model.PublicListEmployees.FirstOrDefault();
        public Employee SelectedMaterialPerson { get => selectedMaterialPerson; set => Set(ref selectedMaterialPerson, value); }

        Department selectedDepartment = model.PublicListDepartments.FirstOrDefault();
        public Department SelectedDepartment { get => selectedDepartment; set => Set(ref selectedDepartment, value); }

        string textStructureName;
        public string TextStructureName { get => textStructureName; set => Set(ref textStructureName, value); }

        DateTime dateProvide = DateTime.Now;
        public DateTime DateProvide { get => dateProvide; set { Set(ref dateProvide, value); Model.ClearList(); } }

        Give selectedGive;
        public Give SelectedGive { get => selectedGive; set => Set(ref selectedGive, value); }

        Give selectedGiveInSelected;
        public Give SelectedGiveInSelected { get => selectedGiveInSelected; set => Set(ref selectedGiveInSelected, value); }

        string textSum;
        public string TextSum { get => textSum; set => Set(ref textSum, value); }

        int Sum { get; set; }


        #endregion

        #region Команды

        #region UpdateTotalSum - Обновление итоговой суммы

        private void UpdateTotalSum()
        {
            Sum = 0;
            foreach(Give give in SelectedGives) Sum += int.Parse(ReaderDatabase.OperationSelect("SELECT TotalValueGivenToEmployee(@idEmp)", [give.Employee.Id]).Rows[0][0].ToString());
            TextSum = Sum + " руб";
        }

        #endregion

        #region AddCommand - Добавление выдачи

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            if (SelectedGive is null) return;
            Model.AddGive(SelectedGive);
            SelectedGive = null;
            OnPropertyChanged(nameof(SelectedGives));
            UpdateTotalSum();
        }
        #endregion

        #region RemoveCommand - Удаление выдачи

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private bool CanRemoveCommandExecute(object p) => true;

        private void OnRemoveCommandExecuted(object p)
        {
            if (SelectedGiveInSelected is null) return;
            Model.RemoveGive(SelectedGiveInSelected);
            SelectedGiveInSelected = null;
            OnPropertyChanged(nameof(SelectedGives));
            UpdateTotalSum();
        }

        #endregion

        #region PrintCommand - Выполнить печать

        private ICommand _PrintCommand;

        public ICommand PrintCommand => _PrintCommand
            ??= new RelayCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

        private bool CanCommitCommandExecute(object p) => SelectedGives.Count() != 0 && !string.IsNullOrEmpty(TextStructureName);
        private void OnCommitCommandExecuted(object p)
        {
            if (Sum > 10000)
            {
                DialogService.ShowWarning("Ошибка печати", "Сумма выдачи не может превышать 10000 руб");
                return;
            }

            string fileName = DialogService.ShowInput("Введите название документа");
            if (string.IsNullOrEmpty(fileName)) return;

            string pathSave = DialogService.ShowBrowserCatalog();
            if (string.IsNullOrEmpty(pathSave)) return;

            var NamesProducts = IncomingProducts
                .Where(gd => gd.Product != null)
                .Select(gd => gd.Product.TextProduct)
                .Distinct()
                .ToList();

            var ProductOKPD = IncomingProducts
                .Where(gd => gd.Product != null)
                .Select(gd => gd.Product.OKPD)
                .ToArray();

            var UnitShortName = IncomingProducts
                .Where(gd => gd.Product != null)
                .Select(gd => gd.Product.Unit.ShortName)
                .ToArray();

            var UnitOKEI = IncomingProducts
                .Where(gd => gd.Product != null)
                .Select(gd => gd.Product.Unit.OKEI)
                .ToArray();

            var ProductPrice = IncomingProducts
                .Where(gd => gd.Product != null)
                .Select(gd => gd.Product.Price)
                .ToArray();

            var ProductCount = IncomingProducts
                .Where(gd => gd.Product != null)
                .Select(gd => gd.Product.CountProduct)
                .ToArray();

            var employeeWithCountList = new List<EmployeeWithCount>();

            var groupedByEmployee = IncomingProducts
                .GroupBy(gd => gd.Give.Employee.Id)
                .Select(group => new
                {
                    EmployeeId = group.Key,
                    EmployeeName = group.First().Give.Employee.TextEmployee,
                    GiveDetails = group.ToList()
                });

            foreach (var employeeGroup in groupedByEmployee)
            {
                var employeeWithCount = new EmployeeWithCount { TextEmployee = employeeGroup.EmployeeName };

                foreach (var giveDetail in employeeGroup.GiveDetails)
                {
                    int productIndex = NamesProducts.IndexOf(giveDetail.Product.TextProduct) + 1;

                    if (productIndex > 0 && productIndex <= 10) 
                    {
                        var property = typeof(EmployeeWithCount).GetProperty($"CountProduct{productIndex}");
                        if (property != null)
                        {
                            int currentCount = (int)property.GetValue(employeeWithCount, null);
                            property.SetValue(employeeWithCount, currentCount + (giveDetail.CountProduct ?? 0));
                        }
                    }
                }

                employeeWithCountList.Add(employeeWithCount);
            }

            string[] namesVariable =
                [
                    "Day",
                    "Month",
                    "Year",
                    "Company",
                    "Department",
                    "MaterialPerson",
                    "TextProduct",
                    "ProductOKPD",
                    "UnitShortName",
                    "UnitOKEI",
                    "ProductPrice",
                    "ProductCount",
                    "EmployeeWithCount"
                ];

            object[] parametrs =
                [
                    dateProvide.Day,
                    dateProvide.ToString("MMMM"),
                    dateProvide.ToString("yy"),
                    TextStructureName,
                    SelectedDepartment.Title,
                    SelectedMaterialPerson.TextEmployee,
                    NamesProducts,
                    ProductOKPD,
                    UnitShortName,
                    UnitOKEI,
                    ProductPrice,
                    ProductCount,
                    employeeWithCountList
                ];

            PrintService.Print("MaterialStatement", pathSave, fileName, namesVariable, parametrs);


            
        }

        #endregion

        #region CloseCommand - Закрытие окна

        private ICommand closeCommand;
        public ICommand CloseCommand => closeCommand ??= new RelayCommand(OnCloseCommandExecuted, CanCloseCommandExecute);
        private bool CanCloseCommandExecute(object p) => p is Window;

        private void OnCloseCommandExecuted(object p)
        {
            DateProvide = DateTime.Now;
            Model.ClearList();
            CommandFactory.CreateCloseWindowCommand().Execute(p);
        }
        #endregion


        #endregion


    }

    class EmployeeWithCount
    {
        public string TextEmployee { get; set; }
        public int CountProduct1 { get; set; }
        public int CountProduct2 { get; set; }
        public int CountProduct3 { get; set; }
        public int CountProduct4 { get; set; }
        public int CountProduct5 { get; set; }
        public int CountProduct6 { get; set; }
        public int CountProduct7 { get; set; }
        public int CountProduct8 { get; set; }
        public int CountProduct9 { get; set; }
        public int CountProduct10 { get; set; }
    }
}
