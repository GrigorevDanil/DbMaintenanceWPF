using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace DbMaintenanceWPF.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm
    {
        public MainForm() => InitializeComponent();


        private StackPanel _currentPanel;

        private void Btn_Checked(object sender, RoutedEventArgs e)
        {
            var toggledButton = sender as ToggleButton;
            if (toggledButton != null)
            {
                string panelName = toggledButton.Tag.ToString();
                StackPanel newTargetPanel = this.FindName(panelName) as StackPanel;

                if (_currentPanel != null && _currentPanel != newTargetPanel)
                {
                    Storyboard leaveSubmenuStoryboard = this.FindResource("LeaveSubmenu") as Storyboard;
                    Storyboard.SetTarget(leaveSubmenuStoryboard, _currentPanel);
                    leaveSubmenuStoryboard.Begin();
                }

                if (newTargetPanel != null && _currentPanel != newTargetPanel)
                {
                    Storyboard enterSubmenuStoryboard = this.FindResource("EnterSubmenu") as Storyboard;
                    Storyboard.SetTarget(enterSubmenuStoryboard, newTargetPanel);
                    enterSubmenuStoryboard.Begin();
                    _currentPanel = newTargetPanel;
                }
            }
        }

        private void Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            var toggledButton = sender as ToggleButton;
            if (toggledButton != null)
            {
                StackPanel targetPanel = _currentPanel;

                if (targetPanel != null)
                {
                    if ((sender as FrameworkElement)?.Parent == targetPanel)
                    {
                        Storyboard leaveSubmenuStoryboard = this.FindResource("LeaveSubmenu") as Storyboard;
                        Storyboard.SetTarget(leaveSubmenuStoryboard, targetPanel);
                        leaveSubmenuStoryboard.Begin();
                    }
                }
            }
        }

    }
}
