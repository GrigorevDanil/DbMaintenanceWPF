using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DbMaintenanceWPF.View
{
    /// <summary>
    /// Логика взаимодействия для Department.xaml
    /// </summary>
    public partial class Department : UserControl
    {
        public Department()
        {
            InitializeComponent();
        }

        private void DepartmentsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Department department)) return;

            if (TextFilter == null) return;
            var filter_text = TextFilter.Text;
            if (filter_text.Length == 0) return;

            if (department.Title.IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) >= 0) return;

            e.Accepted = false;
        }

    }
}
