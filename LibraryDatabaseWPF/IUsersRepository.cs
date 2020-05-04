using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    public interface IUsersRepository
    {
        Users CreateUser(string name, string address, string phoneNumber, string email);

        IList<Users> FetchUsersRentingBookByISBN(int isbn);

        IList<Books> GetBooksFromUser(int userId);

        IList<Users> GetTopUsers(); 
    }
}
