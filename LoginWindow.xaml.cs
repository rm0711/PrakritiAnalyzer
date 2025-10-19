using PrakritiAnalyzer.models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace PrakritiAnalyzer
{
    public partial class LoginWindow : Window
    {
        private readonly string userFile = "D:\\WPF_Intern\\PrakritiAnalyzer\\data\\users.txt";
        private bool isRegisterMode = false;

        public LoginWindow()
        {
            InitializeComponent();
            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(userFile));
            if (!File.Exists(userFile))
                File.WriteAllText(userFile, "[]");
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            isRegisterMode = !isRegisterMode;
            TitleText.Text = isRegisterMode ? "Register" : "Login";
            EmailBox.Visibility = isRegisterMode ? Visibility.Visible : Visibility.Collapsed;
            PrimaryButton.Content = isRegisterMode ? "Register" : "Login";
            SwitchButton.Content = isRegisterMode ? "Already have an account?" : "Create Account";
        }

        private void PrimaryButton_Click(object sender, RoutedEventArgs e)
        {
            string jsonText = File.ReadAllText(userFile);

            if (string.IsNullOrWhiteSpace(jsonText))
            {
                jsonText = "[]";
            }

            // Deserialize into List<User>, not List<QuizResult>
            var users = JsonSerializer.Deserialize<List<User>>(jsonText) ?? new List<User>();

            if (isRegisterMode)
            {
                if (users.Any(u => u.Username == UsernameBox.Text))
                {
                    MessageBox.Show("Username already exists!");
                    return;
                }

                var user = new User
                {
                    Username = UsernameBox.Text,
                    Email = EmailBox.Text,
                    Password = PasswordBox.Password
                };
                users.Add(user);

                File.WriteAllText(userFile, JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true }));
                MessageBox.Show("Registration successful!");
                SwitchButton_Click(sender, e);
            }
            else
            {
                var user = users.FirstOrDefault(u =>
                    (u.Username == UsernameBox.Text || u.Email == UsernameBox.Text) && u.Password == PasswordBox.Password);

                if (user != null)
                {
                    MainWindow.LoggedUser = user.Username;
                    MessageBox.Show($"Welcome {user.Username}!");
                    Close();
                }
                else
                    MessageBox.Show("Invalid credentials!");
            }
        }
    }
}
