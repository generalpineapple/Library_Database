﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    public interface IBookRepository
    {

        Books CreateBook(string isbn, string authorName, string title, string genreName, string conditionType);

        Books EditBookQuality(int bookId, int conditionId);

        IReadOnlyList<Books> FetchBookByAuthor(string authorName);

        Books FetchBookFromISBN(string isbn);

        Books FetchBookByTitle(string title);

        IReadOnlyList<Books> FetchBooksToReplace();

        IReadOnlyList<Books> GetBooksFromUser(string userName);

        IReadOnlyList<Books> GetTopBooksByGenre();

    }
}
