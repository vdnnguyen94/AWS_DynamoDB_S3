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
using DotNetEnv;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace _301289600Nguyen_Lab2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public LoginWindow()
        {
            InitializeComponent();

            // read env for access
            Env.Load();
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var region = Amazon.RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION"));

            _client = new AmazonDynamoDBClient(accessKey, secretKey, region);
            _context = new DynamoDBContext(_client);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // checking connection before operation
            try
            {
                // testing connection
                await _client.ListTablesAsync();

                StatusTextBlock.Text = "Connected to AWS";
                StatusTextBlock.Foreground = Brushes.Green;
                LoginButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = "Failed Connection, Failed Access Key";
                StatusTextBlock.Foreground = Brushes.Red;
                LoginButton.IsEnabled = false;
                MessageBox.Show($"Failed to connect to AWS: {ex.Message}", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userId = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // fetching user
                var userProfile = await _context.LoadAsync<BookshelfItem>(userId, userId);

                if (userProfile != null && userProfile.Password == password)
                {
                    // Login successful
                    MessageBox.Show($"Welcome, {userProfile.FullName}!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                    // NEXT Open the next window (BookshelfWindow)
                    // var bookshelfWindow = new BookshelfWindow(userProfile);
                    // bookshelfWindow.Show();
                    // this.Close();
                }
                else
                {
                    // Login failed
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
