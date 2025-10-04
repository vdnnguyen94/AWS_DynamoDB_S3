using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;


namespace _301289600Nguyen_Lab2
{
    [DynamoDBTable("Bookshelf")]
    public class BookshelfItem
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }

        [DynamoDBRangeKey]
        public string BookId { get; set; }

        [DynamoDBProperty]
        public string Password { get; set; }

        [DynamoDBProperty]
        public string FullName { get; set; }

        [DynamoDBProperty]
        public string Title { get; set; }

        [DynamoDBProperty]
        public List<string> Authors { get; set; }

        [DynamoDBProperty]
        public string S3Key { get; set; }

        [DynamoDBProperty]
        public int BookmarkedPage { get; set; }

        [DynamoDBProperty]
        public string LastReadTimestamp { get; set; }
    }
}
