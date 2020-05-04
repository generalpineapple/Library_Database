using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    public interface ICheckedOutRepository
    {

<<<<<<< HEAD
        //void CreateCheckedOut(int bookId, int userId, DateTime checkoutDate, DateTime returnedDate, DateTime dueDate);
        void CreateCheckedOut(int bookId, int userId);

        CheckedOut CreateCheckedOut(int bookId, int userId);
    }
}
