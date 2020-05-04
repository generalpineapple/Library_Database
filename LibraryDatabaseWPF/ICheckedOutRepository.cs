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

        void CreateCheckedOut(int bookId, int userId);

    }
}
