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
        public ImageSourceHelper()
        {
            BitmapImage bitmap;
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri("ImageEmployee.png", UriKind.Relative);
            bitmap.EndInit();
            DefaultImageUser = bitmap;
        }
        public ImageSource DefaultImageUser { get; set; }

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
            else return DefaultImageUser;
        }

        public BitmapImage ReduceBitmapImageSize(BitmapImage originalImage)
        {
            const long targetSizeKilobytes = 16777;
            const long targetSize = targetSizeKilobytes * 1024;


            MemoryStream memoryStream = new MemoryStream();
            BitmapImage resizedImage = new BitmapImage();

            // Сначала попробуем изменить размер изображения
            for (double scale = 1.0; scale > 0.1; scale -= 0.1)
            {
                memoryStream.SetLength(0); // Сбросить memoryStream

                // Масштабируем изображение
                TransformedBitmap scaledBitmap = new TransformedBitmap(
                    originalImage,
                    new ScaleTransform(scale, scale)
                );

                // Проверяем, требуется ли нам сохранять в формате PNG
                if (originalImage.Format == PixelFormats.Bgra32 ||
                    originalImage.Format == PixelFormats.Pbgra32 ||
                    originalImage.Format == PixelFormats.Bgr32)
                {
                    PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
                    pngEncoder.Frames.Add(BitmapFrame.Create(scaledBitmap));
                    pngEncoder.Save(memoryStream);
                }
                else // Иначе используем JPEG
                {
                    JpegBitmapEncoder jpegEncoder = new JpegBitmapEncoder { QualityLevel = 50 }; // Выбираем среднее значение качества
                    jpegEncoder.Frames.Add(BitmapFrame.Create(scaledBitmap));
                    jpegEncoder.Save(memoryStream);
                }

                // Если размер файла удовлетворяет требованиям, загружаем его как BitmapImage
                if (memoryStream.Length <= targetSize)
                {
                    resizedImage.BeginInit();
                    resizedImage.StreamSource = new MemoryStream(memoryStream.ToArray()); // Делаем копию потока
                    resizedImage.CacheOption = BitmapCacheOption.OnLoad;
                    resizedImage.EndInit();
                    resizedImage.Freeze(); // Замораживаем для использования в других потоках
                    return resizedImage;
                }
            }

            throw new InvalidOperationException("Невозможно уменьшить размер изображения до желаемого размера.");
        }

    }
}
