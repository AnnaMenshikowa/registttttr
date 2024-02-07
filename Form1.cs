using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace register
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Data Source=LAPTOP-TF13KO06\\SQLEXPRESS;Initial Catalog=userss;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = LoginTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите имя пользователя и пароль");
                return;
            }
            if (password.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать не менее 8 символов");
                return;
            }

            // Проверка наличия букв верхнего и нижнего регистров
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower))
            {
                MessageBox.Show("Пароль должен содержать заглавные и строчные буквы");
                return;
            }

            // Проверка наличия символов @ и !
            if (!password.Contains("@") || !password.Contains("!"))
            {
                MessageBox.Show("Пароль должен содержать символы '@' и '!'");
                return;
            }

            if (UserExists(username))
            {
                MessageBox.Show("Пользователь уже зарегистрирован");
                return;
            }

            RegisterUser(username, password);
        }
        private bool UserExists(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        private void RegisterUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Регистрация прошла успешно");
        }

    }
}


      

