using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace DbMaintenanceWPF.Infrastructure.Extensions
{
    public class FilterCheckBoxExtension : Behavior<CheckBox>
    {
        public ComboBox TargetComboBox
        {
            get { return (ComboBox)GetValue(TargetComboBoxProperty); }
            set { SetValue(TargetComboBoxProperty, value); }
        }

        public static readonly DependencyProperty TargetComboBoxProperty =
            DependencyProperty.Register("TargetComboBox", typeof(ComboBox), typeof(FilterCheckBoxExtension), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Checked += OnChecked;
            this.AssociatedObject.Unchecked += OnUnchecked;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Checked -= OnChecked;
            this.AssociatedObject.Unchecked -= OnUnchecked;
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            if (TargetComboBox != null && TargetComboBox.Items.Count > 0)
            {
                TargetComboBox.SelectedIndex = 1;
            }
        }

        private void OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (TargetComboBox != null)
            {
                TargetComboBox.SelectedIndex = -1;
            }
        }
    }
}
