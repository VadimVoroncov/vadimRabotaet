using System;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Interop;

namespace vadimRabotaet.TypesOfEmployees.Admin.OrderAdditionFunctions
{
    class Barcode
    {
        // Метод для отображения QR-кода в окне WPF и возврата BitmapImage
        public BitmapImage DisplayQRCode(Bitmap qrCodeImage)
        {
            System.Windows.Controls.Image qrCodeImageView = new System.Windows.Controls.Image
            {
                Source = ConvertBitmapToBitmapImage(qrCodeImage),
                Width = 200,
                Height = 200
            };

            Window window = new Window();
            window.Content = qrCodeImageView;
            window.ShowDialog();

            return ConvertBitmapToBitmapImage(qrCodeImage);
        }

        // Метод для преобразования Bitmap в BitmapImage
        private BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new MemoryStream(GetBitmapBytes(bitmapSource));
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }

        // Метод для получения байтов изображения BitmapSource
        private byte[] GetBitmapBytes(BitmapSource bitmapSource)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
