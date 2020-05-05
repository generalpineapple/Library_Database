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
        void CreateUser(string name, string address, string phoneNumber, string email);

        UserReport CreateUserReport(string userName);

        void EditUserById(int userId, string userName, string userAddress, string phoneNumber, string email);

        IReadOnlyList<Users> FetchUserRentingBookByISBN(string isbn);

        IReadOnlyList<Users> GetTopUsers();

        IReadOnlyList<Users> FetchAllUsers();

        IReadOnlyList<Users> FetchUserByName(string name);

    }
}
