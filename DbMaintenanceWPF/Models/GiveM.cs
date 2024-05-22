using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class GiveM
    {
        readonly GiveRepository GiveR;
        readonly EmployeeRepository EmployeeR;
        public ReadOnlyObservableCollection<Give> PublicListGives => new(new ObservableCollection<Give>(GiveR.GetAll()));
        public ReadOnlyObservableCollection<Employee> PublicListEmployees => new(new ObservableCollection<Employee>(EmployeeR.GetAll()));

        public GiveM(GiveRepository giveR, EmployeeRepository employeeR)
        {
            GiveR = giveR;
            EmployeeR = employeeR;
        }

        public void Update(Give give) => GiveR.Update(give.Id, give);
        public void Remove(Give give) => GiveR.Remove(give);
    }
}
