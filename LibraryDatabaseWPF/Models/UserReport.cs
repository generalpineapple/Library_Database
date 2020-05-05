using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseWPF.Models
{
    public class UserReport
    {
        public string Name { get; }
        public int UserId { get; }
        public int TotalCheckouts { get; }
        public int OnTimeReturns { get; }
        public int LateReturns { get; }
        public int OverDueBooks { get;  }
        public int LateFees { get;  }

        public UserReport(string name, int userId, int totalCheckouts, int onTimeReturns, int lateReturns, int overDueBooks, int lateFees)
        {
            Name = name; 
            UserId = userId;
            TotalCheckouts = totalCheckouts;
            OnTimeReturns = onTimeReturns;
            LateReturns = lateReturns;
            OverDueBooks = overDueBooks;
            LateFees = lateFees;
        }
    }
}
