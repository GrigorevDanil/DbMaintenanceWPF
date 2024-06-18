using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using MySqlConnector;
using System.Data;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IImageHelper
    {
        ImageSource DefaultImageUser { get; }
        byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource);
        ImageSource BytesToImageSource(MySqlDataReader reader, string columnName);
        BitmapImage ReduceBitmapImageSize(BitmapImage originalImage);
    }
}
