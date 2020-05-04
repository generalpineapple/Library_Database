using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    public class ViewModel
    {
        public IList<Books> BookList { get; set; }
        public IList<Users> UserList { get; set; }
        public IBookRepository bookRepository;
        public IUsersRepository usersRepository;
        public ICheckedOutRepository checkedOutRepository;

        public ViewModel(IBookRepository bookRepository, IUsersRepository usersRepository, ICheckedOutRepository checkedOutRepository)
        {
            this.checkedOutRepository = checkedOutRepository;
            this.bookRepository = bookRepository;
            this.usersRepository = usersRepository;
            BookList = bookRepository.FetchAllBooks().ToList();
            UserList = usersRepository.FetchAllUsers().ToList();
        }
    }
}
