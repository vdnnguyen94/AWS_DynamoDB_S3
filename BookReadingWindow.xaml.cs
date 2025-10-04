using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Amazon.S3.Model;
using DotNetEnv;
using System;
using System.Collections.Generic;
using System.IO;
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
namespace _301289600Nguyen_Lab2
{
    /// <summary>
    /// Interaction logic for BookReadingWindow.xaml
    /// </summary>
    public partial class BookReadingWindow : Window
    {
        private readonly BookshelfItem _selectedBook;
        private readonly IDynamoDBContext _context;
        public event Action<BookshelfItem> BookUpdated;

        public BookReadingWindow(BookshelfItem selectedBook, IDynamoDBContext context)
        {
            InitializeComponent();
            _selectedBook = selectedBook;
            _context = context;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = _selectedBook.Title;

            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = "aws-van-bookshelf-lab2",
                    Key = _selectedBook.S3Key
                };

                DebugInfoText.Text = $"Fetching: {_selectedBook.S3Key}...";

                // Fetch object from S3 using shared Helper client
                using (var response = await Helper.s3Client.GetObjectAsync(request))
                {
                    var memoryStream = new MemoryStream();
                    await response.ResponseStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    // Keep in memory globally
                    Helper.SelectedBookStream = memoryStream;
                    Helper.SelectedS3Key = _selectedBook.S3Key;
                    Helper.SelectedTitle = _selectedBook.Title;

                    DebugInfoText.Text = $"Book: {_selectedBook.Title}\nS3Key: {_selectedBook.S3Key}\nSize: {memoryStream.Length / 1024.0:F1} KB";

                    // Load into PDF viewer
                    pdfViewerControl.Load(Helper.SelectedBookStream);

                    if (_selectedBook.BookmarkedPage > 1)
                        pdfViewerControl.CurrentPage = _selectedBook.BookmarkedPage;
                }
            }
            catch (Exception ex)
            {
                DebugInfoText.Text = $"Failed to load: {ex.Message}";
                MessageBox.Show($"Failed to load book from S3: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private async void BookmarkButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveProgressAsync();
            MessageBox.Show($"Page {pdfViewerControl.CurrentPage} has been bookmarked.", "Bookmark Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await SaveProgressAsync();
        }

        private async Task SaveProgressAsync()
        {
            try
            {
                int currentPage = pdfViewerControl.CurrentPage;
                _selectedBook.BookmarkedPage = currentPage;
                _selectedBook.LastReadTimestamp = DateTime.UtcNow.ToString("o");
                await _context.SaveAsync(_selectedBook);
                BookUpdated?.Invoke(_selectedBook);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving progress: {ex.Message}");
            }
        }
    }
}