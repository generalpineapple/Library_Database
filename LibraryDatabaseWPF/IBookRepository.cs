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

        Books CreateBook(string isbn, string authorName, string title, string genreName, string conditionType);

    }
}
