using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class DepartmentM(DepartmentRepository departmentR)
    {
        readonly DepartmentRepository DepartmentR = departmentR;
        public ReadOnlyObservableCollection<Department> PublicListDepartments => new(new ObservableCollection<Department>(DepartmentR.GetAll()));

        public void Create(Department department) => DepartmentR.Add(department);
        public void Update(Department department) => DepartmentR.Update(department.Id, department);
        public void Remove(Department department) => DepartmentR.Remove(department);
    }
}
