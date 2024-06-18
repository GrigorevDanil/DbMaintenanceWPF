using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace DbMaintenanceWPF.View
{
    /// <summary>
    /// Логика взаимодействия для Employee.xaml
    /// </summary>
    public partial class Employee : UserControl
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void EmployeesCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Employee employee)) return;

            bool filterPassed = true;

            if (TextFilter == null) return;

            // Фильтр по ФИО
            var filter_text = TextFilter.Text;
            if (!string.IsNullOrWhiteSpace(filter_text) && employee.TextEmployee.IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) == -1)
            {
                filterPassed = false;
            }

            // Фильтр по отделу (Department)
            if (checkBoxDepartment.IsChecked == true && comboBoxDepartment.SelectedItem is Models.Items.Department selectedDepartment)
            {
                if (employee.Department.Id != selectedDepartment.Id)
                {
                    filterPassed = false;
                }
            }

            // Фильтр по должности (Post)
            if (checkBoxPost.IsChecked == true && comboBoxPost.SelectedItem is Models.Items.Post selectedPost)
            {
                if (employee.Post.Id != selectedPost.Id)
                {
                    filterPassed = false;
                }
            }

            e.Accepted = filterPassed;
        }

    }
}
