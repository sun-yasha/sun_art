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
    /// Логика взаимодействия для registration_page.xaml
    /// </summary>
    public partial class registration_page : Page
    {
        db database = new db();
        public registration_page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = log_in.Text;
            var pass = password.Password.ToString();
            string querystring = $"insert into Users(login, password) values('{login}', '{pass}')";
            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            database.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успешно!");
                Mananger.MainFrame.Navigate(new avtorization_page());
            }
            else
                MessageBox.Show("Аккаунт не создан!", "Ошибка");
        }

        private void registration_page_load(object sender, RoutedEventArgs e)
        {
            password.PasswordChar = '*';
        }

        private Boolean checkuser()
        {
            var login = log_in.Text;
            var pass = password.Password.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select login, password from Users where login = '{login}' and password = '{pass}'";
            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
                return false;
        }
    }
}
