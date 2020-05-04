using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseWPF.Models
{
    public class Inventory
    {
        public string ISBN { get; }
        public int TotalCopies { get; }
        public int TotalCheckouts { get; }


        public Inventory(string isbn, int totalCopies, int totalCheckouts)
        {
            ISBN = isbn;
            TotalCopies = totalCopies;
            TotalCheckouts = totalCheckouts;
        }
    }
}
