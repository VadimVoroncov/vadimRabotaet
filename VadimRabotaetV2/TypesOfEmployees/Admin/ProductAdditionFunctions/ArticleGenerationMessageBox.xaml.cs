using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VadimRabotaetV2.DALandDomain;

namespace VadimRabotaetV2.TypesOfEmployees.Admin.ProductAdditionFunctions
{
    /// <summary>
    /// Логика взаимодействия для ArticleGenerationMessageBox.xaml
    /// </summary>
    public partial class ArticleGenerationMessageBox : Window
    {
        public static string EnteredText;
        List<double?> context;
        public ArticleGenerationMessageBox()
        {
            InitializeComponent();
            GbError.Visibility = Visibility.Hidden;
            using (var db = new Entities())
            {
                context = db.Товар.Select(x => x.артикул).ToList();
            }
        }
        private void TbArticul_SelectionChanged(object sender, RoutedEventArgs e) => GbError.Visibility = Visibility.Hidden;

        #region Передаём значение в textbox основного окна
        private void SaveArticul_Click(object sender, RoutedEventArgs e)
        {
            Regex _onlyNumbers = new Regex(@"^\d{6}$");
            if (!_onlyNumbers.IsMatch(TbArticul.Text))
            {
                GbError.Visibility = Visibility.Visible;
                TbError.Text = "В строке должно быть 6 цифр";
                return;
            }
            // Передаем информацию на AdminWindow и закрываем это
            EnteredText = TbArticul.Text;
            Close();
        }
        #endregion

        #region Генерация числа
        private void ArticleGeneration_Click(object sender, RoutedEventArgs e)
        {
            TbArticul.Text = NumberGeneraton().ToString();
        }

        private int NumberGeneraton()
        {
            // После генерации числа идёт проверка на его уникальность и повторную генерацию,
            // если оно уже было использовано
            Random random = new Random();
            int generatedNumber;

            do { generatedNumber = random.Next(100000, 999999); }
            while (context.Contains(generatedNumber));

            context.Add(generatedNumber);
            return generatedNumber;
        }
        #endregion
    }
}
