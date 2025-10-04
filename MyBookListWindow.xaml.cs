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
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DotNetEnv;

namespace _301289600Nguyen_Lab2
{
    /// <summary>
    /// Interaction logic for MyBookListWindow.xaml
    /// </summary>
    public partial class MyBookListWindow : Window
    {
        private readonly BookshelfItem _currentUser;
        private readonly BookOps _bookOps;
        private readonly IDynamoDBContext _context;
    
        public MyBookListWindow(BookshelfItem userProfile)
        {
            InitializeComponent();
            _currentUser = userProfile;

            // Initialize the AWS client and context
            Env.Load();
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var region = Amazon.RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION"));

            var client = new AmazonDynamoDBClient(accessKey, secretKey, region);
            _context = new DynamoDBContext(client);
            _bookOps = new BookOps(_context);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // message
            WelcomeTextBlock.Text = $"Hello, {_currentUser.FullName}";

            // Load the books
            try
            {
                var userBooks = await _bookOps.GetBooksForUserAsync(_currentUser.UserId);
                BooksListView.ItemsSource = userBooks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BooksListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (BooksListView.SelectedItem is BookshelfItem selectedBook)
            {
                MessageBox.Show($"You selected the book: {selectedBook.Title}\n\n(The book reader window will open here in the next step.)");

                //next step
            }
        }

    }
}
