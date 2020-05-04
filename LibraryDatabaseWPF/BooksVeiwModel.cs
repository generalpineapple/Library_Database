using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    public class BooksVeiwModel
    {
        public IEnumerable<Books> BookList { get; set; }
        public IBookRepository bookRepository;

        public BooksVeiwModel(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
            BookList = bookRepository.RetrieveBooks().AsEnumerable();
        }
    }
}
