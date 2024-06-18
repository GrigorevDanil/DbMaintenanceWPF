using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using DbMaintenanceWPF.Resources.CustomControls;

namespace DbMaintenanceWPF.Resources
{
    public class RadioButtonHellper
    {
        #region ResetChildIsChecked
        /// <summary>
        /// Свойство для регистрации IsChecked родительского элемента
        /// при изменение значения он сбрасывает значения CustomRadioBut.IsChecked  дочерних элементов в false
        /// </summary>
        public static readonly DependencyProperty ResetChildIsCheckedProperty = DependencyProperty.RegisterAttached(
                    "ResetChildIsChecked",
                    typeof(bool),
                    typeof(RadioButtonHellper),
                    new PropertyMetadata(false, OnResetChildIsCheckedChanged));

        public static void SetResetChildIsChecked(DependencyObject element, bool value) => element.SetValue(ResetChildIsCheckedProperty, value);

        public static bool GetResetChildIsChecked(DependencyObject element) => (bool)element.GetValue(ResetChildIsCheckedProperty);

        private static void OnResetChildIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomRadioBut radioButton && !(bool)e.NewValue)
            {
                var temp = FindVisualChildren<CustomRadioBut>(radioButton);
                foreach (var child in temp)
                {
                    child.IsChecked = false; // Выключаем видимость SubPanel
                }
            }
        }
        #endregion


        #region CustomRadioButHideClickPropertyHideClick
        /// <summary>
        /// Свойство для регистрации события Click,
        /// при изменение клике он сбрасывает значения CustomRadioBut.IsChecked  дочерних элементов в false если SubPanelVisibility видима.
        /// </summary>
        public static readonly DependencyProperty CustomRadioButHideClickProperty = DependencyProperty.RegisterAttached(
                    "CustomRadioButHideClickProperty",
                    typeof(bool),
                    typeof(RadioButtonHellper),
                    new PropertyMetadata(false, OnCustomRadioButHideChanged));

        public static void SetCustomRadioButHideClick(DependencyObject element, bool value) => element.SetValue(CustomRadioButHideClickProperty, value);

        public static bool GetCustomRadioButHideClick(DependencyObject element) => (bool)element.GetValue(CustomRadioButHideClickProperty);

        private static void OnCustomRadioButHideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomRadioBut radioButton)
            {
                //Регистрируем событие Click
                radioButton.Click += (sender, args) =>
                {
                    if (args.OriginalSource != radioButton) //Если событие вызвано не кнопкой у которой задано событие, то прекращаем выполнение.
                        return;
                    if (radioButton.SubPanelVisibility == Visibility.Visible) //Если задана видимость, выполняем поиск и деактивируем видимость.
                    {
                        var temp = RadioButtonHellper.FindVisualChildren<CustomRadioBut>(radioButton);
                        foreach (var child in temp)
                        {
                            if (radioButton.SubPanelVisibility == Visibility.Visible)
                                child.IsChecked = false;
                        }
                    }
                };
            }
        }
        #endregion


        /// <summary>
        /// Поиск дочерних элементов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
