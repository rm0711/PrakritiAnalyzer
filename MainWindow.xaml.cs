using System.Windows;

namespace PrakritiAnalyzer
{
    public partial class MainWindow : Window
    {
        public static string LoggedUser = null;

        public MainWindow()
        {
            InitializeComponent();
            UpdateLoginState();
        }

        private void UpdateLoginState()
        {
            if (LoggedUser != null)
            {
                GreetingText.Text = $"Hello, {LoggedUser}";
                LoginLogoutButton.Content = "Logout";
            }
            else
            {
                GreetingText.Text = "";
                LoginLogoutButton.Content = "Login / Register";
            }
        }

        private void LoginLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoggedUser == null)
            {
                // Show login window
                LoginWindow login = new LoginWindow();
                login.ShowDialog();

                if (LoggedUser != null)
                {
                    UpdateLoginState();
                }
            }
            else
            {
                // Logout logic
                LoggedUser = null;
                UpdateLoginState();
            }
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
            if (LoggedUser == null)
            {
                MessageBox.Show("Please login first!");
                return;
            }

            AnalyzeWindow quiz = new AnalyzeWindow(LoggedUser);
            quiz.ShowDialog();
        }
    }
}
