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

namespace DbMaintenanceWPF.Resources.CustomControls
{
    public class CustomRadioBut : RadioButton
    {
        static CustomRadioBut() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomRadioBut), new FrameworkPropertyMetadata(typeof(CustomRadioBut)));

        public static readonly DependencyProperty SubPanelProperty = DependencyProperty.Register(
            nameof(SubPanel),
            typeof(object),
            typeof(CustomRadioBut), new PropertyMetadata(null));

        public object SubPanel
        {
            get { return (object)GetValue(SubPanelProperty); }
            set { SetValue(SubPanelProperty, value); }
        }

        #region SubPanelVisibility
        public static readonly DependencyProperty SubPanelVisibilityProperty = DependencyProperty.Register(
                   nameof(SubPanelVisibility),
                   typeof(Visibility),
                   typeof(CustomRadioBut), new PropertyMetadata(Visibility.Collapsed));


        public Visibility SubPanelVisibility
        {
            get { return (Visibility)GetValue(SubPanelVisibilityProperty); }
            set { SetValue(SubPanelVisibilityProperty, value); }
        }
        #endregion
    }

}
