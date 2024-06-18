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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class User : UserControl
    {
        public User()
        {
            InitializeComponent();
        }

        private void UsersCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.User user)) return;

            bool filterPassed = true;

            if (TextFilter == null) return;

            var filter_text = TextFilter.Text;

            if (!string.IsNullOrWhiteSpace(filter_text) && user.Login.ToString().IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) == -1) filterPassed = false;

            if (checkBoxRole.IsChecked == true && comboBoxRole.SelectedItem is string selectedRole) if (user.Role != selectedRole) filterPassed = false;

            e.Accepted = filterPassed;
        }
    }
}
