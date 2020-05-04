using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseWPF.Models
{
    public class Users
    {
        public int UserId { get; }
        public string Name { get; }
        public int TotalCheckouts { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public int LateReturns { get; }

        public Users(int userId, string name, int totalCheckouts, string phoneNumber, string email, int lateReturns)
        {
            UserId = userId;
            Name = name;
            TotalCheckouts = totalCheckouts;
            PhoneNumber = PhoneNumber;
            Email = email;
            LateReturns = lateReturns;
        }
    }
}
