using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class DepartmentM
    {
        readonly DepartmentRepository DepartmentR;
        public ReadOnlyObservableCollection<Department> PublicListDepartments => new(new ObservableCollection<Department>(DepartmentR.GetAll()));

        public DepartmentM(DepartmentRepository departmentR) => DepartmentR = departmentR;

        public void Create(Department department) => DepartmentR.Add(department);
        public void Update(Department department) => DepartmentR.Update(department.Id, department);
        public void Remove(Department department) => DepartmentR.Remove(department);
    }
}
