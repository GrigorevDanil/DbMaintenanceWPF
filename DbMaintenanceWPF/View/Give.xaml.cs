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
    /// Логика взаимодействия для Give.xaml
    /// </summary>
    public partial class Give : UserControl
    {
        public Give()
        {
            InitializeComponent();
        }

        private void GivesCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Give give)) return;

            if (TextFilter == null) return;

            bool filterPassed = true;

            var filter_text = TextFilter.Text;
            if (!string.IsNullOrWhiteSpace(filter_text) && give.StringDateGive.ToString().IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) == -1) filterPassed = false;

            if (checkBoxEmployee.IsChecked == true && comboBoxEmployee.SelectedItem is Models.Items.Employee selectedemployee) if (give.Employee.Id != selectedemployee.Id) filterPassed = false;

            e.Accepted = filterPassed;
        }

    }
}
