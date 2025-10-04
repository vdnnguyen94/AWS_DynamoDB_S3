using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace _301289600Nguyen_Lab2
{
    public class BookOps
    {
        private readonly IDynamoDBContext _context;

        public BookOps(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<List<BookshelfItem>> GetBooksForUserAsync(string userId)
        {
            //list all items
            var allUserItems = await _context.QueryAsync<BookshelfItem>(userId).GetRemainingAsync();

            //remover user profile then sorts by timestamp
            return allUserItems
                .Where(item => item.BookId != item.UserId) 
                .OrderByDescending(b => b.LastReadTimestamp)     
                .ToList();
        }
    }
}
