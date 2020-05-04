using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseWPF.Models
{
    public class Books
    {
        public int BookId { get; }
        public string ISBN { get; }
        public string AuthorName { get; }
        public string Title { get; }
        public string GenreName { get; }
        public string ConditionType { get; }


        public Books(int bookId, string isbn, string authorName, string title, string genreName, string conditionType)
        {
            BookId = bookId;
            ISBN = isbn;
            AuthorName = authorName;
            Title = title;
            GenreName = genreName;
            ConditionType = conditionType;
        }

    }
}