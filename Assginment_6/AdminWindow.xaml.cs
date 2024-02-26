using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

namespace Assginment_6
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        string conn = "server=localhost;uid=root; pwd=;database=restaurants";
        public AdminWindow()
        {
            InitializeComponent();
        }
        private void DisplayUsers()
        {
            using MySqlConnection connection = new MySqlConnection(conn);
            try
            {
                connection.Open();
                string query = "SELECT * FROM Users";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                datagrid1.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDisplayUser_Click(object sender, RoutedEventArgs e)
        {
            DisplayUsers();
        }

        private void BtnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            using MySqlConnection connection = new MySqlConnection(conn);
            try
            {
                connection.Open();
                string query = "INSERT INTO Users (username, password, first_name, last_name, telephone, role) VALUES (@username, @password, @firstname, @lastname, @telephone, @role)";
                MySqlCommand create = new MySqlCommand(query, connection);
                create.Parameters.AddWithValue("@username", txtUsername.Text);
                create.Parameters.AddWithValue("@password", txtPassword.Text);
                create.Parameters.AddWithValue("@firstname", txtFirstName.Text);
                create.Parameters.AddWithValue("@lastname", txtLastName.Text);
                create.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                create.Parameters.AddWithValue("@role", txtRole.Text);
                create.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtUsername.Clear();
            txtPassword.Clear();
            txtTelephone.Clear();
            txtRole.Clear();
            DisplayUsers();
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            using MySqlConnection connection = new MySqlConnection(conn);
            try
            {
                connection.Open();
                string query = "UPDATE Users SET username=@username, password=@password, first_name=@firstname, last_name=@lastname, telephone=@telephone, role=@role WHERE id = @id";
                MySqlCommand edit = new MySqlCommand(query, connection);
                edit.Parameters.AddWithValue("@username", txtUsername.Text);
                edit.Parameters.AddWithValue("@password", txtPassword.Text);
                edit.Parameters.AddWithValue("@firstname", decimal.Parse(txtFirstName.Text));
                edit.Parameters.AddWithValue("@lastname", txtLastName.Text);
                edit.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                edit.Parameters.AddWithValue("@role", txtRole.Text);
                edit.Parameters.AddWithValue("@id", txtID.Text);
                edit.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtUsername.Clear();
            txtPassword.Clear();
            txtTelephone.Clear();
            txtRole.Clear();
            txtID.Clear();
            DisplayUsers();
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            using MySqlConnection connection = new MySqlConnection(conn);
            try
            {
                connection.Open();
                string query = "DELETE FROM Users WHERE id = @id";
                MySqlCommand delete = new MySqlCommand(query, connection);
                delete.Parameters.AddWithValue("@id", txtID.Text);
                delete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtID.Clear();
            DisplayUsers();
        }
    }
}
