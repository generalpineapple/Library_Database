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
        public IList<Books> BookList { get; set; }
        public IList<Users> UserList { get; set; }
        public IBookRepository bookRepository;
        public IUsersRepository usersRepository;

        public BooksVeiwModel(IBookRepository bookRepository, IUsersRepository usersRepository)
        {
            this.bookRepository = bookRepository;
            this.usersRepository = usersRepository;
            BookList = bookRepository.RetrieveBooks().ToList();
        }
    }
}
