using System.ComponentModel;
using System.Windows;

namespace DbMaintenanceWPF.View.Windows.DialogWindows
{
    public partial class CategoryContext
    {
        #region TextWindow - Заголовок окна

        public static readonly DependencyProperty TextWindowProperty =
            DependencyProperty.Register(
                nameof(TextWindow),
                typeof(string),
                typeof(CategoryContext),
                new PropertyMetadata(null));

        public string TextWindow { get => (string)GetValue(TextWindowProperty); set => SetValue(TextWindowProperty, value); }

        #endregion

        #region TextCategory : string - Название категории

        public static readonly DependencyProperty TextCategoryProperty =
            DependencyProperty.Register(
                nameof(TextCategory),
                typeof(string),
                typeof(CategoryContext),
                new PropertyMetadata(default(string)));

        [Description("Название категории")]
        public string TextCategory { get => (string)GetValue(TextCategoryProperty); set => SetValue(TextCategoryProperty, value); }

        #endregion

        public CategoryContext() => InitializeComponent();
    }
}
