using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DbMaintenanceWPF.Infrastructure.Extensions
{
    class RefreshCollectionViewSourceExtension : TriggerAction<DependencyObject>
    {
        public CollectionViewSource Source
        {
            get { return (CollectionViewSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(CollectionViewSource), typeof(RefreshCollectionViewSourceExtension), new PropertyMetadata(null));

        protected override void Invoke(object parameter)
        {
            Source?.View?.Refresh();
        }
    }
}
