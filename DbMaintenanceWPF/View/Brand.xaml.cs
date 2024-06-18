using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using DbMaintenanceWPF.Models.Items;

namespace DbMaintenanceWPF.View
{
    /// <summary>
    /// Логика взаимодействия для Brand.xaml
    /// </summary>
    public partial class Brand : UserControl
    {
        public Brand() => InitializeComponent();

        private void BrandCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Brand brand)) return;

            if (TextFilter == null) return;

            var filter_text = TextFilter.Text;
            if (filter_text.Length == 0) return;

            if (brand.Title.IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) >= 0) return;

            e.Accepted = false;
        }

    }
}
