using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DbMaintenanceWPF.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginContext.xaml
    /// </summary>
    public partial class LoginContext
    {
        public LoginContext() => InitializeComponent();
        
        
        private void AnimateElementDownUp(UIElement element, double offset, TimeSpan duration, bool flagHide)
        {
            // Убедитесь, что у элемента есть Transform, который можно анимировать
            if (element.RenderTransform == null ||
                !(element.RenderTransform is TranslateTransform))
            {
                element.RenderTransform = new TranslateTransform();
            }

            // Создать Storyboard
            var storyboard = new Storyboard();

            // Анимация перемещения вниз или вверх
            var moveDownAnimation = new DoubleAnimation()
            {
                From = 0,
                To = offset,
                Duration = duration,
                AccelerationRatio = 0.2, // Начать медленно
                DecelerationRatio = 0.8 // Закончить быстро
            };
            Storyboard.SetTarget(moveDownAnimation, element);
            Storyboard.SetTargetProperty(moveDownAnimation, new PropertyPath("RenderTransform.Y"));

            // Добавить анимации в Storyboard
            storyboard.Children.Add(moveDownAnimation);
            if (flagHide) storyboard.Children.Add(AnimVisInvis(1.0f, 0.0f, Visibility.Hidden));
            else storyboard.Children.Add(AnimVisInvis(0.0f, 1.0f, Visibility.Visible));


            // Запустить анимацию
            storyboard.Begin();

            DoubleAnimation AnimVisInvis(float from, float to, Visibility stat)
            {
                // Анимация исчезновения и появления
                var fadeAnimation = new DoubleAnimation()
                {
                    From = from,
                    To = to,
                    Duration = duration
                };
                Storyboard.SetTarget(fadeAnimation, element);
                Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(UIElement.OpacityProperty));
                element.Visibility = stat;
                return fadeAnimation;
            }
        }



        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
            {
                //Анимация смены полей для входа по серверу
                AnimateElementDownUp(textBoxLog, 10, TimeSpan.FromSeconds(0.5), true);
                AnimateElementDownUp(textBoxHost, -10, TimeSpan.FromSeconds(0.5), false);
                AnimateElementDownUp(comboBoxLogin, -10, TimeSpan.FromSeconds(0.5), false);
                AnimateElementDownUp(imgHost, -10, TimeSpan.FromSeconds(0.5), false);
                AnimateElementDownUp(imgEmp, -10, TimeSpan.FromSeconds(0.5), false);
            }
            else
            {
                //Анимация смены полей для программного входа
                AnimateElementDownUp(textBoxLog, -10, TimeSpan.FromSeconds(0.5), false);
                AnimateElementDownUp(textBoxHost, 10, TimeSpan.FromSeconds(0.5), true);
                AnimateElementDownUp(comboBoxLogin, 10, TimeSpan.FromSeconds(0.5), true);
                AnimateElementDownUp(imgHost, 10, TimeSpan.FromSeconds(0.5), true);
                AnimateElementDownUp(imgEmp, -10, TimeSpan.FromSeconds(0.5), false);
            }
        }
        
        
        private void textBoxPass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (buttonEye.IsChecked == false)
            {
                textBoxPass.Visibility = Visibility.Hidden;
                passwordBox.Focus();
            }
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Length == 0)
            {
                textBoxPass.Visibility = Visibility.Visible;
                textBoxPass.Text = "";
                passwordBox.Password = "";
            }
        }

        private void buttonEye_Click(object sender, RoutedEventArgs e)
        {

            if (buttonEye.IsChecked == false)
            {
                textBoxPass.Visibility = Visibility.Hidden;
                passwordBox.Visibility = Visibility.Visible;
                if (passwordBox.Password.Length == 0) textBoxPass.Visibility = Visibility.Visible;
                else textBoxPass.Visibility = Visibility.Hidden;
            }
            else
            {
                passwordBox.Visibility = Visibility.Hidden;
                textBoxPass.Visibility = Visibility.Visible;
            }
        }

    }
}
