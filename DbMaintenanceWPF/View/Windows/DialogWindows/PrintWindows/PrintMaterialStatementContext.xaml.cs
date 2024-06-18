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
using System.Windows.Shapes;

namespace DbMaintenanceWPF.View.Windows.DialogWindows.PrintWindows
{
    /// <summary>
    /// Логика взаимодействия для PrintMaterialStatementContext.xaml
    /// </summary>
    public partial class PrintMaterialStatementContext : Window
    {
        public PrintMaterialStatementContext()
        {
            InitializeComponent();
        }

        private void GivesCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Give give)) return;

            e.Accepted = datePickerDateProvider.SelectedDate == give.DateGive;
        }
    }
}
