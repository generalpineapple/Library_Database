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
        private readonly string connectionString = @"Data Source = <DB FQDN OR DB IP>; Database = <DB NAME>; User ID = <DB UID>; Password=<DB PASSWORD>";


        public IList<Books> BookList { get; set; }
        public IList<Users> UserList { get; set; }
        public IBookRepository bookRepository;
        public IUsersRepository usersRepository;
        public ICheckedOutRepository checkedOutRepository;

        public ViewModel()
        {
            bookRepository = new SqlBookRepository(connectionString);
            usersRepository = new SqlUserRepository(connectionString);
            checkedOutRepository = new SqlCheckedOutRepository(connectionString);
            BookList = bookRepository.FetchAllBooks().ToList();
            UserList = usersRepository.FetchAllUsers().ToList();
        }

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
