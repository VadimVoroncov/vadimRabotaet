using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using vadimRabotaet.DALandDomain;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Input;
using vadimRabotaet.TypesOfEmployees.Admin.ProductAdditionFunctions;
using System.Diagnostics;

namespace vadimRabotaet.TypesOfEmployees.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private byte[] _photoBytes;

        public AdminWindow()
        {
            InitializeComponent();

            ImgIzobrazhenieTovara.MouseLeftButtonDown += Image_Click;

            // Загрузка данных из базы данных и привязка к ComboBox
            WriteCombobox();

            DpDateIzgotovlenia.SelectedDate = DateTime.Now;
            DpSrokGodnosti.SelectedDate = DateTime.Now.AddYears(1);


        }

        #region Загрузка таблиц в comboboxs (WriteCombobox, LoadDataIntoComboBox)
        private void WriteCombobox() 
        {
            using (var db = new Entities())
            {
                // Категории
                var tableKategori = db.Категоря_товров.ToList();
                LoadDataIntoComboBox(tableKategori, CbKategori);
                // Единицы измерения
                var tableEdIzmerenia = db.Ед__измерения.ToList();
                LoadDataIntoComboBox(tableEdIzmerenia, CbEdIzmerenia);
                // Фирма
                var tableFirma = db.Фирма.ToList();
                LoadDataIntoComboBox(tableFirma, CbFirma);
                // Страна произваодитель
                var tableStranaProizvoitel = db.Страна_произваодителя.ToList();
                LoadDataIntoComboBox(tableStranaProizvoitel, CbStranaProizvoditel);
            }
        }
        private void LoadDataIntoComboBox<T>(List<T> data, ComboBox comboBox)
        {
            comboBox.ItemsSource = data;
            comboBox.DisplayMemberPath = "Наименование";
            comboBox.SelectedValuePath = "Код";
        }
        #endregion

        #region Сохранение Товара в базу данных + Получение выбранного элемента из ComboBox + Очистка полей форм 
        private void SaveProduct_Click(object sender, RoutedEventArgs e) // Сохранение Товара в базу данных
        {
            try
            {
                // Получаем данные товара из полей формы и сохраняем товар в базу данных
                using (var db = new Entities())
                {
                    // Извлекаем выбранный элемент из комбо-бокса и получаем значение его свойства "Код".
                    // Категории
                    var selectItemKategori = SelectedComboBoxItem(db.Категоря_товров.ToList(), CbKategori);
                    // Единицы измерения
                    var selectItemEdIzmerenia = SelectedComboBoxItem(db.Ед__измерения.ToList(), CbEdIzmerenia);
                    // Фирма
                    var selectItemFirma = SelectedComboBoxItem(db.Фирма.ToList(), CbFirma);
                    // Страна произваодитель
                    var selectItemProizvoitel = SelectedComboBoxItem(db.Страна_произваодителя.ToList(), CbStranaProizvoditel);

                    var product = new Товар
                    {
                        артикул = Convert.ToDouble(TbArticulAw.Text),
                        Наименование = TbName.Text,
                        КодКатегории = selectItemKategori,
                        Цена = Convert.ToDouble(TbCena.Text),
                        Изображение = _photoBytes,
                        КодЕдиницыИзмерения = selectItemEdIzmerenia,
                        ДатаИзготовления = DpDateIzgotovlenia.SelectedDate,
                        СрокомГодности = DpSrokGodnosti.SelectedDate,
                        КодФирмы = selectItemFirma,
                        КодСтраныПроизводителя = selectItemProizvoitel

                    };
                    db.Товар.Add(product);
                    db.SaveChanges();
                }


                // Очистка полей формы
                ClearingTextFields();

                MessageBox.Show("Товар успешно сохранен!");
            }
            catch (FormatException)
            {
                // Обработка исключения "входная строка имела неверный формат"
                MessageBox.Show("Убедитесь в правильности введённых значений", "Произошла ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //catch (Exception ex)
            //{
                
            //    Process proc = new Process();
            //    proc.StartInfo.FileName = "команда.exe";
            //    proc.StartInfo.Arguments = "<ее параметры>";
            //    proc.StartInfo.UseShellExecute = false;
            //    proc.StartInfo.RedirectStandardOutput = true;
            //    proc.Start();

            //    //Получаете результат выполнения
            //    TbConsole.Text = proc.StandardOutput.ReadToEnd();
            //}
        }

        // Извлекаем выбранный элемент из комбо-бокса и получаем значение его свойства "Код".
        private int SelectedComboBoxItem<T>(List<T> table, ComboBox combobox)
        {
            var selectedItem = combobox.SelectedItem as object;
            // Проверяем, что выбран элемент комбо-бокса и он является типом "T". "T" - это обобщенный тип,
            if (selectedItem != null && selectedItem is T)
            {
                // Приводим элемент к типу "object" с помощью оператора "as", чтобы получить доступ к его свойствам.
                var selectedTable = (T)selectedItem;
                var codeProperty = typeof(T).GetProperty("Код");
                //  Проверяем, существует ли у типа "T" свойство с именем "Код" и является ли его тип "int".
                if (codeProperty != null && codeProperty.PropertyType == typeof(int))
                {
                    // Получаем значение свойства "Code" выбранного элемента и возвращаем его как результат метода.
                    return (int)codeProperty.GetValue(selectedTable);
                }
            }
            // Возврат значения по умолчанию, если выбранный элемент не найден или не удалось получить значение свойства "Код"
            return 0;
        }

        private void ClearingTextFields() // Очистка полей формы
        {
            TbArticulAw.Text = "";
            TbCena.Text = "";
            DpDateIzgotovlenia.SelectedDate = DateTime.Now;
            CbEdIzmerenia.Text = "";
            CbFirma.Text = "";
            CbKategori.Text = "";
            TbName.Text = "";
            DpSrokGodnosti.SelectedDate = DateTime.Now.AddYears(1); ;
            CbStranaProizvoditel.Text = "";
            ImgIzobrazhenieTovara.Source = new BitmapImage(new Uri("Res/Images/NoImage.png", UriKind.Relative));
        }

        #endregion

        #region Проверка и генерация значения артикула по нажатию на текстовое поле
        private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var agbx = new ArticleGenerationMessageBox();
            agbx.Show();            
        }
        private void SetArticl_Click(object sender, RoutedEventArgs e)
        {
            TbArticulAw.Text = ArticleGenerationMessageBox.EnteredText;
        }
        #endregion

        #region Работа с изображением 

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем метод SelectPhoto() 
            SelectPhoto(UpdateSelectedPhoto);
        }

        private void UpdateSelectedPhoto(string filePath)
        {
            // Отображаем выбранную фотографию в элементе Image
            // Предполагается, что у вас в окне MainWindow есть элемент Image с именем ImgIzobrazhenieTovara
            ImgIzobrazhenieTovara.Source = new BitmapImage(new Uri(filePath));

            // Можно также сохранить байты фотографии в поле класса MainWindow, если они понадобятся для сохранения в базу данных
            _photoBytes = File.ReadAllBytes(filePath);
        }

        private static void SelectPhoto(Action<string> onPhotoSelected)
        {
            // Открыть диалог выбора файла фотографии
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                // Прочитать фотографию в бинарный формат
                byte[] photoBytes = File.ReadAllBytes(openFileDialog.FileName);

                // Вызываем обработчик с выбранным файлом
                onPhotoSelected?.Invoke(openFileDialog.FileName);
            }
        }

        #endregion

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
