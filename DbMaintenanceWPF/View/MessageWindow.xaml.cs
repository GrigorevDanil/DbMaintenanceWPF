using DbMaintenanceWPF.ViewModel;
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

namespace DbMaintenanceWPF.View
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string headerText, string text, MessageBoxButton buts = MessageBoxButton.OKCancel, MessageBoxImage images = MessageBoxImage.Question)
        {
            InitializeComponent();
            DataContext = new MessageWindowVM(headerText, text, buts, images);
        }

        private void Button_Click(object sender, RoutedEventArgs e) => DialogResult = true;
    }
}
