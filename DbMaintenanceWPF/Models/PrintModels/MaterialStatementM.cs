using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace DbMaintenanceWPF.Models.PrintModels
{
    public class MaterialStatementM
    {
        public MaterialStatementM(ICreaterEntity<Employee> createrEmployee, ICreaterEntity<Department> createrDepartment, ICreaterEntity<Give> createrGive, ICreaterEntity<GiveDetail> createrGiveDetail)
        {
            PublicListEmployees = createrEmployee.GetList();
            PublicListDepartments = createrDepartment.GetList();
            ListGiveDetails = createrGiveDetail.GetList();

            listGives = new(createrGive.GetList());
            PublicListGives = new(listGives);

            selectedGives = new ObservableCollection<Give>();
            PublicSelectedGives = new(selectedGives);

            incomingProducts = new ObservableCollection<GiveDetail>();
            PublicIncommingProducts = new(incomingProducts);
        }

        #region Свойства

        public readonly IEnumerable<Employee> PublicListEmployees;
        public readonly IEnumerable<Department> PublicListDepartments;

        readonly IEnumerable<GiveDetail> ListGiveDetails;

        public readonly ReadOnlyObservableCollection<Give> PublicListGives;
        public readonly ObservableCollection<Give> listGives;

        public readonly ReadOnlyObservableCollection<Give> PublicSelectedGives;
        readonly ObservableCollection<Give> selectedGives;

        public ReadOnlyObservableCollection<GiveDetail> PublicIncommingProducts;
        readonly ObservableCollection<GiveDetail> incomingProducts;

        #endregion

        #region Команды

        public void AddGive(Give give)
        {
            listGives.Remove(give);
            selectedGives.Add(give);
            foreach (var detail in ListGiveDetails.Where(gd => gd.Give.Id == give.Id)) incomingProducts.Add(detail);
        }

        public void RemoveGive(Give give)
        {
            listGives.Add(give);
            selectedGives.Remove(give);
            foreach (var detail in ListGiveDetails.Where(gd => gd.Give.Id == give.Id)) incomingProducts.Remove(detail);
        }

        public void ClearList()
        {
            foreach (Give give in selectedGives) listGives.Add(give);
            selectedGives.Clear();
            incomingProducts.Clear();
        }

        #endregion

    }
}
