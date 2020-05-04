using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    public interface IBookRepository
    {

        void CreateBook(string isbn, string authorName, string title, string genreName, string conditionType);

        IReadOnlyList<Books> FetchBookByAuthor(string authorName);

        void EditBookQuality(int bookId, int conditionId);

        IReadOnlyList<Books> FetchAllBooks();

        Books FetchBookFromISBN(string isbn);

        Books FetchBookByTitle(string title);

        IReadOnlyList<Books> FetchBooksToReplace();

        IReadOnlyList<Books> GetBooksFromUser(string userName);

        IReadOnlyList<Books> GetTopBooksByGenre();

    }
}
