using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using MySqlConnector;
using System.Data;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service
{
    public class ImageSourceHelper : IImageHelper
    {
        public byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        {
            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;
            if (bitmapSource != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }
            return bytes;
        }
        public ImageSource BytesToImageSource(MySqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            if (!reader.IsDBNull(columnIndex))
            {
                long blobSize = reader.GetBytes(columnIndex, 0, null, 0, 0);
                byte[] buffer = new byte[blobSize];
                reader.GetBytes(columnIndex, 0, buffer, 0, (int)blobSize);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    var image = new BitmapImage();
                    memoryStream.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = memoryStream;
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
            }
            else return new BitmapImage(new Uri("ImageEmployee.png", UriKind.Relative));
        }

        public ImageSource BytesToImageSource(DataRow row, string columnName)
        {
            if (!row.IsNull(columnName))
            {
                byte[] buffer = (byte[])row[columnName];

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    var image = new BitmapImage();
                    memoryStream.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = memoryStream;
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
            }
            else return new BitmapImage(new Uri("ImageEmployee.png", UriKind.Relative));
        }

    }
}
