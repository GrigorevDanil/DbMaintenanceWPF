using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using DbMaintenanceWPF.Model;
using System.Windows.Controls;
using DbMaintenanceWPF.Utilities;
using System.Diagnostics;

namespace DbMaintenanceWPF.ViewModel
{
    class MessageWindowVM : Utilities.ViewModelBase
    {
        Visibility visButOk;
        MessageWindow currentMessage;
        public MessageWindow CurrentMessage
        {
            get { return currentMessage; }
            set { currentMessage = value; OnPropertyChanged(nameof(CurrentMessage)); }
        }

        public Visibility VisButOk
        {
            get { return visButOk; }
            set { visButOk = value; OnPropertyChanged(nameof(VisButOk)); }
        }

        public MessageWindowVM(string headerText, string text, MessageBoxButton buts = MessageBoxButton.OKCancel, MessageBoxImage images = MessageBoxImage.Question)
        {
            CurrentMessage = new MessageWindow
            {
                HeaderText = headerText,
                Text = text
            };

            switch (buts)
            {
                case MessageBoxButton.OK:
                    {
                        VisButOk = Visibility.Collapsed;
                    }
                    break;
            }
            switch (images)
            {
                case MessageBoxImage.Information:
                    {
                        CurrentMessage.ImageMessage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/ImageEmployee.png", UriKind.RelativeOrAbsolute));
                    }
                    break;
                case MessageBoxImage.Error:
                    {
                        CurrentMessage.ImageMessage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/IconWarning.png", UriKind.RelativeOrAbsolute));

                    }
                    break;
                case MessageBoxImage.Question:
                    {
                        CurrentMessage.ImageMessage = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/IconQuestion.png", UriKind.RelativeOrAbsolute));
                    }
                    break;
            }
        }

    }
}
