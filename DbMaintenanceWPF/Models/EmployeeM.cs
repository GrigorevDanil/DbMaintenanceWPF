using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class EmployeeM
    {
        readonly EmployeeRepository EmployeeR;
        readonly DepartmentRepository DepartmentR;
        readonly PostRepository PostR;
        public ReadOnlyObservableCollection<Employee> PublicListEmployees => new(new ObservableCollection<Employee>(EmployeeR.GetAll()));
        public ReadOnlyObservableCollection<Department> PublicListDepartments => new(new ObservableCollection<Department>(DepartmentR.GetAll()));
        public ReadOnlyObservableCollection<Post> PublicListPosts => new(new ObservableCollection<Post>(PostR.GetAll()));

        public EmployeeM(EmployeeRepository employeeR, DepartmentRepository departmentR, PostRepository postR)
        {
            EmployeeR = employeeR;
            DepartmentR = departmentR;
            PostR = postR;
        }

        public void Update(Employee employee) => EmployeeR.Update(employee.Id, employee);
        public void Remove(Employee employee) => EmployeeR.Remove(employee);
    }
}
