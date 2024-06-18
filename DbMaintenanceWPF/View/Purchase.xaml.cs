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
    /// Логика взаимодействия для Purchase.xaml
    /// </summary>
    public partial class Purchase : UserControl
    {
        public Purchase()
        {
            InitializeComponent();
        }

        private void PurchasesCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Purchase purchase)) return;

            bool filterPassed = true;

            if (TextFilter == null) return;

            var filter_text = TextFilter.Text;

            if (!string.IsNullOrWhiteSpace(filter_text) && purchase.Price.ToString().IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) == -1) filterPassed = false;

            if (checkBoxProduct.IsChecked == true && comboBoxProduct.SelectedItem is Models.Items.Product selectedProduct) if (purchase.Product.Id != selectedProduct.Id) filterPassed = false;

            if (checkBoxProvider.IsChecked == true && comboBoxProvider.SelectedItem is Models.Items.Provider selectedProvider) if (purchase.Provider.Id != selectedProvider.Id) filterPassed = false;


            e.Accepted = filterPassed;
        }
    }
}
