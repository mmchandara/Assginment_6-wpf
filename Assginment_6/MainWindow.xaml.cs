using System.CodeDom;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Assginment_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string conn = "server=localhost;uid=root; pwd=;database=restaurants";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            using MySqlConnection connection = new MySqlConnection(conn);
            try
            {
                connection.Open();
                string query = "SELECT role FROM Users WHERE username = @username AND password = @password";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", txtUsername.Text);
                command.Parameters.AddWithValue("@password", txtPassword.Password);

                string role = command.ExecuteScalar() as string;

                if (role == "admin")
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else if (role == "regular")
                {
                    RegularWindow regularWindow = new RegularWindow();
                    regularWindow.Show();
                }
                else
                {
                    MessageBox.Show("Invalid login.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}