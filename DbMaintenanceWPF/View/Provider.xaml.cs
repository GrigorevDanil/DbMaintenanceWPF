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
    /// Логика взаимодействия для Provider.xaml
    /// </summary>
    public partial class Provider : UserControl
    {
        public Provider()
        {
            InitializeComponent();
        }

        private void ProvidersCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Provider provider)) return;

            bool filterPassed = true;

            if (TextFilter == null) return;

            var filter_text = TextFilter.Text;

            if (!string.IsNullOrWhiteSpace(filter_text) && provider.TextProvider.ToString().IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) == -1) filterPassed = false;


            e.Accepted = filterPassed;
        }
    }
}
