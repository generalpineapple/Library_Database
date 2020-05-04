using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseWPF.Models
{
    public class Authors
    {
        public int AuthorId { get; }
        public string AuthorName { get; }

        public Authors(int id, string name)
        {
            AuthorId = id;
            AuthorName = name;
        }
    }
}
