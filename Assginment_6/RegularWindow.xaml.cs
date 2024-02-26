using MySql.Data.MySqlClient;
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

namespace Assginment_6
{
    /// <summary>
    /// Interaction logic for RegularWindow.xaml
    /// </summary>
    public partial class RegularWindow : Window
    {
        string conn = "server=localhost;uid=root; pwd=;database=restaurants";
        public RegularWindow()
        {
            InitializeComponent();
        }

        private void BtnChangePass_Click(object sender, RoutedEventArgs e)
        {
            using MySqlConnection connection = new MySqlConnection(conn);
            try
            {
                connection.Open();
                string currentPassword = txtCurrentPassword.Password;
                string newPassword = txtNewPassword.Password;
                string confirmNewPassword = txtConfirmPassword.Password;

                if (newPassword != confirmNewPassword)
                {
                    MessageBox.Show("New password and confirm password do not match.");
                    return;
                }
                string updateQuery = "UPDATE Users SET password = @newPassword WHERE password = @currentPassword";

                using MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@newPassword", newPassword); 
                updateCommand.Parameters.AddWithValue("@currentPassword", currentPassword);

                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password changed successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to change password. Ensure you have the correct current password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }
        private void BtnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            using MySqlConnection connection = new MySqlConnection(conn);
            try
            {
                connection.Open();
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string query = "UPDATE Users SET first_name=@firstname, last_name=@lastname, telephone=@telephone WHERE username=@currentuser";
                MySqlCommand edit = new MySqlCommand(query, connection);
                if (decimal.TryParse(txtFirstName.Text, out decimal firstNameDecimal))
                {
                    edit.Parameters.AddWithValue("@firstname", firstNameDecimal);
                }
                else
                {
                    MessageBox.Show("Invalid decimal value for first name.");
                }
                edit.Parameters.AddWithValue("@lastname", txtLastName.Text);
                edit.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                edit.Parameters.AddWithValue("@currentuser", userName);

                int rowsAffected = edit.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Information changed successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to change information.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
