using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseWPF.Models
{
    public class UserReport
    {
        public int UserId { get; }
        public int CurrentCheckouts { get; }
        public int OnTimeReturns { get; }
        public int LateReturns { get; }
        public int OverDueBooks { get;  }
        public int DaysLate { get;  }

        public UserReport(int userId, int currentCheckouts, int onTimeReturns, int lateReturns, int overDueBooks, int daysLate)
        {
            UserId = userId;
            CurrentCheckouts = currentCheckouts;
            OnTimeReturns = onTimeReturns;
            LateReturns = LateReturns;
            OverDueBooks = overDueBooks;
            DaysLate = daysLate;
        }
    }
}
