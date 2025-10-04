# Bookshelf Reader (AWS + Syncfusion WPF)

A C#/.NET WPF application that allows users to:
- Log in via AWS DynamoDB user profile  
- View their personal bookshelf retrieved from DynamoDB  
- Read PDF books stored in Amazon S3 using the **Syncfusion PDF Viewer**  
- Automatically **bookmark** their current page  
- Sync bookmarks back to DynamoDB whenever they close or click “Bookmark”  

Built as part of **Lab 2** for COMP306 (Cloud-Based Application Development).  

---

## ?? Features

| Feature | Description |
|----------|--------------|
| ?? AWS DynamoDB | Stores user info, bookshelf list, and bookmarked pages |
| ?? AWS S3 | Stores book PDF files, downloaded securely by key |
| ?? Syncfusion PDF Viewer | Displays books within the app |
| ?? Auto Bookmark | Automatically saves current page when closing or bookmarking |
| ?? Real-time Refresh | Updates bookshelf automatically when returning from reader |
| ?? Environment Config | All secrets stored in .env file for easy setup |

---

## ?? Project Structure

\`\`\`
?? _301289600Nguyen_Lab2
?
??? App.xaml / App.xaml.cs          ? Loads Syncfusion license
??? Helper.cs                       ? Global AWS S3 client & shared memory stream
??? BookOps.cs                      ? Handles DynamoDB book retrieval
??? BookshelfItem.cs                ? DynamoDB table model
??? MyBookListWindow.xaml(.cs)      ? Displays book list, handles refresh
??? BookReadingWindow.xaml(.cs)     ? Displays PDF, handles bookmarking
??? .env                            ? Stores AWS credentials and license key
\`\`\`

---

## ?? Prerequisites

- .NET 6.0 or higher  
- AWS Account (with DynamoDB & S3 setup)  
- Syncfusion License Key (free for community developers)  
- Visual Studio 2022 (recommended)

---

## ?? Dependencies

Install these NuGet packages:

| Package | Purpose |
|----------|----------|
| \`AWSSDK.S3\` | Connect and fetch files from S3 |
| \`AWSSDK.DynamoDBv2\` | Query & update DynamoDB |
| \`DotNetEnv\` | Read environment variables from .env |
| \`Syncfusion.PdfViewer.WPF\` | Render PDF content in the WPF viewer |

---

## ?? .env Setup

Create a file named \`.env\` at the **root of your project** with the following contents:

\`\`\`bash
# AWS Credentials
AWS_ACCESS_KEY_ID=your-access-key
AWS_SECRET_ACCESS_KEY=your-secret-key
AWS_REGION=us-east-2

# S3 Bucket
AWS_BUCKET_NAME=aws-van-bookshelf-lab2

# Syncfusion License Key
SYNCFUSION_LICENSE_KEY=your-syncfusion-license-key
\`\`\`

?? **Important:**  
Never upload \`.env\` to GitHub — it contains private credentials.  
Add \`.env\` to your \`.gitignore\` file.

---

## ??? How to Run

1. **Clone or open** the project in Visual Studio.  
2. Create and fill in your \`.env\` file as shown above.  
3. **Install dependencies** via NuGet:
   \`\`\`
   Install-Package AWSSDK.S3
   Install-Package AWSSDK.DynamoDBv2
   Install-Package DotNetEnv
   Install-Package Syncfusion.PdfViewer.WPF
   \`\`\`
4. **Build** the project (Ctrl + Shift + B).  
5. **Run** the app (F5).

---

## ?? How It Works

### ?? 1. Login
User logs in with credentials stored in DynamoDB (Bookshelf table).  
The system identifies the \`UserId\` and retrieves all books for that user.

### ?? 2. Bookshelf Display
\`MyBookListWindow\` loads the user's books:
- Title, Authors, and bookmark info from DynamoDB.  
- Books are ordered by \`LastReadTimestamp\`.

### ?? 3. Reading Window
When a book is double-clicked:
- \`BookReadingWindow\` opens.  
- Fetches the book's PDF from S3 using its \`S3Key\`.  
- Loads the PDF into the Syncfusion viewer.  
- Keeps the stream alive using \`Helper.SelectedBookStream\`.

### ?? 4. Bookmark Save
When:
- The user clicks the “Bookmark” button, **or**
- Closes the reader window,  
the app saves \`BookmarkedPage\` and \`LastReadTimestamp\` to DynamoDB.

\`MyBookListWindow\` automatically refreshes via an event listener.

---

## ?? Environment Variables in Use

| Variable | Used In | Purpose |
|-----------|----------|----------|
| AWS_ACCESS_KEY_ID | Helper.cs | AWS authentication |
| AWS_SECRET_ACCESS_KEY | Helper.cs | AWS authentication |
| AWS_REGION | Helper.cs | AWS region (e.g., us-east-2) |
| AWS_BUCKET_NAME | BookReadingWindow.xaml.cs | Target S3 bucket |
| SYNCFUSION_LICENSE_KEY | App.xaml.cs | License for PDF viewer |

---

## ?? Example DynamoDB Table (Bookshelf)

| UserId | BookId | Title | S3Key | BookmarkedPage | LastReadTimestamp |
|--------|--------|--------|--------|----------------|------------------|
| u123 | book1 | AWS Week 1 Overview | week1.pdf | 5 | 2025-10-04T15:22:00Z |
| u123 | book2 | Week 2 – Cloud Storage | week2.pdf | 1 | 2025-10-02T14:05:10Z |

---

## ?? Optional Enhancements

- ? Add a “Continue Reading” button that reloads \`Helper.SelectedBookStream\`
- ? Display formatted last-read date in the bookshelf
- ? Use AWS Cognito for login authentication
- ? Add a progress bar to show % of book read

---

## ????? Author

**Van Nguyen**  
COMP306 – Lab 2  
Centennial College  
London, Ontario, Canada  

