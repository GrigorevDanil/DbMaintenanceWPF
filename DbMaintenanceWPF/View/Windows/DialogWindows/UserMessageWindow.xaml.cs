using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DbMaintenanceWPF.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserMessageWindow.xaml
    /// </summary>
    public partial class UserMessageWindow
    {
        public UserMessageWindow() => InitializeComponent();

        public Visibility VisibleButtonOk { get => butOk.Visibility; set => butOk.Visibility = value; }
        public string MessageTitle { get => tbTitle.Text; set => tbTitle.Text = value; }
        public string MessageText { get => tbText.Text; set => tbText.Text = value; }
        public ImageSource MessageImage { get => imgMessage.Source; set => imgMessage.Source = value; }

        private void OnButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!(E.Source is Button button)) return;
            DialogResult = !button.IsCancel;
            Close();
        }

    }
}
