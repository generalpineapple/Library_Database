using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseWPF.Models
{
    public class CheckedOut
    {
        public int TransactionId { get; }
        public int BookId { get; }
        public int UserId { get; }
        public DateTime CheckoutDate { get; }
        public DateTime? ReturnedDate { get; }
        public DateTime DueDate { get; }

        public CheckedOut(int transactionId, int bookId, int userId, DateTime checkoutDate, DateTime? returnedDate, DateTime dueDate)
        {
            TransactionId = transactionId;
            BookId = bookId;
            UserId = UserId;
            CheckoutDate = checkoutDate;
            DueDate = dueDate;
            ReturnedDate = returnedDate;            
        }
        //YYYY-MM-DD
    }
}