﻿using System;
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

namespace DbMaintenanceWPF.View
{
    /// <summary>
    /// Логика взаимодействия для Unit.xaml
    /// </summary>
    public partial class Unit : UserControl
    {
        public Unit()
        {
            InitializeComponent();
        }

        private void UnitsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Unit unit)) return;

            if (TextFilter == null) return;

            var filter_text = TextFilter.Text;
            if (filter_text.Length == 0) return;

            if (unit.Title.IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) >= 0) return;

            e.Accepted = false;
        }
    }
}
