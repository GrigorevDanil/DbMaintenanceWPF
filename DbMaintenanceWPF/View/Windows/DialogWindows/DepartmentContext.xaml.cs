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

namespace DbMaintenanceWPF.View.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для DepartmentContext.xaml
    /// </summary>
    public partial class DepartmentContext : Window
    {
        #region TextWindow - Заголовок окна

        public static readonly DependencyProperty TextWindowProperty =
            DependencyProperty.Register(
                nameof(TextWindow),
                typeof(string),
                typeof(DepartmentContext),
                new PropertyMetadata(null));

        public string TextWindow { get => (string)GetValue(TextWindowProperty); set => SetValue(TextWindowProperty, value); }

        #endregion

        #region TextDepartment

        public static readonly DependencyProperty TextDepartmentProperty =
            DependencyProperty.Register(
                nameof(TextDepartment),
                typeof(string),
                typeof(DepartmentContext),
                new PropertyMetadata(null));

        public string TextDepartment { get => (string)GetValue(TextDepartmentProperty); set => SetValue(TextDepartmentProperty, value); }

        #endregion

        public DepartmentContext()
        {
            InitializeComponent();
        }


    }
}
