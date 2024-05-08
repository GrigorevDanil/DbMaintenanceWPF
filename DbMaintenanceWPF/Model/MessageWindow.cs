using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DbMaintenanceWPF.Model
{
    class MessageWindow : Utilities.ViewModelBase
    {
        ImageSource imageMessage;
        string headerText, text;

        public ImageSource ImageMessage
        {
            get { return imageMessage; }
            set { imageMessage = value; OnPropertyChanged(nameof(ImageMessage)); }
        }

        public string HeaderText
        {
            get { return headerText; }
            set { headerText = value; OnPropertyChanged(nameof(HeaderText)); }
        }

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(nameof(Text)); }
        }

    }
}
