using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace StuffApp
{
    /// <summary>
    /// Логика взаимодействия для Auth2.xaml
    /// </summary>
    public partial class Auth2 : Window
    {
        public Auth2()
        {
            InitializeComponent();
        }

        private void Auth2_Click(object sender, RoutedEventArgs e)
        {
            MainWindow a = new MainWindow();
            a.Show();
            Hide();
             
            StringBuilder errors = new StringBuilder();
            using (var db = new ContractsBaseEntities())
            {
                var pass = db.Users.AsNoTracking().FirstOrDefault(u => u.login == textBoxLogin.Text && u.password == passBox.Text);
                var login = db.Users.AsNoTracking().FirstOrDefault(u => u.login == textBoxLogin.Text);
                if (login == null)
                {
                    errors.AppendLine("Пользователь не найден ");
                }

                if (pass == null)
                {
                    errors.AppendLine("Неверный пароль");
                }

                if (errors.Length > 0)
                    MessageBox.Show(errors.ToString());
                if (errors.Length == 0)
                {
                    if (pass.isAdmin == true)
                    {
                        Manager.MainFrame.Navigate(new StuffPage());
                    }
                    else
                    {
                        Manager.MainFrame.Navigate(new PageForUser());
                    }
                }
            }
        }
    }
}
