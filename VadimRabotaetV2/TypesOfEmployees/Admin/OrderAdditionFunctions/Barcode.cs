using Aspose.BarCode.Generation;
using System;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace VadimRabotaetV2.TypesOfEmployees.Admin.OrderAdditionFunctions
{
    class Barcode
    {
        public string Text { get; set; }

        public Barcode(string tbCodeText)
        {
            Text = tbCodeText;
        }

        public void BarcodeSettings(System.Windows.Controls.Image imgDynamic)
        {
            var imageType = "Png";

            // Получить формат изображения из перечисления
            var imageFormat = (BarCodeImageFormat)Enum.Parse(typeof(BarCodeImageFormat), imageType.ToString());

            // Установить по умолчанию как Code128
            var encodeType = EncodeTypes.QR;

            try
            {
                // Сгенерируйте штрих-код с дополнительными параметрами и получите путь к изображению
                var generator = GenerateBarcodeWithOptions(imageType, imageFormat, encodeType);
                Bitmap barcodeImage = generator.GenerateImage();

                // Показать изображение
                using (MemoryStream stream = new MemoryStream())
                {
                    barcodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();

                    imgDynamic.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private BarcodeGenerator GenerateBarcodeWithOptions(string _imageType, BarCodeImageFormat _imageFormat, SymbologyEncodeType _encodeType)
        {
            // Инициализировать генератор штрих-кода
            var generator = new BarcodeGenerator(_encodeType, Text);

            generator.Parameters.Barcode.XDimension.Pixels = 4;
            //установить автоматическую версию
            generator.Parameters.Barcode.QR.QrVersion = QRVersion.Auto;
            //Установите тип автоматического кодирования QR
            generator.Parameters.Barcode.QR.QrEncodeType = QREncodeType.Auto;

            return generator;
        }

        public void SaveBarcodeToDatabase()
        {
            // Генерация изображения штрих-кода
            var generator = GenerateBarcodeWithOptions(imageType, imageFormat, encodeType);
            var barcodeImage = generator.GenerateImage();

            // Преобразование изображения в массив байтов
            byte[] imageData;
            using (var stream = new MemoryStream())
            {
                barcodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                imageData = stream.ToArray();
            }

            // Сохранение изображения в базу данных с использованием Entity Framework

        }

        public void LoadBarcodeFromDatabase(Image imgDynamic)
        {
            using (var dbContext = new BarcodeDbContext())
            {
                var barcodeData = dbContext.Barcodes.Find(barcodeId);

                if (barcodeData != null)
                {
                    var imageData = barcodeData.Image;
                    using (var stream = new MemoryStream(imageData))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = stream;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();

                        imgDynamic.Source = bitmap;
                    }
                }
            }
        }
    }
}
