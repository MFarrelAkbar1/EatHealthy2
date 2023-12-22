using EatHealthyWPF;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace EatHealthy
{
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-D34GQHM8; Initial Catalog=LoginDB; Integrated Security=True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT COUNT(1) FROM tblUser WHERE Username=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username atau Password salah");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.ShowDialog();
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            btnSubmit_Click(sender, e);
        }
    }
}