using System.Data.Entity;
using System.Linq;
using System.Windows;
using VadimRabotaetV2.DALandDomain;
using VadimRabotaetV2.TypesOfEmployees;

namespace vadimRabotaet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Tblog.Text == "")
            {
                MessageBox.Show("Пустое поле Логин !");
                return;
            }
            if (TbPas.Text == "")
            {
                MessageBox.Show("Пустое поле Пароль !");
                return;
            }

            using (var db = new Entities())
            {
                // Если заходим под сотрудником
                if (cbSotr.IsChecked == true)
                {
                    // Проверка на правильность введённых значений
                    var contextEploee =  db.Сотрудник.FirstOrDefault(x => x.Логин.Equals(Tblog.Text));
                    if (contextEploee == null)
                    {
                        MessageBox.Show("Сотрудника с таким логином нет в базе данных !");
                        return;
                    }
                    if (!contextEploee.Пароль.Equals(TbPas.Text))
                    {
                        MessageBox.Show("Сотрудник с таким логином есть, но введённый к нему пароль является неверным !");
                        return;
                    }
                    // Переходим на окно взависимости от должности
                    switch (contextEploee.КодДолжности)
                    { 
                        case 1: // Админ
                            var adminWindow = new TypesOfEmployees.Admin.AdminWindow();
                            adminWindow.Show();
                            break;
                        case 2: // Сборщик
                            var collectorWindow = new TypesOfEmployees.Сollector.CollectorWindow();
                            collectorWindow.Show();
                            break; 
                        case 3: // Менеджер
                            var managerWindow = new TypesOfEmployees.Manager.ManagerWindow();
                            break;
                    }
                    this.Close();
                    return;
                }
                // Если заходим под Клиентом
                var contextClient = db.Клиент.FirstOrDefault(x => x.Логин.Equals(Tblog.Text) && x.Пароль.Equals(TbPas.Text));
                if (contextClient == null)
                {
                    MessageBox.Show("Клиента с таким логином нет в базе данных !");
                    return;
                }
                if (!contextClient.Пароль.Equals(TbPas.Text))
                {
                    MessageBox.Show("Клиент с таким логином есть, но введённый к нему пароль является неверным !");
                    return;
                }
                // Переходим на окно клиента и закрываем это
                var clientWindow = new Client.ClientWindow();
                clientWindow.Show();
                this.Close();
            }
        }
    }
}
