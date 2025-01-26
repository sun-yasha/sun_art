using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KR_SunArt_shusharina
{
    /// <summary>
    /// Логика взаимодействия для avtorization_page.xaml
    /// </summary>
    public partial class avtorization_page : Page
    {
        db db = new db();
        public avtorization_page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var loginUser = login.Text;
            var passUser = password.Password.ToString();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            if (loginUser == "admin")
            {
                string queryString = $"select ID, login, password from Users where login = '{loginUser}' and password = '{passUser}'";
                SqlCommand command = new SqlCommand(queryString, db.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                if (table.Rows.Count == 1)
                {
                    MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButton.OK);
                    Mananger.MainFrame.Navigate(new shop_page());
                }
                else
                    MessageBox.Show("Такого аккаунта не существует!", "Ошибка!", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButton.OK);
                Mananger.MainFrame.Navigate(new shop_user_page());
            }
        }

        private void avtorization_page_load(object sender, RoutedEventArgs e)
        {
            password.PasswordChar = '*';
            login.MaxLength = 50;
            password.MaxLength = 50;
        }

        private void noacc_Click(object sender, RoutedEventArgs e)
        {
            Mananger.MainFrame.Navigate(new registration_page());
        }
    }
}
