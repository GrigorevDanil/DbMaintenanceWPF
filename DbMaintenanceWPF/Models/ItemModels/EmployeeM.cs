using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class EmployeeM(EmployeeRepository employeeR, DepartmentRepository departmentR, PostRepository postR)
    {
        readonly EmployeeRepository EmployeeR = employeeR;
        readonly DepartmentRepository DepartmentR = departmentR;
        readonly PostRepository PostR = postR;
        public ReadOnlyObservableCollection<Employee> PublicListEmployees => new(new ObservableCollection<Employee>(EmployeeR.GetAll()));
        public ReadOnlyObservableCollection<Department> PublicListDepartments => new(new ObservableCollection<Department>(DepartmentR.GetAll()));
        public ReadOnlyObservableCollection<Post> PublicListPosts => new(new ObservableCollection<Post>(PostR.GetAll()));

        public void Create(Employee employee) => EmployeeR.Add(employee);
        public void Update(Employee employee) => EmployeeR.Update(employee.Id, employee);
        public void Remove(Employee employee) => EmployeeR.Remove(employee);
    }
}
