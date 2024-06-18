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

namespace DbMaintenanceWPF.View
{
    /// <summary>
    /// Логика взаимодействия для Post.xaml
    /// </summary>
    public partial class Post : UserControl
    {
        public Post()
        {
            InitializeComponent();
        }

        private void PostsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Items.Post post)) return;

            if (TextFilter == null) return;

            var filter_text = TextFilter.Text;
            if (filter_text.Length == 0) return;

            if (post.Title.IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) >= 0) return;

            e.Accepted = false;
        }
        
    }
}
