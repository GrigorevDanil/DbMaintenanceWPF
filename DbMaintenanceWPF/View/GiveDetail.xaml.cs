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
    /// Логика взаимодействия для GiveDetail.xaml
    /// </summary>
    public partial class GiveDetail : UserControl
    {
        public GiveDetail()
        {
            InitializeComponent();
        }

        private void GiveDetailsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.GiveDetail giveDetail)) return;

            if (TextFilter == null) return;

            bool filterPassed = true;

            var filter_text = TextFilter.Text;
            if (!string.IsNullOrWhiteSpace(filter_text) && giveDetail.CountProduct.ToString().IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) == -1) filterPassed = false;

            if (checkBoxGive.IsChecked == true && comboBoxGive.SelectedItem is Models.Items.Give selectedGive) if (giveDetail.Give.Id != selectedGive.Id) filterPassed = false;

            if (checkBoxProduct.IsChecked == true && comboBoxProduct.SelectedItem is Models.Items.Product selectedProduct)  if (giveDetail.Product.Id != selectedProduct.Id) filterPassed = false; 

            e.Accepted = filterPassed;
        }


    }
}
