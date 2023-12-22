using System;
using System.Data.SqlClient;
using System.Windows;

namespace EatHealthy
{
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private int GetLatestUserId()
        {
            // Query the database to get the latest user ID
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-D34GQHM8; Initial Catalog=LoginDB; Integrated Security=True;"))
            {
                sqlCon.Open();
                string query = "SELECT MAX(UserID) FROM tblUser";
                using (SqlCommand sqlCmd = new SqlCommand(query, sqlCon))
                {
                    var result = sqlCmd.ExecuteScalar();
                    return (result == DBNull.Value) ? 0 : (int)result;
                }
            }
        }

        private int GetNextUserId()
        {
            // Get the latest user ID from the database and increment
            return GetLatestUserId() + 1;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int userId = GetNextUserId();
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-D34GQHM8; Initial Catalog=LoginDB; Integrated Security=True;"))
                {
                    sqlCon.Open();
                    string query = "INSERT INTO tblUser (UserID, Username, Password) VALUES (@UserID, @Username, @Password)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@UserID", userId);
                    sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                    sqlCmd.ExecuteNonQuery();              
                }
                this.Hide();
                MessageBox.Show("Congratulation!!! Your account is saved");
                Window4 loginPage = new Window4();
                loginPage.ShowDialog();
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window4 loginPage = new Window4();
            loginPage.ShowDialog();
            this.Close();
        }
        private void textUsername_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtUsername.Focus();
        }

        private void textUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            textUsername.Visibility = string.IsNullOrEmpty(txtUsername.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void textPassword_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void textPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            textPassword.Visibility = string.IsNullOrEmpty(txtPassword.Password) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}