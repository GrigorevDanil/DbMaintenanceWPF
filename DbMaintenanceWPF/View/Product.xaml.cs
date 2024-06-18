using DbMaintenanceWPF.Models.Items;
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
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : UserControl
    {
        public Product()
        {
            InitializeComponent();
        }


        private void ProductsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Product product)) return;

            bool filterPassed = true;

            if (TextFilter == null) return;

            var filter_text = TextFilter.Text;

            if (!string.IsNullOrWhiteSpace(filter_text) && product.TextProduct.ToString().IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) == -1) filterPassed = false;

            if (checkBoxCategory.IsChecked == true && comboBoxCategory.SelectedItem is Models.Items.Category selectedCategory) if (product.Category.Id != selectedCategory.Id) filterPassed = false;
            
            if (checkBoxBrand.IsChecked == true && comboBoxBrand.SelectedItem is Models.Items.Brand selectedBrand) if (product.Brand.Id != selectedBrand.Id) filterPassed = false;
            
            if (checkBoxUnit.IsChecked == true && comboBoxUnit.SelectedItem is Models.Items.Unit selectedUnit) if (product.Unit.Id != selectedUnit.Id) filterPassed = false;


            e.Accepted = filterPassed;
        }
        
    }
}
