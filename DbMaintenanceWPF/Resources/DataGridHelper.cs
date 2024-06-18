using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DbMaintenanceWPF.Resources
{
    class DataGridHelper : DependencyObject
    {
        public static string GetHidableColumnIndices(DependencyObject attachingElement) => (string)attachingElement.GetValue(HidableColumnIndicesProperty);
        public static void SetHidableColumnIndices(DependencyObject attachingElement, string value) => attachingElement.SetValue(HidableColumnIndicesProperty, value);

        public static readonly DependencyProperty HidableColumnIndicesProperty = DependencyProperty.RegisterAttached(
            "HidableColumnIndices",
            typeof(string),
            typeof(DataGridHelper),
            new PropertyMetadata(default(string), OnHidableColumnIndicesChanged));

        public static Visibility GetColumnVisibility(DependencyObject attachingElement) => (Visibility)attachingElement.GetValue(ColumnVisibilityProperty);
        public static void SetColumnVisibility(DependencyObject attachingElement, Visibility value) => attachingElement.SetValue(ColumnVisibilityProperty, value);

        public static readonly DependencyProperty ColumnVisibilityProperty = DependencyProperty.RegisterAttached(
          "ColumnVisibility",
          typeof(Visibility),
          typeof(DataGridHelper),
          new PropertyMetadata(default(Visibility), OnColumnVisibilityChanged));

        private static void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;

            // Отписываемся от события, чтобы избежать повторного выполнения
            dataGrid.Loaded -= DataGrid_Loaded;

            ToggleColumnVisibility(dataGrid);
        }

        private static void OnColumnVisibilityChanged(DependencyObject attachingElement, DependencyPropertyChangedEventArgs e)
        {
            if (attachingElement is DataGrid dataGrid) dataGrid.Loaded += DataGrid_Loaded;
            else throw new ArgumentException("Attaching element must be of type DataGrid.");
        }

        private static void OnHidableColumnIndicesChanged(DependencyObject attachingElement, DependencyPropertyChangedEventArgs e)
        {
            if (attachingElement is DataGrid dataGrid) dataGrid.Loaded += DataGrid_Loaded;
            else throw new ArgumentException("Attaching element must be of type DataGrid.");
        }


        private static void ToggleColumnVisibility(DataGrid dataGrid)
        {
            IEnumerable<int> columnIndices = GetHidableColumnIndices(dataGrid)
              .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
              .Select(numericChar => int.Parse(numericChar));
            foreach (int columnIndex in columnIndices)
            {
                dataGrid.Columns[columnIndex].Visibility = GetColumnVisibility(dataGrid);
            }
        }
    }
}
