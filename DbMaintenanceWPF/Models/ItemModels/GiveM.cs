using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class GiveM(GiveRepository giveR, EmployeeRepository employeeR)
    {
        readonly GiveRepository GiveR = giveR;
        readonly EmployeeRepository EmployeeR = employeeR;
        public ReadOnlyObservableCollection<Give> PublicListGives => new(new ObservableCollection<Give>(GiveR.GetAll()));
        public ReadOnlyObservableCollection<Employee> PublicListEmployees => new(new ObservableCollection<Employee>(EmployeeR.GetAll()));

        public void Create(Give give) => GiveR.Add(give);
        public void Update(Give give) => GiveR.Update(give.Id, give);
        public void Remove(Give give) => GiveR.Remove(give);
    }
}
