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

        private void TextFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text_box = (TextBox)sender;
            var collection = (CollectionViewSource)text_box.FindResource("EmployeesCollection");
            collection.View.Refresh();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo_box = (ComboBox)sender;
            var collection = (CollectionViewSource)combo_box.FindResource("EmployeesCollection");
            collection.View.Refresh();
        }

        private void checkBoxDepartment_Unchecked(object sender, System.Windows.RoutedEventArgs e) => comboBoxDepartment.SelectedIndex = -1;

        private void checkBoxPost_Unchecked(object sender, System.Windows.RoutedEventArgs e) => comboBoxPost.SelectedIndex = -1;

        private void checkBoxPost_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (comboBoxPost.Items.Count != 0) comboBoxPost.SelectedIndex = 1;
        }

        private void checkBoxDepartment_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (comboBoxPost.Items.Count != 0) comboBoxDepartment.SelectedIndex = 1;
        }
    }
}
